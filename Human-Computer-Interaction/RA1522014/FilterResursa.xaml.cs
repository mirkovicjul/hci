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
using HelpSistem;

namespace RA1522014
{
    /// <summary>
    /// Interaction logic for FilterResursa.xaml
    /// </summary>
    public partial class FilterResursa : Window
    {
        public FilterResursa()
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            foreach (Etiketa etiketa in MainWindow.etikete.Values)
            {
                etiketeResursa.Items.Add(etiketa.Oznaka);
            }
        }

        private void filtrirajResurse_Click(object sender, RoutedEventArgs e)
        {
            PrikaziResurse.ResursiFilter.Clear();
            bool odgovara;
            foreach (Resurs resurs in PrikaziResurse.Resursi)
            {
                odgovara = true;
                if (oznakaCheckBox.IsChecked.Value)
                {
                    if (!resurs.Oznaka.Contains(oznakaResursa.Text))
                    {
                        odgovara = false;
                    }
                }
                if (nazivCheckBox.IsChecked.Value)
                {
                    if (!resurs.Naziv.Contains(nazivResursa.Text))
                    {
                        odgovara = false;
                    }
                }

                if (tipCheckBox.IsChecked.Value)
                {
                    if (!resurs.TipOznaka.Equals(tipResursa.Text))
                    {
                        odgovara = false;
                    }
                }

                if (mjeraCheckBox.IsChecked.Value)
                {
                    if (!resurs.Mjera.Equals(mjera.Text))
                    {
                        odgovara = false;
                    }
                }

                if (pojavljivanjeCheckBox.IsChecked.Value)
                {
                    if (!resurs.Pojavljivanje.Equals(pojavljivanje.Text))
                    {
                        odgovara = false;
                    }
                }

                if (obnovljivCheckBox.IsChecked.Value)
                {
                    if (obnovljivTrue.IsChecked.Value)
                    {
                        if (!resurs.Obnovljiv.Equals(true))
                        {
                            odgovara = false;
                        }
                    }
                    else if (obnovljivFalse.IsChecked.Value)
                    {
                        if (!resurs.Obnovljiv.Equals(false))
                        {
                            odgovara = false;
                        }
                    }
                }

                if (vaznostCheckBox.IsChecked.Value)
                {
                    if (vazanTrue.IsChecked.Value)
                    {
                        if (!resurs.Vaznost.Equals(true))
                        {
                            odgovara = false;
                        }
                    }
                    else if (vazanFalse.IsChecked.Value)
                    {
                        if (!resurs.Vaznost.Equals(false))
                        {
                            odgovara = false;
                        }
                    }
                }

                if (eksploatacijaCheckBox.IsChecked.Value)
                {
                    if (eksploatacijaTrue.IsChecked.Value)
                    {
                        if (!resurs.Eksploatacija.Equals(true))
                        {
                            odgovara = false;
                        }
                    }
                    else if (eksploatacijaFalse.IsChecked.Value)
                    {
                        if (!resurs.Eksploatacija.Equals(false))
                        {
                            odgovara = false;
                        }
                    }
                }

                if (cijenaCheckBox.IsChecked.Value)
                {
                    if (!resurs.Cijena.ToString().Equals(cijena.Text))
                    {
                        odgovara = false;
                    }
                }

                if (datumCheckBox.IsChecked.Value)
                {
                    if (!resurs.Datum.Equals(datum.Text))
                    {
                        odgovara = false;
                    }
                }

                if (opisCheckBox.IsChecked.Value)
                {
                    if (!resurs.Opis.Contains(opisResursa.Text))
                    {
                        odgovara = false;
                    }
                }

                if (etiketeCheckBox.IsChecked.Value)
                {
                    if (resurs.Etikete.Count == 0 && etiketeResursa.SelectedItems != null)
                    {
                        odgovara = false;
                    }
                    else
                    {
                        foreach (Etiketa etik in resurs.Etikete)
                        {
                           
                            foreach (string etiketa in etiketeResursa.SelectedItems)
                            {
                                if (!etik.Oznaka.Equals(etiketa))
                                {
                                    odgovara = false;
                                }
                            }
                        }
                    }
                }

                if (odgovara)
                {
                    PrikaziResurse.ResursiFilter.Add(resurs);
                }
            }

            this.Close();
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            HelpProvider.ShowHelp("filterResursa", this);
        }
    }
}
