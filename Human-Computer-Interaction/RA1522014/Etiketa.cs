using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RA1522014
{
    [Serializable]
    public class Etiketa
    {
        
        private Guid _guid;
        private string _oznaka;
        private string _boja;
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

        public string Boja
        {
            get { return _boja; }
            set { _boja = value; }
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

        public Etiketa(string oznaka, string boja, string opis)
        {
            Guid = Guid.NewGuid();
            Oznaka = oznaka;
            Boja = boja;
            Opis = opis;
            Resursi = new List<Resurs>();
        }

    }
}
