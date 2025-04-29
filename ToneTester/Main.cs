using Microsoft.ML;
using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ToneTester
{
    public partial class Main : Form
    {
        static Dictionary<string,List<string>> dict;
        static Dictionary<string, List<string>> dict2;

        static Random rnd = new Random();
        public Main()
        {
            InitializeComponent();
            CenterToScreen(); // Just makes the UI appear in the center of the screen. 

            string s = Properties.Resources.NewResource;
            string[] lines = s.Split(
                new string[] { Environment.NewLine },
                StringSplitOptions.None
            );

            var tempDict = new Dictionary<string, List<string>>();
            foreach (string str in lines)
            {
                string[] splits = str.Split(',',2);

                KeyValuePair<string, string> pair = new KeyValuePair<string, string>(splits[0],splits[1]);

                string[] split = pair.Value.Split(new Char[] { ';', '|', ',' },
                                 StringSplitOptions.RemoveEmptyEntries);
                if (!tempDict.ContainsKey(pair.Key))
                {
                    tempDict.Add(pair.Key, split.ToList());
                } else
                {
                    foreach(string ss in split)
                    {
                        if (ss != "" && !(ss.Distinct().Count()==1&&ss.ElementAt(0)!=','))
                        {
                            tempDict[pair.Key].Add(ss);
                        }
                    }
                }
            }
            dict = tempDict;

            s = Properties.Resources.String1;
            lines = s.Split(
                new string[] { Environment.NewLine },
                StringSplitOptions.None
            );
            //dict = lines.Select(line => line.Split(',')).ToDictionary(line => line[0], line => line[1]);
            var tempDict2 = new Dictionary<string, List<string>>();
            foreach (string str in lines)
            {
                string[] splits = str.Split(',');

                KeyValuePair<string, string> pair = new KeyValuePair<string, string>(splits[0], splits[1]);

                string[] split = pair.Value.Split(new Char[] { ';', '|', ',' },
                                 StringSplitOptions.RemoveEmptyEntries);
                if (!tempDict2.ContainsKey(pair.Key))
                {
                    tempDict2.Add(pair.Key, split.ToList());
                }
                else
                {
                    foreach (string ss in split)
                    {
                        if (ss != "" && !(ss.Distinct().Count() == 1 && ss.ElementAt(0) != ','))
                        {
                            tempDict2[pair.Key].Add(ss);
                            //Debug.Write("Added " + ss);
                        }
                    }
                }
            }
            dict2 = tempDict2;
        }
        public static bool evaluated = false;
        public static Dictionary<string, float> moods = new Dictionary<string, float>();

        private void pictureBox2_Resize(object sender,
        EventArgs pe)
        {
            pictureBox2.Invalidate();
        }
            
        private void pictureBox2_Paint(object sender,
        System.Windows.Forms.PaintEventArgs pe)
        {

            // Declares the Graphics object and sets it to the Graphics object  
            // supplied in the PaintEventArgs.  
            Graphics g = pe.Graphics;
            g.Clear(pictureBox2.BackColor);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            Pen p = new Pen(Color.Black, 2);
            // Insert code to paint the form here.  
            Rectangle rec = new Rectangle(0, 0, pictureBox2.Width-2, pictureBox2.Height-2);

            if (evaluated)
            {
                
                float total = 0;
                foreach (KeyValuePair<string, float> pair in moods)
                {
                    //Debug.Write("Showing pair");
                    float degree = pair.Value * 360;

                    //g.DrawPie(p, rec, total, degree);
                    Brush b1 = new SolidBrush(moodColors[pair.Key.ToLower()]);
                    g.FillPie(b1, rec, total, degree);
                    total += degree;
                    b1.Dispose();
                }
            }
            p.Dispose();
            
        }


        public struct SentenceData
        {
            public string type;
            public float confidence;
            public float[] score;
        }
        public SentenceData GetMostProminent(string input, bool loadPieValues = false)
        {
            string str = "";
            SentenceData data = new SentenceData();
            dynamic sampleData = null;
            dynamic result = null;
            Dataset.ModelOutput output;
            if (checkBox1.Checked)
            {
                sampleData = new DatasetLightweight.ModelInput()
                {
                    Col0 = input,
                };
                result = DatasetLightweight.Predict(sampleData);
            }
            else
            {
                sampleData = new Dataset.ModelInput()
                {
                    Col0 = input,
                };
                result = Dataset.Predict(sampleData);
            }

            if (result != null)
            {
                data.type = result.PredictedLabel;
                data.confidence = result.Score[1];
                data.score = result.Score;
            }

            if (loadPieValues && result != null)
            {
                evaluated = true;
                int i = 0;
                foreach(float val in result.Score)
                {
                    
                    moods[moodColors.Keys.ElementAt(i)] = val;
                    //Debug.WriteLine("Added mood");
                    i++;
                }
                pictureBox2.Invalidate();
                //foreach(string type in GetSlotNames())
            }

            return data;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox2.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox2_Paint);
            pictureBox2.Resize += new EventHandler(this.pictureBox2_Resize);
            this.DoubleBuffered = true;
        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            label1.Text = "Mood: " + GetMostProminent(textBox1.Text.ToLower(),true).type.FirstCharToUpper();
            richTextBox1.Text = textBox1.Text.ToLower().CapitalizeFirst();
            SetBoxText(richTextBox1);
        }

        public void SetBoxText(RichTextBox box)
        {
            int index = 0;
            foreach (string word in box.Text.Split(' '))
            {
                string mood = GetMostProminent(word).type;
                int selectStart = box.SelectionStart;
                if (moodColors.ContainsKey(mood.ToLower()))
                {
                    box.Select(index, word.Length);
                    box.SelectionColor = moodColors[mood.ToLower()];
                    box.Select(selectStart, 0);
                    box.SelectionColor = box.ForeColor;
                }
                index += word.Length + 1;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
        }
        public Dictionary<string, Color> moodColors = new Dictionary<string, Color>() { ["joy"] = Color.Green,  ["fear"] = Color.BlueViolet, ["surprise"] = Color.Orange, ["anger"] = Color.Red, ["sadness"] = Color.Blue, ["love"] = Color.Pink };

        // Quit function
        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // Just an about me menu

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("ToneTester 0.2\nBuilt by Austin Phillips\n2022-2025");
        }
    }
}
public static class StringExtensions
{
    public static string FirstCharToUpper(this string input) =>
        input switch
        {
            "" => throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input)),
            _ => string.Concat(input[0].ToString().ToUpper(), input.AsSpan(1))
        };
    public static string CapitalizeFirst(this string s)
    {
        bool IsNewSentense = true;
        var result = new StringBuilder(s.Length);
        for (int i = 0; i < s.Length; i++)
        {
            if (IsNewSentense && char.IsLetter(s[i]))
            {
                result.Append(char.ToUpper(s[i]));
                IsNewSentense = false;
            }
            else
                result.Append(s[i]);

            if (s[i] == '!' || s[i] == '?' || s[i] == '.')
            {
                IsNewSentense = true;
            }
        }

        return result.ToString();
    }
}