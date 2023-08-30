using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BdT_Mosconi
{
    public class Prestazione
    {
        private string _id;
        private string _description;
        private Utente _requester;
        private Utente _worker;
        private int _hours;
        private DateTime _date;
        private bool _complete;

        public string Id
        {
            get
            {
                return _id;
            }
            private set
            {
                if (value != null)
                    _id = value;
                else
                    throw new Exception("Id non valido");
            }
        }
        public string Description
        {
            get
            {
                return _description;
            }
            private set
            {
                if (value != null)
                    _description = value;
                else
                    throw new Exception("Descrizione non valido");
            }
        }
        public Utente Requester
        {
            get
            {
                return _requester;
            }
            private set
            {
                if (value != null)
                    _requester = value;
                else
                    throw new Exception("Beneficiario non valido");
            }
        }
        public Utente Worker
        {
            get
            {
                return _worker;
            }
            private set
            {
                if (value != null)
                    _worker = value;
                else
                    throw new Exception("Lavoratore non valido");
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
                if (value > 0)
                    _hours = value;
                else
                    throw new Exception("Numero di Ore non valido");
            }
        }
        public DateTime Date
        {
            get
            {
                return _date;
            }
            private set
            {
                if (value != null)
                    _date = value;
                else
                    throw new Exception("Data di Esecuzione non valida");
            }
        }
        public bool Complete
        {
            get
            {
                return _complete;
            }
            private set
            {
                if (value != null)
                    _complete = value;
                else
                    throw new Exception("Completamento non valido");
            }
        }
        public Prestazione(string id, string description, Utente requester, int hours)
        {
            Id = id;
            Description = description;
            Requester = requester;
            Hours = hours;
            Complete = false;
        }
        public Prestazione(string id, string description, Utente requester, Utente worker, int hours, DateTime date, bool complete)
        {
            Id = id;
            Description = description;
            Requester = requester;
            Worker = worker;
            Hours = hours;
            Date = date;
            Complete = complete;
        }

        protected Prestazione(Prestazione other) : this(other.Id, other.Description, other.Requester, other.Worker, other.Hours, other.Date, other.Complete)
        {
        }
        public Prestazione Clone()
        {
            return new Prestazione(this);
        }
        public bool Equals(Prestazione p)
        {
            if (p == null) return false;

            if (this == p) return true;

            return (this.Id == p.Id);
        }
        public override string ToString()
        {
            return $"Prestazione: {Id}; {Description}; {Requester}; {Worker}; {Hours}; {Date}; {Complete}";
        }

        public void Completamento(Utente worker, DateTime date)
        {
            Worker = worker;
            Date = date;
            Complete = true;
        }
    }
}
