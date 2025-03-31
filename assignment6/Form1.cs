using OrderSystem;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace assignment6
{
    public partial class Form1 : Form
    {
        static OrderManager manager;
        public Form1()
        {
            InitializeComponent();
            manager = new OrderManager();
        }

        public void button1_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2(manager);
            form2.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3(manager);
            form3.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string input = textBox1.Text;
            List <Order> result = manager.Find(o => o.ID == int.Parse(input));
            if (result?.Count > 0)
            {
                // 将结果拼接成一个字符串，并用换行符分隔
                StringBuilder message = new StringBuilder();
                result.ForEach(item => message.AppendLine(item.ToString()));

                // 在消息框中显示结果
                MessageBox.Show(message.ToString(), "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("No items to display.", "Result", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4(manager);
            form4.ShowDialog();
        }
    }
}
