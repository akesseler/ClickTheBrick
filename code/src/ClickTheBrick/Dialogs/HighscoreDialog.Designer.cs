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
    partial class HighscoreDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HighscoreDialog));
            this.btnOK = new System.Windows.Forms.Button();
            this.valHighestScore = new System.Windows.Forms.Label();
            this.valBiggestBlock = new System.Windows.Forms.Label();
            this.valNeededTime = new System.Windows.Forms.Label();
            this.cmbType = new System.Windows.Forms.ComboBox();
            this.valSmallestRemaining = new System.Windows.Forms.Label();
            this.lblHighestScore = new System.Windows.Forms.Label();
            this.lblBiggestBlock = new System.Windows.Forms.Label();
            this.lblNeededTime = new System.Windows.Forms.Label();
            this.lblSmallestRemaining = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.lblType = new System.Windows.Forms.Label();
            this.scoreList = new System.Windows.Forms.ListView();
            this.colTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colTotal = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colLeft = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colBiggest = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colScore = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.datHighestScore = new System.Windows.Forms.Label();
            this.datBiggestBlock = new System.Windows.Forms.Label();
            this.datNeededTime = new System.Windows.Forms.Label();
            this.datSmallestRemaining = new System.Windows.Forms.Label();
            this.btnOpen = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnOK.Image = global::plexdata.ClickTheBrick.Properties.Resources.Apply;
            this.btnOK.Location = new System.Drawing.Point(372, 325);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(80, 25);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "&OK";
            this.btnOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnOK.Click += new System.EventHandler(this.OnButtonOkClick);
            // 
            // valHighestScore
            // 
            this.valHighestScore.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.valHighestScore.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.valHighestScore.Location = new System.Drawing.Point(120, 42);
            this.valHighestScore.Margin = new System.Windows.Forms.Padding(3);
            this.valHighestScore.Name = "valHighestScore";
            this.valHighestScore.Size = new System.Drawing.Size(112, 21);
            this.valHighestScore.TabIndex = 4;
            this.valHighestScore.Text = "???";
            this.valHighestScore.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolTip.SetToolTip(this.valHighestScore, "Highest score value.");
            // 
            // valBiggestBlock
            // 
            this.valBiggestBlock.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.valBiggestBlock.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.valBiggestBlock.Location = new System.Drawing.Point(120, 69);
            this.valBiggestBlock.Margin = new System.Windows.Forms.Padding(3);
            this.valBiggestBlock.Name = "valBiggestBlock";
            this.valBiggestBlock.Size = new System.Drawing.Size(112, 21);
            this.valBiggestBlock.TabIndex = 7;
            this.valBiggestBlock.Text = "???";
            this.valBiggestBlock.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolTip.SetToolTip(this.valBiggestBlock, "Size of biggest block.");
            // 
            // valNeededTime
            // 
            this.valNeededTime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.valNeededTime.AutoEllipsis = true;
            this.valNeededTime.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.valNeededTime.Location = new System.Drawing.Point(120, 96);
            this.valNeededTime.Margin = new System.Windows.Forms.Padding(3);
            this.valNeededTime.Name = "valNeededTime";
            this.valNeededTime.Size = new System.Drawing.Size(112, 21);
            this.valNeededTime.TabIndex = 10;
            this.valNeededTime.Text = "???";
            this.valNeededTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolTip.SetToolTip(this.valNeededTime, "Shortest needed time.");
            // 
            // cmbType
            // 
            this.cmbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbType.FormattingEnabled = true;
            this.cmbType.Location = new System.Drawing.Point(120, 15);
            this.cmbType.Name = "cmbType";
            this.cmbType.Size = new System.Drawing.Size(120, 21);
            this.cmbType.TabIndex = 2;
            this.cmbType.SelectedIndexChanged += new System.EventHandler(this.OnTypeSelectedIndexChanged);
            // 
            // valSmallestRemaining
            // 
            this.valSmallestRemaining.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.valSmallestRemaining.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.valSmallestRemaining.Location = new System.Drawing.Point(120, 123);
            this.valSmallestRemaining.Margin = new System.Windows.Forms.Padding(3);
            this.valSmallestRemaining.Name = "valSmallestRemaining";
            this.valSmallestRemaining.Size = new System.Drawing.Size(112, 21);
            this.valSmallestRemaining.TabIndex = 13;
            this.valSmallestRemaining.Text = "???";
            this.valSmallestRemaining.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolTip.SetToolTip(this.valSmallestRemaining, "Smallest remaining brick block.");
            // 
            // lblHighestScore
            // 
            this.lblHighestScore.Location = new System.Drawing.Point(12, 42);
            this.lblHighestScore.Margin = new System.Windows.Forms.Padding(3);
            this.lblHighestScore.Name = "lblHighestScore";
            this.lblHighestScore.Size = new System.Drawing.Size(100, 21);
            this.lblHighestScore.TabIndex = 3;
            this.lblHighestScore.Text = "Highest Score:";
            this.lblHighestScore.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblBiggestBlock
            // 
            this.lblBiggestBlock.Location = new System.Drawing.Point(12, 69);
            this.lblBiggestBlock.Margin = new System.Windows.Forms.Padding(3);
            this.lblBiggestBlock.Name = "lblBiggestBlock";
            this.lblBiggestBlock.Size = new System.Drawing.Size(100, 21);
            this.lblBiggestBlock.TabIndex = 6;
            this.lblBiggestBlock.Text = "Biggest Block:";
            this.lblBiggestBlock.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblNeededTime
            // 
            this.lblNeededTime.Location = new System.Drawing.Point(12, 96);
            this.lblNeededTime.Margin = new System.Windows.Forms.Padding(3);
            this.lblNeededTime.Name = "lblNeededTime";
            this.lblNeededTime.Size = new System.Drawing.Size(100, 21);
            this.lblNeededTime.TabIndex = 9;
            this.lblNeededTime.Text = "Needed Time:";
            this.lblNeededTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSmallestRemaining
            // 
            this.lblSmallestRemaining.Location = new System.Drawing.Point(12, 123);
            this.lblSmallestRemaining.Margin = new System.Windows.Forms.Padding(3);
            this.lblSmallestRemaining.Name = "lblSmallestRemaining";
            this.lblSmallestRemaining.Size = new System.Drawing.Size(100, 21);
            this.lblSmallestRemaining.TabIndex = 12;
            this.lblSmallestRemaining.Text = "Smallest Block Left:";
            this.lblSmallestRemaining.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblType
            // 
            this.lblType.Location = new System.Drawing.Point(12, 15);
            this.lblType.Margin = new System.Windows.Forms.Padding(3);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(100, 21);
            this.lblType.TabIndex = 1;
            this.lblType.Text = "&Game Type:";
            this.lblType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // scoreList
            // 
            this.scoreList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.scoreList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colTime,
            this.colType,
            this.colTotal,
            this.colLeft,
            this.colBiggest,
            this.colScore});
            this.scoreList.FullRowSelect = true;
            this.scoreList.HideSelection = false;
            this.scoreList.Location = new System.Drawing.Point(12, 155);
            this.scoreList.Name = "scoreList";
            this.scoreList.Size = new System.Drawing.Size(440, 164);
            this.scoreList.TabIndex = 15;
            this.scoreList.UseCompatibleStateImageBehavior = false;
            this.scoreList.View = System.Windows.Forms.View.Details;
            this.scoreList.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.OnScoreListColumnClick);
            // 
            // colTime
            // 
            this.colTime.Text = "Time";
            this.colTime.Width = 88;
            // 
            // colType
            // 
            this.colType.Text = "Type";
            this.colType.Width = 88;
            // 
            // colTotal
            // 
            this.colTotal.Text = "Total";
            this.colTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // colLeft
            // 
            this.colLeft.Text = "Left";
            this.colLeft.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // colBiggest
            // 
            this.colBiggest.Text = "Biggest";
            this.colBiggest.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // colScore
            // 
            this.colScore.Text = "Score";
            this.colScore.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // datHighestScore
            // 
            this.datHighestScore.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.datHighestScore.AutoEllipsis = true;
            this.datHighestScore.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.datHighestScore.Location = new System.Drawing.Point(238, 42);
            this.datHighestScore.Name = "datHighestScore";
            this.datHighestScore.Size = new System.Drawing.Size(70, 21);
            this.datHighestScore.TabIndex = 5;
            this.datHighestScore.Text = "0000-00-00";
            this.datHighestScore.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // datBiggestBlock
            // 
            this.datBiggestBlock.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.datBiggestBlock.AutoEllipsis = true;
            this.datBiggestBlock.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.datBiggestBlock.Location = new System.Drawing.Point(238, 69);
            this.datBiggestBlock.Name = "datBiggestBlock";
            this.datBiggestBlock.Size = new System.Drawing.Size(70, 21);
            this.datBiggestBlock.TabIndex = 8;
            this.datBiggestBlock.Text = "0000-00-00";
            this.datBiggestBlock.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // datNeededTime
            // 
            this.datNeededTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.datNeededTime.AutoEllipsis = true;
            this.datNeededTime.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.datNeededTime.Location = new System.Drawing.Point(238, 96);
            this.datNeededTime.Name = "datNeededTime";
            this.datNeededTime.Size = new System.Drawing.Size(70, 21);
            this.datNeededTime.TabIndex = 11;
            this.datNeededTime.Text = "0000-00-00";
            this.datNeededTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // datSmallestRemaining
            // 
            this.datSmallestRemaining.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.datSmallestRemaining.AutoEllipsis = true;
            this.datSmallestRemaining.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.datSmallestRemaining.Location = new System.Drawing.Point(238, 123);
            this.datSmallestRemaining.Name = "datSmallestRemaining";
            this.datSmallestRemaining.Size = new System.Drawing.Size(70, 21);
            this.datSmallestRemaining.TabIndex = 14;
            this.datSmallestRemaining.Text = "0000-00-00";
            this.datSmallestRemaining.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnOpen
            // 
            this.btnOpen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOpen.Image = global::plexdata.ClickTheBrick.Properties.Resources.Open;
            this.btnOpen.Location = new System.Drawing.Point(12, 325);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(80, 25);
            this.btnOpen.TabIndex = 16;
            this.btnOpen.Text = "O&pen";
            this.btnOpen.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnOpen.Click += new System.EventHandler(this.OnButtonOpenClick);
            // 
            // HighscoreDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.CancelButton = this.btnOK;
            this.ClientSize = new System.Drawing.Size(464, 362);
            this.Controls.Add(this.btnOpen);
            this.Controls.Add(this.datSmallestRemaining);
            this.Controls.Add(this.datNeededTime);
            this.Controls.Add(this.datBiggestBlock);
            this.Controls.Add(this.datHighestScore);
            this.Controls.Add(this.scoreList);
            this.Controls.Add(this.lblType);
            this.Controls.Add(this.lblSmallestRemaining);
            this.Controls.Add(this.lblNeededTime);
            this.Controls.Add(this.lblBiggestBlock);
            this.Controls.Add(this.lblHighestScore);
            this.Controls.Add(this.valSmallestRemaining);
            this.Controls.Add(this.cmbType);
            this.Controls.Add(this.valNeededTime);
            this.Controls.Add(this.valBiggestBlock);
            this.Controls.Add(this.valHighestScore);
            this.Controls.Add(this.btnOK);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(480, 400);
            this.Name = "HighscoreDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Highscore";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label valHighestScore;
        private System.Windows.Forms.Label valBiggestBlock;
        private System.Windows.Forms.Label valNeededTime;
        private System.Windows.Forms.ComboBox cmbType;
        private System.Windows.Forms.Label valSmallestRemaining;
        private System.Windows.Forms.Label lblHighestScore;
        private System.Windows.Forms.Label lblBiggestBlock;
        private System.Windows.Forms.Label lblNeededTime;
        private System.Windows.Forms.Label lblSmallestRemaining;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.ListView scoreList;
        private System.Windows.Forms.ColumnHeader colTime;
        private System.Windows.Forms.ColumnHeader colType;
        private System.Windows.Forms.ColumnHeader colTotal;
        private System.Windows.Forms.ColumnHeader colLeft;
        private System.Windows.Forms.ColumnHeader colBiggest;
        private System.Windows.Forms.ColumnHeader colScore;
        private System.Windows.Forms.Label datHighestScore;
        private System.Windows.Forms.Label datBiggestBlock;
        private System.Windows.Forms.Label datNeededTime;
        private System.Windows.Forms.Label datSmallestRemaining;
        private System.Windows.Forms.Button btnOpen;
    }
}