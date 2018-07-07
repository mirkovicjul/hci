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
using RA1522014.UC;

namespace RA1522014
{
    /// <summary>
    /// Interaction logic for PrikaziEtikete.xaml
    /// </summary>
    public partial class PrikaziEtikete : UserControl, INotifyPropertyChanged
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

     
        public static ObservableCollection<Etiketa> Etikete
        {
            get;
            set;
        }

        public static ObservableCollection<Etiketa> EtiketeFilter
        {
            get;
            set;
        }

        public PrikaziEtikete(ObservableCollection<Etiketa> etiketeTab)
        {
            InitializeComponent();
            this.DataContext = this;
            spisakEtiketa.AutoGeneratingColumn += spisak;
            spisakEtiketa.SelectedItem = null;
            Etikete = new ObservableCollection<Etiketa>();
            EtiketeFilter = new ObservableCollection<Etiketa>();
            foreach (Etiketa etiketa in etiketeTab)
            {
                Etikete.Add(etiketa);
                EtiketeFilter.Add(etiketa);
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

            if (e.PropertyName == "Boja")
                e.Column.Header = "Boja";

            if (e.PropertyName == "Opis")
                e.Column.Header = "Opis";
        }


        private void izbrisiEtiketu_Click(object sender, RoutedEventArgs e)
        {

            if (spisakEtiketa.SelectedItem == null)
            {
                MessageBoxResult result = MessageBox.Show("Etiketa nije izabrana!", "", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            MessageBoxResult brisi = MessageBox.Show("Da li ste sigurni da želite obrisati izabranu etiketu?", "", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (brisi == MessageBoxResult.Yes)
            {

                Etiketa selektovanaEtiketa = (Etiketa)spisakEtiketa.SelectedItem;
                MainWindow.obrisiEtiketu(selektovanaEtiketa);

                List<Resurs> resursiEtikete = selektovanaEtiketa.Resursi;
                foreach (Resurs resursEt in resursiEtikete)
                {
                    foreach (Resurs resurs in MainWindow.resursi.Values)
                    {
                        if (resurs.Oznaka.Equals(resursEt.Oznaka))
                        {
                            if (resurs.Etikete.Contains(selektovanaEtiketa))
                            {
                                resurs.Etikete.Remove(selektovanaEtiketa);
                                
                            }

                            if (resurs.Etikete.Count() == 0)
                            {
                                resurs.TabelaEt = "Resurs nema etiketu";
                            }
                            else if (resurs.Etikete.Count == 1)
                            {
                                foreach (Etiketa etiketa in resurs.Etikete)
                                {
                                    resurs.TabelaEt = etiketa.Oznaka;
                                }
                            }
                            else
                            {
                                resurs.TabelaEt = "Resurs ima više etiketa";
                            }
                        }
                        
                    }
                }


                EtiketeFilter.Clear();
                foreach (Etiketa etiketa in MainWindow.etikete.Values)
                {
                    EtiketeFilter.Add(etiketa);
                }
            }
        }


        private void izmijeniEtiketu_Click(object sender, RoutedEventArgs e)
        {

            if (spisakEtiketa.SelectedItem == null)
            {
                MessageBoxResult result = MessageBox.Show("Etiketa nije izabrana!", "", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            Etiketa selektovanaEtiketa = (Etiketa)spisakEtiketa.SelectedItem;

            //IzmijeniEtiketu ie = new IzmijeniEtiketu(selektovanaEtiketa);
            //ie.ShowDialog();


            Etikete.Clear();
            foreach (Etiketa etiketa in MainWindow.etikete.Values)
            {
                Etikete.Add(etiketa);
            }

        }

        private void izmijeniEtiketu_Click1(object sender, RoutedEventArgs e)
        {
            if (spisakEtiketa.SelectedItem == null)
            {
                MessageBoxResult result = MessageBox.Show("Etiketa nije izabrana!", "", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            Etiketa selektovanaEtiketa = (Etiketa)spisakEtiketa.SelectedItem;

            //MainWindow.izmijeniEtiketu(selektovanaEtiketa);
            ww.izmijeniEtiketu(selektovanaEtiketa);

            Etikete.Clear();
            foreach (Etiketa etiketa in MainWindow.etikete.Values)
            {
                Etikete.Add(etiketa);
            }
        }

        private void filter(object sender, RoutedEventArgs e)
        {
            FilterEtikete fe = new FilterEtikete();
            fe.ShowDialog();
        }

        private void resetujFilter_Click(object sender, RoutedEventArgs e)
        {
            EtiketeFilter.Clear();
            foreach (Etiketa etiketa in Etikete)
                EtiketeFilter.Add(etiketa);
        }


    }
}
