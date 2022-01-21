using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinPad
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string path = @".\NewTextFile.txt";
        string path2 = @".\lastsave.data";

        private void button1_Click(object sender, EventArgs e)
        {
            var time = DateTime.Now.ToString("HH:mm:ss tt");

            string lastsave = path + " - Last Saved: " + time;
            status.Text = lastsave;

            try
            {
                // Create the file, or overwrite if the file exists.
                using (FileStream fs = File.Create(path))
                {
                    byte[] info = new UTF8Encoding(true).GetBytes(richTextBox1.Text);
                    // Add some information to the file.
                    fs.Write(info, 0, info.Length);
                }

                using (FileStream fs = File.Create(path2))
                {
                    byte[] info = new UTF8Encoding(true).GetBytes(lastsave);
                    // Add some information to the file.
                    fs.Write(info, 0, info.Length);
                }

            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            listBox1.Items.Clear();
            DirectoryInfo dinfo = new DirectoryInfo(@".\");
            FileInfo[] Files = dinfo.GetFiles("*.txt");
            foreach (FileInfo file in Files)
            {
                listBox1.Items.Add(file.Name);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (File.Exists(path2))
            {
                // Read entire text file content in one string  
                string text = File.ReadAllText(path2);
                Console.WriteLine(text);
                status.Text = text;
            }

            if (File.Exists(path))
            {
                // Read entire text file content in one string  
                string text2 = File.ReadAllText(path);
                Console.WriteLine(text2);
                richTextBox1.Text = text2;
            }

            listBox1.Items.Clear();
            DirectoryInfo dinfo = new DirectoryInfo(@".\");
            FileInfo[] Files = dinfo.GetFiles("*.txt");
            foreach (FileInfo file in Files)
            {
                listBox1.Items.Add(file.Name);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            path = @".\" + textBox1.Text + ".txt";

            if (File.Exists(path))
            {
                // Read entire text file content in one string  
                string text2 = File.ReadAllText(path);
                Console.WriteLine(text2);
                richTextBox1.Text = text2;
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            DirectoryInfo dinfo = new DirectoryInfo(@".\");
            FileInfo[] Files = dinfo.GetFiles("*.txt");
            foreach (FileInfo file in Files)
            {
                listBox1.Items.Add(file.Name);
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Please select an Item!");
            }
            else
            {
                string curItem = listBox1.SelectedItem.ToString();
                Console.WriteLine(curItem);
                path = path = @".\" + curItem;
                string path3 = curItem.Replace(".txt", "");


                if (File.Exists(path))
                {
                    textBox1.Text = path3;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (File.Exists(path))
            {
                File.Delete(path);

                listBox1.Items.Clear();
                DirectoryInfo dinfo = new DirectoryInfo(@".\");
                FileInfo[] Files = dinfo.GetFiles("*.txt");
                foreach (FileInfo file in Files)
                {
                    listBox1.Items.Add(file.Name);
                }
            }
        }
    }
}
