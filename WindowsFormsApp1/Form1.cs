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

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            this.MaximizeBox = false;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(FolderPath.Text) && !string.IsNullOrEmpty(FileName.Text) && Directory.Exists(FolderPath.Text))
            {
                AddFileRecursion(FolderPath.Text, FileName.Text);
                MessageBox.Show("文件追加成功", "温馨提示");
            }
            else
            {
                MessageBox.Show("文件夹路径不存在", "温馨提示");
            }

        }
        public void AddFileRecursion(string FolderPath, string FileName)
        {
            AddFile(FolderPath, FileName);
            foreach (var ChildrenFolderPath in Directory.GetDirectories(FolderPath))
            {
                AddFileRecursion(ChildrenFolderPath, FileName);
            }
        }
        public void AddFile(string FolderPath, string FileName)
        {
            if (Directory.GetFiles(FolderPath).Length == 0)
            {
                FileStream fs = new FileStream(FolderPath + @"\" + FileName, FileMode.Create, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine();
                sw.Close();
                fs.Close();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
