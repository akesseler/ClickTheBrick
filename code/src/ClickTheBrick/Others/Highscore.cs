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
using System.Diagnostics;
using System.Windows.Forms;
using System.Collections.Generic;

namespace plexdata.ClickTheBrick
{
    public class Highscore : ICloneable
    {
        public static string DefaultDateValue = "0000-00-00";
        public static string DefaultTimeValue = "00:00:00";

        // Keep in mind to make changes in function Save() and Parse() if something has changed!
        public static string HighscoreHeader = "Date;Time;Type;Total;Left;Biggest;Score;";
        public static string HighscoreFormat = "{0};{1};{2};{3};{4};{5};{6};";

        public Highscore()
            : base()
        {
            this.Date = String.Empty;
            this.Time = String.Empty;
            this.Type = GameType.Unknown;
            this.Total = -1;
            this.Left = -1;
            this.Biggest = -1;
            this.Score = -1;
        }

        public Highscore(DateTime date)
            : this()
        {
            this.Date = Highscore.Format(date);
        }

        public Highscore(Highscore other)
            : this()
        {
            if (other != null)
            {
                this.Type = other.Type;
                this.Total = other.Total;
                this.Left = other.Left;
                this.Biggest = other.Biggest;
                this.Score = other.Score;
                this.Time = other.Time;
                this.Date = other.Date;
            }
            else
            {
                throw new ArgumentNullException("other");
            }
        }

        #region Public static member function section.

        public static string Filename
        {
            get
            {
                try
                {
                    return Path.ChangeExtension(Application.ExecutablePath, "sco").ToLower();
                }
                catch (Exception exception)
                {
                    Debug.WriteLine(exception);
                    return String.Empty;
                }

            }
        }

        public static bool Save(List<Highscore> items)
        {
            return Highscore.Save(Highscore.Filename, items);
        }

        public static bool Save(string filename, List<Highscore> items)
        {
            try
            {
                if (items != null && items.Count > 0)
                {
                    bool header = !File.Exists(filename);

                    using (TextWriter writer = new StreamWriter(filename, true))
                    {
                        if (header)
                        {
                            writer.WriteLine(Highscore.HighscoreHeader);
                        }

                        foreach (Highscore item in items)
                        {
                            writer.WriteLine(item.ToLine());
                        }
                        return true;
                    }
                }
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
            }
            return false;
        }

        public static bool Load(out List<Highscore> result)
        {
            return Highscore.Load(Highscore.Filename, out result);
        }

        public static bool Load(string filename, out List<Highscore> result)
        {
            result = new List<Highscore>();

            try
            {
                using (TextReader reader = new StreamReader(filename))
                {
                    // Strip the file header.
                    string header = reader.ReadLine();

                    // Read the rest of the file.
                    while (reader.Peek() != -1)
                    {
                        Highscore item = Highscore.Parse(reader.ReadLine());
                        if (item != null) { result.Add(item); }
                    }
                }
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
            }

            return result.Count > 0;
        }

        public static Highscore Parse(string value)
        {
            Highscore result = new Highscore();

            if (!String.IsNullOrEmpty(value))
            {
                string[] items = value.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                if (items != null && items.Length >= 7)
                {
                    result.Date = items[0];
                    result.Time = items[1];

                    if (Enum.IsDefined(typeof(GameType), items[2]))
                    {
                        result.Type = (GameType)Enum.Parse(typeof(GameType), items[2]);
                    }

                    int helper;

                    if (Int32.TryParse(items[3], out helper)) { result.Total = helper; }
                    if (Int32.TryParse(items[4], out helper)) { result.Left = helper; }
                    if (Int32.TryParse(items[5], out helper)) { result.Biggest = helper; }
                    if (Int32.TryParse(items[6], out helper)) { result.Score = helper; }
                }
            }

            return result.IsValid ? result : null;
        }

        public static string Format(DateTime date)
        {
            try
            {
                if (date != null)
                {
                    return date.ToString("yyyy-MM-dd");
                }
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
            }
            return Highscore.DefaultDateValue;
        }

        public static string Format(TimeSpan time)
        {
            try
            {
                if (time != null)
                {
                    return String.Format("{0}:{1}:{2}",
                        time.Hours.ToString("00"),
                        time.Minutes.ToString("00"),
                        time.Seconds.ToString("00"));
                }
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
            }
            return Highscore.DefaultTimeValue;
        }

        #endregion // Public static member function section.

        #region Public property section.

        public bool IsValid
        {
            get
            {
                return
                    !String.IsNullOrEmpty(this.Date) &&
                    !String.IsNullOrEmpty(this.Time) &&
                    this.Type != GameType.Unknown &&
                    this.Total != -1 && this.Left != -1 &&
                    this.Biggest != -1 && this.Score != -1;
            }
        }

        public string Date { get; set; }

        public string Time { get; set; }

        public GameType Type { get; set; }

        public int Total { get; set; }

        public int Left { get; set; }

        public int Biggest { get; set; }

        public int Score { get; set; }

        #endregion // Public property section.

        #region Public member function section.

        public string ToLine()
        {
            return String.Format(Highscore.HighscoreFormat,
                (String.IsNullOrEmpty(this.Date) ? Highscore.DefaultDateValue : this.Date),
                (String.IsNullOrEmpty(this.Time) ? Highscore.DefaultTimeValue : this.Time),
                this.Type, this.Total, this.Left, this.Biggest, this.Score);
        }

        public override string ToString()
        {
            return String.Format(
                "Date={0}, Time={1}, Type={2}, Total={3}, Left={4}, Biggest={5}, Score={6}",
                (String.IsNullOrEmpty(this.Date) ? Highscore.DefaultDateValue : this.Date),
                (String.IsNullOrEmpty(this.Time) ? Highscore.DefaultTimeValue : this.Time),
                this.Type, this.Total, this.Left, this.Biggest, this.Score);
        }

        public object Clone()
        {
            return new Highscore(this);
        }

        #endregion // Public member function section.
    }
}
