using Newtonsoft.Json;
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

namespace BdT_Mosconi
{
    public partial class Completamento : Form
    {
        public string id;
        Prestazione request;
        Utente worker;
        Utente requester;
        public Completamento()
        {
            InitializeComponent();
        }

        private void Completamento_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Visible = false;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Il campo 'Name' è errato");
                return;
            }
            if (String.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Il campo 'Surname' è errato");
                return;
            }


            Find(true);

            if (request.Requester.Name == textBox1.Text && request.Requester.Name == textBox2.Text)
            {
                MessageBox.Show("L'utente inserito è uguale al richiedente");
                return;
            }
            else
            {

                bool exist = false;
                var Q1 = new FileStream(@"Users.json", FileMode.OpenOrCreate, FileAccess.ReadWrite);
                Q1.Close();

                StreamReader srU = new StreamReader(@"Users.json");
                StreamWriter swU = new StreamWriter(@"./Users2.json");

                string line = "";

                while (!srU.EndOfStream)
                {
                    line = srU.ReadLine();

                    if (line != null)
                    {
                        Utente temp = JsonConvert.DeserializeObject<Utente>(line);
                        if (temp.Name == textBox1.Text && temp.Surname == textBox2.Text)//worker
                        {
                            worker = temp;
                            exist = true;
                        }
                        else if (temp.Id == request.Requester.Id)//requester
                        {
                            requester = temp;
                        }
                        else
                        {
                            swU.WriteLine(line);
                        }
                    }
                }
                if (!exist)
                {
                    MessageBox.Show("Utente Inesistente");
                }
                if (exist)
                {
                    bool done2 = true;
                    if (worker.Area != request.Requester.Area)
                    {
                        MessageBox.Show("Gli utenti non sono della stessa area");
                        done2 = false;
                    }
                    if (worker.Job != request.Job)
                    {
                        MessageBox.Show("Il lavoratore non è competente per questo lavoro");
                        done2 = false;
                    }
                    if (done2)
                    {
                        worker.Hours += request.Hours;
                        string jsonString = JsonConvert.SerializeObject(worker, Formatting.None);
                        swU.WriteLine(jsonString);

                        requester.Hours -= request.Hours;
                        jsonString = JsonConvert.SerializeObject(requester, Formatting.None);
                        swU.WriteLine(jsonString);
                    }
                    else
                    {
                        string jsonString = JsonConvert.SerializeObject(worker, Formatting.None);
                        swU.WriteLine(jsonString);
                        jsonString = JsonConvert.SerializeObject(requester, Formatting.None);
                        swU.WriteLine(jsonString);
                    }
                }

                srU.Close();
                swU.Close();


                System.IO.File.Delete(@"Users.json");
                System.IO.File.Move(@"./Users2.json", @"Users.json");

                Find(false);
            }

            this.Close();

        }

        private void Find(bool first)
        {
            var Q2 = new FileStream(@"Tasks.json", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            Q2.Close();
            StreamReader srT = new StreamReader(@"Tasks.json");
            StreamWriter swT = new StreamWriter(@"./Tasks2.json");

            string line = "";
            while (!srT.EndOfStream)
            {
                line = srT.ReadLine();

                if (line != null)
                {
                    Prestazione temp = JsonConvert.DeserializeObject<Prestazione>(line);
                    if (temp.Id != id)
                    {
                        swT.WriteLine(line);
                    }
                    else
                    {
                        if (first)
                        {
                            request = temp;
                            string jsonString = JsonConvert.SerializeObject(request, Formatting.None);
                            swT.WriteLine(jsonString);
                        }
                        else
                        {
                            request = new Prestazione(temp.Id, temp.Job, temp.Description, temp.Requester, worker, temp.Hours, DateTime.Now, true);
                            string jsonString = JsonConvert.SerializeObject(request, Formatting.None);
                            swT.WriteLine(jsonString);
                        }
                    }
                }
            }

            srT.Close();
            swT.Close();
            System.IO.File.Delete(@"Tasks.json");
            System.IO.File.Move(@"./Tasks2.json", @"Tasks.json");

        }
    }
}
