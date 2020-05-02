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
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace plexdata.ClickTheBrick
{
    public class Highscore : ICloneable
    {
        private const String DateFormat = "yyyy-MM-dd";

        public static String DefaultDateValue = "0000-00-00";
        public static String DefaultTimeValue = "00:00:00";

        // Keep in mind to make changes in function Save() and Parse() if something has changed!
        public static String HighscoreHeader = "Date;Time;Type;Total;Left;Biggest;Score;";
        public static String HighscoreFormat = "{0};{1};{2};{3};{4};{5};{6};";

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

        public static String Filename
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

        public static Boolean Save(List<Highscore> items)
        {
            return Highscore.Save(Highscore.Filename, items);
        }

        public static Boolean Save(String filename, List<Highscore> items)
        {
            try
            {
                if (items != null && items.Count > 0)
                {
                    Boolean header = !File.Exists(filename);

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

        public static Boolean Load(out List<Highscore> result)
        {
            return Highscore.Load(Highscore.Filename, out result);
        }

        public static Boolean Load(String filename, out List<Highscore> result)
        {
            result = new List<Highscore>();

            try
            {
                using (TextReader reader = new StreamReader(filename))
                {
                    // Strip the file header.
                    String header = reader.ReadLine();

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

        public static List<Highscore> LoadHighscoreToday()
        {
            List<Highscore> result = new List<Highscore>();

            if (Highscore.Load(Highscore.Filename, out List<Highscore> helper))
            {
                String date = DateTime.Today.ToString(Highscore.DateFormat);

                result.AddRange(helper.Where(x => x.Date == date));
            }

            return result;
        }

        public static Highscore Parse(String value)
        {
            Highscore result = new Highscore();

            if (!String.IsNullOrEmpty(value))
            {
                String[] items = value.Split(new Char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                if (items != null && items.Length >= 7)
                {
                    result.Date = items[0];
                    result.Time = items[1];

                    if (Enum.IsDefined(typeof(GameType), items[2]))
                    {
                        result.Type = (GameType)Enum.Parse(typeof(GameType), items[2]);
                    }

                    Int32 helper;

                    if (Int32.TryParse(items[3], out helper)) { result.Total = helper; }
                    if (Int32.TryParse(items[4], out helper)) { result.Left = helper; }
                    if (Int32.TryParse(items[5], out helper)) { result.Biggest = helper; }
                    if (Int32.TryParse(items[6], out helper)) { result.Score = helper; }
                }
            }

            return result.IsValid ? result : null;
        }

        public static String Format(DateTime date)
        {
            try
            {
                if (date != null)
                {
                    return date.ToString(Highscore.DateFormat);
                }
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
            }
            return Highscore.DefaultDateValue;
        }

        public static String Format(TimeSpan time)
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

        public Boolean IsValid
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

        public String Date { get; set; }

        public String Time { get; set; }

        public GameType Type { get; set; }

        public Int32 Total { get; set; }

        public Int32 Left { get; set; }

        public Int32 Biggest { get; set; }

        public Int32 Score { get; set; }

        #endregion // Public property section.

        #region Public member function section.

        public String ToLine()
        {
            return String.Format(Highscore.HighscoreFormat,
                (String.IsNullOrEmpty(this.Date) ? Highscore.DefaultDateValue : this.Date),
                (String.IsNullOrEmpty(this.Time) ? Highscore.DefaultTimeValue : this.Time),
                this.Type, this.Total, this.Left, this.Biggest, this.Score);
        }

        public override String ToString()
        {
            return String.Format(
                "Date={0}, Time={1}, Type={2}, Total={3}, Left={4}, Biggest={5}, Score={6}",
                (String.IsNullOrEmpty(this.Date) ? Highscore.DefaultDateValue : this.Date),
                (String.IsNullOrEmpty(this.Time) ? Highscore.DefaultTimeValue : this.Time),
                this.Type, this.Total, this.Left, this.Biggest, this.Score);
        }

        public Object Clone()
        {
            return new Highscore(this);
        }

        #endregion // Public member function section.
    }
}
