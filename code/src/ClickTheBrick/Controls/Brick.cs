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
using System.Windows.Forms;

namespace plexdata.ClickTheBrick
{
    public class Brick : ICloneable
    {
        public Brick(Point offset, Size size)
            : base()
        {
            this.Size = size != null ? new Size(size.Width, size.Height) : new Size();
            this.Offset = offset != null ? new Point(offset.X, offset.Y) : new Point();
            this.Position = new Point();
            this.Color = SystemColors.Control;
            this.Border = Border3DStyle.RaisedOuter;
        }

        public Brick(Brick other)
            : base()
        {
            // Safety check!
            if (other == null) { throw new ArgumentNullException("other"); }

            this.Size = other.Size;
            this.Offset = other.Offset;
            this.Position = other.Position;
            this.Color = other.Color;
            this.Border = other.Border;
        }

        public Size Size { get; private set; }

        public Point Offset { get; private set; }

        public Point Position { get; set; }

        public Color Color { get; set; }

        public Border3DStyle Border { get; set; }

        public Rectangle Bounds
        {
            get
            {
                return new Rectangle(
                    this.Offset.X + this.Position.X * this.Size.Width,
                    this.Offset.Y + this.Position.Y * this.Size.Height,
                    this.Size.Width,
                    this.Size.Height);
            }
        }

        public Boolean Contains(Point point)
        {
            return this.Bounds.Contains(point);
        }

        public Boolean IsSibling(Brick other)
        {
            if (other != null && other != this)
            {
                return this.Color == other.Color;
            }
            else
            {
                return false;
            }
        }

        public void Move(Int32 row, Int32 col)
        {
            Point position = new Point(this.Position.X, this.Position.Y);
            if (row >= 0) { position.Y = row; }
            if (col >= 0) { position.X = col; }
            this.Position = position;
        }

        public void Draw(Graphics graphics)
        {
            this.Draw(graphics, false);
        }

        public void Draw(Graphics graphics, Boolean focused)
        {
            if (graphics != null)
            {
                Rectangle bounds = this.Bounds;
                using (Brush brush = new SolidBrush(this.GetColor(focused)))
                {
                    graphics.FillRectangle(brush, bounds);
                }
                ControlPaint.DrawBorder3D(graphics, bounds, this.Border);
            }
        }

        private Color GetColor(Boolean focused)
        {
            if (focused)
            {
                return ControlPaint.Light(this.Color, 1.25f);
            }
            else
            {
                return this.Color;
            }
        }

        #region ICloneable implementation.

        public Object Clone()
        {
            return new Brick(this);
        }

        #endregion // ICloneable implementation.
    }
}
