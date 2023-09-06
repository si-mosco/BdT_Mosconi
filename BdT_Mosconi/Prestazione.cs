using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BdT_Mosconi
{
    public class Prestazione: IEquatable<Prestazione>
    {
        private string _id;
        private string _description;
        private Utente _requester;
        private Utente _worker;
        private int _hours;
        private string _job;
        private DateTime _date;
        private bool _complete;

        [JsonProperty]
        public string Id
        {
            get
            {
                return _id;
            }
            private set
            {
                if (!String.IsNullOrWhiteSpace(value))
                    _id = value;
                else
                    throw new Exception("Id non valido");
            }
        }
        [JsonProperty]
        public string Description
        {
            get
            {
                return _description;
            }
            private set
            {
                if (!String.IsNullOrWhiteSpace(value))
                    _description = value;
                else
                    throw new Exception("Descrizione non valido");
            }
        }
        [JsonProperty]
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
        [JsonProperty]
        public Utente Worker
        {
            get
            {
                return _worker;
            }
            private set
            {
                //if (value != null)
                    _worker = value;
                /*else
                    throw new Exception("Lavoratore non valido");
                    ho tolto il controllo a causa del deserialize della libreria
                */
            }
        }
        [JsonProperty]
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
                    throw new Exception("Job non valido");
            }
        }
        [JsonProperty]
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
        [JsonProperty]
        public bool Complete
        {
            get
            {
                return _complete;
            }
            private set
            {
                if (value == true || value==false)
                    _complete = value;
                else
                    throw new Exception("Completamento non valido");
            }
        }
        public Prestazione(string id, string job, string description, Utente requester, int hours)
        {
            Id = id;
            Description = description;
            Job = job;
            Requester = requester;
            Hours = hours;
            Complete = false;
        }
        public Prestazione(string id, string job, string description, Utente requester, Utente worker, int hours, DateTime date, bool complete)
        {
            Id = id;
            Job = job;
            Description = description;
            Requester = requester;
            Worker = worker;
            Hours = hours;
            Date = date;
            Complete = complete;
        }
        public Prestazione()
        {

        }

        protected Prestazione(Prestazione other) : this(other.Id, other.Job, other.Description, other.Requester, other.Worker, other.Hours, other.Date, other.Complete)
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
            return $"Prestazione: {Id}; {Job}; {Description}; {Requester}; {Worker}; {Hours}; {Date}; {Complete}";
        }
    }
}
