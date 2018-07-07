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
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections.ObjectModel;
using HelpSistem;
using RA1522014.UC;



namespace RA1522014
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public static Dictionary<Guid, Resurs> resursi = new Dictionary<Guid, Resurs>();
        public static Dictionary<Guid, Etiketa> etikete = new Dictionary<Guid, Etiketa>();
        public static Dictionary<Guid, Tip> tipovi = new Dictionary<Guid, Tip>();
        public static UserControl1 mapaUC = new UserControl1();
        public static NoviResursUserControl resursUC = new NoviResursUserControl();
        public static NoviTipUserControl1 tipUC = new NoviTipUserControl1();
        public static NovaEtiketaUserControl etiketaUC = new NovaEtiketaUserControl();

        public static PrikaziEtikete prikaziEtiketeUC;
        public static PrikaziTipove prikaziTipoveUC;
        public static PrikaziResurse prikaziResurseUC;

        public static IzmijeniEtiketuUserControl ieUC;
        public static IzmijeniResursUserControl irUC;
        public static IzmijeniTipUserControl itUC;

        public static IzmijeniEtiketuUserControl ie;
        private readonly string SacuvaniResursi;
        private readonly string SacuvaniTipovi;
        private readonly string SacuvaneEtikete;

        public static bool tutorialMode = false;
        public static bool tutorialMode1 = false;
        public Dictionary<Guid, Etiketa> Etikete
        {
            get { return etikete; }
            set { etikete = value; }
        }

        public Dictionary<Guid, Tip> Tipovi
        {
            get { return tipovi; }
            set { tipovi = value; }
        }

        public Dictionary<Guid, Resurs> Resursi
        {
            get { return resursi; }
            set { resursi = value; }
        }

      
        public MainWindow()
        {
            InitializeComponent();

            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            
            

            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = null;

           
            SacuvaniResursi = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"Resursi.baza");
            SacuvaniTipovi = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"Tipovi.baza");
            SacuvaneEtikete = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"Etikete.baza");
            if (File.Exists(SacuvaniResursi))
            {
                try
                {
                    stream = File.Open(SacuvaniResursi, FileMode.Open);
                    resursi = (Dictionary<Guid, Resurs>)formatter.Deserialize(stream);
                }
                catch
                {

                }
                finally
                {
                    if (stream != null)
                        stream.Dispose();
                }

            }
            else
            {
                resursi = new Dictionary<Guid, Resurs>();
            }

            if (File.Exists(SacuvaniTipovi))
            {
                try
                {
                    stream = File.Open(SacuvaniTipovi, FileMode.Open);
                    tipovi = (Dictionary<Guid, Tip>)formatter.Deserialize(stream);
                }
                catch
                {

                }
                finally
                {
                    if (stream != null)
                        stream.Dispose();
                }

            }
            else
            {
                tipovi = new Dictionary<Guid, Tip>();
            }

            if (File.Exists(SacuvaneEtikete))
            {
                try
                {
                    stream = File.Open(SacuvaneEtikete, FileMode.Open);
                    etikete = (Dictionary<Guid, Etiketa>)formatter.Deserialize(stream);
                }
                catch
                {

                }
                finally
                {
                    if (stream != null)
                        stream.Dispose();
                }

            }
            else
            {
                etikete = new Dictionary<Guid, Etiketa>();
            }

            //this.contentControl.Content = new UserControl1();
            this.contentControl.Content = new UserControl1();
        }


        private void MapaClick(object sender, RoutedEventArgs e) 
        {
            this.contentControl.Content = new UserControl1();
        }

        private void NoviResursClick(object sender, RoutedEventArgs e)
        {
            resursUC.tipResursa.Items.Clear();
            foreach (Tip tip in MainWindow.tipovi.Values)
            {
                resursUC.tipResursa.Items.Add(tip.Oznaka);
            }

            resursUC.etiketaResursa.Items.Clear();
            foreach (Etiketa etiketa in MainWindow.etikete.Values)
            {
                resursUC.etiketaResursa.Items.Add(etiketa.Oznaka);
            }

            this.contentControl.Content = resursUC;

           
        }

        private void NoviTipClick(object sender, RoutedEventArgs e)
        {
            if (!tutorialMode)
            {
                this.contentControl.Content = tipUC;
            }
            else
            {
                this.contentControl.Content = tipUC;
                this.contentControl.IsEnabled = true;
                dodajTipTB.Visibility = Visibility.Hidden;
                dodajTipTut.Visibility = Visibility.Hidden;
                tipUC.dodajOznakuTut.Visibility = Visibility.Visible;
                tipUC.dodajOznakuTB.Visibility = Visibility.Visible;
                tipUC.nazivTipa.IsEnabled = false;
                tipUC.opisTipa.IsEnabled = false;
                tipUC.dodajIkonicu.IsEnabled = false;
                tipUC.sacuvajTip.IsEnabled = false;
                this.noviTipBtn.IsEnabled = false;
            }
        }

        private void NovaEtiketaClick(object sender, RoutedEventArgs e)
        {
           
            this.contentControl.Content = etiketaUC;
        }
        
        public void snimiVrste()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = null;

            try
            {
                stream = File.Open(SacuvaniResursi, FileMode.OpenOrCreate);
                formatter.Serialize(stream, resursi);
            }
            catch
            {

            }
            finally
            {
                if (stream != null)
                    stream.Dispose();
            }
        }

        public void snimiTipove()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = null;

            try
            {
                stream = File.Open(SacuvaniTipovi, FileMode.OpenOrCreate);
                formatter.Serialize(stream, tipovi);
            }
            catch
            {
        
            }
            finally
            {
                if (stream != null)
                    stream.Dispose();
            }
        }

        public void snimiEtikete()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = null;

            try
            {
                stream = File.Open(SacuvaneEtikete, FileMode.OpenOrCreate);
                formatter.Serialize(stream, etikete);
            }
            catch
            {
        
            }
            finally
            {
                if (stream != null)
                    stream.Dispose();
            }
        }


        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        { 
                MessageBoxResult result = MessageBox.Show("Da li želite da sačuvate unesene izmjene?", "", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    snimiVrste();
                    snimiTipove();
                    snimiEtikete();
                }
                   
                base.OnClosing(e);            
        }

        public void prikaziResurse_Click(object sender, RoutedEventArgs e)
        {
            prikaziResurseUC = new PrikaziResurse(new ObservableCollection<Resurs>(resursi.Values));
            this.contentControl.Content = prikaziResurseUC;
           
        }

        public void prikaziTipove_Click(object sender, RoutedEventArgs e)
        {
            prikaziTipoveUC = new PrikaziTipove(new ObservableCollection<Tip>(tipovi.Values));
            this.contentControl.Content = prikaziTipoveUC;

            if (tutorialMode1)
            {
                prikaziTipTB.Visibility = Visibility.Hidden;
                prikaziTipTut.Visibility = Visibility.Hidden;
                //prikaziTipoveUC.filterTB.Visibility = Visibility.Visible;
               // prikaziTipoveUC.filterTut.Visibility = Visibility.Visible;
                this.contentControl.IsEnabled = true;
                prikaziTipoveUC.brisiBtn.IsEnabled = false;
                prikaziTipoveUC.izmijeniBtn.IsEnabled = false;
                prikaziTipoveUC.resetBtn.IsEnabled = false;
                //prikaziTipoveUC.filterBtn.IsEnabled = true;
            }
        }

        public void prikaziEtikete_Click(object sender, RoutedEventArgs e)
        {
            prikaziEtiketeUC = new PrikaziEtikete(new ObservableCollection<Etiketa>(etikete.Values));
            this.contentControl.Content = prikaziEtiketeUC;
        }



        public void izmijeniEtiketu(Etiketa e)
        {
            ieUC = new IzmijeniEtiketuUserControl(e);
            this.contentControl.Content = ieUC;
        }

        public void izmijeniResurs(Resurs r)
        {
            irUC = new IzmijeniResursUserControl(r);
            this.contentControl.Content = irUC;
        }

        public void izmijeniTip(Tip t)
        {
            itUC = new IzmijeniTipUserControl(t);
            this.contentControl.Content = itUC;
        }

        public static void obrisiResurs(Resurs r)
        {
            resursi.Remove(r.Guid);

        }
        
        public static void obrisiEtiketu(Etiketa e)
        {
            etikete.Remove(e.Guid);
        }

        public static void obrisiTip(Tip t)
        {
            tipovi.Remove(t.Guid);
        }


        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (this.contentControl.Content.Equals(resursUC))
                HelpProvider.ShowHelp("noviResurs", this);
            else if (this.contentControl.Content.Equals(tipUC))
                HelpProvider.ShowHelp("noviTip", this);
            else if (this.contentControl.Content.Equals(etiketaUC))
                HelpProvider.ShowHelp("novaEtiketa", this);
            else if (this.contentControl.Content.Equals(prikaziResurseUC))
                HelpProvider.ShowHelp("tabelaResursa", this);
            else if (this.contentControl.Content.Equals(prikaziTipoveUC))
                HelpProvider.ShowHelp("tabelaTipova", this);
            else if (this.contentControl.Content.Equals(prikaziEtiketeUC))
                HelpProvider.ShowHelp("tabelaEtiketa", this);
            else if (this.contentControl.Content.Equals(ieUC))
                HelpProvider.ShowHelp("izmijeniEtiketu", this);
            else if (this.contentControl.Content.Equals(itUC))
                HelpProvider.ShowHelp("izmijeniTip", this);
            else if (this.contentControl.Content.Equals(irUC))
                HelpProvider.ShowHelp("izmijeniResurs", this);
            else
                HelpProvider.ShowHelp("pomoc", this);
        }


        private void tutorijal_Click(object sender, RoutedEventArgs e)
        {
            //izborTutorijala it = new izborTutorijala();
            //it.ShowDialog();
            
            tutorialMode = true;
            zatvoriTutorijalBtn.Visibility = Visibility.Visible;
            dodajTipTut.Visibility = Visibility.Visible;
            dodajTipTB.Visibility = Visibility.Visible;
            //meniTut.Visibility = Visibility.Hidden;
            novaEtiketaBtn.IsEnabled = false;
            noviResursBtn.IsEnabled = false;
            prikaziEtiketeBtn.IsEnabled = false;
            prikaziTipoveBtn.IsEnabled = false;
            prikaziResurseBtn.IsEnabled = false;
            tutorijalBtn.IsEnabled = false;
            filterTutBtn.IsEnabled = false;
            mapaBtn.IsEnabled = false;
            this.contentControl.IsEnabled = false;
            
           
        }


        private void zatvoriTutorijal_Click(object sender, RoutedEventArgs e)
        {
            if (tutorialMode)
            {
                tutorialMode = false;
                zatvoriTutorijalBtn.Visibility = Visibility.Hidden;
                //meniTut.Visibility = Visibility.Visible;
                noviResursBtn.IsEnabled = true;
                noviTipBtn.IsEnabled = true;
                novaEtiketaBtn.IsEnabled = true;
                prikaziEtiketeBtn.IsEnabled = true;
                prikaziResurseBtn.IsEnabled = true;
                prikaziTipoveBtn.IsEnabled = true;
                tutorijalBtn.IsEnabled = true;
                filterTutBtn.IsEnabled = true;
                mapaBtn.IsEnabled = true;

                dodajTipTB.Visibility = Visibility.Hidden;
                dodajTipTut.Visibility = Visibility.Hidden;

                tipUC.dodajOznakuTB.Visibility = Visibility.Hidden;
                tipUC.dodajOznakuTut.Visibility = Visibility.Hidden;
                tipUC.dodajNazivTB.Visibility = Visibility.Hidden;
                tipUC.dodajNazivTut.Visibility = Visibility.Hidden;
                tipUC.dodajOpisTB.Visibility = Visibility.Hidden;
                tipUC.dodajOpisTut.Visibility = Visibility.Hidden;
                tipUC.dodajIkonicuTB.Visibility = Visibility.Hidden;
                tipUC.dodajIkonicuTut.Visibility = Visibility.Hidden;
                tipUC.tipSacuvanTB.Visibility = Visibility.Hidden;
                tipUC.tipSacuvanTut.Visibility = Visibility.Hidden;

                tipUC.oznakaTipa.IsEnabled = true;
                tipUC.nazivTipa.IsEnabled = true;
                tipUC.opisTipa.IsEnabled = true;
                tipUC.sacuvajTip.IsEnabled = true;
                tipUC.dodajIkonicu.IsEnabled = true;
                this.contentControl.IsEnabled = true;
            }
            else if (tutorialMode1)
            {
                tutorialMode1 = false;
                zatvoriTutorijalBtn.Visibility = Visibility.Hidden;
                //meniTut.Visibility = Visibility.Visible;
                noviResursBtn.IsEnabled = true;
                noviTipBtn.IsEnabled = true;
                novaEtiketaBtn.IsEnabled = true;
                prikaziEtiketeBtn.IsEnabled = true;
                prikaziResurseBtn.IsEnabled = true;
                prikaziTipoveBtn.IsEnabled = true;
                tutorijalBtn.IsEnabled = true;
                filterTutBtn.IsEnabled = true;
                mapaBtn.IsEnabled = true;

                prikaziTipTB.Visibility = Visibility.Hidden;
                prikaziTipTut.Visibility = Visibility.Hidden;

                this.contentControl.IsEnabled = true;

                krajTB.Visibility = Visibility.Hidden;
                krajTut.Visibility = Visibility.Hidden;

                if (this.contentControl.Content.GetType() == typeof(PrikaziTipove))
                {
                    prikaziTipoveUC.brisiBtn.IsEnabled = true;
                    prikaziTipoveUC.izmijeniBtn.IsEnabled = true;
                    prikaziTipoveUC.resetBtn.IsEnabled = true;
                    prikaziTipoveUC.filterBtn.IsEnabled = true;
                    prikaziTipoveUC.filterTut.Visibility = Visibility.Hidden;
                    prikaziTipoveUC.filterTB.Visibility = Visibility.Hidden;
                }

               

            }
            
        }

        private void tutorijalPrikazi_Click(object sender, RoutedEventArgs e)
        {
            if (tipovi.Values.Count == 0)
            {
                MessageBoxResult res = MessageBox.Show("Da biste pokrenuli tutorial za prikaz tipova, potrebno je da makar jedan tip bude kreiran!", "", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else 
            {
                tutorialMode1 = true;
                zatvoriTutorijalBtn.Visibility = Visibility.Visible;
                //dodajTipTut.Visibility = Visibility.Visible;
                //dodajTipTB.Visibility = Visibility.Visible;
                prikaziTipTut.Visibility = Visibility.Visible;
                prikaziTipTB.Visibility = Visibility.Visible;
                //meniTut.Visibility = Visibility.Hidden;
                novaEtiketaBtn.IsEnabled = false;
                noviResursBtn.IsEnabled = false;
                noviTipBtn.IsEnabled = false;
                prikaziEtiketeBtn.IsEnabled = false;
                //prikaziTipoveBtn.IsEnabled = false;
                prikaziResurseBtn.IsEnabled = false;
                tutorijalBtn.IsEnabled = false;
                filterTutBtn.IsEnabled = false;
                mapaBtn.IsEnabled = false;
                
                this.contentControl.IsEnabled = false;
            }
        }
    }
}
