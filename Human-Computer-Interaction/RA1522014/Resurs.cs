using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows;
namespace RA1522014
{
    [Serializable]
    public class Resurs 
    {
       
        private Guid _guid;
        private string _oznaka;
        private string _naziv;
        private Tip _tip;
        private string _tipOznaka;
        private List<Etiketa> _etikete;
        private string _tabelaEt;
        private bool _obnovljiv;
        private bool _eksploatacija;
        private bool _vaznost;
        private string _ikonica;
        private string _datum;
        private string _mjera;
        private string _pojavljivanje;
        private double _cijena;
        private string _opis;
        private bool _imaSvojuIkonicu;
        public Point Lokacija { get; set; }
        
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

        public Tip Tip
        {
            get { return _tip; }
            set { _tip = value; }
        }

        public string TipOznaka 
        {
            get { return _tipOznaka; }
            set { _tipOznaka = value; }
        }

        public List<Etiketa> Etikete
        {
            get { return _etikete; }
            set { _etikete = value; }
        }

        public string TabelaEt
        {
            get { return _tabelaEt; }
            set { _tabelaEt = value; }
        }
        
        public bool Obnovljiv
        {
            get { return _obnovljiv; }
            set { _obnovljiv = value; }
        }

        public bool Eksploatacija
         {
            get { return _eksploatacija; }
            set { _eksploatacija = value; }
        }

        public bool Vaznost
        {
            get { return _vaznost; }
            set { _vaznost = value; }
        }

        public string Ikonica
        {
            get { return _ikonica; }
            set { _ikonica = value; }
        }

        public string Datum
        {
            get { return _datum; }
            set { _datum = value; }
        }

        public string Mjera
        {
            get { return _mjera; }
            set { _mjera = value; }
        }

        public string Pojavljivanje
        {
            get { return _pojavljivanje; }
            set { _pojavljivanje = value; }
        }

        public double Cijena
        {
            get { return _cijena; }
            set { _cijena = value; }
        }

        public string Opis
        {
            get { return _opis; }
            set { _opis = value; }
        }

        public bool ImaSvojuIkonicu
        {
            get { return _imaSvojuIkonicu; }
            set { _imaSvojuIkonicu = value; }
        }

        public Resurs(string naziv, string oznaka, List<Etiketa> etikete, string tip, string ikonica, string datum, bool obnovljiv, bool eksploatacija, bool vaznost, string mjera, string pojavljivanje, double cijena, string opis, bool imaSvojuIkonicu)
        {
            Guid = Guid.NewGuid();
            this.Naziv = naziv;
            this.Oznaka = oznaka;
            this.Etikete = etikete;
            this.Ikonica = ikonica;
            this.Datum = datum;
            this.Obnovljiv = obnovljiv;
            this.Eksploatacija = eksploatacija;
            this.Vaznost = vaznost;
            this.Mjera = mjera;
            this.Pojavljivanje = pojavljivanje;
            this.Cijena = cijena;
            this.Opis = opis;
            this.TipOznaka = tip;
            this.ImaSvojuIkonicu = imaSvojuIkonicu;
            Lokacija = new Point();

            if (Etikete.Count() == 0)
            {
                _tabelaEt = "Resurs nema etiketu";
            }
            else if(Etikete.Count == 1)
            {
                foreach (Etiketa nalep in Etikete)
                {
                    _tabelaEt = nalep.Oznaka;
                }
            }
            else
            {
               _tabelaEt= "Resurs ima više etiketa";
            }
            

            foreach (Tip postojeciTip in MainWindow.tipovi.Values)
            {
                if (postojeciTip.Oznaka.Equals(tip))
                {
                    this.Tip = postojeciTip;
                    Tip.Resursi.Add(this);
                }
            }

            foreach (Etiketa etiketa in Etikete)
            {
                etiketa.Resursi.Add(this);
            }
            
            
        }



    }
}
