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
using System.Xml;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace plexdata.ClickTheBrick
{
    public enum GameType { Unknown = -1, Standard, Countdown, Marathon, };

    [XmlRoot("Settings")]
    public class ApplicationSettings : ICloneable
    {
        private readonly Font DefaultFont = new Font("Comic Sans MS", 12F);
        private readonly Color DefaultForeColor = Color.Black;
        private readonly Color DefaultBackColor = Color.WhiteSmoke;

        public ApplicationSettings()
            : base()
        {
            this.Font = this.DefaultFont;
            this.ForeColor = this.DefaultForeColor;
            this.BackColor = this.DefaultBackColor;
            this.LastGame = GameType.Standard;
            this.BrickPanel = new BrickPanelSettings();
        }

        public ApplicationSettings(ApplicationSettings other)
            : this()
        {
            if (other != null)
            {
                this.Font = other.Font;
                this.ForeColor = other.ForeColor;
                this.BackColor = other.BackColor;
                this.LastGame = other.LastGame;
                this.BrickPanel = other.BrickPanel.Clone() as BrickPanelSettings;
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
                    return Path.ChangeExtension(Application.ExecutablePath, "cfg").ToLower();
                }
                catch (Exception exception)
                {
                    Debug.WriteLine(exception);
                    return String.Empty;
                }

            }
        }

        public static bool Save(ApplicationSettings settings)
        {
            return ApplicationSettings.Save(ApplicationSettings.Filename, settings);
        }

        public static bool Save(string filename, ApplicationSettings settings)
        {
            try
            {
                using (TextWriter writer = new StreamWriter(filename))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(ApplicationSettings));
                    serializer.Serialize(writer, settings);
                    return true;
                }
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
                return false;
            }
        }

        public static bool Load(out ApplicationSettings settings)
        {
            return ApplicationSettings.Load(ApplicationSettings.Filename, out settings);
        }

        public static bool Load(string filename, out ApplicationSettings settings)
        {
            settings = null;

            try
            {
                if (File.Exists(filename))
                {
                    using (TextReader reader = new StreamReader(filename))
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof(ApplicationSettings));
                        settings = serializer.Deserialize(reader) as ApplicationSettings;
                    }
                }
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
            }

            return settings != null;
        }

        #endregion // Public static member function section.

        #region Public property section.

        [XmlIgnore]
        public Font Font { get; set; }

        [XmlElement("Font")]
        public string FontXML
        {
            get
            {
                try
                {
                    return (new FontConverter()).ConvertToString(this.Font);
                }
                catch (Exception exception)
                {
                    Debug.WriteLine(exception);
                }
                return String.Empty;
            }
            set
            {
                try
                {
                    this.Font = (new FontConverter()).ConvertFromString(value) as Font;
                }
                catch (Exception exception)
                {
                    Debug.WriteLine(exception);
                    this.Font = this.DefaultFont;
                }
            }
        }

        [XmlIgnore]
        public Color ForeColor { get; set; }

        [XmlElement("ForeColor")]
        public string ForeColorXML
        {
            get
            {
                return ColorTranslator.ToHtml(this.ForeColor);
            }
            set
            {
                try
                {
                    this.ForeColor = ColorTranslator.FromHtml(value);
                }
                catch (Exception exception)
                {
                    Debug.WriteLine(exception);
                    this.ForeColor = this.DefaultForeColor;
                }
            }
        }

        [XmlIgnore]
        public Color BackColor { get; set; }

        [XmlElement("BackColor")]
        public string BackColorXML
        {
            get
            {
                return ColorTranslator.ToHtml(this.BackColor);
            }
            set
            {
                try
                {
                    this.BackColor = ColorTranslator.FromHtml(value);
                }
                catch (Exception exception)
                {
                    Debug.WriteLine(exception);
                    this.BackColor = this.DefaultBackColor;
                }
            }
        }

        public GameType LastGame { get; set; }

        public BrickPanelSettings BrickPanel { get; set; }

        #endregion // Public property section.

        #region Public member function section.

        public object Clone()
        {
            return new ApplicationSettings(this);
        }

        #endregion // Public member function section.
    }

    public class BrickPanelSettings : ICloneable
    {
        private readonly Font DefaultFont = new Font("Comic Sans MS", 15.75F, FontStyle.Bold);
        private readonly Color DefaultForeColor = Color.DarkViolet;
        private readonly Color DefaultBackColor = Color.PowderBlue;
        private readonly Color[] DefaultBrickColors = new Color[] { Color.Blue, Color.Yellow, Color.Green, Color.Red };

        public BrickPanelSettings()
            : base()
        {
            this.Font = this.DefaultFont;
            this.ForeColor = this.DefaultForeColor;
            this.BackColor = this.DefaultBackColor;
            this.Highlight = true;
            this.Dimension = new Dimension();
            this.BrickColors = this.DefaultBrickColors;
        }

        public BrickPanelSettings(BrickPanelSettings other)
            : this()
        {
            if (other != null)
            {
                this.Font = other.Font;
                this.ForeColor = other.ForeColor;
                this.BackColor = other.BackColor;
                this.Highlight = other.Highlight;

                if (other.Dimension != null)
                {
                    this.Dimension = other.Dimension.Clone() as Dimension;
                }

                if (other.BrickColors != null && other.BrickColors.Length > 0)
                {
                    this.BrickColors = (new List<Color>(other.BrickColors)).ToArray();
                }
            }
            else
            {
                throw new ArgumentNullException("other");
            }
        }

        #region Public property section.

        [XmlIgnore]
        public Font Font { get; set; }

        [XmlElement("Font")]
        public string FontXML
        {
            get
            {
                try
                {
                    return (new FontConverter()).ConvertToString(this.Font);
                }
                catch (Exception exception)
                {
                    Debug.WriteLine(exception);
                }
                return String.Empty;
            }
            set
            {
                try
                {
                    this.Font = (new FontConverter()).ConvertFromString(value) as Font;
                }
                catch (Exception exception)
                {
                    Debug.WriteLine(exception);
                    this.Font = this.DefaultFont;
                }
            }
        }

        [XmlIgnore]
        public Color ForeColor { get; set; }

        [XmlElement("ForeColor")]
        public string ForeColorXML
        {
            get
            {
                return ColorTranslator.ToHtml(this.ForeColor);
            }
            set
            {
                try
                {
                    this.ForeColor = ColorTranslator.FromHtml(value);
                }
                catch (Exception exception)
                {
                    Debug.WriteLine(exception);
                    this.ForeColor = this.DefaultForeColor;
                }
            }
        }

        [XmlIgnore]
        public Color BackColor { get; set; }

        [XmlElement("BackColor")]
        public string BackColorXML
        {
            get
            {
                return ColorTranslator.ToHtml(this.BackColor);
            }
            set
            {
                try
                {
                    this.BackColor = ColorTranslator.FromHtml(value);
                }
                catch (Exception exception)
                {
                    Debug.WriteLine(exception);
                    this.BackColor = this.DefaultBackColor;
                }
            }
        }

        public bool Highlight { get; set; }

        public Dimension Dimension { get; set; }

        [XmlIgnore]
        public Color[] BrickColors { get; set; }

        [XmlArray("BrickColors")]
        [XmlArrayItem(ElementName = "Color", Type = typeof(string))]
        public string[] BrickColorsXML
        {
            get
            {
                List<string> result = new List<string>();
                if (this.BrickColors != null)
                {
                    foreach (Color color in this.BrickColors)
                    {
                        result.Add(ColorTranslator.ToHtml(color));
                    }
                }
                return result.ToArray();
            }
            set
            {
                List<Color> colors = new List<Color>();
                if (value != null && value.Length > 0)
                {
                    foreach (string name in value)
                    {
                        try
                        {
                            colors.Add(ColorTranslator.FromHtml(name));
                        }
                        catch (Exception exception)
                        {
                            Debug.WriteLine(exception);
                        }
                    }
                    this.BrickColors = colors.ToArray();
                }
                else
                {
                    this.BrickColors = this.DefaultBrickColors;
                }
            }
        }

        #endregion // Public property section.

        #region Public member function section.

        public object Clone()
        {
            return new BrickPanelSettings(this);
        }

        #endregion // Public member function section.
    }

    public class Dimension : ICloneable
    {
        public Dimension()
            : this(12, 8)
        {
        }

        public Dimension(int rows, int cols)
            : base()
        {
            this.Rows = rows;
            this.Cols = cols;
        }

        public Dimension(Dimension other)
            : this()
        {
            if (other != null)
            {
                this.Rows = other.Rows;
                this.Cols = other.Cols;
            }
            else
            {
                throw new ArgumentNullException("other");
            }
        }

        #region Public property section.

        public int Rows { get; set; }

        public int Cols { get; set; }

        [XmlIgnore]
        public string Display
        {
            get
            {
                return String.Format("{0} \u00D7 {1}", this.Rows, this.Cols);
            }
        }

        [XmlIgnore]
        public bool IsValid
        {
            get
            {
                // Number of rows and columns must be greater than zero and resulting dimension must be even!
                return this.Rows > 0 && this.Cols > 0 && ((this.Rows * this.Cols) & 1) == 0;
            }
        }

        #endregion // Public property section.

        #region Public member function section.

        public Point ToPoint()
        {
            // Not a mistake! -> point(x,y);
            return new Point(this.Cols, this.Rows);
        }

        public void FromPoint(Point point)
        {
            if (point != null)
            {
                // Not a mistake! -> point(x,y);
                this.Rows = point.Y;
                this.Cols = point.X;
            }
        }

        public object Clone()
        {
            return new Dimension(this);
        }

        public override string ToString()
        {
            return String.Format("Rows={0}, Cols={1}, Valid={2}",
                this.Rows, this.Cols, (this.IsValid ? "true" : "false"));
        }

        #endregion // Public member function section.
    }
}
