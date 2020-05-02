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

namespace plexdata.ClickTheBrick
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.toolBar = new System.Windows.Forms.ToolStrip();
            this.tbbExit = new System.Windows.Forms.ToolStripButton();
            this.tbsSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.tbbType = new System.Windows.Forms.ToolStripSplitButton();
            this.tbmTypeStandard = new System.Windows.Forms.ToolStripMenuItem();
            this.tbmTypeCountdown = new System.Windows.Forms.ToolStripMenuItem();
            this.tbmTypeMarathon = new System.Windows.Forms.ToolStripMenuItem();
            this.tbbUndo = new System.Windows.Forms.ToolStripButton();
            this.tbbStart = new System.Windows.Forms.ToolStripButton();
            this.tbsSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.tbbSettings = new System.Windows.Forms.ToolStripButton();
            this.tbsSep3 = new System.Windows.Forms.ToolStripSeparator();
            this.tbbInfo = new System.Windows.Forms.ToolStripSplitButton();
            this.tbmInfoHighscore = new System.Windows.Forms.ToolStripMenuItem();
            this.tbmInfoSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.tbmInfoAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.elapseTimer = new System.Windows.Forms.Timer(this.components);
            this.panScore = new System.Windows.Forms.Panel();
            this.lblRanking = new System.Windows.Forms.Label();
            this.valTime = new System.Windows.Forms.Label();
            this.lblTime = new System.Windows.Forms.Label();
            this.valRanking = new System.Windows.Forms.Label();
            this.valType = new System.Windows.Forms.Label();
            this.lblType = new System.Windows.Forms.Label();
            this.valScore = new System.Windows.Forms.Label();
            this.lblScore = new System.Windows.Forms.Label();
            this.valBiggest = new System.Windows.Forms.Label();
            this.lblBiggest = new System.Windows.Forms.Label();
            this.valLeft = new System.Windows.Forms.Label();
            this.lblLeft = new System.Windows.Forms.Label();
            this.valTotal = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.panGame = new System.Windows.Forms.Panel();
            this.marathonTimer = new System.Windows.Forms.Timer(this.components);
            this.brickProgress = new plexdata.Controls.ProgressBar3D();
            this.brickPanel = new plexdata.ClickTheBrick.BrickPanel();
            this.toolBar.SuspendLayout();
            this.panScore.SuspendLayout();
            this.panGame.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolBar
            // 
            this.toolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbbExit,
            this.tbsSep1,
            this.tbbType,
            this.tbbUndo,
            this.tbbStart,
            this.tbsSep2,
            this.tbbSettings,
            this.tbsSep3,
            this.tbbInfo});
            this.toolBar.Location = new System.Drawing.Point(0, 0);
            this.toolBar.Name = "toolBar";
            this.toolBar.Size = new System.Drawing.Size(394, 39);
            this.toolBar.TabIndex = 0;
            this.toolBar.Paint += new System.Windows.Forms.PaintEventHandler(this.OnToolBarPaint);
            // 
            // tbbExit
            // 
            this.tbbExit.AutoToolTip = false;
            this.tbbExit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbbExit.Image = global::plexdata.ClickTheBrick.Properties.Resources.Exit;
            this.tbbExit.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tbbExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbbExit.Name = "tbbExit";
            this.tbbExit.Size = new System.Drawing.Size(36, 36);
            this.tbbExit.Text = "Exit";
            this.tbbExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tbbExit.Click += new System.EventHandler(this.OnButtonExitClick);
            // 
            // tbsSep1
            // 
            this.tbsSep1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.tbsSep1.Name = "tbsSep1";
            this.tbsSep1.Size = new System.Drawing.Size(6, 39);
            // 
            // tbbType
            // 
            this.tbbType.AutoToolTip = false;
            this.tbbType.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbbType.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbmTypeStandard,
            this.tbmTypeCountdown,
            this.tbmTypeMarathon});
            this.tbbType.Image = global::plexdata.ClickTheBrick.Properties.Resources.GameType;
            this.tbbType.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tbbType.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbbType.Name = "tbbType";
            this.tbbType.Size = new System.Drawing.Size(48, 36);
            this.tbbType.Text = "Type";
            this.tbbType.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tbbType.Click += new System.EventHandler(this.OnButtonTypeClick);
            // 
            // tbmTypeStandard
            // 
            this.tbmTypeStandard.CheckOnClick = true;
            this.tbmTypeStandard.Name = "tbmTypeStandard";
            this.tbmTypeStandard.Size = new System.Drawing.Size(137, 22);
            this.tbmTypeStandard.Tag = "";
            this.tbmTypeStandard.Text = "Standard";
            this.tbmTypeStandard.Click += new System.EventHandler(this.OnTypeMenuItemClick);
            // 
            // tbmTypeCountdown
            // 
            this.tbmTypeCountdown.CheckOnClick = true;
            this.tbmTypeCountdown.Name = "tbmTypeCountdown";
            this.tbmTypeCountdown.Size = new System.Drawing.Size(137, 22);
            this.tbmTypeCountdown.Text = "Countdown";
            this.tbmTypeCountdown.Click += new System.EventHandler(this.OnTypeMenuItemClick);
            // 
            // tbmTypeMarathon
            // 
            this.tbmTypeMarathon.CheckOnClick = true;
            this.tbmTypeMarathon.Name = "tbmTypeMarathon";
            this.tbmTypeMarathon.Size = new System.Drawing.Size(137, 22);
            this.tbmTypeMarathon.Text = "Marathon";
            this.tbmTypeMarathon.Click += new System.EventHandler(this.OnTypeMenuItemClick);
            // 
            // tbbUndo
            // 
            this.tbbUndo.AutoToolTip = false;
            this.tbbUndo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbbUndo.Image = global::plexdata.ClickTheBrick.Properties.Resources.Undo;
            this.tbbUndo.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tbbUndo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbbUndo.Name = "tbbUndo";
            this.tbbUndo.Size = new System.Drawing.Size(36, 36);
            this.tbbUndo.Text = "Undo";
            this.tbbUndo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tbbUndo.Click += new System.EventHandler(this.OnButtonUndoClick);
            // 
            // tbbStart
            // 
            this.tbbStart.AutoToolTip = false;
            this.tbbStart.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbbStart.Image = global::plexdata.ClickTheBrick.Properties.Resources.Start;
            this.tbbStart.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tbbStart.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbbStart.Name = "tbbStart";
            this.tbbStart.Size = new System.Drawing.Size(36, 36);
            this.tbbStart.Text = "Start";
            this.tbbStart.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tbbStart.Click += new System.EventHandler(this.OnButtonStartClick);
            // 
            // tbsSep2
            // 
            this.tbsSep2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.tbsSep2.Name = "tbsSep2";
            this.tbsSep2.Size = new System.Drawing.Size(6, 39);
            // 
            // tbbSettings
            // 
            this.tbbSettings.AutoToolTip = false;
            this.tbbSettings.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbbSettings.Image = ((System.Drawing.Image)(resources.GetObject("tbbSettings.Image")));
            this.tbbSettings.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tbbSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbbSettings.Name = "tbbSettings";
            this.tbbSettings.Size = new System.Drawing.Size(36, 36);
            this.tbbSettings.Text = "Settings";
            this.tbbSettings.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tbbSettings.Click += new System.EventHandler(this.OnButtonSettingsClick);
            // 
            // tbsSep3
            // 
            this.tbsSep3.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.tbsSep3.Name = "tbsSep3";
            this.tbsSep3.Size = new System.Drawing.Size(6, 39);
            // 
            // tbbInfo
            // 
            this.tbbInfo.AutoToolTip = false;
            this.tbbInfo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbbInfo.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbmInfoHighscore,
            this.tbmInfoSep1,
            this.tbmInfoAbout});
            this.tbbInfo.Image = global::plexdata.ClickTheBrick.Properties.Resources.Info;
            this.tbbInfo.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tbbInfo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbbInfo.Name = "tbbInfo";
            this.tbbInfo.Size = new System.Drawing.Size(48, 36);
            this.tbbInfo.Text = "Settings";
            this.tbbInfo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tbbInfo.Click += new System.EventHandler(this.OnButtonInfoClick);
            // 
            // tbmInfoHighscore
            // 
            this.tbmInfoHighscore.Name = "tbmInfoHighscore";
            this.tbmInfoHighscore.Size = new System.Drawing.Size(137, 22);
            this.tbmInfoHighscore.Text = "Highscore...";
            this.tbmInfoHighscore.Click += new System.EventHandler(this.OnInfoMenuItemClick);
            // 
            // tbmInfoSep1
            // 
            this.tbmInfoSep1.Name = "tbmInfoSep1";
            this.tbmInfoSep1.Size = new System.Drawing.Size(134, 6);
            // 
            // tbmInfoAbout
            // 
            this.tbmInfoAbout.Name = "tbmInfoAbout";
            this.tbmInfoAbout.Size = new System.Drawing.Size(137, 22);
            this.tbmInfoAbout.Text = "About...";
            this.tbmInfoAbout.Click += new System.EventHandler(this.OnInfoMenuItemClick);
            // 
            // elapseTimer
            // 
            this.elapseTimer.Interval = 1000;
            this.elapseTimer.Tick += new System.EventHandler(this.OnElapseTimerTick);
            // 
            // panScore
            // 
            this.panScore.Controls.Add(this.lblRanking);
            this.panScore.Controls.Add(this.valTime);
            this.panScore.Controls.Add(this.lblTime);
            this.panScore.Controls.Add(this.valRanking);
            this.panScore.Controls.Add(this.valType);
            this.panScore.Controls.Add(this.lblType);
            this.panScore.Controls.Add(this.valScore);
            this.panScore.Controls.Add(this.lblScore);
            this.panScore.Controls.Add(this.valBiggest);
            this.panScore.Controls.Add(this.lblBiggest);
            this.panScore.Controls.Add(this.valLeft);
            this.panScore.Controls.Add(this.lblLeft);
            this.panScore.Controls.Add(this.valTotal);
            this.panScore.Controls.Add(this.lblTotal);
            this.panScore.Dock = System.Windows.Forms.DockStyle.Left;
            this.panScore.Location = new System.Drawing.Point(0, 39);
            this.panScore.Name = "panScore";
            this.panScore.Padding = new System.Windows.Forms.Padding(6);
            this.panScore.Size = new System.Drawing.Size(200, 183);
            this.panScore.TabIndex = 1;
            // 
            // lblRanking
            // 
            this.lblRanking.AutoSize = true;
            this.lblRanking.Location = new System.Drawing.Point(9, 55);
            this.lblRanking.Name = "lblRanking";
            this.lblRanking.Size = new System.Drawing.Size(50, 13);
            this.lblRanking.TabIndex = 4;
            this.lblRanking.Text = "Ranking:";
            // 
            // valTime
            // 
            this.valTime.AutoSize = true;
            this.valTime.Location = new System.Drawing.Point(85, 9);
            this.valTime.Name = "valTime";
            this.valTime.Size = new System.Drawing.Size(25, 13);
            this.valTime.TabIndex = 1;
            this.valTime.Text = "???";
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.Location = new System.Drawing.Point(9, 9);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(33, 13);
            this.lblTime.TabIndex = 0;
            this.lblTime.Text = "Time:";
            // 
            // valRanking
            // 
            this.valRanking.AutoSize = true;
            this.valRanking.Location = new System.Drawing.Point(85, 55);
            this.valRanking.Name = "valRanking";
            this.valRanking.Size = new System.Drawing.Size(25, 13);
            this.valRanking.TabIndex = 5;
            this.valRanking.Text = "???";
            // 
            // valType
            // 
            this.valType.AutoSize = true;
            this.valType.Location = new System.Drawing.Point(85, 147);
            this.valType.Name = "valType";
            this.valType.Size = new System.Drawing.Size(25, 13);
            this.valType.TabIndex = 13;
            this.valType.Text = "???";
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Location = new System.Drawing.Point(9, 147);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(34, 13);
            this.lblType.TabIndex = 12;
            this.lblType.Text = "Type:";
            // 
            // valScore
            // 
            this.valScore.AutoSize = true;
            this.valScore.Location = new System.Drawing.Point(85, 32);
            this.valScore.Name = "valScore";
            this.valScore.Size = new System.Drawing.Size(25, 13);
            this.valScore.TabIndex = 3;
            this.valScore.Text = "???";
            // 
            // lblScore
            // 
            this.lblScore.AutoSize = true;
            this.lblScore.Location = new System.Drawing.Point(9, 32);
            this.lblScore.Name = "lblScore";
            this.lblScore.Size = new System.Drawing.Size(38, 13);
            this.lblScore.TabIndex = 2;
            this.lblScore.Text = "Score:";
            // 
            // valBiggest
            // 
            this.valBiggest.AutoSize = true;
            this.valBiggest.Location = new System.Drawing.Point(85, 78);
            this.valBiggest.Name = "valBiggest";
            this.valBiggest.Size = new System.Drawing.Size(25, 13);
            this.valBiggest.TabIndex = 7;
            this.valBiggest.Text = "???";
            // 
            // lblBiggest
            // 
            this.lblBiggest.AutoSize = true;
            this.lblBiggest.Location = new System.Drawing.Point(9, 78);
            this.lblBiggest.Name = "lblBiggest";
            this.lblBiggest.Size = new System.Drawing.Size(45, 13);
            this.lblBiggest.TabIndex = 6;
            this.lblBiggest.Text = "Biggest:";
            // 
            // valLeft
            // 
            this.valLeft.AutoSize = true;
            this.valLeft.Location = new System.Drawing.Point(85, 101);
            this.valLeft.Name = "valLeft";
            this.valLeft.Size = new System.Drawing.Size(25, 13);
            this.valLeft.TabIndex = 9;
            this.valLeft.Text = "???";
            // 
            // lblLeft
            // 
            this.lblLeft.AutoSize = true;
            this.lblLeft.Location = new System.Drawing.Point(9, 101);
            this.lblLeft.Name = "lblLeft";
            this.lblLeft.Size = new System.Drawing.Size(28, 13);
            this.lblLeft.TabIndex = 8;
            this.lblLeft.Text = "Left:";
            // 
            // valTotal
            // 
            this.valTotal.AutoSize = true;
            this.valTotal.Location = new System.Drawing.Point(85, 124);
            this.valTotal.Name = "valTotal";
            this.valTotal.Size = new System.Drawing.Size(25, 13);
            this.valTotal.TabIndex = 11;
            this.valTotal.Text = "???";
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Location = new System.Drawing.Point(9, 124);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(34, 13);
            this.lblTotal.TabIndex = 10;
            this.lblTotal.Text = "Total:";
            // 
            // panGame
            // 
            this.panGame.AutoSize = true;
            this.panGame.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panGame.Controls.Add(this.brickProgress);
            this.panGame.Controls.Add(this.brickPanel);
            this.panGame.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panGame.Location = new System.Drawing.Point(200, 39);
            this.panGame.Name = "panGame";
            this.panGame.Padding = new System.Windows.Forms.Padding(6);
            this.panGame.Size = new System.Drawing.Size(194, 183);
            this.panGame.TabIndex = 2;
            // 
            // marathonTimer
            // 
            this.marathonTimer.Interval = 1000;
            this.marathonTimer.Tick += new System.EventHandler(this.OnMarathonTimerTick);
            // 
            // brickProgress
            // 
            this.brickProgress.Location = new System.Drawing.Point(9, 144);
            this.brickProgress.Name = "brickProgress";
            this.brickProgress.Size = new System.Drawing.Size(173, 23);
            this.brickProgress.TabIndex = 1;
            this.brickProgress.TextEnabled = false;
            this.brickProgress.VisibleChanged += new System.EventHandler(this.OnBrickProgressVisibilityChanged);
            // 
            // brickPanel
            // 
            this.brickPanel.BackColor = System.Drawing.SystemColors.ControlDark;
            this.brickPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.brickPanel.BrickColors = new System.Drawing.Color[] {
        System.Drawing.Color.Blue,
        System.Drawing.Color.Yellow,
        System.Drawing.Color.Green,
        System.Drawing.Color.Red};
            this.brickPanel.Dimension = new System.Drawing.Point(5, 4);
            this.brickPanel.Frozen = false;
            this.brickPanel.Location = new System.Drawing.Point(9, 9);
            this.brickPanel.Name = "brickPanel";
            this.brickPanel.Padding = new System.Windows.Forms.Padding(10);
            this.brickPanel.Size = new System.Drawing.Size(173, 129);
            this.brickPanel.TabIndex = 0;
            this.brickPanel.Text = "???";
            this.brickPanel.GameOver += new System.EventHandler<System.EventArgs>(this.OnBrickPanelGameOver);
            this.brickPanel.AllBricksRemoved += new System.EventHandler<System.EventArgs>(this.OnBrickPanelAllBricksRemoved);
            this.brickPanel.BrickCountChanged += new System.EventHandler<System.EventArgs>(this.OnBrickPanelBrickCountChanged);
            this.brickPanel.Undone += new System.EventHandler<plexdata.ClickTheBrick.UndoneEventArgs>(this.OnBrickPanelUndone);
            this.brickPanel.SizeChanged += new System.EventHandler(this.OnBrickPanelSizeChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(394, 222);
            this.Controls.Add(this.panGame);
            this.Controls.Add(this.panScore);
            this.Controls.Add(this.toolBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(400, 250);
            this.Name = "MainForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "???";
            this.toolBar.ResumeLayout(false);
            this.toolBar.PerformLayout();
            this.panScore.ResumeLayout(false);
            this.panScore.PerformLayout();
            this.panGame.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private plexdata.ClickTheBrick.BrickPanel brickPanel;
        private System.Windows.Forms.ToolStrip toolBar;
        private System.Windows.Forms.Timer elapseTimer;
        private System.Windows.Forms.ToolStripButton tbbExit;
        private System.Windows.Forms.ToolStripSplitButton tbbType;
        private System.Windows.Forms.ToolStripMenuItem tbmTypeStandard;
        private System.Windows.Forms.ToolStripMenuItem tbmTypeCountdown;
        private System.Windows.Forms.ToolStripMenuItem tbmTypeMarathon;
        private System.Windows.Forms.ToolStripButton tbbSettings;
        private System.Windows.Forms.ToolStripButton tbbStart;
        private System.Windows.Forms.ToolStripSeparator tbsSep1;
        private System.Windows.Forms.ToolStripSeparator tbsSep2;
        private System.Windows.Forms.ToolStripSeparator tbsSep3;
        private System.Windows.Forms.Panel panScore;
        private System.Windows.Forms.Panel panGame;
        private System.Windows.Forms.Label valBiggest;
        private System.Windows.Forms.Label lblBiggest;
        private System.Windows.Forms.Label valLeft;
        private System.Windows.Forms.Label lblLeft;
        private System.Windows.Forms.Label valTotal;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label valTime;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Label valType;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.Label valScore;
        private System.Windows.Forms.Label lblScore;
        private System.Windows.Forms.ToolStripSplitButton tbbInfo;
        private System.Windows.Forms.ToolStripMenuItem tbmInfoHighscore;
        private System.Windows.Forms.ToolStripSeparator tbmInfoSep1;
        private System.Windows.Forms.ToolStripMenuItem tbmInfoAbout;
        private plexdata.Controls.ProgressBar3D brickProgress;
        private System.Windows.Forms.Timer marathonTimer;
        private System.Windows.Forms.Label lblRanking;
        private System.Windows.Forms.Label valRanking;
        private System.Windows.Forms.ToolStripButton tbbUndo;

    }
}

