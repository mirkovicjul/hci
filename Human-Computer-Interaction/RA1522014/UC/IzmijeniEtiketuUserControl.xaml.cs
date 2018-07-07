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

namespace RA1522014.UC
{
    /// <summary>
    /// Interaction logic for IzmijeniEtiketuUserControl.xaml
    /// </summary>
    public partial class IzmijeniEtiketuUserControl : UserControl
    {
         private Etiketa izmijenjenaEtiketa;
         MainWindow ww = Application.Current.MainWindow as MainWindow;
        public IzmijeniEtiketuUserControl(Etiketa e)
        {

            InitializeComponent();

            izmijenjenaEtiketa = e;
            oznakaEtikete.Text = e.Oznaka;
            bojaEtikete.SelectedColor = (Color)ColorConverter.ConvertFromString(e.Boja);
            opisEtikete.Text = e.Opis;           
        }


        private void sacuvajIzmjenuEtikete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult izmijeni = MessageBox.Show("Da li ste sigurni da želite sačuvati izmjene?", "", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (izmijeni == MessageBoxResult.Yes)
            {
                if (opisEtikete.Text == null)
                {
                    opisEtikete.Text = "";
                }

                izmijenjenaEtiketa.Oznaka = oznakaEtikete.Text;
                izmijenjenaEtiketa.Opis = opisEtikete.Text;
                izmijenjenaEtiketa.Boja = bojaEtikete.SelectedColorText;

                MainWindow.obrisiEtiketu(izmijenjenaEtiketa);
                MainWindow.etikete.Add(izmijenjenaEtiketa.Guid, izmijenjenaEtiketa);

                ww.prikaziEtikete_Click(sender, e);
               
            }
        }

        private void ponistiIzmjenu_Click(object sender, RoutedEventArgs e)
        {
            ww.prikaziEtikete_Click(sender, e);
        }

    }
}
