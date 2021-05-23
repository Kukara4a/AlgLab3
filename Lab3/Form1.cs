using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Numerics;
using System.Xml;

namespace Lab3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 0 && textBox1.Text.Contains('/'))
            {
                if (textBox1.Text.Contains(' '))
                {
                    textBox1.Text = Calculation(textBox1.Text);
                    return;
                }
                var num = textBox1.Text.Split('/');
                var numerator = new BigInteger(Int64.Parse(num[0]));
                var denominator = new BigInteger(Int64.Parse(num[1]));

                var GCD =  RationalNumbers.GetGreatestCommonDivisor(numerator, denominator);

                var ratNum = new RationalNumbers(numerator / GCD, denominator / GCD);

                textBox1.Text = ratNum.ToString();
            }
            else
                MessageBox.Show("Введите число!");
        }

        public string Calculation(string str)
        {
            var input = str.Split();

            var a1 = input[0].Split('/');
            var a = new RationalNumbers(new BigInteger(Int64.Parse(a1[0])), new BigInteger(Int64.Parse(a1[1])));

            var sign = input[1];

            var a2 = input[2].Split('/');
            var b = new RationalNumbers(new BigInteger(Int64.Parse(a2[0])), new BigInteger(Int64.Parse(a2[1])));

            string result = "";

            switch (sign)
            {
                case ("+"):
                    result = (a + b).ToString();
                    break;
                case ("-"):
                    result = (a - b).ToString();
                    break;
                case("*"):
                    result = (a * b).ToString();
                    break;
                case ("/"):
                    result = (a / b).ToString();
                    break;
                default:
                    break;
            }
            return result;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 0)
            {
                var num = textBox1.Text.Split('/');
                var numerator = new BigInteger(Int64.Parse(num[0]));
                var denominator = new BigInteger(Int64.Parse(num[1]));

                var ratNum = new RationalNumbers(numerator, denominator).GetPeriod();

                textBox1.Text = ratNum.ToString();
            }
            else
                MessageBox.Show("Введите число!");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 0 && textBox1.Text.Contains('.'))
            {
                textBox1.Text = RationalNumbers.GetRational(textBox1.Text);
            }
            else
                MessageBox.Show("Введите число!");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load("C:\\Users\\Илья\\source\\repos\\Lab3\\Lab3\\in.xml");

            XmlElement xRoot = xDoc.DocumentElement; // получим корневой элемент

            foreach (XmlNode xnode in xRoot)
            {
                textBox2.Text += (xnode.InnerText + "\r\n");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (textBox2.Text.Length > 0)
            {
                var numbers = textBox2.Text.Split('\n');
                foreach(var number in numbers)
                {
                    if (number == "")
                        continue; 

                    var num = number.Remove(number.Length - 1).Split('/');

                    var numerator = new BigInteger(Int64.Parse(num[0]));
                    var denominator = new BigInteger(Int64.Parse(num[1]));

                    var GCD = RationalNumbers.GetGreatestCommonDivisor(numerator, denominator);

                    var ratNum = new RationalNumbers(numerator / GCD, denominator / GCD);

                    textBox3.Text += ratNum + "      " + ratNum.GetPeriod() + "\r\n";
                }               
            }
            else
                MessageBox.Show("Загрузите данные");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load("C:\\Users\\Илья\\source\\repos\\Lab3\\Lab3\\out.xml");

            if (textBox3.Text.Length > 0)
            {
                var numbers = textBox3.Text.Split('\n');
                foreach (var number in numbers)
                {
                    if (number == "")
                        continue;

                    XmlElement xRoot = xDoc.DocumentElement;
                    XmlElement numElem = xDoc.CreateElement("num");
                    XmlText numText = xDoc.CreateTextNode(number);

                    numElem.AppendChild(numText);
                    xRoot.AppendChild(numElem);

                }

                xDoc.Save("C:\\Users\\Илья\\source\\repos\\Lab3\\Lab3\\out.xml");
            }
            else
                MessageBox.Show("Загрузите данные и преобразуйте их");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load("C:\\Users\\Илья\\source\\repos\\Lab3\\Lab3\\out.xml");

            XmlElement xRoot = xDoc.DocumentElement;
            xRoot.RemoveAll();
            xDoc.Save("C:\\Users\\Илья\\source\\repos\\Lab3\\Lab3\\out.xml");                   
        }
    }
}
