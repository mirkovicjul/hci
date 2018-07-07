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

namespace RA1522014
{
    /// <summary>
    /// Interaction logic for NoviResursUserControl.xaml
    /// </summary>
    public partial class NoviResursUserControl : UserControl
    {
        private string ikonicaResursaPath = "";
        private string tipOznaka = "";

        public NoviResursUserControl()
        {
            InitializeComponent();
        }

        
        private void dodajResurs_Click(object sender, RoutedEventArgs e)
        {
            
            string mjera = "";
            string pojavljivanje = "";
            string ikonica = "";
            bool obnovljiv, eksploatacija, vaznost;
            double cijena;
            bool imaIkonicu = false;
            List<Etiketa> etiketeRes = new List<Etiketa>();


            if (oznakaResursa.Text == null || oznakaResursa.Text == "")
            {
                MessageBoxResult result = MessageBox.Show("Nisu popunjena sva polja!", "", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                oznakaResursa.Focus();
                return;
            }


            if (nazivResursa.Text == null || nazivResursa.Text == "")
            {
                MessageBoxResult result = MessageBox.Show("Nisu popunjena sva polja!", "", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                nazivResursa.Focus();
                return;
            }

         
            if (tipResursa.SelectedItem == null)
            {
                MessageBoxResult result = MessageBox.Show("Nisu popunjena sva polja!", "", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                tipResursa.Focus();
                return;
            }
            else
            {
                tipOznaka = tipResursa.SelectedItem.ToString();  
            }


            if (obnovljivTrue.IsChecked == true)
            {
                obnovljiv = true;
            }
            else if (obnovljivFalse.IsChecked == true)
            {
                obnovljiv = false;
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Nisu popunjena sva polja!", "", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            

            if (eksploatacijaTrue.IsChecked == true)
            {
                eksploatacija = true;
            }
            else if (eksploatacijaFalse.IsChecked == true)
            {
                eksploatacija = false;
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Nisu popunjena sva polja!", "", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }


            if (vaznostTrue.IsChecked == true)
            {
                vaznost = true;
            }
            else if (vaznostFalse.IsChecked == true)
            {
                vaznost = false;
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Nisu popunjena sva polja!", "", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }


            if (mjeraCombo.SelectedItem == null)
            {
                MessageBoxResult result = MessageBox.Show("Nisu popunjena sva polja!", "", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                mjeraCombo.Focus();
                return;
            }
            else
            {
                mjera = ((ComboBoxItem)mjeraCombo.SelectedItem).Content.ToString();
            }


            if (pojavljivanjeCombo.SelectedItem == null)
            {
                MessageBoxResult result = MessageBox.Show("Nisu popunjena sva polja!", "", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                pojavljivanjeCombo.Focus();
                return;
            }
            else
            {
                pojavljivanje = ((ComboBoxItem)pojavljivanjeCombo.SelectedItem).Content.ToString();
            }


            if (cijenaResursa.Text == null || cijenaResursa.Text == "")
            {
                MessageBoxResult result = MessageBox.Show("Nisu popunjena sva polja!", "", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                cijenaResursa.Focus();
                return;
            }
            else    
            {
                cijena = Convert.ToDouble(cijenaResursa.Text);
            }


            if (datum.Text == null || datum.Text == "")
            {
                MessageBoxResult result = MessageBox.Show("Nisu popunjena sva polja!", "", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                datum.Focus();
                return;
            }


            if (ikonicaResursaPath.Equals(""))
            { 
                    foreach (Tip tip in MainWindow.tipovi.Values)
                    {
                        if (tip.Oznaka.Equals(tipOznaka))
                        {
                            ikonica = tip.Ikonica;
                        }
                    }
            }
            else
            {
                ikonica = ikonicaResursaPath;
                imaIkonicu = true;
            }

            foreach (Resurs postojeciResurs in MainWindow.resursi.Values)
            {
                if (postojeciResurs.Oznaka.Equals(oznakaResursa.Text))
                {
                    MessageBoxResult result = MessageBox.Show("Resurs sa istom oznakom već postoji! Unesite drugu oznaku!", "", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    oznakaResursa.Focus();
                    return;
                }
            }


            foreach (string selektovanaEtiketa in etiketaResursa.SelectedItems)
            {
                foreach (Etiketa etiketa in MainWindow.etikete.Values)
                {
                    if (etiketa.Oznaka.Equals(selektovanaEtiketa))
                    {
                        etiketeRes.Add(etiketa);
                    }
                }
            }


            Resurs noviResurs = new Resurs(nazivResursa.Text, oznakaResursa.Text, etiketeRes, tipOznaka, ikonica, datum.Text, obnovljiv, eksploatacija, vaznost, mjera, pojavljivanje, cijena, opis.Text, imaIkonicu);
            MainWindow.resursi.Add(noviResurs.Guid, noviResurs);
            
            MessageBoxResult sacuvano = MessageBox.Show("Resurs sačuvan!", "", MessageBoxButton.OK, MessageBoxImage.Information);

            //resurs sacuvan, sva polja restartovana
            oznakaResursa.Text = "";
            nazivResursa.Text = "";
            datum.Text = null;
            cijenaResursa.Text = "";
            opis.Text = "";
            tipResursa.SelectedItem = null;
            etiketaResursa.SelectedItem = null;
            vaznostTrue.IsChecked = false;
            vaznostFalse.IsChecked = false;
            eksploatacijaTrue.IsChecked = false;
            eksploatacijaFalse.IsChecked = false;
            obnovljivTrue.IsChecked = false;
            obnovljivFalse.IsChecked = false;
            pojavljivanjeCombo.SelectedItem = null;
            mjeraCombo.SelectedItem = null;
            ikonicaName.Source = null;
            ikonicaResursaPath = "";
        }


        private void ucitajIkonicuTipa_Click(object sender, SelectionChangedEventArgs e)
        {
            if (ikonicaResursaPath.Equals(""))
            {
                foreach (Tip tip in MainWindow.tipovi.Values)
                {
                    if (tip.Oznaka.Equals(tipResursa.SelectedItem))
                    {
                        ikonicaName.Source = new BitmapImage(new Uri(tip.Ikonica, UriKind.Absolute));

                    }
                }
            }
         }


        private void traziIkonicuClick(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog 
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".png";
            dlg.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png|JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";
            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 

                ikonicaResursaPath = dlg.FileName;
                ikonicaName.Source = new BitmapImage(new Uri(ikonicaResursaPath, UriKind.Absolute));

            }
        }


        private void CijenaTextBox_TextInput(object sender, TextCompositionEventArgs e)
        {
            if (e.Text != "." && IsNumber(e.Text) == false)
            {
                e.Handled = true;
            }
            else if (e.Text == ".")
            {
                if (((TextBox)sender).Text.IndexOf(e.Text) > -1)
                {
                    e.Handled = true;
                }
            }
        }

        private bool IsNumber(string Text)
        {
            int output;
            return int.TryParse(Text, out output);
        }

        private void ponistiResurs_Click(object sender, RoutedEventArgs e)
        {
            oznakaResursa.Text = "";
            nazivResursa.Text = "";
            datum.Text = null;
            cijenaResursa.Text = "";
            opis.Text = "";
            tipResursa.SelectedItem = null;
            etiketaResursa.SelectedItem = null;
            vaznostTrue.IsChecked = false;
            vaznostFalse.IsChecked = false;
            eksploatacijaTrue.IsChecked = false;
            eksploatacijaFalse.IsChecked = false;
            obnovljivTrue.IsChecked = false;
            obnovljivFalse.IsChecked = false;
            pojavljivanjeCombo.SelectedItem = null;
            mjeraCombo.SelectedItem = null;
            ikonicaName.Source = null;
            ikonicaResursaPath = "";
        }
       


    }
}
