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

namespace RA1522014.UC
{
    /// <summary>
    /// Interaction logic for IzmijeniResursUserControl.xaml
    /// </summary>
    public partial class IzmijeniResursUserControl : UserControl
    {
        MainWindow ww = Application.Current.MainWindow as MainWindow;
        public Resurs izmijenjeniResurs;
        private string ikonicaResursaPath = "";
        private bool imaIkonicu;

        public IzmijeniResursUserControl(Resurs r)
        {
            InitializeComponent();


            foreach (Tip tip in MainWindow.tipovi.Values)
            {
                tipResursa.Items.Add(tip.Oznaka);
            }
            foreach (Etiketa etiketa in MainWindow.etikete.Values)
            {
                etiketaResursa.Items.Add(etiketa.Oznaka);
            }

            izmijenjeniResurs = r;
            foreach (Etiketa etiketa in izmijenjeniResurs.Etikete)
            {
                etiketaResursa.SelectedItems.Add(etiketa.Oznaka);

            }

            oznakaResursa.Text = r.Oznaka;
            opisResursa.Text = r.Opis;
            nazivResursa.Text = r.Naziv;

            tipResursa.SelectedItem = r.TipOznaka;
            tipResursa.Text = r.TipOznaka;
            datum.Text = r.Datum;
            imaIkonicu = r.ImaSvojuIkonicu;

            if (r.Obnovljiv)
            {
                obnovljivTrue.IsChecked = true;
            }
            else
            {
                obnovljivTrue.IsChecked = true;
            }

            if (r.Eksploatacija)
            {
                eksploatacijaTrue.IsChecked = true;
            }
            else
            {
                eksploatacijaFalse.IsChecked = true;
            }

            if (r.Vaznost)
            {
                vaznostTrue.IsChecked = true;
            }
            else
            {
                vaznostFalse.IsChecked = true;
            }

            mjeraCombo.SelectedValue = r.Mjera;
            mjeraCombo.Text = r.Mjera;
            pojavljivanjeCombo.SelectedValue = r.Pojavljivanje;
            pojavljivanjeCombo.Text = r.Pojavljivanje;
            cijenaResursa.Text = r.Cijena.ToString();
            ikonicaResursaPath = r.Ikonica;
            ikonicaName.Source = new BitmapImage(new Uri(r.Ikonica, UriKind.Absolute));
        }


        private void sacuvajIzmjenuResursa_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult izmijeni = MessageBox.Show("Da li ste sigurni da želite sačuvati izmjene?", "", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (izmijeni == MessageBoxResult.Yes)
            {
                string tipResOznaka = "";
                string mjera = "";
                string pojavljivanje = "";
                string ikonica = "";
                bool obnovljiv, eksploatacija, vaznost;
                double cijena;
                List<Etiketa> etiketeResursa = new List<Etiketa>();


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
                    tipResOznaka = tipResursa.SelectedItem.ToString();
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


                if (!imaIkonicu)
                {
                    foreach (Tip tip in MainWindow.tipovi.Values)
                    {
                        if (tip.Oznaka.Equals(tipResOznaka))
                        {
                            ikonica = tip.Ikonica;
                        }
                    }
                }
                else
                {
                    ikonica = ikonicaResursaPath;
                }


                etiketeResursa.Clear();
                foreach (string etiketaSelektovanog in etiketaResursa.SelectedItems)
                {
                    foreach (Etiketa etiketa in MainWindow.etikete.Values)
                    {
                        if (etiketa.Oznaka.Equals(etiketaSelektovanog))
                        {
                            etiketeResursa.Add(etiketa);
                            if (!(etiketa.Resursi.Contains(izmijenjeniResurs)))
                            {
                                etiketa.Resursi.Add(izmijenjeniResurs);
                            }
                        }
                        else
                        {
                            if (etiketa.Resursi.Contains(izmijenjeniResurs))
                            {
                                etiketa.Resursi.Remove(izmijenjeniResurs);
                            }
                        }
                    }
                }

                if (etiketeResursa.Count() == 0)
                {
                    izmijenjeniResurs.TabelaEt = "Resurs nema etiketu";
                }
                else if (etiketeResursa.Count() == 1)
                {
                    foreach (Etiketa etiketa in etiketeResursa)
                    {
                        izmijenjeniResurs.TabelaEt = etiketa.Oznaka;
                    }
                }
                else
                {
                    izmijenjeniResurs.TabelaEt = "Resurs ima više etiketa";
                }


                izmijenjeniResurs.Oznaka = oznakaResursa.Text;
                izmijenjeniResurs.Naziv = nazivResursa.Text;
                izmijenjeniResurs.Ikonica = ikonica;
                izmijenjeniResurs.TipOznaka = tipResOznaka;
                izmijenjeniResurs.Etikete = etiketeResursa;
                foreach (Tip tip in MainWindow.tipovi.Values)
                {
                    if (tip.Oznaka.Equals(tipResOznaka))
                    {
                        izmijenjeniResurs.Tip = tip;
                        tip.Resursi.Add(izmijenjeniResurs);
                    }
                }
                izmijenjeniResurs.Opis = opisResursa.Text;
                izmijenjeniResurs.Datum = datum.Text;
                izmijenjeniResurs.Obnovljiv = obnovljiv;
                izmijenjeniResurs.Eksploatacija = eksploatacija;
                izmijenjeniResurs.Vaznost = vaznost;
                izmijenjeniResurs.Pojavljivanje = pojavljivanje;
                izmijenjeniResurs.Mjera = mjera;
                izmijenjeniResurs.Cijena = cijena;
                izmijenjeniResurs.ImaSvojuIkonicu = imaIkonicu;

                MainWindow.obrisiResurs(izmijenjeniResurs);
                MainWindow.resursi.Add(izmijenjeniResurs.Guid, izmijenjeniResurs);

                ww.prikaziResurse_Click(sender, e);
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

        private void izmijeniIkonicu_Click(object sender, RoutedEventArgs e)
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
                imaIkonicu = true;
            }
        }

        private void ucitajIkonicuTipa_Click(object sender, SelectionChangedEventArgs e)
        {
            if (imaIkonicu == false)
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

        private void ponistiIzmjenu_Click(object sender, RoutedEventArgs e)
        {
            ww.prikaziResurse_Click(sender, e);
        }
    }
}
