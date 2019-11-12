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
using System.Text.RegularExpressions;
namespace protect_inf_LR1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            N = 33;
        }

        char[] characters = new char[] { 'Я', 'А', 'Б', 'В', 'Г', 'Д', 'Е', 'Ё', 'Ж', 'З', 'И',
                                                        'Й', 'К', 'Л', 'М', 'Н', 'О', 'П', 'Р', 'С', 
                                                        'Т', 'У', 'Ф', 'Х', 'Ц', 'Ч', 'Ш', 'Щ', 'Ь', 'Ы', 'Ъ',
                                                        'Э', 'Ю', 'Я' };

        private int N; //длина алфавита

        //зашифровать
        private void buttonEncrypt_Click(object sender, EventArgs e)
        {
            if (radioButtonGamma.Checked)
            {
                string s;

                StreamReader sr = new StreamReader("in.txt");
                StreamWriter sw = new StreamWriter("out.txt");

                while (!sr.EndOfStream)
                {
                    s = sr.ReadLine();
                    sw.WriteLine(Encode(s, Generate_Pseudorandom_KeyWord(s.Length, 100)));
                }

                sr.Close();
                sw.Close();
            }

            else
            {
                string lines = textBox1.Text;
                string writePath = "in.txt";


                using (StreamWriter sw = new StreamWriter(writePath, false, System.Text.Encoding.UTF8))
                {
                    sw.WriteLine(lines);
                }
                if (textBoxKeyWord.Text.Length > 0)
                {
                    string s;

                    StreamReader sr = new StreamReader("in.txt");
                    StreamWriter sw = new StreamWriter("out.txt");

                    while (!sr.EndOfStream)
                    {
                        s = sr.ReadLine();
                        sw.WriteLine(Encode(s, textBoxKeyWord.Text));
                    }

                    sr.Close();
                    sw.Close();
                }
                
                else
                    MessageBox.Show("Введите ключевое слово!");

                
                if(textBox1.Text!="")
                textBox2.Text = File.ReadAllText("out.txt", Encoding.UTF8);
                else
                    MessageBox.Show("Введите  слово!");

            }
        }
        
    
        //расшифровать
        private void buttonDecipher_Click(object sender, EventArgs e)
        {
            if (radioButtonGamma.Checked)
            {
                string s;

                StreamReader sr = new StreamReader("in.txt");
                StreamWriter sw = new StreamWriter("out.txt");

                while (!sr.EndOfStream)
                {
                    s = sr.ReadLine();
                    sw.WriteLine(Decode(s, Generate_Pseudorandom_KeyWord(s.Length, 100)));
                }

                sr.Close();
                sw.Close();
            }
            else
            {
                if (textBoxKeyWord.Text.Length > 0)
                {
                    string s;

                    StreamReader sr = new StreamReader("in.txt");
                    StreamWriter sw = new StreamWriter("out.txt");

                    while (!sr.EndOfStream)
                    {
                        s = sr.ReadLine();
                        sw.WriteLine(Decode(s, textBoxKeyWord.Text));
                    }

                    sr.Close();
                    sw.Close();
                }

                else
                    MessageBox.Show("Введите ключевое слово!");

                
                textBox2.Text = File.ReadAllText("out.txt", Encoding.UTF8);
            }
        }

        //зашифровать
        private string Encode(string input, string keyword)
        {
            input = input.ToUpper();
            keyword = keyword.ToUpper();

            string result = "";

            int keyword_index = 0;
            
                
                foreach (char symbol in input)
                {
                try
                {
                    int c = (Array.IndexOf(characters, symbol) +
                        Array.IndexOf(characters, keyword[keyword_index])) % N;
                
                    result += characters[c];
                    keyword_index++;
                }
                catch (IndexOutOfRangeException ex)
               { MessageBox.Show("Неверно ввел исходные данные", "Ошибка дурака"); }
                

                    if ((keyword_index ) == keyword.Length)
                        keyword_index = 0;
                }
                
            
            return result;
        }

        //расшифровать
        private string Decode(string input, string keyword)
        {
            input = input.ToUpper();
            keyword = keyword.ToUpper();

            string result = "";

            int keyword_index = 0;

            foreach (char symbol in input)
            {
                int p = (Array.IndexOf(characters, symbol) + N -
                    Array.IndexOf(characters, keyword[keyword_index])) % N;

                result += characters[p];

                keyword_index++;

                if ((keyword_index ) == keyword.Length)
                    keyword_index = 0;
            }

            return result;
        }

        private string Generate_Pseudorandom_KeyWord(int lenght, int startSeed)
        {
            Random rand = new Random(startSeed);

            string result = "";

            for (int i = 0; i < lenght; i++)
                result += characters[rand.Next(0, characters.Length)];

            return result;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
           //     OpenFileDialog o = new OpenFileDialog();
           //     o.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            //    if (o.ShowDialog() == DialogResult.OK)
                {
                    textBox2.Text = File.ReadAllText("out.txt", Encoding.UTF8);
                }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = File.ReadAllText("out.txt", Encoding.UTF8);
            string lines = textBox1.Text;
            string writePath = "in.txt";


            using (StreamWriter sw = new StreamWriter(writePath, false, System.Text.Encoding.UTF8))
            {
                sw.WriteLine(lines);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

            { 
                string tempstr = "";
                foreach (char sym in textBox1.Text)
                {
                    if (!TextBoxСheck(sym.ToString()))
                        tempstr += sym.ToString();
                }
                textBox1.Text = tempstr;
                textBox1.SelectionStart = textBox1.TextLength;
            }

             bool TextBoxСheck(string text)
            {
                return new Regex("[^А-Яа-я]").IsMatch(text);
            }

        }

        private void textBoxKeyWord_TextChanged(object sender, EventArgs e)
        {
            {
                string tempstr = "";
                foreach (char sym in textBoxKeyWord.Text)
                {
                    if (!TextBoxСheck(sym.ToString()))
                        tempstr += sym.ToString();
                }
                textBoxKeyWord.Text = tempstr;
                textBoxKeyWord.SelectionStart = textBoxKeyWord.TextLength;
            }

            bool TextBoxСheck(string text)
            {
                return new Regex("[^А-Яа-я]").IsMatch(text);
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}