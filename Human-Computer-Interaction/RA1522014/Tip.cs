using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RA1522014
{
    [Serializable]
    public class Tip
    {

        private Guid _guid;
        private string _oznaka;
        private string _naziv;
        private string _ikonica;
        private string _opis;
        private List<Resurs> _resursi;

        public Guid Guid
        {
            get { return _guid; }
            set { _guid = value; }
        }

        public string Oznaka
        {
            get { return _oznaka; }
            set { _oznaka = value; }
        }

        public string Naziv
        {
            get { return _naziv; }
            set { _naziv = value; }
        }

        public string Ikonica
        {
            get { return _ikonica; }
            set { _ikonica = value; }
        }

        public string Opis
        {
            get { return _opis; }
            set { _opis = value; }
        }

        public List<Resurs> Resursi
        {
            get { return _resursi; }
            set { _resursi = value; }
        }

        public Tip(string oznaka, string naziv, string ikonica, string opis)
        {
            Guid = Guid.NewGuid();
            Oznaka = oznaka;
            Naziv = naziv;
            Ikonica = ikonica;
            Opis = opis;
            Resursi = new List<Resurs>();
        }

    }
}
