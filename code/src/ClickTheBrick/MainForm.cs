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
using System.Linq;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;

namespace plexdata.ClickTheBrick
{
    public partial class MainForm : Form
    {
        // BUG: Undo handling for game type "Marathon" does not really work.

        private readonly int MarathonRepeats = 10;
        private readonly string DefaultRankingValue = "\u2014";

        private DateTime startTime = default(DateTime);
        private ApplicationSettings settings = new ApplicationSettings();
        private List<Highscore> highscore = new List<Highscore>();

        public MainForm()
            : base()
        {
            this.InitializeComponent();

            this.Text = AboutBox.Title;

            this.tbmTypeStandard.Tag = GameType.Standard;
            this.tbmTypeStandard.Name = GameType.Standard.ToString();

            this.tbmTypeCountdown.Tag = GameType.Countdown;
            this.tbmTypeCountdown.Name = GameType.Countdown.ToString();

            this.tbmTypeMarathon.Tag = GameType.Marathon;
            this.tbmTypeMarathon.Name = GameType.Marathon.ToString();

            this.marathonTimer.Tag = this.MarathonRepeats;

            this.UpdateToolbar();
        }

        [Browsable(false)]
        public int ScoreValue
        {
            get
            {
                int result = 0;
                if (Int32.TryParse(this.valScore.Text, out result))
                {
                    return result;
                }
                else
                {
                    return -1;
                }
            }
        }

        [Browsable(false)]
        public int BricksLeft
        {
            get
            {
                int result = 0;
                if (Int32.TryParse(this.valLeft.Text, out result))
                {
                    return result;
                }
                else
                {
                    return -1;
                }
            }
        }

        #region Main Window Event Handling.

        protected override void OnLoad(EventArgs args)
        {
            Size size = this.Size; // Get current window size. See below...

            base.OnLoad(args);

            ApplicationSettings helper = null;
            if (ApplicationSettings.Load(out helper))
            {
                this.settings = new ApplicationSettings(helper);
            }

            this.ApplySettings(this.settings);

            // BUGFIX: Main window position correction.
            // The main window is centered based on its default size which
            // is set in the designer. Therefore, it becomes necessary to 
            // correct this position based on real window size.
            size = Size.Subtract(this.Size, size);
            this.Location = new Point(
                Math.Max(this.Location.X - size.Width / 2, 10),
                Math.Max(this.Location.Y - size.Height / 2, 10));
        }

        protected override void OnClosed(EventArgs args)
        {
            base.OnClosed(args);

            this.settings.LastGame = this.GetGameType();
            ApplicationSettings.Save(this.settings);

            Highscore.Save(this.highscore);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keys)
        {
            if (keys == Keys.F1)
            {
                this.tbmInfoAbout.PerformClick();
            }
            else if (keys == Keys.F2 || keys == Keys.F5)
            {
                this.tbbStart.PerformClick();
            }
            else if ((keys & Keys.Control) == Keys.Control && (keys & Keys.Z) == Keys.Z)
            {
                this.tbbUndo.PerformClick();
            }

            return base.ProcessCmdKey(ref msg, keys);
        }

        #endregion // Main Window Event Handling.

        #region Brick Panel and Brick Progress Event Handling.

        private void OnBrickPanelSizeChanged(object sender, EventArgs args)
        {
            this.SetupBrickProgress();
        }

        private void OnBrickPanelBrickCountChanged(object sender, EventArgs args)
        {
            this.valLeft.Text = this.brickPanel.BrickCount.ToString();

            // Alternatively use: this.GetGameType() == GameType.Marathon...
            if (this.brickProgress.Visible)
            {
                this.valTotal.Text = this.brickPanel.TotalBrickCount.ToString();
            }

            this.SetScore(this.brickPanel.LastBlockSize * this.brickPanel.LastBlockSize);
            this.SetBiggest(this.brickPanel.LastBlockSize);

            this.UpdateToolbar();
        }

        private void OnBrickPanelGameOver(object sender, EventArgs args)
        {
            // Alternatively use: this.GetGameType() == GameType.Marathon...
            if (this.brickProgress.Visible)
            {
                // A Marathon game is over as soon as 
                // no other bricks could be added.
                if (!this.brickPanel.AddTopBricks())
                {
                    this.HandleGameOver();
                }
            }
            else
            {
                this.HandleGameOver();
            }
        }

        private void OnBrickPanelAllBricksRemoved(object sender, EventArgs args)
        {
            if (this.GetGameType() == GameType.Standard)
            {
                this.HandleGameOver();
            }
            else
            {
                // Continue playing for all non standard games.
                this.NextGame();
            }
        }

        private void OnBrickProgressVisibilityChanged(object sender, EventArgs args)
        {
            this.SetupBrickProgress();
        }

        private void OnBrickPanelUndone(object sender, UndoneEventArgs args)
        {
            // Simply restore those values.
            this.valScore.Text = args.ScoreValue.ToString();
            this.valLeft.Text = args.BricksLeft.ToString();
        }

        #endregion // Brick Panel and Brick Progress Event Handling.

        #region Toolbar Event Handling.

        private void OnToolBarPaint(object sender, PaintEventArgs args)
        {
            using (Brush brush = new SolidBrush(this.BackColor))
            {
                args.Graphics.FillRectangle(brush, args.ClipRectangle);
            }
        }

        private void OnButtonExitClick(object sender, EventArgs args)
        {
            this.StopGame();
            this.Close();
        }

        private void OnButtonTypeClick(object sender, EventArgs args)
        {
            this.tbbType.ShowDropDown();
        }

        private void OnTypeMenuItemClick(object sender, EventArgs args)
        {
            foreach (ToolStripItem item in this.tbbType.DropDownItems)
            {
                if (item is ToolStripMenuItem)
                {
                    (item as ToolStripMenuItem).Checked = false;

                    if (item == sender) { this.SetGameType(item); }
                }
            }
        }

        private void OnButtonUndoClick(object sender, EventArgs args)
        {
            this.brickPanel.Undo();
            this.UpdateToolbar();
        }

        private void OnButtonStartClick(object sender, EventArgs args)
        {
            this.ClearGame();
            this.SetupGame();
            this.StartGame(this.GetGameType());

            this.UpdateToolbar();
        }

        private void OnButtonSettingsClick(object sender, EventArgs args)
        {
            SettingsDialog dialog = new SettingsDialog(this.settings.BrickPanel);
            if (DialogResult.OK == dialog.ShowDialog(this))
            {
                this.settings.BrickPanel = new BrickPanelSettings(dialog.Settings);
                this.ApplySettings(this.settings.BrickPanel);
                this.ClearGame();
            }
        }

        private void OnButtonInfoClick(object sender, EventArgs args)
        {
            this.tbbInfo.ShowDropDown();
        }

        private void OnInfoMenuItemClick(object sender, EventArgs args)
        {
            if (sender == this.tbmInfoAbout)
            {
                AboutBox dialog = new AboutBox();
                dialog.ShowDialog(this);
            }
            else if (sender == this.tbmInfoHighscore)
            {
                HighscoreDialog dialog = new HighscoreDialog(this.highscore);
                dialog.ShowDialog(this);
            }
        }

        #endregion // Toolbar Event Handling.

        #region Elapse and Marathon Timer Event Handling.

        private void OnElapseTimerTick(object sender, EventArgs args)
        {
            this.HandleElapseTick(this.GetGameType());
        }

        private void OnMarathonTimerTick(object sender, EventArgs args)
        {
            if (this.brickProgress.Value >= this.brickProgress.Maximum)
            {
                // Reduce current timer value by 100 ms.
                int interval = this.marathonTimer.Interval - 100;

                // Check if minimum timer interval touches the lowest limit.
                if (interval < 100) { interval = 100; }

                // A Marathon game is over as soon as 
                // no other bricks could be added.
                if (!this.brickPanel.AddTopBricks())
                {
                    this.HandleGameOver();
                }
                else
                {
                    this.brickProgress.Value = 0;

                    if ((int)this.marathonTimer.Tag <= 0)
                    {
                        this.marathonTimer.Interval = interval;

                        this.marathonTimer.Tag = this.MarathonRepeats;
                    }
                    else
                    {
                        this.marathonTimer.Tag = (int)this.marathonTimer.Tag - 1;
                    }
                }
            }
            else
            {
                this.brickProgress.Value += 1;
            }
        }

        #endregion // Elapse and Marathon Timer Event Handling.

        #region Private member function section.

        private GameType GetGameType()
        {
            foreach (ToolStripItem item in this.tbbType.DropDownItems)
            {
                if (item is ToolStripMenuItem && (item as ToolStripMenuItem).Checked)
                {
                    if (item.Tag != null && item.Tag is GameType)
                    {
                        return (GameType)item.Tag;
                    }
                }
            }

            return GameType.Unknown;
        }

        private void SetGameType(ToolStripItem item)
        {
            try
            {
                this.valType.Text = String.Empty;
                if (item != null && item is ToolStripMenuItem && item.Tag != null && item.Tag is GameType)
                {
                    GameType type = (GameType)item.Tag;
                    if (type == GameType.Standard || type == GameType.Countdown || type == GameType.Marathon)
                    {
                        this.valType.Text = type.ToString();
                        (item as ToolStripMenuItem).Checked = true;
                        this.brickProgress.Visible = type == GameType.Marathon;
                        this.ClearGame();
                    }
                    else
                    {
                        throw new NotSupportedException(
                            "Game type " + (int)type + " is not supported!");
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, this.Text,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetupGame()
        {
            this.elapseTimer.Stop();
            this.marathonTimer.Stop();

            this.valTime.Text = Highscore.DefaultTimeValue;

            this.brickPanel.Text = null;
            this.brickPanel.Frozen = true;

            do  // Try setting up the game as long 
            {   // as no sibling bricks exist!
                this.brickPanel.Setup();
            }
            while (!brickPanel.IsPossible);

            this.brickProgress.Value = 0;
            this.brickProgress.Maximum = this.brickPanel.Dimension.X;

            this.valLeft.Text = this.brickPanel.BrickCount.ToString();
            this.valTotal.Text = this.brickPanel.TotalBrickCount.ToString();
        }

        private void StartGame(GameType type)
        {
            this.elapseTimer.Stop();
            this.marathonTimer.Stop();

            this.valTime.Text = Highscore.DefaultTimeValue;

            this.brickPanel.Text = null;
            this.brickPanel.Frozen = false;

            switch (type)
            {
                case GameType.Standard:

                    this.startTime = DateTime.Now;
                    break;

                case GameType.Countdown:

                    // 0.5 seconds per brick (plus 1) for the countdown.
                    this.startTime = DateTime.Now.AddSeconds(this.brickPanel.TotalBrickCount / 2 + 1);
                    break;

                case GameType.Marathon:

                    this.brickProgress.Value = 0;
                    this.startTime = DateTime.Now;
                    this.marathonTimer.Start();
                    break;

                default:
                    return; // Not (yet) supported.
            }

            // Remove unused milliseconds.
            this.startTime = this.startTime.AddMilliseconds(-this.startTime.Millisecond);

            // Update time label and start timer.
            this.HandleElapseTick(type);
            this.elapseTimer.Start();
        }

        private void NextGame()
        {
            int total = 0;

            // Determine current total count.
            bool success = Int32.TryParse(this.valTotal.Text, out total);

            // Keep me informed...
            Debug.Assert(success);

            do  // Try setting up the game as long 
            {   // as no sibling bricks exist!
                this.brickPanel.Setup();
            }
            while (!brickPanel.IsPossible);

            this.valLeft.Text = this.brickPanel.BrickCount.ToString();
            this.valTotal.Text = (total + this.brickPanel.TotalBrickCount).ToString();
        }

        private void StopGame()
        {
            this.elapseTimer.Stop();
            this.marathonTimer.Stop();

            this.brickPanel.Frozen = true;

            this.UpdateToolbar();
        }

        private void ClearGame()
        {
            this.elapseTimer.Stop();
            this.marathonTimer.Stop();

            this.valTime.Text = Highscore.DefaultTimeValue;
            this.valRanking.Text = this.DefaultRankingValue;

            this.brickPanel.Text = null;
            this.brickPanel.Frozen = true;

            this.brickPanel.Clear();

            this.brickProgress.Value = 0;
            this.brickProgress.Maximum = 0;

            this.valLeft.Text = this.brickPanel.BrickCount.ToString();
            this.valTotal.Text = this.brickPanel.TotalBrickCount.ToString();

            this.SetScore(0);
            this.SetBiggest(0);
        }

        private void SetupBrickProgress()
        {
            if (this.brickProgress.Visible)
            {
                this.brickProgress.Top =
                    this.brickPanel.Bottom + this.brickPanel.Margin.Bottom +
                    this.brickProgress.Margin.Top;

                this.brickProgress.Left = this.brickPanel.Left;
                this.brickProgress.Width = this.brickPanel.Width;
            }
        }

        private void SetScore(int value)
        {
            int helper = 0;
            if (value <= 0)
            {
                this.valScore.Text = helper.ToString();
            }
            else if (Int32.TryParse(this.valScore.Text, out helper))
            {
                this.valScore.Text = (helper + value).ToString();
            }
        }

        private void SetBiggest(int value)
        {
            int helper = 0;
            if (value <= 0)
            {
                this.valBiggest.Text = helper.ToString();
            }
            else if (Int32.TryParse(this.valBiggest.Text, out helper))
            {
                if (helper < value) { this.valBiggest.Text = value.ToString(); }
            }
        }

        private void HandleGameOver()
        {
            this.HandleGameOver(false);
        }

        private void HandleGameOver(bool elapsed)
        {
            this.StopGame();

            // All bricks removed? This gets a bonus...
            if (this.brickPanel.BrickCount == 0)
            {
                this.brickPanel.Text = "Awesome!";
                this.SetScore(this.brickPanel.TotalBrickCount * 4);
            }
            else if (this.brickPanel.BrickCount == 1)
            {
                this.brickPanel.Text = "Not bad!";
                this.SetScore(this.brickPanel.TotalBrickCount);
            }
            else if (elapsed)
            {
                this.brickPanel.Text = "Time Over";
            }
            else
            {
                this.brickPanel.Text = "Game Over";
            }

            Highscore item = new Highscore(DateTime.Now);
            item.Type = this.GetGameType();
            item.Time = this.valTime.Text;

            int helper;
            if (Int32.TryParse(this.valTotal.Text, out helper)) { item.Total = helper; }
            if (Int32.TryParse(this.valLeft.Text, out helper)) { item.Left = helper; }
            if (Int32.TryParse(this.valBiggest.Text, out helper)) { item.Biggest = helper; }
            if (Int32.TryParse(this.valScore.Text, out helper)) { item.Score = helper; }

            if (item.IsValid)
            {
                this.highscore.Add(item);

                // Calculate current ranking for current game type.
                List<Highscore> copy = new List<Highscore>(this.highscore.Where(type => type.Type == item.Type));
                copy.Sort(delegate(Highscore left, Highscore right) { return right.Score.CompareTo(left.Score); });
                this.valRanking.Text = (copy.FindIndex(value => value.Score == item.Score) + 1).ToString();
            }
        }

        private void HandleElapseTick(GameType type)
        {
            if (type == GameType.Standard || type == GameType.Marathon)
            {
                TimeSpan left = DateTime.Now.Subtract(this.startTime);
                this.valTime.Text = Highscore.Format(left);
            }
            else if (type == GameType.Countdown)
            {
                TimeSpan left = this.startTime.Subtract(DateTime.Now);
                if (left.TotalSeconds <= 0)
                {
                    this.HandleGameOver(true);
                }
                else
                {
                    this.valTime.Text = Highscore.Format(left);
                }
            }
        }

        private void UpdateToolbar()
        {
            this.tbbUndo.Enabled = this.brickPanel.CanUndo;
        }

        private void ApplySettings(ApplicationSettings settings)
        {
            if (settings.LastGame != GameType.Unknown)
            {
                this.SetGameType(this.tbbType.DropDownItems[settings.LastGame.ToString()]);
            }
            else
            {
                this.SetGameType(this.tbbType.DropDownItems[GameType.Standard.ToString()]);
            }

            this.panScore.Font = settings.Font;
            this.ForeColor = settings.ForeColor;
            this.BackColor = settings.BackColor;

            this.ApplySettings(settings.BrickPanel);
        }

        private void ApplySettings(BrickPanelSettings settings)
        {
            this.brickPanel.Font = settings.Font;
            this.brickPanel.ForeColor = settings.ForeColor;
            this.brickPanel.BackColor = settings.BackColor;
            this.brickPanel.Highlight = settings.Highlight;
            this.brickPanel.BrickColors = settings.BrickColors;

            this.brickProgress.ForeColorLight = ControlPaint.Light(settings.BackColor, 1.25F);
            this.brickProgress.ForeColorDark = ControlPaint.Dark(settings.BackColor, 0.25F);

            if (settings.Dimension.IsValid)
            {
                this.brickPanel.Dimension = settings.Dimension.ToPoint();
            }
            else
            {
                // Do an error correction on the global settings!
                this.settings.BrickPanel.Dimension.FromPoint(this.brickPanel.Dimension);
            }
        }

        #endregion // Private member function section.
    }
}
