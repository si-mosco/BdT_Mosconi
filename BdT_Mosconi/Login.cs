using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;

namespace BdT_Mosconi
{
    public partial class Login : Form
    {
        public bool permission = false;
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool done=true;
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

            if (done)
            {
                bool exist = false;
                StreamReader sr = new StreamReader(@"Users.json");

                string line = "";
                while (!sr.EndOfStream)
                {
                    line = sr.ReadLine();

                    if (line != null)
                    {
                        Utente temp = JsonConvert.DeserializeObject<Utente>(line);
                        if (temp.Name == textBox1.Text && temp.Surname == textBox2.Text)
                        {
                            exist = true;
                            if (temp.Job == "Segreteria")
                                permission = true;
                        }
                    }
                }
                sr.Close();

                if (!exist)
                    MessageBox.Show("Utente Inesistente");
                if (!permission && exist)
                    MessageBox.Show("L'utente non ha i permessi");
                if (permission && exist)
                {
                    MessageBox.Show("Login effettuato");
                    this.Close();
                }
            }
        }

        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Visible = false;
        }
    }
}
