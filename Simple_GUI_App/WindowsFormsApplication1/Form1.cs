using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //Environment.GetFolderPath(Environment.SpecialFolder.MyPictures)
            this.button1.BackgroundImage =
                Image.FromFile( + "//Card1.png");
        }

        private string oldFilePath, newFolderPath;

        private void button3_Click(object sender, EventArgs e)
        {
            string fileName = oldFilePath.Substring(oldFilePath.LastIndexOf("\\")+1);
            System.IO.File.Copy(oldFilePath, newFolderPath + "\\"+ fileName);
            MessageBox.Show("File copied Successfully!");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                oldFilePath = openFileDialog1.FileName;
                textBox1.Text = oldFilePath;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                newFolderPath = folderBrowserDialog1.SelectedPath;
                textBox2.Text = newFolderPath;
            }
        }
    }
}
