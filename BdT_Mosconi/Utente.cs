using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BdT_Mosconi
{
    public class Utente
    {
        public string _id;
        public string _name;
        public string _surname;
        public DateTime _birth;
        public int _telephone;
        public string _area;
        public int _hours;

        public string Id
        {
            get
            {
                return _id;
            }
            private set
            {
                if(value != null)
                    _id = value;
                else
                    throw new Exception("Id non valido");
            }
        }
        public string Name
        {
            get
            {
                return _name;
            }
            private set
            {
                if (value != null)
                    _name = value;
                else
                    throw new Exception("Nome non valido");
            }
        }
        public string Surname
        {
            get
            {
                return _surname;
            }
            private set
            {
                if (value != null)
                    _surname = value;
                else
                    throw new Exception("Cognome non valido");
            }
        }
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
        public int Telephone
        {
            get
            {
                return _telephone;
            }
            private set
            {
                if (value != null && value.ToString().Length==10)
                    _telephone = value;
                else
                    throw new Exception("Numero di Telefono non valido");
            }
        }
        public string Area
        {
            get
            {
                return _area;
            }
            private set
            {
                if (value != null && (value == "Centro Storico" || value == "Cittadella" || value == "Lunetta" || value == "Frassino" || value == "Valdaro" || value == "Virgiliana"))
                    _area = value;
                else
                    throw new Exception("Area non valida");
            }
        }
        public int Hours
        {
            get
            {
                return _hours;
            }
            private set
            {
                if (value != null)
                    _hours = value;
                else
                    throw new Exception("Numero di Ore non valido");
            }
        }

        public Utente(string name, string surname, DateTime birth, int telephone, string area)
        {
            Id = CalcolaId(name, surname);
            Name = name;
            Surname = surname;
            Birth = birth;
            Telephone = telephone;
            Area = area;
            Hours = 0;
        }
        public Utente(string id, string name, string surname, DateTime birth, int telephone, string area, int hours)
        {
            Id = id;
            Name = name;
            Surname = surname;
            Birth = birth;
            Telephone = telephone;
            Area = area;
            Hours = hours;
        }


        protected Utente(Utente other) : this(other.Id, other.Name, other.Surname, other.Birth, other.Telephone, other.Area, other.Hours)
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
            return $"Utente: {Id}; {Name}; {Surname}; {Birth.ToString("d")}; {Telephone}; {Area}; {Hours}";
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
