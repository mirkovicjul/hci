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
    /// Interaction logic for NoviTipUserControl1.xaml
    /// </summary>
    public partial class NoviTipUserControl1 : UserControl
    {
        private string ikonicaTipaPath = "";
        MainWindow ww = Application.Current.MainWindow as MainWindow;

        public NoviTipUserControl1()
        {
            InitializeComponent();
        }

   
        private void sacuvajTip_Click(object sender, RoutedEventArgs e)
        {
            if (oznakaTipa.Text == null || oznakaTipa.Text == "")
            {
                MessageBoxResult result = MessageBox.Show("Nisu popunjena sva polja!", "", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                oznakaTipa.Focus();
                return;
            }

            if (nazivTipa.Text == null || nazivTipa.Text == "")
            {
                MessageBoxResult result = MessageBox.Show("Nisu popunjena sva polja!", "", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                nazivTipa.Focus();
                return;
            }

            if (ikonicaTipaPath == "")
            {
                MessageBoxResult result = MessageBox.Show("Niste odabrali ikonicu!", "", MessageBoxButton.OK, MessageBoxImage.Exclamation);
           
                return;
            }

            if (opisTipa.Text == null)
            {
                opisTipa.Text = "";
            }


            foreach (Tip postojeciTip in MainWindow.tipovi.Values)
            {
                if (postojeciTip.Oznaka.Equals(oznakaTipa.Text))
                {
                    MessageBoxResult result = MessageBox.Show("Tip sa istom oznakom već postoji! Unesite drugu oznaku!", "", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    oznakaTipa.Focus();
                    return;
                }
            }


            Tip noviTip = new Tip(oznakaTipa.Text, nazivTipa.Text, ikonicaTipaPath, opisTipa.Text);
            MainWindow.tipovi.Add(noviTip.Guid, noviTip);
            if (!MainWindow.tutorialMode)
            {
                MessageBoxResult sacuvano = MessageBox.Show("Tip sačuvan!", "", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                dodajOpisTut.Visibility = Visibility.Hidden;
                dodajOpisTB.Visibility = Visibility.Hidden;
                tipSacuvanTB.Visibility = Visibility.Visible;
                tipSacuvanTut.Visibility = Visibility.Visible;
               
                oznakaTipa.IsEnabled = false;
                opisTipa.IsEnabled = false;
                dodajIkonicu.IsEnabled = false;
                sacuvajTip.IsEnabled = false;
                nazivTipa.IsEnabled = false;

                //ww.prikaziTipoveBtn.Visibility = Visibility.Hidden;
                //Button prikaziTipove = ww.prikaziTipoveBtn;
                //prikaziTipove.IsEnabled = true;
                //MainWindow.prikaziTipoveBtn.isEnabled = true;

                //ww.mapaBtn.IsEnabled = true;
                
            }
            //tip sacuvan, sva polja restartovana
            oznakaTipa.Text = "";
            nazivTipa.Text = "";
            opisTipa.Text = "";
            ikonicaTipa.Source = null;
            ikonicaTipaPath = "";
        }

        

        private void traziIkonicuTip_Click(object sender, RoutedEventArgs e)
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
                //string filename = dlg.FileName;
                ikonicaTipaPath = dlg.FileName;
                ikonicaTipa.Source = new BitmapImage(new Uri(ikonicaTipaPath, UriKind.Absolute));
                        //  = filename;   
                if (MainWindow.tutorialMode)
                {
                    if (!string.IsNullOrEmpty(ikonicaTipaPath))
                    {
                        dodajIkonicuTB.Visibility = Visibility.Hidden;
                        dodajIkonicuTut.Visibility = Visibility.Hidden;
                        dodajOpisTB.Visibility = Visibility.Visible;
                        dodajOpisTut.Visibility = Visibility.Visible;
                        opisTipa.IsEnabled = true;
                        sacuvajTip.IsEnabled = true;
                    }
                }
                   
             }
            
        }

        private void unijetaOznaka(object sender, TextChangedEventArgs e)
        {
            //if (oznakaTipa.Text.Length == 0)
           // {
           // //    oznakaTipa.Focus();
           // }
           // else
           // {
            if (MainWindow.tutorialMode)
            {
                nazivTipa.IsEnabled = true;
                dodajOznakuTB.Visibility = Visibility.Hidden;
                dodajOznakuTut.Visibility = Visibility.Hidden;
                if(string.IsNullOrEmpty(nazivTipa.Text))
                {
                    dodajNazivTB.Visibility = Visibility.Visible;
                    dodajNazivTut.Visibility = Visibility.Visible;
                }
            }
            //}
        }

        private void unijetNaziv(object sender, TextChangedEventArgs e)
        {
           
            //if (nazivTipa.Text.Length == 0)
           // {
           //     nazivTipa.Focus();
           // }
           // else 
           // 
            if(MainWindow.tutorialMode)
            {
                if (!string.IsNullOrEmpty(nazivTipa.Text))
                {
                    dodajIkonicu.IsEnabled = true;
                    dodajNazivTut.Visibility = Visibility.Hidden;
                    dodajNazivTB.Visibility = Visibility.Hidden;
                    if (string.IsNullOrEmpty(ikonicaTipaPath))
                    {
                        dodajIkonicuTut.Visibility = Visibility.Visible;
                        dodajIkonicuTB.Visibility = Visibility.Visible;
                    }
                }
            }
           // }
        }

        private void oznakaTipa_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (MainWindow.tutorialMode)
            {
                if (string.IsNullOrEmpty(oznakaTipa.Text))
                {
                    oznakaTipa.Focus();
                }
            }
        }

        private void nazivTipa_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (MainWindow.tutorialMode)
            {
                if (string.IsNullOrEmpty(oznakaTipa.Text))
                {
                    oznakaTipa.Focus();
                }
                else if (string.IsNullOrEmpty(nazivTipa.Text))
                {
                    nazivTipa.Focus();
                }
            }
        }

        private void ponistiTip_Click(object sender, RoutedEventArgs e)
        {
            oznakaTipa.Text = "";
            nazivTipa.Text = "";
            opisTipa.Text = "";
            ikonicaTipa.Source = null;
            ikonicaTipaPath = "";
        }

        

    }
}
