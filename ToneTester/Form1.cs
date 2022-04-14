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

            int r = rnd.Next(dict.Count);

            if (dict.ContainsKey(word))
            {
                return dict[word][r];
            }
            return word;
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

        public string RegenerateSentence(string sentence, int wordCount=3)
        {
            string[] words = sentence.Split(' ');
            for (int i = 0; i < wordCount; i++) {
                int r = rnd.Next(words.Length);
                words[r] = GetSynonym(words[r]);
            }


            return sentence;
        }
        public string GetMostProminent(string input)
        {
            string str = "";

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
                return result.PredictedLabel;
            }
            return "None";
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            label1.Text = GetMostProminent(textBox1.Text).FirstCharToUpper();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

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
