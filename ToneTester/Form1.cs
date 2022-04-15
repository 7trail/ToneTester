using Microsoft.ML;
using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ToneTester
{
    public partial class Form1 : Form
    {
        static Dictionary<string,List<string>> dict;
        static Dictionary<string, List<string>> dict2;

        static Random rnd = new Random();
        public Form1()
        {
            InitializeComponent();
            CenterToScreen();
            //this.Controls.Add(pictureBox2);
            comboBox1.SelectedIndex = 0;
            //dict = ReadAllResourceLines(@"WordnetSynonymsEdit.csv").Select(line => line.Split(',')).ToDictionary(line => line[0], line => line[1]);
            string s = Properties.Resources.NewResource;
            string[] lines = s.Split(
                new string[] { Environment.NewLine },
                StringSplitOptions.None
            );
            //dict = lines.Select(line => line.Split(',')).ToDictionary(line => line[0], line => line[1]);
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
                            //Debug.Write("Added " + ss);
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

        public string GetSynonym(string word)
        {
            word = word.ToLower();
            if (word != null)
            {
                if ( (checkBox2.Checked&&dict.ContainsKey(word)) || (!checkBox2.Checked&& dict2.ContainsKey(word)))
                {

                    int r = 0;
                    string newWord = "";

                    if (!checkBox2.Checked)
                    {
                        r = rnd.Next(dict2[word].Count);
                        newWord = dict2[word][r];
                    } else
                    {
                        r = rnd.Next(dict[word].Count);
                        newWord= dict[word][r];
                    }
                    //newWord = "This (remove me) works fine!";

                    newWord = Regex.Replace(newWord, @"\(.*\)", "");
                    // Remove text between brackets.
                    newWord = newWord.Replace(",","");
                    // Remove extra spaces.
                    newWord = Regex.Replace(newWord, @"\s+", " ");
                    return newWord.Trim();
                }
                return word;
            }
            return "";
        }

        string[] ReadAllResourceLines(string resourceName)
        {
            using (Stream stream = Assembly.GetEntryAssembly()
                .GetManifestResourceStream(resourceName))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    return EnumerateLines(reader).ToArray();
                }
            }
        }

        IEnumerable<string> EnumerateLines(TextReader reader)
        {
            string line;

            while ((line = reader.ReadLine()) != null)
            {
                yield return line;
            }
        }
        public List<string> bannedWords = new List<string>() { "i", "am","was","is", "you", "we", "us", "the", "a", "an", "and","but","so","for","yet","because" };
        public string RegenerateSentence(string sentence, int wordCount=3)
        {
            string[] words = sentence.Split(' ');
            for (int i = 0; i < wordCount; i++) {
                int r = rnd.Next(words.Length);
                if (!bannedWords.Contains(words[r]))
                {
                    //bool plural = words[r].IsPlural();
                    string syn = GetSynonym(words[r]);
                    words[r] = syn;
                    //if (syn.Length >= 3 && words[r].Length >= 3 && syn.Substring(0, 3) != words[r].Substring(0, 3)) {
                    //    words[r] = syn;
                    //}
                    //if (plural) { words[r] += words[r].Pluralize(); }
                }
            }

            string s = "";
            foreach(string w in words)
            {
                s += w + " ";
            }
            return s;
        }
        public struct SentenceData
        {
            public string type;
            public float confidence;
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
            richTextBox1.Text = textBox1.Text.ToLower();
            SetBoxText(richTextBox1);
            //progressBar1.Value = (int)(GetMostProminent(textBox1.Text).confidence*100);
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


        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (evaluated)
            {
                string startSentence = textBox1.Text.ToLower();
                //string mood = GetMostProminent(textBox1.Text.ToLower()).type;
                string mood = comboBox1.Text;
                string currentSentence = startSentence;
                float bestConfidence = 0;
                Dictionary<string, float> newSentences = new Dictionary<string, float>();
                for (int i = 0; i < 100; i++)
                {
                    string newSentence = RegenerateSentence(startSentence, (int)((trackBar1.Value / 25f) * startSentence.Split(' ').Length));
                    SentenceData data = GetMostProminent(newSentence);
                    /*if (data.type == mood && data.confidence > bestConfidence)
                    {
                        bestConfidence = data.confidence;
                        currentSentence = newSentence;
                    }*/
                    newSentences[newSentence] = data.confidence;
                    if (i % 5 == 0)
                    {
                        label2.Text = "Generating sentence variants: " + (i + 1) + "%";
                        this.Refresh();
                    }
                }
                label2.Text = "Creating Rich Text: 0%";
                this.Refresh();
                var thing = newSentences.OrderByDescending(key => key.Value);
                richTextBox2.Text = thing.ElementAt(0).Key;
                SetBoxText(richTextBox2);
                label2.Text = "Creating Rich Text: 33%";
                this.Refresh();
                if (thing.Count() > 1)
                {
                    richTextBox3.Text = thing.ElementAt(1).Key;
                    SetBoxText(richTextBox3);
                }
                label2.Text = "Creating Rich Text: 66%";
                this.Refresh();
                if (thing.Count() > 2)
                {
                    richTextBox4.Text = thing.ElementAt(2).Key;
                    SetBoxText(richTextBox4);
                }
                label2.Text = "Done!";
                this.Refresh();

                //Clipboard.SetText(currentSentence);
                //label2.Text = currentSentence;
            }
        }
        public Dictionary<string, Color> moodColors = new Dictionary<string, Color>() { ["joy"] = Color.Green,  ["fear"] = Color.BlueViolet, ["surprise"] = Color.Orange, ["anger"] = Color.Red, ["sadness"] = Color.Blue, ["love"] = Color.Pink };

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {
            if (evaluated && richTextBox2.Text != "Improved Sentence")
            {
                Clipboard.SetText(richTextBox2.Text);
                MessageBox.Show("Copied improved text to clipboard!");
            }
        }

        private void richTextBox3_TextChanged(object sender, EventArgs e)
        {
            if (evaluated && richTextBox3.Text != "Improved Sentence")
            {
                Clipboard.SetText(richTextBox3.Text);
                MessageBox.Show("Copied improved text to clipboard!");
            }
        }

        private void richTextBox4_TextChanged(object sender, EventArgs e)
        {
            if (evaluated && richTextBox4.Text != "Improved Sentence")
            {
                Clipboard.SetText(richTextBox4.Text);
                MessageBox.Show("Copied improved text to clipboard!");
            }
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
}
