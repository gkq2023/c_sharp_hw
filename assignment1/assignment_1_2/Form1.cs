using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace assignment_1_2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitializeOthers();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            int op = comboBox1.SelectedIndex;
            double a,b = 0;
          
            if(double.TryParse(textBox1.Text,out a) && double.TryParse(textBox1.Text, out b))
            {
                double result;
                switch (op)
                {
                    case 0: result = a + b; break;
                    case 1: result = a - b; break;
                    case 2: result = a * b; break;
                    case 3:
                        if (b == 0) throw new DivideByZeroException();
                        result = (double)a / b;
                        break;
                    default:
                        throw new InvalidOperationException();
                }

                this.label1.Text = $"结果：{result}";
            }
            else
            {
                MessageBox.Show("请输入数字");
            }

        }

   
    }
}
