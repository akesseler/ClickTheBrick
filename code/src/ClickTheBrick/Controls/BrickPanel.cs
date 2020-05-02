/*
 * MIT License
 * 
 * Copyright (c) 2020 plexdata.de
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */

using System;
using System.Drawing;
using System.Collections;
using System.Diagnostics;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;

namespace plexdata.ClickTheBrick
{
    public class BrickPanel : Panel
    {
        private static Size BrickSize = new Size(25, 25);

        public event EventHandler<EventArgs> GameOver;
        public event EventHandler<EventArgs> AllBricksRemoved;
        public event EventHandler<EventArgs> BrickCountChanged;
        public event EventHandler<UndoneEventArgs> Undone;

        private int brickCount = 0;
        private Brick lastBrick = null;
        private Brick[,] bricks = null;
        private List<Brick> blocks = null;
        private Stack undoStack = null;
        private object critical = new object();

        public BrickPanel()
            : base()
        {
            this.Size = this.DefaultSize;
            this.BackColor = SystemColors.ControlDark;
            this.Highlight = true;
            this.Dimension = new Point(8, 12);

            this.Text = null;
            this.Frozen = false;

            this.LastBlockSize = 0;
            this.TotalBrickCount = 0;

            this.BrickColors = new Color[] { Color.Blue, Color.Yellow, Color.Green, Color.Red };
            this.BrickColorTable = null;

            this.undoStack = new Stack();
        }

        #region Public property section.

        [Browsable(false)]
        public bool Frozen { get; set; }

        [Browsable(false)]
        public bool IsPossible
        {
            get
            {
                if (this.bricks != null)
                {
                    foreach (Brick brick in this.bricks)
                    {
                        if (brick != null)
                        {
                            List<Brick> siblings = new List<Brick>();
                            this.GetSiblingBricks(brick, ref siblings);

                            // The game is possible as long as at least 
                            // one sibling block exists.
                            if (siblings.Count > 1) { return true; }
                        }
                    }
                }
                return false;
            }
        }

        [Browsable(false)]
        public bool CanUndo
        {
            get
            {
                return !this.Frozen && this.undoStack != null && this.undoStack.Count > 0;
            }
        }

        [Browsable(true)]
        [DefaultValue(true)]
        public bool Highlight { get; set; }

        [Browsable(true)]
        [DefaultValue("")]
        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value;
            }
        }

        [Browsable(true)]
        [TypeConverter(typeof(PointConverter))]
        [DefaultValue(typeof(Point), "8, 12")]
        public Point Dimension
        {
            get
            {
                int rows, cols;
                this.GetDimension(out rows, out cols);
                return new Point(cols, rows);
            }
            set
            {
                if (value != null)
                {
                    int rows, cols;
                    this.GetDimension(out rows, out cols);

                    if (value.X != cols || value.Y != rows)
                    {
                        this.SetDimension(value.Y, value.X);
                    }
                }
            }
        }

        [Browsable(true)]
        [TypeConverter(typeof(ArrayConverter))]
        [DefaultValue(typeof(Color[]), "Color.Blue, Color.Yellow, Color.Green, Color.Red")]
        public Color[] BrickColors { set; get; }

        [Browsable(false)]
        public int BrickCount
        {
            get
            {
                return this.brickCount;
            }
            private set
            {
                if (this.brickCount != value)
                {
                    this.brickCount = value;

                    // Raise change event.
                    if (this.BrickCountChanged != null)
                    {
                        this.BrickCountChanged(this, EventArgs.Empty);
                    }
                }
            }
        }

        [Browsable(false)]
        public int TotalBrickCount { get; private set; }

        [Browsable(false)]
        public int LastBlockSize { get; private set; }

        // This table represents the number of brick available for each color.
        [Browsable(false)]
        public Dictionary<Color, int> BrickColorTable { get; private set; }

        #endregion // Public property section.

        #region Public method section.

        public void Setup()
        {
            if (this.BrickColors == null)
            {
                throw new ArgumentNullException("BrickColors");
            }

            int rows, cols;
            this.GetDimension(out rows, out cols);

            Point offset = new Point(
                this.DisplayRectangle.Left + (this.DisplayRectangle.Width - cols * BrickSize.Width) / 2,
                this.DisplayRectangle.Top + (this.DisplayRectangle.Height - rows * BrickSize.Height) / 2);

            this.undoStack.Clear();

            this.bricks = new Brick[rows, cols];
            this.blocks = null;

            this.Frozen = false;
            this.LastBlockSize = 0;
            this.TotalBrickCount = rows * cols;
            this.brickCount = this.TotalBrickCount; // Do not cause the firing of the change event!

            // Initialize the brick color table.
            this.BrickColorTable = new Dictionary<Color, int>();
            foreach (Color current in this.BrickColors)
            {
                this.BrickColorTable[current] = 0;
            }

            // Initialize the random color table. Keep in 
            // mind, total brick count is already even.
            List<Color> colors = new List<Color>();
            for (int index = 0; index < this.TotalBrickCount / 2; index++)
            {
                // Add next color pair.
                colors.Add(this.BrickColors[index % this.BrickColors.Length]);
                colors.Add(this.BrickColors[index % this.BrickColors.Length]);
            }

            // Keep me informed...
            Debug.Assert(this.TotalBrickCount == colors.Count);

            Random random = new Random((int)DateTime.Now.Ticks);
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    // Get next color index for the random color table.
                    int index = random.Next(0, colors.Count);

                    this.bricks[row, col] = new Brick(offset, BrickPanel.BrickSize);

                    this.bricks[row, col].Position = new Point(col, row); // not a mistake! -> point(x,y);

                    // Then apply corresponding color to current brick.
                    this.bricks[row, col].Color = colors[index];

                    // Update corresponding counter within brick color table.
                    this.BrickColorTable[colors[index]] += 1;

                    // Remove already applied color from random color table.
                    colors.RemoveAt(index);
                }
            }

            this.Invalidate();
        }

        public void Clear()
        {
            this.undoStack.Clear();

            this.bricks = null;
            this.blocks = null;

            this.LastBlockSize = 0;
            this.TotalBrickCount = 0;
            this.BrickCount = 0;

            this.Invalidate();
        }

        public void Undo()
        {
            if (this.CanUndo)
            {
                UndoneEventArgs undoItem = (this.undoStack.Pop() as UndoneEventArgs);

                // Safety check, but should never apply.
                if (undoItem == null) { return; }

                lock (this.critical)
                {
                    this.bricks = undoItem.BricksCopy;

                    this.brickCount = undoItem.BricksLeft; // Do not cause the firing of the change event!

                    this.ClearBlocks();

                    this.DoUpdateHighlight();

                    this.lastBrick = null;
                    this.Refresh();

                    this.DoGameStateCheck();
                }

                // Fire event, if possible.
                if (this.Undone != null) { this.Undone(this, undoItem); }

            }
        }

        public bool AddTopBricks()
        {
            int added = 0;

            if (this.bricks != null)
            {
                // Lock access to bricks matrix!
                lock (this.critical)
                {
                    int rows, cols;
                    this.GetDimension(out rows, out cols);

                    Point offset = new Point(
                        this.DisplayRectangle.Left + (this.DisplayRectangle.Width - cols * BrickSize.Width) / 2,
                        this.DisplayRectangle.Top + (this.DisplayRectangle.Height - rows * BrickSize.Height) / 2);

                    // Initialize the random color table. Keep in 
                    // mind, total brick count is already even.
                    List<Color> colors = new List<Color>();
                    for (int index = 0; index < cols; index++)
                    {
                        colors.Add(this.BrickColors[index % this.BrickColors.Length]);
                    }

                    Random random = new Random((int)DateTime.Now.Ticks);
                    for (int col = 0; col < cols; col++)
                    {
                        for (int row = 0; row < rows; row++)
                        {
                            // Determine current adding conditions.
                            bool a = (row - 1 >= 0 && this.bricks[row - 1, col] == null && this.bricks[row, col] != null);
                            bool b = (row + 1 == rows && this.bricks[row, col] == null);

                            if (!a && !b) { continue; }

                            if (a) { row--; }

                            // Get next color index for the random color table.
                            int index = random.Next(0, colors.Count);

                            this.bricks[row, col] = new Brick(offset, BrickPanel.BrickSize);

                            this.bricks[row, col].Position = new Point(col, row); // not a mistake! -> point(x,y);

                            // Then apply corresponding color to current brick.
                            this.bricks[row, col].Color = colors[index];

                            // Update corresponding counter within brick color table.
                            this.BrickColorTable[colors[index]] += 1;

                            // Remove already applied color from random color table.
                            colors.RemoveAt(index);

                            // A brick could be added.
                            added++;

                            if (a) { row++; }
                        }
                    }

                    if (added > 0)
                    {
                        this.Invalidate();
                        this.TotalBrickCount += added;
                        this.BrickCount += added;
                    }
                }
            }

            return added > 0;
        }

        public void SetDimension(int rows, int cols)
        {
            // The dimension must not be less or equal to zero!
            if (rows <= 0 || cols <= 0 || rows * cols <= 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            // The dimension must be even!
            else if (((rows * cols) & 1) != 0)
            {
                throw new ArgumentException("The matrix must contain an even number of bricks!");
            }
            else
            {
                int border = 0;
                switch (this.BorderStyle)
                {
                    case BorderStyle.Fixed3D:
                        border = 4;
                        break;
                    case BorderStyle.FixedSingle:
                        border = 2;
                        break;
                    default:
                        border = 0;
                        break;
                }

                int cx = cols * BrickSize.Width + this.Padding.Horizontal + border;
                int cy = rows * BrickSize.Height + this.Padding.Vertical + border;

                this.Size = new Size(cx, cy);
            }
        }

        public void GetDimension(out int rows, out int cols)
        {
            // An even number of bricks is required! Otherwise the game cannot be solved.

            rows = this.DisplayRectangle.Height / BrickSize.Height;
            cols = this.DisplayRectangle.Width / BrickSize.Width;

            // If last bit is 1 then the value is odd.
            if (((rows * cols) & 1) != 0)
            {
                rows -= 1;

                // Keep me informed...
                Debug.Assert(((rows * cols) > 0) && (((rows * cols) & 1) == 0));
            }
        }

        #endregion // Public method section.

        #region Inherited property and method reimplementation.

        protected override Size DefaultSize
        {
            get { return new Size(200, 300); }
        }

        protected override void OnMouseEnter(EventArgs args)
        {
            base.OnMouseEnter(args);
            this.lastBrick = null;
            this.ClearBlocks();
        }

        protected override void OnMouseMove(MouseEventArgs args)
        {
            base.OnMouseMove(args);

            if (!this.Frozen) { this.DoUpdateHighlight(); }
        }

        protected override void OnClick(EventArgs args)
        {
            base.OnClick(args);

            if (!this.Frozen && this.bricks != null && this.blocks != null)
            {
                // Clone current brick set and assign current 
                // score and left bricks as well.
                UndoneEventArgs undoItem = new UndoneEventArgs(
                    this.GetBricksCopy(),
                    Program.MainForm.ScoreValue,
                    Program.MainForm.BricksLeft);

                // Mark all chosen bricks as bricks to be removed.
                foreach (Brick current in this.blocks)
                {
                    this.BrickColorTable[this.bricks[current.Position.Y, current.Position.X].Color] -= 1;
                    this.bricks[current.Position.Y, current.Position.X] = null;
                }

                int rows = this.bricks.GetUpperBound(0) + 1;
                int cols = this.bricks.GetUpperBound(1) + 1;
                int off = 0;

                #region Row removal...

                // Search from left to right.
                for (int col = 0; col < cols; col++)
                {
                    // Search from bottom to top.
                    for (int row = rows - 1; row >= 0; row--)
                    {
                        // Check if current block is set as empty.
                        if (this.bricks[row, col] == null)
                        {
                            off = row; // Save current copy destination.

                            do // Find next non-empty block or begin of row.
                            {
                                row--;
                            }
                            while (row >= 0 && this.bricks[row, col] == null);

                            // Move all currently found non-empty blocks.
                            while (row >= 0 && this.bricks[row, col] != null)
                            {
                                // Move current block to next empty location.
                                this.bricks[off, col] = this.bricks[row, col];
                                this.bricks[row, col] = null;

                                // Adjust internal offset value.
                                this.bricks[off, col].Move(off, col);

                                // Move both indexes.
                                off--; row--;
                            }

                            // Move back to last known empty block.
                            while (row >= 0 && row < rows && this.bricks[row, col] == null)
                            {
                                row++;
                            }
                        }
                    }
                }

                #endregion Row removal...

                #region Column removal...

                // Obviously, it doesn't work without having a clone.
                Brick[,] clone = new Brick[rows, cols];

                // Search from left to right.
                for (int col = off = 0; col < cols; col++)
                {
                    bool empty = true;

                    // Search from bottom to top because it is very 
                    // likely that the lowest brick is not NULL!
                    for (int row = rows - 1; row >= 0 && empty; row--)
                    {
                        // Check if current block is set as empty.
                        empty &= this.bricks[row, col] == null;
                    }

                    if (!empty)
                    {
                        for (int row = 0; row < rows; row++)
                        {
                            clone[row, off] = this.bricks[row, col];
                            if (clone[row, off] != null)
                            {
                                // Adjust internal offset value.
                                clone[row, off].Move(row, off);
                            }
                        }
                        off++;
                    }
                }

                this.bricks = clone;

                #endregion // Column removal...

                // Add previous brick set to undo stack, if possible.
                if (undoItem != null && undoItem.BricksCopy != null)
                {
                    this.undoStack.Push(undoItem);
                }

                this.LastBlockSize = this.blocks.Count;
                this.BrickCount -= this.blocks.Count;
                this.ClearBlocks();

                this.DoUpdateHighlight();

                this.lastBrick = null;
                this.Refresh();

                this.DoGameStateCheck();
            }
        }

        protected override void OnDoubleClick(EventArgs args)
        {
            this.OnClick(args);
        }

        protected override void OnMouseLeave(EventArgs args)
        {
            base.OnMouseLeave(args);
            this.lastBrick = null;
            this.ClearBlocks();
        }

        protected override void OnPaintBackground(PaintEventArgs args)
        {
            // Change clipping region to minimize flickering while invalidating.
            if (this.bricks != null)
            {
                using (Region clip = new Region(args.ClipRectangle))
                {
                    foreach (Brick brick in this.bricks)
                    {
                        if (brick != null)
                        {
                            clip.Exclude(brick.Bounds);
                        }
                    }
                    args.Graphics.Clip = clip;
                }
            }
            base.OnPaintBackground(args);
        }

        protected override void OnPaint(PaintEventArgs args)
        {
            if (this.bricks != null)
            {
                foreach (Brick brick in this.bricks)
                {
                    if (brick != null)
                    {
                        brick.Draw(args.Graphics);
                    }
                }
            }

            this.RefreshText(args.Graphics);
        }

        protected override void OnTextChanged(EventArgs args)
        {
            base.OnTextChanged(args);
            this.Refresh();
        }

        #endregion // Inherited property and method reimplementation.

        #region Private method section.

        private void ClearBlocks()
        {
            if (this.blocks != null)
            {
                using (Graphics graphics = Graphics.FromHwnd(this.Handle))
                {
                    foreach (Brick current in this.blocks)
                    {
                        current.Draw(graphics);
                    }
                    this.blocks = null;

                    this.RefreshText(graphics);
                }
            }
        }

        private Brick BrickFromMousePoint()
        {
            return this.BrickFromPoint(this.PointToClient(Control.MousePosition));
        }

        private Brick BrickFromPoint(Point point)
        {
            if (this.bricks != null)
            {
                foreach (Brick brick in this.bricks)
                {
                    if (brick != null && brick.Contains(point))
                    {
                        return brick;
                    }
                }
            }
            return null;
        }

        private Brick BrickFromPosition(int row, int col)
        {
            if (this.bricks != null)
            {
                if (row >= 0 && row < this.bricks.GetLength(0))
                {
                    if (col >= 0 && col < this.bricks.GetLength(1))
                    {
                        return this.bricks[row, col];
                    }
                }
            }
            return null;
        }

        private void GetSiblingBricks(Brick brick, ref List<Brick> result)
        {
            if (brick != null)
            {
                result.Add(brick);

                int row = brick.Position.Y;
                int col = brick.Position.X;
                Brick sibling = null;

                // Sibling from the north.
                sibling = this.BrickFromPosition(row - 1, col);
                if (!result.Contains(sibling) && brick.IsSibling(sibling))
                {
                    this.GetSiblingBricks(sibling, ref result);
                }

                // Sibling from the east.
                sibling = this.BrickFromPosition(row, col + 1);
                if (!result.Contains(sibling) && brick.IsSibling(sibling))
                {
                    this.GetSiblingBricks(sibling, ref result);
                }

                // Sibling from the south.
                sibling = this.BrickFromPosition(row + 1, col);
                if (!result.Contains(sibling) && brick.IsSibling(sibling))
                {
                    this.GetSiblingBricks(sibling, ref result);
                }

                // Sibling from the west.
                sibling = this.BrickFromPosition(row, col - 1);
                if (!result.Contains(sibling) && brick.IsSibling(sibling))
                {
                    this.GetSiblingBricks(sibling, ref result);
                }
            }
        }

        private void DoGameStateCheck()
        {
            if (this.AllBricksRemoved != null && this.BrickCount == 0)
            {
                this.AllBricksRemoved(this, EventArgs.Empty);
            }
            else if (this.GameOver != null && !this.IsPossible)
            {
                this.GameOver(this, EventArgs.Empty);
            }
        }

        private void DoUpdateHighlight()
        {
            Brick brick = BrickFromMousePoint();
            if (brick != null && this.lastBrick != brick)
            {
                List<Brick> siblings = new List<Brick>();
                this.GetSiblingBricks(brick, ref siblings);

                using (Graphics graphics = Graphics.FromHwnd(this.Handle))
                {
                    // Restore border of previously highlighted blocks.
                    if (this.blocks != null && this.Highlight)
                    {
                        foreach (Brick current in this.blocks)
                        {
                            if (!siblings.Contains(current))
                            {
                                current.Draw(graphics);
                            }
                        }
                    }

                    if (siblings.Count > 1 && this.Highlight)
                    {
                        foreach (Brick sibling in siblings)
                        {
                            sibling.Draw(graphics, true);
                        }
                    }

                    this.blocks = (siblings.Count > 1) ? siblings : null;

                    this.RefreshText(graphics);
                }

                this.lastBrick = brick;
            }
            else if (brick == null && this.lastBrick != null)
            {
                this.lastBrick = null;
                this.ClearBlocks();
            }
        }

        private void RefreshText(Graphics graphics)
        {
            if (!String.IsNullOrEmpty(this.Text))
            {
                using (StringFormat format = new StringFormat())
                using (Brush brush = new SolidBrush(this.ForeColor))
                {
                    format.Alignment = StringAlignment.Center;
                    format.LineAlignment = StringAlignment.Center;
                    graphics.DrawString(this.Text, this.Font, brush, this.DisplayRectangle, format);
                }
            }
        }

        private Brick[,] GetBricksCopy()
        {
            bool valid = false;
            Brick[,] result = null;

            if (this.bricks != null)
            {
                // Lock access to bricks matrix!
                lock (this.critical)
                {
                    int rows = this.bricks.GetUpperBound(0) + 1;
                    int cols = this.bricks.GetUpperBound(1) + 1;

                    result = new Brick[rows, cols];

                    for (int row = 0; row < rows; row++)
                    {
                        for (int col = 0; col < cols; col++)
                        {
                            if (this.bricks[row, col] != null)
                            {
                                result[row, col] = this.bricks[row, col].Clone() as Brick;

                                // Set flag as soon as at least one brick is available.
                                if (!valid && result[row, col] != null) { valid = true; }
                            }
                            else
                            {
                                result[row, col] = null;
                            }
                        }
                    }
                }
            }
            return valid ? result : null;
        }

        #endregion // Private method section.
    }

    public class UndoneEventArgs : EventArgs
    {
        public UndoneEventArgs(Brick[,] copy, int score, int left)
            : base()
        {
            this.ScoreValue = score;
            this.BricksLeft = left;
            this.BricksCopy = copy;
        }

        public int ScoreValue { get; private set; }

        public int BricksLeft { get; private set; }

        public Brick[,] BricksCopy { get; private set; }
    }
}


