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
using System.Diagnostics;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;

namespace plexdata.ClickTheBrick
{
    public partial class SettingsDialog : Form
    {
        public SettingsDialog()
            : base()
        {
            this.InitializeComponent();

            this.Icon = Properties.Resources.MainIcon;
            this.Settings = new BrickPanelSettings();

            this.dimensionCheck.SetIconAlignment(this.numRows, ErrorIconAlignment.MiddleLeft);
            this.dimensionCheck.SetIconAlignment(this.numCols, ErrorIconAlignment.MiddleLeft);
            this.colorsCheck.SetIconAlignment(this.lstColors, ErrorIconAlignment.TopLeft);
            this.colorsCheck.SetIconPadding(this.lstColors, -(this.colorsCheck.Icon.Width));
        }

        public SettingsDialog(BrickPanelSettings settings)
            : this()
        {
            if (settings != null)
            {
                this.Settings = new BrickPanelSettings(settings);
            }
        }

        public BrickPanelSettings Settings { get; private set; }

        protected override void OnLoad(EventArgs args)
        {
            base.OnLoad(args);

            try
            {
                this.chkHighlight.Checked = this.Settings.Highlight;
                this.numRows.Value = this.Settings.Dimension.Rows;
                this.numCols.Value = this.Settings.Dimension.Cols;

                this.cmbDimensions.DisplayMember = "Display";
                this.cmbDimensions.ValueMember = "Display";
                this.cmbDimensions.DataSource = new List<Dimension>(new Dimension[]{
                    new Dimension(10,8),  new Dimension(12,8),  new Dimension(14,8),
                    new Dimension(12,10), new Dimension(14,10), new Dimension(16,10),
                    new Dimension(14,12), new Dimension(16,12), new Dimension(18,12),
                    new Dimension(16,14), new Dimension(18,14), new Dimension(20,14),
                    new Dimension(18,16), new Dimension(20,16), new Dimension(22,16),
                });
                this.cmbDimensions.SelectedValue = this.Settings.Dimension.Display;
                if (this.cmbDimensions.SelectedValue == null)
                {
                    this.cmbDimensions.SelectedIndex = 0;
                    this.chkCustom.Checked = true;
                }

                this.lstColors.Items.AddRange(this.Settings.BrickColorsXML);
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
            }
        }

        private void OnButtonApplyClick(object sender, EventArgs args)
        {
            try
            {
                this.Settings.Highlight = this.chkHighlight.Checked;

                if (this.chkCustom.Checked)
                {
                    this.Settings.Dimension.Rows = Convert.ToInt32(this.numRows.Value);
                    this.Settings.Dimension.Cols = Convert.ToInt32(this.numCols.Value);
                }
                else
                {
                    Dimension dimension = this.cmbDimensions.SelectedItem as Dimension;
                    if (dimension != null) { this.Settings.Dimension = dimension; }
                }

                List<string> colors = new List<string>();
                foreach (object current in this.lstColors.Items)
                {
                    if (current != null) { colors.Add(current.ToString()); }
                }
                this.Settings.BrickColorsXML = colors.ToArray();
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
            }
        }

        private void OnValidatingDimension(object sender, CancelEventArgs args)
        {
            if (this.chkCustom.Checked)
            {
                int rows = Convert.ToInt32(this.numRows.Value);
                int cols = Convert.ToInt32(this.numCols.Value);

                args.Cancel = (((rows * cols) & 1) != 0);
                if (args.Cancel)
                {
                    this.dimensionCheck.SetError((sender as Control),
                        "The matrix of rows and columns must consist of an even number of elements!");
                }
                else
                {
                    this.dimensionCheck.SetError(this.numRows, String.Empty);
                    this.dimensionCheck.SetError(this.numCols, String.Empty);
                }
            }
        }

        private void OnValidatingColors(object sender, CancelEventArgs args)
        {
            args.Cancel = !this.ColorsValidate();
        }

        private void OnCustomCheckedChanged(object sender, EventArgs args)
        {
            this.numRows.Enabled = this.chkCustom.Checked;
            this.numCols.Enabled = this.chkCustom.Checked;
            this.cmbDimensions.Enabled = !this.chkCustom.Checked;
        }

        private void OnButtonAddClick(object sender, EventArgs args)
        {
            ColorDialog dialog = new ColorDialog();

            dialog.AnyColor = true;
            dialog.SolidColorOnly = true;
            dialog.AllowFullOpen = true;

            if (DialogResult.OK == dialog.ShowDialog(this))
            {
                string name = ColorTranslator.ToHtml(dialog.Color);
                if (!this.lstColors.Items.Contains(name))
                {
                    this.lstColors.Items.Add(name);
                }
            }
            this.ColorsValidate();
        }

        private void OnButtonRemoveClick(object sender, EventArgs args)
        {
            while (this.lstColors.SelectedItems.Count > 0)
            {
                this.lstColors.Items.Remove(this.lstColors.SelectedItems[0]);
            }
            this.ColorsValidate();
        }

        private void OnColorListSectionChanged(object sender, EventArgs args)
        {
            this.btnRemove.Enabled = this.lstColors.SelectedIndices.Count > 0;
        }

        private void OnColorListDrawItem(object sender, DrawItemEventArgs args)
        {
            try
            {
                args.DrawBackground();

                string text = this.lstColors.Items[args.Index].ToString();
                Color item = ColorTranslator.FromHtml(text);

                Rectangle itemRect = args.Bounds;
                Rectangle textRect = args.Bounds;

                using (Brush itemBrush = new SolidBrush(item))
                {
                    itemRect.X += 1;
                    itemRect.Y += 1;
                    itemRect.Width = itemRect.Height * 2 - 1;
                    itemRect.Height -= 3;

                    args.Graphics.FillRectangle(itemBrush, itemRect);
                    args.Graphics.DrawRectangle(Pens.Black, itemRect);
                }

                using (Brush textBrush = new SolidBrush(args.ForeColor))
                {
                    textRect.X += itemRect.Width + 3;
                    textRect.Width -= itemRect.Width + 3;

                    args.Graphics.DrawString(text, args.Font, textBrush, textRect);
                }
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
            }
        }

        private bool ColorsValidate()
        {
            if (this.lstColors.Items.Count <= 0)
            {
                this.colorsCheck.SetError(this.lstColors,
                    "The list of brick colors must not be empty!");
                return false;
            }
            else
            {
                this.colorsCheck.SetError(this.lstColors, String.Empty);
                return true;
            }
        }
    }
}
