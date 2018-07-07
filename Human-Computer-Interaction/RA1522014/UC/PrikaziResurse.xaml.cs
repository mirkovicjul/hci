using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace RA1522014
{
    /// <summary>
    /// Interaction logic for PrikaziResurse.xaml
    /// </summary>
    public partial class PrikaziResurse : UserControl, INotifyPropertyChanged
    {
        MainWindow ww = Application.Current.MainWindow as MainWindow;
        public event PropertyChangedEventHandler PropertyChanged;
        public static MainWindow glavni;
        protected void OnPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
        

        public static ObservableCollection<Resurs> Resursi
        {
            get;
            set;
        }

        public static ObservableCollection<Resurs> ResursiFilter
        {
            get;
            set;
        }

        public PrikaziResurse(ObservableCollection<Resurs> resursiTab)
        {
            InitializeComponent();

            this.DataContext = this;
            spisakResursa.AutoGeneratingColumn += spisak;
            spisakResursa.SelectedItem = null;
            Resursi = new ObservableCollection<Resurs>();
            ResursiFilter = new ObservableCollection<Resurs>();
            foreach (Resurs resurs in resursiTab)
            {
                Resursi.Add(resurs);
                ResursiFilter.Add(resurs);
            }
        }


        void spisak(object sender, System.Windows.Controls.DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName == "Guid")
                e.Cancel = true;

            if (e.PropertyName == "Etikete")
                e.Cancel = true;

            if (e.PropertyName == "TabelaEt")
                e.Column.Header = "Etikete";

            if (e.PropertyName == "Tip")
                e.Cancel = true;

            if (e.PropertyName == "TipOznaka")
                e.Column.Header = "Tip";

            if (e.PropertyName == "Ikonica")
                e.Cancel = true;

            if (e.PropertyName == "Oznaka")
                e.Column.Header = "Oznaka";

            if (e.PropertyName == "Opis")
                e.Column.Header = "Opis";

            if (e.PropertyName == "Ime")
                e.Column.Header = "Ime";

            if (e.PropertyName == "Datum")
                e.Column.Header = "Datum otkrivanja";

            if (e.PropertyName == "Eksploatacija")
                e.Column.Header = "Moguća eksploatacija?";

            if (e.PropertyName == "Vaznost")
                e.Column.Header = "Od strateške važnosti?";

            if (e.PropertyName == "Obnovljiv")
                e.Column.Header = "Obnovljiv?";

            if (e.PropertyName == "Mjera")
                e.Column.Header = "Mjera";

            if (e.PropertyName == "Pojavljivanje")
                e.Column.Header = "Pojavljivanje";

            if (e.PropertyName == "Cijena")
                e.Column.Header = "Cijena";

            if (e.PropertyName == "ImaSvojuIkonicu")
                e.Cancel = true;

            if (e.PropertyName == "Lokacija")
                e.Cancel = true;
        }


        private void obrisiResurs_Click(object sender, RoutedEventArgs e)
        {

            if (spisakResursa.SelectedItem == null)
            {
                    MessageBoxResult result = MessageBox.Show("Resurs nije izabran", "", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
            }

            MessageBoxResult brisi = MessageBox.Show("Da li ste sigurni da želite obrisati izabrani resurs?", "", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (brisi == MessageBoxResult.Yes)
            {

                Resurs selektovaniResurs = (Resurs)spisakResursa.SelectedItem;
                List<Etiketa> etiketeRes = selektovaniResurs.Etikete;
                foreach (Etiketa etiketaRes in etiketeRes)
                {
                    foreach (Etiketa etiketa in MainWindow.etikete.Values)
                    {
                        if (etiketa.Oznaka.Equals(etiketaRes.Oznaka))
                        {
                            etiketa.Resursi.Remove(selektovaniResurs);
                        }
                    }
                }

                Tip tipRes = selektovaniResurs.Tip;
                foreach (Tip tip in MainWindow.tipovi.Values)
                {
                    if(tip.Oznaka.Equals(tipRes.Oznaka))
                        tip.Resursi.Remove(selektovaniResurs);
                }

                MainWindow.obrisiResurs(selektovaniResurs);
          
                ResursiFilter.Clear();
                foreach (Resurs resurs in MainWindow.resursi.Values)
                {
                    ResursiFilter.Add(resurs);
                }
            }
        }

        private void izmijeniResurs_Click(object sender, RoutedEventArgs e)
        {
            if (spisakResursa.SelectedItem == null)
            {
                MessageBoxResult result = MessageBox.Show("Resurs nije izabran!", "", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            Resurs selektovaniResurs = (Resurs)spisakResursa.SelectedItem;

            //IzmijeniResurs ie = new IzmijeniResurs(selektovaniResurs);
            //ie.ShowDialog();
            ww.izmijeniResurs(selektovaniResurs);

            Resursi.Clear();
            foreach (Resurs res in MainWindow.resursi.Values)
            {
                Resursi.Add(res);
            }
        }

        private void filter(object sender, RoutedEventArgs e)
        {
            FilterResursa fr = new FilterResursa();
            fr.ShowDialog();
        }

        private void resetujFilter_Click(object sender, RoutedEventArgs e)
        {
            ResursiFilter.Clear();
            foreach (Resurs resurs in Resursi)
                ResursiFilter.Add(resurs);
        }
    }
}
