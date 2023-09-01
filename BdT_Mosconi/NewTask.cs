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
using System.Security.Policy;

namespace BdT_Mosconi
{
    public partial class NewTask : Form
    {
        Utente requester= new Utente();
        public NewTask()
        {
            InitializeComponent();
        }

        private void NewTask_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool done = true;
            if (String.IsNullOrWhiteSpace(textBox1.Text))
            {
                done = false;
                MessageBox.Show("Il campo 'Id' è errato");
            }
            else
            {
                StreamReader sr = new StreamReader(@"Users.json");
                string line = "";
                bool exist=false;  
                while (!sr.EndOfStream)
                {
                    line = sr.ReadLine();

                    if (line != null)
                    {
                        Utente temp = JsonConvert.DeserializeObject<Utente>(line);
                        if (temp.Id == textBox1.Text)
                        {
                            exist = true;
                            requester = temp;
                        }
                    }
                }
                sr.Close();

                if (!exist)
                {
                    MessageBox.Show("Il campo 'Id' non corrisponde a nessun utente");
                }
            }
            if (String.IsNullOrWhiteSpace(textBox2.Text))
            {
                done = false;
                MessageBox.Show("Il campo 'Description' è errato");
            }
            if (String.IsNullOrWhiteSpace(textBox3.Text))
            {
                try
                {
                    double temp = double.Parse(textBox3.Text);
                }
                catch
                {
                    done = false;
                    MessageBox.Show("Il campo 'Number of Hours' è errato");
                }
            }

            if (done)
            {
                Prestazione nuovo = new Prestazione(CreateId(textBox1.Text, comboBox1.Text, textBox3.Text), textBox2.Text, requester, int.Parse(textBox3.Text));
                Aggiungi(nuovo);
                MessageBox.Show("Aggiunta eseguita con SUCCESSO");

                this.Visible = false;
            }
        }

        private void NewTask_Load(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            comboBox1.Items.Clear();
            comboBox1.Text = comboBox1.Items[0].ToString();
        }
        public static string CreateId(string RequesterId, string Job, string Hours)
        {
            return ($"{RequesterId}{Job}{Hours}");
        }

        public static void Aggiungi(Prestazione nuovo)
        {
            var Q = new FileStream(@"Tasks.json", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            Q.Close();
            StreamReader sr = new StreamReader(@"Tasks.json");
            StreamWriter sw = new StreamWriter(@"./Tasks2.json");

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

            System.IO.File.Delete(@"Tasks.json");
            System.IO.File.Move(@"./Tasks2.json", @"Tasks.json");
        }

        private void textBox1_MouseLeave(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(textBox1.Text))
            {
                StreamReader sr = new StreamReader(@"Users.json");
                string line = "";
                bool exist = false;
                while (!sr.EndOfStream)
                {
                    line = sr.ReadLine();

                    if (line != null)
                    {
                        Utente temp = JsonConvert.DeserializeObject<Utente>(line);
                        if (temp.Id == textBox1.Text)
                        {
                            exist = true;
                            requester = temp;
                        }
                    }
                }
                sr.Close();

                if (!exist)
                {
                    MessageBox.Show("Il campo 'Id' non corrisponde a nessun utente");
                }
                else
                {
                    comboBox1.Items.Clear();
                    string[] jobs = new string[8] { "Elettricista", "Idraulico", "Giardiniere", "Parrucchiere", "Pulizie", "Insegnante", "Babysitter", "Segreteria" };
                    for (int i = 0; i < 8; i++)
                    {
                        if (jobs[i] != requester.Job)
                            comboBox1.Items.Add(jobs[i]);
                    }
                    comboBox1.Text = comboBox1.Items[0].ToString();
                }
            }
        }
    }
}
