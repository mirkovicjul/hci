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
    /// Interaction logic for FilterTipa.xaml
    /// </summary>
    public partial class FilterTipa : Window
    {
        MainWindow ww = Application.Current.MainWindow as MainWindow;

        public FilterTipa()
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            if (MainWindow.tutorialMode1)
            {
                filterTB.Visibility = Visibility.Visible;
                filterTut.Visibility = Visibility.Visible;
                filterBtn.IsEnabled = false;
                nazivTipa.IsEnabled = false;
                nazivCheckBox.IsEnabled = false;
                opisTipa.IsEnabled = false;
                opisCheckBox.IsEnabled = false;
            }
        }

        private void filtrirajTipove_Click(object sender, RoutedEventArgs e)
        {
           
            if (MainWindow.tutorialMode1)
            {
                if (string.IsNullOrEmpty(oznakaTipa.Text))
                    oznakaTipa.Focus();
                else
                {
                    PrikaziTipove.TipoviFilter.Clear();
                    bool odgovara;
                    foreach (Tip tip in PrikaziTipove.Tipovi)
                    {
                        odgovara = true;
                        if (oznakaCheckBox.IsChecked.Value)
                        {
                            if (!tip.Oznaka.Contains(oznakaTipa.Text))
                            { odgovara = false; }
                        }

                        if (nazivCheckBox.IsChecked.Value)
                        {
                            if (!tip.Naziv.Contains(nazivTipa.Text))
                            { odgovara = false; }
                        }

                        if (opisCheckBox.IsChecked.Value)
                        {
                            if (!tip.Opis.Contains(opisTipa.Text))
                            { odgovara = false; }
                        }

                        if (odgovara)
                        {
                            PrikaziTipove.TipoviFilter.Add(tip);
                        }
                    }
                    this.Close();
                    ww.krajTut.Visibility = Visibility.Visible;
                    ww.krajTB.Visibility = Visibility.Visible;
                    ww.prikaziTipoveBtn.IsEnabled = false;
                    
                }
            }
            else
            { 
                PrikaziTipove.TipoviFilter.Clear();
                bool odgovara;
                foreach (Tip tip in PrikaziTipove.Tipovi)
                {
                    odgovara = true;
                    if (oznakaCheckBox.IsChecked.Value)
                    {
                        if (!tip.Oznaka.Contains(oznakaTipa.Text))
                        { odgovara = false; }
                    }

                    if (nazivCheckBox.IsChecked.Value)
                    {
                        if (!tip.Naziv.Contains(nazivTipa.Text))
                        { odgovara = false; }
                    }

                    if (opisCheckBox.IsChecked.Value)
                    {
                        if (!tip.Opis.Contains(opisTipa.Text))
                        { odgovara = false; }
                    }

                    if (odgovara)
                    {
                        PrikaziTipove.TipoviFilter.Add(tip);
                    }
                }
                this.Close();
            }

            

            
        }

        private void oznakaCheck(object sender, RoutedEventArgs e)
        {
            if (MainWindow.tutorialMode1)
            {
                oznakaTB.Visibility = Visibility.Visible;
                oznakaTut.Visibility = Visibility.Visible;
                filterKlikTB.Visibility = Visibility.Visible;
                filterKlikTut.Visibility = Visibility.Visible;
                filterTB.Visibility = Visibility.Hidden;
                filterTut.Visibility = Visibility.Hidden;
                filterBtn.IsEnabled = true;
            }
        }

        private void oznakaUncheck(object sender, RoutedEventArgs e)
        {
            filterKlikTB.Visibility = Visibility.Hidden;
            filterKlikTut.Visibility = Visibility.Hidden;
            oznakaTut.Visibility = Visibility.Hidden;
            oznakaTB.Visibility = Visibility.Hidden;
            filterTB.Visibility = Visibility.Visible;
            filterTut.Visibility = Visibility.Visible;
          
            filterBtn.IsEnabled = false;
        }

        private void oznakaTipa_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(oznakaTipa.Text))
                oznakaTipa.Focus();
        }

        private void oznakaTipa_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(oznakaTipa.Text))
                oznakaTipa.Focus();
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            HelpProvider.ShowHelp("filterTipa", this);
        }

       
    }
}
