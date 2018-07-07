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
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using HelpSistem;

namespace RA1522014
{
    /// <summary>
    /// Interaction logic for FilterEtikete.xaml
    /// </summary>
    public partial class FilterEtikete : Window
    {

      

        public FilterEtikete()
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void filtrirajEtikete_Click(object sender, RoutedEventArgs e)
        {
            PrikaziEtikete.EtiketeFilter.Clear();
            bool odgovara;
            foreach (Etiketa etiketa in PrikaziEtikete.Etikete)
            {
                odgovara = true;
                if (oznakaCheckBox.IsChecked.Value)
                {
                    if (!etiketa.Oznaka.Contains(oznakaEtikete.Text))
                    { odgovara = false; }
                }

                if (bojaCheckBox.IsChecked.Value)
                {
                    if (!etiketa.Boja.Equals(bojaEtikete.SelectedColorText))
                        odgovara = false;
                }

                if (opisCheckBox.IsChecked.Value)
                {
                    if (!etiketa.Opis.Contains(opisEtikete.Text))
                        odgovara = false;
                }

                if (odgovara)
                {
                    PrikaziEtikete.EtiketeFilter.Add(etiketa);
                }

            }

            this.Close();
            
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            HelpProvider.ShowHelp("filterEtikete", this);
        }
    }
}
