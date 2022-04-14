using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ToneTester
{
    public partial class Form1 : Form
    {
        static Dictionary<string,List<string>> dict;

        static Random rnd = new Random();
        protected static Stream GetResourceStream(string resourcePath)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            List<string> resourceNames = new List<string>(assembly.GetManifestResourceNames());

            resourcePath = resourcePath.Replace(@"/", ".");
            resourcePath = resourceNames.FirstOrDefault(r => r.Contains(resourcePath));

            if (resourcePath == null)
                throw new FileNotFoundException("Resource not found");

            return assembly.GetManifestResourceStream(resourcePath);
        }
        public Form1()
        {
            InitializeComponent();
            //dict = ReadAllResourceLines(@"WordnetSynonymsEdit.csv").Select(line => line.Split(',')).ToDictionary(line => line[0], line => line[1]);
            string s = Properties.Resources.String1;
            string[] lines = s.Split(
                new string[] { Environment.NewLine },
                StringSplitOptions.None
            );
            //dict = lines.Select(line => line.Split(',')).ToDictionary(line => line[0], line => line[1]);
            var tempDict = new Dictionary<string, List<string>>();
            foreach (string str in lines)
            {
                

                string[] splits = str.Split(',');

                KeyValuePair<string, string> pair = new KeyValuePair<string, string>(splits[0],splits[1]);

                string[] split = pair.Value.Split(new Char[] { ';', '|' },
                                 StringSplitOptions.RemoveEmptyEntries);
                if (!tempDict.ContainsKey(pair.Key))
                {
                    tempDict.Add(pair.Key, split.ToList());
                }
            }
            dict = tempDict;
        }

        public string GetSynonym(string word)
        {


            if (word != null)
            {
                if (dict.ContainsKey(word))
                {
                    int r = rnd.Next(dict[word].Count);
                    return dict[word][r];
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
                    if (syn.Length >= 3 && words[r].Length >= 3 && syn.Substring(0, 3) != words[r].Substring(0, 3)) {
                        words[r] = syn;
                    }
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
        public SentenceData GetMostProminent(string input)
        {
            string str = "";
            SentenceData data = new SentenceData();
            dynamic sampleData = null;
            dynamic result = null;
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
            return data;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            label1.Text = GetMostProminent(textBox1.Text.ToLower()).type.FirstCharToUpper();
            //progressBar1.Value = (int)(GetMostProminent(textBox1.Text).confidence*100);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string startSentence = textBox1.Text.ToLower();
            string mood = GetMostProminent(textBox1.Text.ToLower()).type;
            string currentSentence = startSentence;
            float bestConfidence = 0;
            Dictionary<string, float> newSentences = new Dictionary<string, float>();
            for (int i = 0; i < 100; i++)
            {
                string newSentence = RegenerateSentence(startSentence);
                SentenceData data = GetMostProminent(newSentence);
                /*if (data.type == mood && data.confidence > bestConfidence)
                {
                    bestConfidence = data.confidence;
                    currentSentence = newSentence;
                }*/
                newSentences[newSentence] = data.confidence;

            }
            var thing = newSentences.OrderByDescending(key => key.Value);
            label2.Text = thing.ElementAt(0).Key;
            label3.Text = thing.ElementAt(1).Key;
            label4.Text = thing.ElementAt(2).Key;


            //Clipboard.SetText(currentSentence);
            //label2.Text = currentSentence;
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
