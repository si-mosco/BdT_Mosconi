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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace BdT_Mosconi
{
    public partial class Form1 : Form
    {
        NewUser Nu = new NewUser();
        Login L = new Login();
        Elenco E = new Elenco();
        NewTask Nt = new NewTask();
        public Form1()
        {
            InitializeComponent();
            listView1.Columns.Add("ID", 120);
            listView1.Columns.Add("REQUESTER", 90);
            listView1.Columns.Add("HOURS", 60);
            listView1.Columns.Add("JOB", 75);
            listView1.Columns.Add("DESCRIPTION", 125);
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Reload();

            /*
            IL paese a prescindere è MANTOVA: i quartieri sono 
            Centro Storico
            Cittadella
            Lunetta
            Frassino
            Valdaro
            Virgiliana  


            I lavori possibili sono
            Elettricista
            Idraulico
            Giardiniere
            Parrucchiere
            Pulizie
            Insegnante
            Babysitter
            Segreteria

            string jsonString = JsonConvert.SerializeObject(nuovo, Formatting.Indented);
            Utente temp = JsonConvert.DeserializeObject<Utente>(jsonString);
            */
        }

        private void Reload()
        {
            LoadTask();
            button1.Enabled = L.permission;
            button4.Enabled = L.permission;
            button5.Enabled = L.permission;
        }

        private void button1_Click(object sender, EventArgs e)//aggiunta utente
        {
            Nu.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            L.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Reload();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            E.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Nt.Show();
        }
        public void LoadTask()
        {
            int i = 0;
            List<Prestazione> tmp = new List<Prestazione>();

            listView1.Items.Clear();
            listView1.View = View.Details;
            listView1.FullRowSelect = true;

            StreamReader sr = new StreamReader(@"Tasks.json");

            string line = "";
            while (!sr.EndOfStream)
            {
                line = sr.ReadLine();

                if (line != null)
                {
                    Prestazione temp = JsonConvert.DeserializeObject<Prestazione>(line);
                    tmp.Add(temp);
                    i++;
                }
            }
            sr.Close();

            /*
             * List<Order> objListOrder = new List<Order>();
             * List<Order> SortedList = objListOrder.OrderBy(o=>o.OrderDate).ToList();

            */

            tmp = tmp.OrderByDescending(o => o.Hours).ToList();
            foreach (Prestazione k in tmp)
            {
                string[] items2 = new string[5];
                items2[0] = k.Id;
                items2[1] = k.Requester.Id;
                items2[2] = k.Hours.ToString();
                items2[3] = k.Job;
                items2[4] = k.Description;
                ListViewItem item = new ListViewItem(items2);
                listView1.Items.Add(item);
            }

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                string id = listView1.SelectedItems[0].SubItems[0].Text;

                var Q = new FileStream(@"Tasks.json", FileMode.OpenOrCreate, FileAccess.ReadWrite);
                Q.Close();
                StreamReader sr = new StreamReader(@"Tasks.json");
                StreamWriter sw = new StreamWriter(@"./Tasks2.json");

                string line = "";
                int i = 0;

                while (!sr.EndOfStream)
                {
                    line = sr.ReadLine();

                    if (line != null)
                    {
                        Utente temp = JsonConvert.DeserializeObject<Utente>(line);
                        if (temp.Id != id)
                        {
                            sw.WriteLine(line);
                        }
                    }
                }
                sr.Close();
                sw.Close();

                System.IO.File.Delete(@"Tasks.json");
                System.IO.File.Move(@"./Tasks2.json", @"Tasks.json");
            }
            LoadTask();
        }
    }
}
