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
using HelpSistem;


namespace RA1522014
{
    /// <summary>
    /// Interaction logic for NovaEtiketaUserControl.xaml
    /// </summary>
    public partial class NovaEtiketaUserControl : UserControl
    {
        
        public NovaEtiketaUserControl()
        {
            InitializeComponent();
        }

        private void sacuvajEtiketu_Click(object sender, RoutedEventArgs e)
        {

            if (oznakaEtikete.Text == null || oznakaEtikete.Text == "")
            {
                MessageBoxResult result = MessageBox.Show("Nisu popunjena sva polja!", "", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                oznakaEtikete.Focus();
                return;
            }

            if (bojaEtikete.SelectedColorText == null || bojaEtikete.SelectedColorText == "")
            {
                MessageBoxResult result = MessageBox.Show("Nisu popunjena sva polja!", "", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                bojaEtikete.IsOpen = true;
                return;
            }

            if (opisEtikete.Text == null)
            {
                opisEtikete.Text = "";
            }


            foreach (Etiketa postojecaEtiketa in MainWindow.etikete.Values)
            {
                if (postojecaEtiketa.Oznaka.Equals(oznakaEtikete.Text))
                {
                    MessageBoxResult result = MessageBox.Show("Etiketa sa istom oznakom već postoji! Unesite drugu oznaku!", "", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    oznakaEtikete.Focus();
                    return;
                }
            }


            Etiketa novaEtiketa = new Etiketa(oznakaEtikete.Text, bojaEtikete.SelectedColorText, opisEtikete.Text);
            MainWindow.etikete.Add(novaEtiketa.Guid, novaEtiketa);

            MessageBoxResult sacuvana = MessageBox.Show("Etiketa sačuvana!","", MessageBoxButton.OK, MessageBoxImage.Information);

            //etiketa sacuvana, sva polja restartovana
            oznakaEtikete.Text = "";
            opisEtikete.Text = "";
            bojaEtikete.SelectedColor = null;
        }

        private void ponistiEtiketu_Click(object sender, RoutedEventArgs e)
        {
            oznakaEtikete.Text = "";
            opisEtikete.Text = "";
            bojaEtikete.SelectedColor = null;
        }

        
    }
}
