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
    /// Interaction logic for PrikaziTipove.xaml
    /// </summary>
    public partial class PrikaziTipove : UserControl, INotifyPropertyChanged
    {
        MainWindow ww = Application.Current.MainWindow as MainWindow;
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
       

        public static ObservableCollection<Tip> Tipovi
        {
            get;
            set;
        }

        public static ObservableCollection<Tip> TipoviFilter
        {
            get;
            set;
        }
        
       

        public PrikaziTipove(ObservableCollection<Tip> tipoviTab)
        {
            InitializeComponent();

            this.DataContext = this;
            spisakTipova.AutoGeneratingColumn += spisak;
            spisakTipova.SelectedItem = null;
            TipoviFilter = new ObservableCollection<Tip>();
            Tipovi = new ObservableCollection<Tip>();
            foreach (Tip tip in tipoviTab)
            {
                Tipovi.Add(tip);
                TipoviFilter.Add(tip);
            }
            if (MainWindow.tutorialMode1)
            {
                filterTB.Visibility = Visibility.Visible;
                filterTut.Visibility = Visibility.Visible;
            }
        }


        void spisak(object sender, System.Windows.Controls.DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName == "Guid")
                e.Cancel = true;

            if (e.PropertyName == "Resursi")
                e.Cancel = true;

            if (e.PropertyName == "Oznaka")
                e.Column.Header = "Oznaka";

            if (e.PropertyName == "Naziv")
                e.Column.Header = "Naziv";

            if (e.PropertyName == "Opis")
                e.Column.Header = "Opis";

            if (e.PropertyName == "Ikonica")
                e.Cancel = true;
        }


        private void izbrisiTip_Click(object sender, RoutedEventArgs e)
        {

            if (spisakTipova.SelectedItem == null)
            {
                MessageBoxResult result = MessageBox.Show("Resurs nije izabran", "", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            MessageBoxResult brisi = MessageBox.Show("Da li ste sigurni da želite obrisati izabrani tip?", "", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (brisi == MessageBoxResult.Yes)
            {
                Tip selektovaniTip = (Tip)spisakTipova.SelectedItem;
                List<Resurs> resursiTipa = selektovaniTip.Resursi;
                if (resursiTipa.Count > 0)
                {
                    MessageBoxResult brisiResurs = MessageBox.Show("Brisanjem ovog tipa, biće izbrisani i svi resursi koji pripadaju ovom tipu. Da li ste sigurni da želite da nastavite?","", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (brisiResurs == MessageBoxResult.Yes)
                    {
                        foreach (Resurs resurs in resursiTipa)
                        {
                            MainWindow.obrisiResurs(resurs);
                        }
                    } 
                }


                MainWindow.obrisiTip(selektovaniTip);

                TipoviFilter.Clear();
                foreach (Tip tip in MainWindow.tipovi.Values)
                {
                    TipoviFilter.Add(tip);
                }
    
            }
        }

        private void izmijeniTip_Click(object sender, RoutedEventArgs e)
        {
            if (spisakTipova.SelectedItem == null)
            {
                MessageBoxResult result = MessageBox.Show("Tip nije izabran!", "", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            Tip selektovanTip = (Tip)spisakTipova.SelectedItem;


            //IzmijeniTip it = new IzmijeniTip(selektovanTip);
            //it.ShowDialog();
            ww.izmijeniTip(selektovanTip);

            Tipovi.Clear();
            foreach (Tip tip in MainWindow.tipovi.Values)
            {
                Tipovi.Add(tip);
            }

        }

        private void filter(object sender, RoutedEventArgs e)
        {
            if (MainWindow.tutorialMode1)
            {
                filterTB.Visibility = Visibility.Hidden;
                filterTut.Visibility = Visibility.Hidden;
                ww.krajTB.Visibility = Visibility.Hidden;
                ww.krajTut.Visibility = Visibility.Hidden;

            }
            FilterTipa ft = new FilterTipa();
            ft.ShowDialog();
          
        }

        private void resetujFilter_Click(object sender, RoutedEventArgs e)
        {
            TipoviFilter.Clear();
            foreach(Tip tip in Tipovi)
            {
                TipoviFilter.Add(tip);
            }
        }

    }
}
