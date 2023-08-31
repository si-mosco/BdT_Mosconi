using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BdT_Mosconi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
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

        private void button1_Click(object sender, EventArgs e)//aggiunta utente
        {
            NewUser NU = new NewUser();
            NU.Show();
        }
    }
}
