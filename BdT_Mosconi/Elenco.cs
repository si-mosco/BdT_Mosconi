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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;
using System.Text.RegularExpressions;
using System.Collections;

namespace BdT_Mosconi
{
    public partial class Elenco : Form
    {
        public Elenco()
        {
            InitializeComponent();
            listView1.Columns.Add("ID", 75);
            listView1.Columns.Add("NAME", 75);
            listView1.Columns.Add("SURNAME", 75);
            listView1.Columns.Add("BIRTH", 80);
            listView1.Columns.Add("TELEPHONE", 90);
            listView1.Columns.Add("AREA", 100);
            listView1.Columns.Add("JOB", 80);
            listView1.Columns.Add("HOURS", 60);
        }

        private void Elenco_Load(object sender, EventArgs e)
        {
            LoadUser();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            LoadUser();
        }
        private void LoadUser()
        {
            listView1.Items.Clear();
            listView1.View = View.Details;
            listView1.FullRowSelect = true;

            StreamReader sr = new StreamReader(@"Users.json");

            string line = "";
            while (!sr.EndOfStream)
            {
                line = sr.ReadLine();

                if (line != null)
                {
                    Utente temp = JsonConvert.DeserializeObject<Utente>(line);

                    string[] items2 = new string[8];
                    if (checkBox1.Checked && temp.Hours < 0)
                    {
                        items2[0] = temp.Id;
                        items2[1] = temp.Name;
                        items2[2] = temp.Surname;
                        items2[3] = temp.Birth.ToString("d");
                        items2[4] = Convert.ToString(temp.Telephone);
                        items2[5] = temp.Area;
                        items2[6] = temp.Job;
                        items2[7] = Convert.ToString(temp.Hours);
                        ListViewItem item = new ListViewItem(items2);
                        listView1.Items.Add(item);
                    }
                    
                    if (!checkBox1.Checked)
                    {
                        items2[0] = temp.Id;
                        items2[1] = temp.Name;
                        items2[2] = temp.Surname;
                        items2[3] = temp.Birth.ToString("d");
                        items2[4] = Convert.ToString(temp.Telephone);
                        items2[5] = temp.Area;
                        items2[6] = temp.Job;
                        items2[7] = Convert.ToString(temp.Hours);
                        ListViewItem item = new ListViewItem(items2);
                        listView1.Items.Add(item);
                    }
                }
            }
            sr.Close();
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                string id = listView1.SelectedItems[0].SubItems[0].Text;

                var Q = new FileStream(@"Users.json", FileMode.OpenOrCreate, FileAccess.ReadWrite);
                Q.Close();
                StreamReader sr = new StreamReader(@"Users.json");
                StreamWriter sw = new StreamWriter(@"./Users2.json");

                string line = "";

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

                System.IO.File.Delete(@"Users.json");
                System.IO.File.Move(@"./Users2.json", @"Users.json");
            }
            LoadUser();
        }

        private void Elenco_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Visible = false;
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
