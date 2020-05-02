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
    partial class SettingsDialog
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
            this.chkHighlight = new System.Windows.Forms.CheckBox();
            this.grpDimension = new System.Windows.Forms.GroupBox();
            this.chkCustom = new System.Windows.Forms.CheckBox();
            this.cmbDimensions = new System.Windows.Forms.ComboBox();
            this.lblRows = new System.Windows.Forms.Label();
            this.lblCols = new System.Windows.Forms.Label();
            this.numCols = new System.Windows.Forms.NumericUpDown();
            this.numRows = new System.Windows.Forms.NumericUpDown();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lstColors = new System.Windows.Forms.ListBox();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnApply = new System.Windows.Forms.Button();
            this.dimensionCheck = new System.Windows.Forms.ErrorProvider(this.components);
            this.grpGeneral = new System.Windows.Forms.GroupBox();
            this.colorsCheck = new System.Windows.Forms.ErrorProvider(this.components);
            this.grpDimension.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCols)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRows)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dimensionCheck)).BeginInit();
            this.grpGeneral.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.colorsCheck)).BeginInit();
            this.SuspendLayout();
            // 
            // chkHighlight
            // 
            this.chkHighlight.AutoSize = true;
            this.chkHighlight.Checked = true;
            this.chkHighlight.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkHighlight.Location = new System.Drawing.Point(9, 19);
            this.chkHighlight.Name = "chkHighlight";
            this.chkHighlight.Size = new System.Drawing.Size(67, 17);
            this.chkHighlight.TabIndex = 0;
            this.chkHighlight.Text = "Highlight";
            this.chkHighlight.UseVisualStyleBackColor = true;
            // 
            // grpDimension
            // 
            this.grpDimension.Controls.Add(this.chkCustom);
            this.grpDimension.Controls.Add(this.cmbDimensions);
            this.grpDimension.Controls.Add(this.lblRows);
            this.grpDimension.Controls.Add(this.lblCols);
            this.grpDimension.Controls.Add(this.numCols);
            this.grpDimension.Controls.Add(this.numRows);
            this.grpDimension.Location = new System.Drawing.Point(12, 60);
            this.grpDimension.Name = "grpDimension";
            this.grpDimension.Size = new System.Drawing.Size(150, 121);
            this.grpDimension.TabIndex = 2;
            this.grpDimension.TabStop = false;
            this.grpDimension.Text = "Dimension";
            // 
            // chkCustom
            // 
            this.chkCustom.AutoSize = true;
            this.chkCustom.Location = new System.Drawing.Point(9, 46);
            this.chkCustom.Name = "chkCustom";
            this.chkCustom.Size = new System.Drawing.Size(113, 17);
            this.chkCustom.TabIndex = 1;
            this.chkCustom.Text = "&Custom Dimension";
            this.chkCustom.UseVisualStyleBackColor = true;
            this.chkCustom.CheckedChanged += new System.EventHandler(this.OnCustomCheckedChanged);
            // 
            // cmbDimensions
            // 
            this.cmbDimensions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDimensions.FormattingEnabled = true;
            this.cmbDimensions.Location = new System.Drawing.Point(9, 19);
            this.cmbDimensions.Name = "cmbDimensions";
            this.cmbDimensions.Size = new System.Drawing.Size(135, 21);
            this.cmbDimensions.TabIndex = 0;
            // 
            // lblRows
            // 
            this.lblRows.AutoSize = true;
            this.lblRows.Location = new System.Drawing.Point(6, 71);
            this.lblRows.Name = "lblRows";
            this.lblRows.Size = new System.Drawing.Size(34, 13);
            this.lblRows.TabIndex = 2;
            this.lblRows.Text = "Rows";
            // 
            // lblCols
            // 
            this.lblCols.AutoSize = true;
            this.lblCols.Location = new System.Drawing.Point(6, 97);
            this.lblCols.Name = "lblCols";
            this.lblCols.Size = new System.Drawing.Size(47, 13);
            this.lblCols.TabIndex = 4;
            this.lblCols.Text = "Columns";
            // 
            // numCols
            // 
            this.numCols.Enabled = false;
            this.numCols.Location = new System.Drawing.Point(74, 95);
            this.numCols.Maximum = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.numCols.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numCols.Name = "numCols";
            this.numCols.Size = new System.Drawing.Size(70, 20);
            this.numCols.TabIndex = 5;
            this.numCols.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numCols.Validating += new System.ComponentModel.CancelEventHandler(this.OnValidatingDimension);
            // 
            // numRows
            // 
            this.numRows.Enabled = false;
            this.numRows.Location = new System.Drawing.Point(74, 69);
            this.numRows.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.numRows.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numRows.Name = "numRows";
            this.numRows.Size = new System.Drawing.Size(70, 20);
            this.numRows.TabIndex = 3;
            this.numRows.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numRows.Validating += new System.ComponentModel.CancelEventHandler(this.OnValidatingDimension);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lstColors);
            this.groupBox1.Controls.Add(this.btnRemove);
            this.groupBox1.Controls.Add(this.btnAdd);
            this.groupBox1.Location = new System.Drawing.Point(168, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(150, 169);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Colors";
            // 
            // lstColors
            // 
            this.lstColors.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lstColors.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lstColors.FormattingEnabled = true;
            this.lstColors.IntegralHeight = false;
            this.lstColors.Location = new System.Drawing.Point(6, 19);
            this.lstColors.Name = "lstColors";
            this.lstColors.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstColors.Size = new System.Drawing.Size(138, 113);
            this.lstColors.TabIndex = 0;
            this.lstColors.Validating += new System.ComponentModel.CancelEventHandler(this.OnValidatingColors);
            this.lstColors.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.OnColorListDrawItem);
            this.lstColors.SelectedIndexChanged += new System.EventHandler(this.OnColorListSectionChanged);
            // 
            // btnRemove
            // 
            this.btnRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemove.CausesValidation = false;
            this.btnRemove.Enabled = false;
            this.btnRemove.Image = global::plexdata.ClickTheBrick.Properties.Resources.Remove;
            this.btnRemove.Location = new System.Drawing.Point(115, 138);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(29, 25);
            this.btnRemove.TabIndex = 2;
            this.btnRemove.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.OnButtonRemoveClick);
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.CausesValidation = false;
            this.btnAdd.Image = global::plexdata.ClickTheBrick.Properties.Resources.Add;
            this.btnAdd.Location = new System.Drawing.Point(80, 138);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(29, 25);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.OnButtonAddClick);
            // 
            // btnCancel
            // 
            this.btnCancel.CausesValidation = false;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = global::plexdata.ClickTheBrick.Properties.Resources.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(238, 187);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 25);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnApply
            // 
            this.btnApply.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnApply.Image = global::plexdata.ClickTheBrick.Properties.Resources.Apply;
            this.btnApply.Location = new System.Drawing.Point(152, 187);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(80, 25);
            this.btnApply.TabIndex = 4;
            this.btnApply.Text = "&Apply";
            this.btnApply.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.OnButtonApplyClick);
            // 
            // dimensionCheck
            // 
            this.dimensionCheck.ContainerControl = this;
            // 
            // grpGeneral
            // 
            this.grpGeneral.Controls.Add(this.chkHighlight);
            this.grpGeneral.Location = new System.Drawing.Point(12, 12);
            this.grpGeneral.Name = "grpGeneral";
            this.grpGeneral.Size = new System.Drawing.Size(150, 42);
            this.grpGeneral.TabIndex = 1;
            this.grpGeneral.TabStop = false;
            this.grpGeneral.Text = "General";
            // 
            // colorsCheck
            // 
            this.colorsCheck.ContainerControl = this;
            // 
            // SettingsDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(330, 224);
            this.Controls.Add(this.grpGeneral);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.grpDimension);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.grpDimension.ResumeLayout(false);
            this.grpDimension.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCols)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRows)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dimensionCheck)).EndInit();
            this.grpGeneral.ResumeLayout(false);
            this.grpGeneral.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.colorsCheck)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox chkHighlight;
        private System.Windows.Forms.GroupBox grpDimension;
        private System.Windows.Forms.Label lblRows;
        private System.Windows.Forms.Label lblCols;
        private System.Windows.Forms.NumericUpDown numCols;
        private System.Windows.Forms.NumericUpDown numRows;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.ErrorProvider dimensionCheck;
        private System.Windows.Forms.GroupBox grpGeneral;
        private System.Windows.Forms.ListBox lstColors;
        private System.Windows.Forms.ErrorProvider colorsCheck;
        private System.Windows.Forms.ComboBox cmbDimensions;
        private System.Windows.Forms.CheckBox chkCustom;
    }
}