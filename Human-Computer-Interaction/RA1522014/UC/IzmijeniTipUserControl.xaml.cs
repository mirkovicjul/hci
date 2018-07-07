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
    /// Interaction logic for IzmijeniTipUserControl.xaml
    /// </summary>
    public partial class IzmijeniTipUserControl : UserControl
    {
        MainWindow ww = Application.Current.MainWindow as MainWindow;
        private Tip izmijenjeniTip;
        private string ikonicaTipaPath = "";
        private string ikonica = "";

        public IzmijeniTipUserControl(Tip t)
        {
            InitializeComponent();
            izmijenjeniTip = t;
            oznakaTipa.Text = t.Oznaka;
            imeTipa.Text = t.Naziv;
            opisTipa.Text = t.Opis;
            ikonicaName.Source = new BitmapImage(new Uri(izmijenjeniTip.Ikonica, UriKind.Absolute));
            ikonica = t.Ikonica;
        }

        private void promijeniIkonicuTipa_Click(object sender, RoutedEventArgs e)
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
                string filename = dlg.FileName;
                ikonicaName.Source = new BitmapImage(new Uri(filename, UriKind.Absolute));
                ikonicaTipaPath = filename;
            }
        }

        private void sacuvajIzmjenuTipa_Click(object sender, RoutedEventArgs e)
        {

            MessageBoxResult izmijeni = MessageBox.Show("Da li ste sigurni da želite sačuvati izmjene?", "", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (izmijeni == MessageBoxResult.Yes)
            {
                if (opisTipa.Text == null)
                {
                    opisTipa.Text = "";
                }

                izmijenjeniTip.Oznaka = oznakaTipa.Text;
                izmijenjeniTip.Opis = opisTipa.Text;
                izmijenjeniTip.Naziv = imeTipa.Text;
                if (!ikonicaTipaPath.Equals(""))
                {
                    izmijenjeniTip.Ikonica = ikonicaTipaPath;
                }
                else
                {
                    izmijenjeniTip.Ikonica = ikonica;
                }

                foreach (Resurs resurs in izmijenjeniTip.Resursi)
                {
                    if (resurs.ImaSvojuIkonicu == false)
                    {
                        resurs.Ikonica = izmijenjeniTip.Ikonica;

                    }
                }


                MainWindow.obrisiTip(izmijenjeniTip);
                MainWindow.tipovi.Add(izmijenjeniTip.Guid, izmijenjeniTip);

                ww.prikaziTipove_Click(sender, e);
            }
        }

        private void ponistiIzmjenu_Click(object sender, RoutedEventArgs e)
        {
            ww.prikaziTipove_Click(sender, e);
        }
    }
}
