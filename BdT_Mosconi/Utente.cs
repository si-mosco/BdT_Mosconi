using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BdT_Mosconi
{
    public class Utente : IEquatable<Utente>
    {
        private string _id;
        private string _name;
        private string _surname;
        private DateTime _birth;
        private double _telephone;
        private string _area;
        private string _job;
        private int _hours;

        [JsonProperty]
        public string Id
        {
            get
            {
                return _id;
            }
            private set
            {
                if(!String.IsNullOrWhiteSpace(value))
                    _id = value;
                else
                    throw new Exception("Id non valido");
            }
        }
        [JsonProperty]
        public string Name
        {
            get
            {
                return _name;
            }
            private set
            {
                if (!String.IsNullOrWhiteSpace(value))
                    _name = value;
                else
                    throw new Exception("Nome non valido");
            }
        }
        [JsonProperty]
        public string Surname
        {
            get
            {
                return _surname;
            }
            private set
            {
                if (!String.IsNullOrWhiteSpace(value))
                    _surname = value;
                else
                    throw new Exception("Cognome non valido");
            }
        }
        [JsonProperty]
        public DateTime Birth
        {
            get
            {
                return _birth;
            }
            private set
            {
                if (value != null && value < DateTime.Now)
                    _birth = value;
                else
                    throw new Exception("Data di Nascita non valida");
            }
        }
        [JsonProperty]
        public double Telephone
        {
            get
            {
                return _telephone;
            }
            private set
            {
                if (!String.IsNullOrWhiteSpace(value.ToString()) && value.ToString().Length==10)
                    _telephone = value;
                else
                    throw new Exception("Numero di Telefono non valido");
            }
        }
        [JsonProperty]
        public string Area
        {
            get
            {
                return _area;
            }
            private set
            {
                if (!String.IsNullOrWhiteSpace(value) && (value == "Centro Storico" || value == "Cittadella" || value == "Lunetta" || value == "Frassino" || value == "Valdaro" || value == "Virgiliana"))
                    _area = value;
                else
                    throw new Exception("Area non valida");
            }
        }
        [JsonProperty]
        public string Job
        {
            get
            {
                return _job;
            }
            private set
            {
                if (!String.IsNullOrWhiteSpace(value))
                    _job = value;
                else
                    throw new Exception("Lavoro non valido");
            }
        }
        [JsonProperty]
        public int Hours
        {
            get
            {
                return _hours;
            }
            set
            {
                if (value >=0)
                    _hours = value;
                else
                    throw new Exception("Numero di Ore non valido");
            }
        }

        public Utente(string name, string surname, DateTime birth, double telephone, string area, string job)
        {
            Id = CalcolaId(name, surname);
            Name = name;
            Surname = surname;
            Birth = birth;
            Telephone = telephone;
            Area = area;
            Job = job;
            Hours = 0;
        }
        public Utente(string id, string name, string surname, DateTime birth, double telephone, string area, string job, int hours)
        {
            Id = id;
            Name = name;
            Surname = surname;
            Birth = birth;
            Telephone = telephone;
            Area = area;
            Job = job;
            Hours = hours;
        }
        public Utente()
        {

        }

        protected Utente(Utente other) : this(other.Id, other.Name, other.Surname, other.Birth, other.Telephone, other.Area, other.Job, other.Hours)
        {
        }
        public Utente Clone()
        {
            return new Utente(this);
        }
        public bool Equals(Utente u)
        {
            if (u == null) return false;

            if (this == u) return true;

            return (this.Id == u.Id);
        }
        public override string ToString()
        {
            return $"Utente: {Id}; {Name}; {Surname}; {Birth.ToString("d")}; {Telephone}; {Area}; {Job}; {Hours}";
        }

        private string CalcolaId(string name, string surname)
        {
            string id = "";
            if (name.Length >= 3)
                id = id + name.Substring(0, 3);
            else
                id = id + name;

            if (surname.Length >= 3)
                id = id + surname.Substring(0, 3);
            else
                id = id + surname;

            return id;
        }
    }
}
