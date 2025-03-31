using OrderSystem;
using System;
using System.Windows.Forms;
namespace assignment6
{
    public partial class Form2 : Form
    {
        static OrderManager manager;
        public Form2(OrderManager m)
        {
            InitializeComponent();
            manager = m;


        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string input1 = textBox1.Text;
            string input2 = textBox2.Text;
            string input3 = textBox3.Text;
            string input4 = textBox4.Text;
            string input5 = textBox5.Text;
            OrderOperate.Add(input1, input2, input3, input4, input5, manager);
            
        }
    }
}
