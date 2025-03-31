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
    public partial class Form3 : Form
    {
        static OrderManager manager;
        public Form3(OrderManager m)
        {
            InitializeComponent();
            manager = m;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string input1 = textBox1.Text;
            string input2 = textBox2.Text;
            string input3 = textBox3.Text;
            string input4 = textBox4.Text;
            string input5 = textBox5.Text;
            OrderOperate.Modify(input1, input2, input3, input4, input5, manager);
        }
    }
}
