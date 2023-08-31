using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;
using static System.Net.Mime.MediaTypeNames;

namespace BdT_Mosconi
{
    public partial class NewUser : Form
    {
        public NewUser()
        {
            InitializeComponent();
        }
        private void NewUser_Load(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            comboBox1.Text = comboBox1.Items[0].ToString();
            comboBox2.Text = comboBox2.Items[0].ToString();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            bool done = true;
            double newphone;
            DateTime birth;
            if (String.IsNullOrWhiteSpace(textBox1.Text))
            {
                done = false;
                MessageBox.Show("Il campo 'Name' è errato");
            }
            if (String.IsNullOrWhiteSpace(textBox2.Text))
            {
                done = false;
                MessageBox.Show("Il campo 'Surname' è errato");
            }
            if (String.IsNullOrWhiteSpace(textBox3.Text) || textBox3.Text.Length!=10)
            {
                try
                {
                    newphone = double.Parse(textBox3.Text);
                }
                catch
                {
                    done = false;
                    MessageBox.Show("Il campo 'Telephone Number' è errato");
                }
            }
            if (comboBox1.Text == string.Empty)
            {
                done = false;
                MessageBox.Show("Il campo 'Area' non è stato selezionato");
            }
            if (comboBox2.Text == string.Empty)
            {
                done = false;
                MessageBox.Show("Il campo 'Job' non è stato selezionato");
            }
            birth = Convert.ToDateTime(monthCalendar1.SelectionRange.Start.ToShortDateString());

            if (done)
            {
                Utente nuovo = new Utente(textBox1.Text, textBox2.Text, birth, double.Parse(textBox3.Text), comboBox1.Text, comboBox2.Text);
                Aggiungi(nuovo);
                MessageBox.Show("Aggiunta eseguita con SUCCESSO");
            }
        }

        public static void Aggiungi(Utente nuovo)
        {
            var Q = new FileStream(@"Users.json", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            Q.Close();
            StreamReader sr = new StreamReader(@"Users.json");
            StreamWriter sw = new StreamWriter(@"./Users2.json");

            string line = "";
            int i = 0;

            while (!sr.EndOfStream || i != 1)
            {
                line = sr.ReadLine();

                if (line != null && i == 0)
                {
                    sw.WriteLine(line);
                }
                else
                {
                    //aggiunta classe jsonata
                    string jsonString = JsonConvert.SerializeObject(nuovo, Formatting.None);
                    sw.WriteLine(jsonString);
                    i = 1;
                }
            }
            sr.Close();
            sw.Close();

            System.IO.File.Delete(@"Users.json");
            System.IO.File.Move(@"./Users2.json", @"Users.json");
        }
    }
}
