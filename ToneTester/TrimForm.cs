using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ToneTester
{
    public partial class TrimForm : Form
    {
        public TrimForm()
        {
            InitializeComponent();
            CenterToScreen();
        }
        public static string fileContent = "";
        private void trackBar1_Scroll(object sender, EventArgs e)
        {

        }
        public static Form1 form;
        public static Stream fileStream;
        private void button2_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //Get the path of specified file
                string filePath = openFileDialog1.FileName;

                //Read the contents of the file into a stream
                 fileStream = openFileDialog1.OpenFile();

                using (StreamReader reader = new StreamReader(fileStream))
                {
                    fileContent = reader.ReadToEnd();
                    //Clipboard.SetText(fileContent);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (fileContent != "")
            {
                Dictionary<string, float> datapoints = new Dictionary<string, float>();
                string[] lines = fileContent.Split(
                    new string[] { Environment.NewLine, "\n" },
                    StringSplitOptions.None
                );
                int l = lines.Length;
                int q = 1;
                foreach(string str in lines)
                {
                    string[] parts = str.Split(';');
                    string mood = form.GetMostProminent(parts[0]).type;
                    float[] scores = form.GetMostProminent(parts[0]).score;

                    datapoints[parts[0]] = scores.Max();

                    label2.Text = "Line " + q + "/" + l;
                    if (q % 5 == 0)
                    {
                        this.Refresh();
                    }
                    q++;
                }
                IOrderedEnumerable<KeyValuePair<string,float>> thing = datapoints.OrderBy(key => key.Value);
                for(int i = 0; i < (int)(thing.Count()*(trackBar1.Value/25f)); i++)
                {
                    datapoints.Remove(thing.ElementAt(0).Key);
                }
                string s = "";
                foreach(string str in datapoints.Keys)
                {
                    s += str + "\n";
                }
                Clipboard.SetText(s);
                MessageBox.Show("Finished product copied to clipboard.");
                /*if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    if ((fileStream = saveFileDialog1.OpenFile()) != null)
                    {
                        // Code to write the stream goes here.

                        fileStream.Close();
                    }
                }*/
            }
        }
    }
}
