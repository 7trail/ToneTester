
namespace ToneTester
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.button2 = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.richTextBox3 = new System.Windows.Forms.RichTextBox();
            this.richTextBox4 = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Segoe UI", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.button1.Location = new System.Drawing.Point(12, 160);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(200, 56);
            this.button1.TabIndex = 0;
            this.button1.Text = "Evaluate";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 12);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(200, 142);
            this.textBox1.TabIndex = 1;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 28F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(218, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(162, 51);
            this.label1.TabIndex = 2;
            this.label1.Text = "Emotion";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(12, 222);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(94, 19);
            this.checkBox1.TabIndex = 3;
            this.checkBox1.Text = "Performance";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Segoe UI", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.button2.Location = new System.Drawing.Point(12, 327);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(200, 72);
            this.button2.TabIndex = 4;
            this.button2.Text = "Improve";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(218, 56);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(252, 96);
            this.richTextBox1.TabIndex = 9;
            this.richTextBox1.Text = "";
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(12, 276);
            this.trackBar1.Maximum = 25;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(200, 45);
            this.trackBar1.TabIndex = 10;
            this.trackBar1.Value = 5;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(114, 222);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(98, 19);
            this.checkBox2.TabIndex = 11;
            this.checkBox2.Text = "VerboseMode";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Joy",
            "Sadness",
            "Anger",
            "Love",
            "Fear",
            "Surprise"});
            this.comboBox1.Location = new System.Drawing.Point(12, 247);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(200, 23);
            this.comboBox1.TabIndex = 12;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(511, 56);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(218, 214);
            this.pictureBox2.TabIndex = 13;
            this.pictureBox2.TabStop = false;
            // 
            // richTextBox2
            // 
            this.richTextBox2.BackColor = System.Drawing.SystemColors.ControlLight;
            this.richTextBox2.Location = new System.Drawing.Point(218, 153);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.ReadOnly = true;
            this.richTextBox2.Size = new System.Drawing.Size(251, 72);
            this.richTextBox2.TabIndex = 14;
            this.richTextBox2.Text = "Improved Sentence";
            this.richTextBox2.Click += new System.EventHandler(this.richTextBox2_TextChanged);
            // 
            // richTextBox3
            // 
            this.richTextBox3.BackColor = System.Drawing.SystemColors.ControlLight;
            this.richTextBox3.Location = new System.Drawing.Point(218, 231);
            this.richTextBox3.Name = "richTextBox3";
            this.richTextBox3.ReadOnly = true;
            this.richTextBox3.Size = new System.Drawing.Size(251, 72);
            this.richTextBox3.TabIndex = 15;
            this.richTextBox3.Text = "Improved Sentence";
            this.richTextBox3.Click += new System.EventHandler(this.richTextBox3_TextChanged);
            // 
            // richTextBox4
            // 
            this.richTextBox4.BackColor = System.Drawing.SystemColors.ControlLight;
            this.richTextBox4.Location = new System.Drawing.Point(218, 309);
            this.richTextBox4.Name = "richTextBox4";
            this.richTextBox4.ReadOnly = true;
            this.richTextBox4.Size = new System.Drawing.Size(251, 72);
            this.richTextBox4.TabIndex = 16;
            this.richTextBox4.Text = "Improved Sentence";
            this.richTextBox4.Click += new System.EventHandler(this.richTextBox4_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(218, 384);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 15);
            this.label2.TabIndex = 17;
            this.label2.Text = "Update Text";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 411);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.richTextBox4);
            this.Controls.Add(this.richTextBox3);
            this.Controls.Add(this.richTextBox2);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Mood Engine";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private System.Windows.Forms.RichTextBox richTextBox3;
        private System.Windows.Forms.RichTextBox richTextBox4;
        private System.Windows.Forms.Label label2;
    }
}

