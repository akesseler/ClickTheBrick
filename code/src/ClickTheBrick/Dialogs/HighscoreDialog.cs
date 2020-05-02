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
using System.IO;
using System.Linq;
using System.Reflection;
using System.Collections;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Forms;
using System.Collections.Generic;

namespace plexdata.ClickTheBrick
{
    public partial class HighscoreDialog : Form
    {
        private List<Highscore> highscore = new List<Highscore>();

        public HighscoreDialog()
            : base()
        {
            this.InitializeComponent();

            // Strip game type Unknown form combo-box items.
            this.cmbType.DataSource = Enum.GetValues(typeof(GameType))
                .Cast<GameType>()
                .Where(item => item != GameType.Unknown)
                .ToArray<GameType>();
            this.cmbType.SelectedIndex = -1;

            PropertyInfo property;
            property = this.scoreList.GetType().GetProperty("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance);
            property.SetValue(this.scoreList, true, null);
        }

        public HighscoreDialog(List<Highscore> highscore)
            : this()
        {
            if (highscore != null)
            {
                this.highscore = new List<Highscore>(highscore);
                this.highscore.Sort(new HighscoreSorter());
            }
        }

        protected override void OnLoad(EventArgs args)
        {
            base.OnLoad(args);

            if (this.highscore != null && this.highscore.Count > 0)
            {
                this.ChooseBestFit();
            }
        }

        protected override void OnShown(EventArgs args)
        {
            base.OnShown(args);

            if (this.highscore == null || this.highscore.Count == 0)
            {
                this.btnOpen.PerformClick();
            }
        }

        private void OnButtonOkClick(object sender, EventArgs args)
        {
            this.Close();
        }

        private void OnButtonOpenClick(object sender, EventArgs args)
        {
            try
            {
                string extension = Path.GetExtension(Highscore.Filename);
                string directory = Path.GetDirectoryName(Highscore.Filename);

                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = String.Format("Highscore Files (*{0})|*{0}|All files (*.*)|*.*", extension);
                dialog.DefaultExt = extension;
                dialog.InitialDirectory = directory;
                dialog.Multiselect = false;
                dialog.RestoreDirectory = true;
                dialog.ShowHelp = false;

                if (DialogResult.OK == dialog.ShowDialog(this))
                {
                    List<Highscore> helper;
                    if (Highscore.Load(dialog.FileName, out helper))
                    {
                        this.highscore = helper;
                        this.highscore.Sort(new HighscoreSorter());

                        if (this.cmbType.SelectedItem != null)
                        {
                            this.LoadValues((GameType)this.cmbType.SelectedItem);
                        }
                        else
                        {
                            this.ChooseBestFit();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Could not load the highscore from selected file.",
                            this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
            }
        }

        private void OnTypeSelectedIndexChanged(object sender, EventArgs args)
        {
            if (this.cmbType.SelectedItem != null)
            {
                this.LoadValues((GameType)this.cmbType.SelectedItem);
            }
        }

        private void OnScoreListColumnClick(object sender, ColumnClickEventArgs args)
        {
            this.Cursor = Cursors.WaitCursor;
            this.scoreList.BeginUpdate();

            try // Belt and suspenders...
            {
                ItemSorter sorter = (this.scoreList.ListViewItemSorter as ItemSorter);
                if (sorter == null)
                {
                    this.scoreList.ListViewItemSorter = new ItemSorter(args.Column);
                }
                else
                {
                    if (sorter.Column != args.Column)
                    {
                        sorter.Column = args.Column;
                    }
                    else
                    {
                        sorter.NextSorting();
                    }
                    this.scoreList.Sort();
                }
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
            }
            finally
            {
                this.Cursor = null;
                this.scoreList.EndUpdate();
            }
        }

        private void LoadValues(GameType type)
        {
            this.Cursor = Cursors.WaitCursor;
            this.scoreList.BeginUpdate();

            try // Belt and suspenders...
            {
                this.ClearValues();

                if (this.highscore != null)
                {
                    NumberFormatInfo info = CultureInfo.CurrentUICulture.NumberFormat;
                    List<Highscore> temp = null;
                    List<Highscore> list = new List<Highscore>(
                        this.highscore.Where<Highscore>(item => item.Type == type));

                    if (list != null && list.Count > 0)
                    {
                        //Sample to sum values using LinQ...
                        //this.valTotalScore.Text = (list.Sum(item => item.Score)).ToString();

                        // Find value of highest score.
                        list.Sort((left, right) => right.Score.CompareTo(left.Score));
                        this.valHighestScore.Text = list[0].Score.ToString("N0", info);

                        // Find latest date fitting value from above.
                        temp = new List<Highscore>(list.Where<Highscore>(item => item.Score == list[0].Score));
                        temp.Sort((left, right) => right.Date.CompareTo(left.Date));
                        this.datHighestScore.Text = temp[0].Date;

                        // Find value of biggest block.
                        list.Sort((left, right) => right.Biggest.CompareTo(left.Biggest));
                        this.valBiggestBlock.Text = list[0].Biggest.ToString("N0", info);

                        // Find latest date fitting value from above.
                        temp = new List<Highscore>(list.Where<Highscore>(item => item.Biggest == list[0].Biggest));
                        temp.Sort((left, right) => right.Date.CompareTo(left.Date));
                        this.datBiggestBlock.Text = temp[0].Date;

                        if (type == GameType.Countdown)
                        {
                            // Find longest time value.
                            list.Sort((left, right) => right.Time.CompareTo(left.Time));

                            // Adjust time label and value settings.
                            this.lblNeededTime.Text = "Remaining Time:";
                            this.toolTip.SetToolTip(this.valNeededTime, "Longest remaining time.");
                        }
                        else
                        {
                            // Find shortest time value.
                            list.Sort((left, right) => left.Time.CompareTo(right.Time));
                        }
                        this.valNeededTime.Text = list[0].Time.ToString();

                        // Find latest date fitting value from above.
                        temp = new List<Highscore>(list.Where<Highscore>(item => item.Time == list[0].Time));
                        temp.Sort((left, right) => right.Date.CompareTo(left.Date));
                        this.datNeededTime.Text = temp[0].Date;

                        // Find value of smallest remaining block.
                        list.Sort((left, right) => left.Left.CompareTo(right.Left));
                        this.valSmallestRemaining.Text = list[0].Left.ToString("N0", info);

                        // Find latest date fitting value from above.
                        temp = new List<Highscore>(list.Where<Highscore>(item => item.Left == list[0].Left));
                        temp.Sort((left, right) => right.Date.CompareTo(left.Date));
                        this.datSmallestRemaining.Text = temp[0].Date;

                        // Build the ListView's items.
                        List<ListViewItem> items = new List<ListViewItem>();
                        List<ListViewGroup> groups = new List<ListViewGroup>();
                        foreach (Highscore current in list)
                        {
                            // Create and add current highscore value.
                            ListViewItem item = new ListViewItem(new string[] { 
                                current.Time, current.Type.ToString(), 
                                current.Total.ToString("N0", info), 
                                current.Left.ToString("N0", info), 
                                current.Biggest.ToString("N0", info), 
                                current.Score.ToString("N0", info) });
                            items.Add(item);

                            // Try to find a corresponding item group.
                            ListViewGroup group = groups.Find(left => left.Header == current.Date);
                            if (group == null)
                            {
                                // Add a new group if not yet exist.
                                group = new ListViewGroup(current.Date, current.Date);
                                groups.Add(group);
                            }

                            // Assign this item to corresponding group.
                            group.Items.Add(item);
                        }
                        groups.Sort(new GroupSorter(SortOrder.Descending));

                        // Include number of total group items in group header.
                        foreach (ListViewGroup group in groups)
                        {
                            group.Header = String.Format(
                                "{0} ({1})", group.Header,
                                group.Items.Count.ToString("N0", info));
                        }

                        this.scoreList.Items.AddRange(items.ToArray());
                        this.scoreList.Groups.AddRange(groups.ToArray());

                        // Ensure that the groups are shown.
                        this.scoreList.ShowGroups = true;
                    }
                }
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
            }
            finally
            {
                this.Cursor = null;
                this.scoreList.EndUpdate();
            }
        }

        private void ClearValues()
        {
            this.valHighestScore.Text = String.Empty;
            this.datHighestScore.Text = String.Empty;

            this.valBiggestBlock.Text = String.Empty;
            this.datBiggestBlock.Text = String.Empty;

            this.toolTip.SetToolTip(this.valNeededTime, "Shortest needed time.");
            this.lblNeededTime.Text = "Needed Time:";
            this.valNeededTime.Text = String.Empty;
            this.datNeededTime.Text = String.Empty;

            this.valSmallestRemaining.Text = String.Empty;
            this.datSmallestRemaining.Text = String.Empty;

            this.scoreList.Groups.Clear();
            this.scoreList.Items.Clear();

            // Reset current item sorter to initially have an unsorted list!
            this.scoreList.ListViewItemSorter = null;
        }

        private void ChooseBestFit()
        {
            do // Try choose first fitting game type.
            {
                if (this.cmbType.SelectedIndex + 1 < this.cmbType.Items.Count)
                {
                    this.cmbType.SelectedItem = this.cmbType.Items[this.cmbType.SelectedIndex + 1];
                }
                else
                {
                    this.cmbType.SelectedIndex = -1;
                }
            }
            while (this.scoreList.Items.Count == 0 && this.cmbType.SelectedIndex != -1);
        }

        private class HighscoreSorter : IComparer<Highscore>
        {
            public HighscoreSorter()
                : base()
            {
            }

            public int Compare(Highscore itemA, Highscore itemB)
            {
                if (itemA == null && itemB == null)
                {
                    return 0;
                }
                else if (itemA == null)
                {
                    return -1;
                }
                else if (itemB == null)
                {
                    return 1;
                }
                else if (itemA == itemB)
                {
                    return 0;
                }
                else
                {
                    return itemA.Date.CompareTo(itemB.Date);
                }
            }
        }

        private class ItemSorter : IComparer
        {
            public ItemSorter()
                : this(SortOrder.None, 0)
            {
            }

            public ItemSorter(int column)
                : this(SortOrder.Ascending, column)
            {
            }

            public ItemSorter(SortOrder sorting, int column)
                : base()
            {
                this.Column = column;
                this.Sorting = sorting;
            }

            public int Column { get; set; }

            public SortOrder Sorting { get; set; }

            public void NextSorting()
            {
                if (this.Sorting == SortOrder.Ascending)
                {
                    this.Sorting = SortOrder.Descending;
                }
                else
                {
                    this.Sorting = SortOrder.Ascending;
                }
            }

            public int Compare(object objA, object objB)
            {
                ListViewItem itemA = objA as ListViewItem;
                ListViewItem itemB = objB as ListViewItem;

                if (itemA == null && itemB == null)
                {
                    return 0;
                }
                else if (itemA == null)
                {
                    return -1;
                }
                else if (itemB == null)
                {
                    return 1;
                }
                else if (itemA == itemB)
                {
                    return 0;
                }
                else
                {
                    int result = 0;
                    int intA = 0;
                    int intB = 0;

                    string strA = itemA.SubItems[this.Column].Text;
                    string strB = itemB.SubItems[this.Column].Text;

                    NumberStyles style = NumberStyles.Number;
                    NumberFormatInfo info = CultureInfo.CurrentUICulture.NumberFormat;

                    if (int.TryParse(strA, style, info, out intA) &&
                        int.TryParse(strB, style, info, out intB))
                    {
                        result = intA.CompareTo(intB);
                    }
                    else
                    {
                        result = strA.CompareTo(strB);
                    }

                    if (this.Sorting != SortOrder.Ascending)
                    {
                        result *= -1;
                    }

                    return result;
                }
            }
        }

        private class GroupSorter : IComparer<ListViewGroup>
        {
            public GroupSorter()
                : this(SortOrder.None)
            {
            }

            public GroupSorter(SortOrder sorting)
                : base()
            {
                this.Sorting = sorting;
            }

            public SortOrder Sorting { get; set; }

            public int Compare(ListViewGroup itemA, ListViewGroup itemB)
            {
                if (itemA == null && itemB == null)
                {
                    return 0;
                }
                else if (itemA == null)
                {
                    return -1;
                }
                else if (itemB == null)
                {
                    return 1;
                }
                else if (itemA == itemB)
                {
                    return 0;
                }
                else
                {
                    int result = itemA.Header.CompareTo(itemB.Header);

                    if (this.Sorting != SortOrder.Ascending)
                    {
                        result *= -1;
                    }

                    return result;
                }
            }
        }
    }
}
