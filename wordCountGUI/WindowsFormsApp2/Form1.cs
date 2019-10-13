using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using wordCount;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        Function function = new Function();
        String filePath;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                textBox1.Enabled = true;
                filePath = textBox1.Text;
                FileStream fs = new FileStream(filePath, FileMode.Open);//打开文件
                fs.Close();
                this.label1.Text = "文件已导入";
                this.button1.Enabled = false;
                this.button2.Enabled = false;
                this.button3.Enabled = true;
                this.button4.Enabled = true;
                this.button5.Enabled = true;
                this.button6.Enabled = true;
            }
            catch
            {
                MessageBox.Show("文件路径无效");
                this.label1.Text = "文件未导入";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            filePath = Environment.CurrentDirectory + "\\test.txt";
            FileStream fs = new FileStream(filePath, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            sw.Write(textBox1.Text);
            this.label1.Text = "单词已导入";
            sw.Flush();
            sw.Close();
            fs.Close();
            this.button1.Enabled = false;
            this.button2.Enabled = false;
            this.button3.Enabled = true;
            this.button4.Enabled = true;
            this.button5.Enabled = true;
            this.button6.Enabled = true;
        }

        private void textBox1_TextChanged(object sender, EventArgs e) {}

        private void button3_Click(object sender, EventArgs e)
        {
            this.listBox1.Items.Add(Function.getChacactor(filePath));
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.listBox1.Items.Add(Function.getRows(filePath));
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.listBox1.Items.Add(Function.totalWord(filePath));
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ArrayList al = Function.countWord(filePath);
            foreach(string message in al)
            {
                this.listBox1.Items.Add(message);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            filePath = null;
            this.button1.Enabled = true;
            this.button2.Enabled = true;
            this.button3.Enabled = false;
            this.button4.Enabled = false;
            this.button5.Enabled = false;
            this.button6.Enabled = false;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            string returnPath = textBox2.Text;
            try
            {
                FileStream fs = new FileStream(filePath, FileMode.Create);//定义文件操作类型,实例化
                StreamWriter sw = new StreamWriter(fs);//用特定方式写入信息,实例化
                foreach (string info in listBox1.Items)
                {
                    sw.Write(info);
                    sw.Write("\r\n");//换行
                }
                sw.Flush();
                sw.Close();
                fs.Close();
                MessageBox.Show("导出成功");
            }
            catch
            {
                MessageBox.Show("保存路径无效");
            }
        }
    }
}
