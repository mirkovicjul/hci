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
using System.Collections.ObjectModel;
using System.Net;
using System.ComponentModel;



namespace RA1522014
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        Point pocetak = new Point();
        private Image slika = null;
        public static Dictionary<Magnet, Resurs> nalijepljene = new Dictionary<Magnet, Resurs>();
        private KeyValuePair<Magnet, Resurs> pomoc;
       // Resurs resursic = null;
        public static ObservableCollection<Resurs> ikonice
        {
            get;
            set;
        }

       
        public UserControl1()
        {
            InitializeComponent();
            this.DataContext = this;

            ikonice = new ObservableCollection<Resurs>();
            
            inicijalizacijaIkonica();
            iscrtavanje();
            
            
        }

        void PromjenaFokusa(object sender, SelectionChangedEventArgs args)
        {
            ListBoxItem lbi = ((sender as ListBox).SelectedItem as ListBoxItem);
            int index = listaIkonica.SelectedIndex;

            if (index == -1)
            {
                return;
            }


            Resurs resurs = ikonice.ElementAt(index);
            resurs.Lokacija = pocetak;

            Magnet par = new Magnet(slika, pocetak);
            nalijepljene.Add(par, resurs);
            listaIkonica.SelectedIndex = -1;
            listaIkonica.UpdateLayout();
        }


        private void Prevlacenje(object sender, DragEventArgs e)
        {
            if (pomoc.Key == null)
            {
                return;
            }
            pomoc.Value.Lokacija = new Point(0, 0);
            nalijepljene.Remove(pomoc.Key);

            pomoc = new KeyValuePair<Magnet, Resurs>();
        }

        private void DragImage(object sender, MouseButtonEventArgs e)
        {
            Image image = e.Source as Image;

            Resurs resurs = e.Source as Resurs;
           // resursic = image.Tag as Resurs;
            Point mousePosition = e.GetPosition(Canvas);

            DataObject data = new DataObject(typeof(ImageSource), image.Source);

            if (mousePosition.X >= 0 && mousePosition.Y >= 0)
            {

                pomoc = nadjiParIzDropovanih(image);

                this.Canvas.Children.Remove(image);
                DragDrop.DoDragDrop(image, data, DragDropEffects.Move);

                return;
            }
            else
            {

                DragDrop.DoDragDrop(image, data, DragDropEffects.Copy);
                //ikonice.Remove(resurs);
            }
        }

        private KeyValuePair<Magnet, Resurs> nadjiParIzDropovanih(Image i)
        {


            foreach (KeyValuePair<Magnet, Resurs> par in nalijepljene)
            {

                if (pomoc.Key != null)
                {
                    if (((Magnet)par.Key).slika.Equals(i))
                    {

                        return par;
                    }
                }
            }



            return new KeyValuePair<Magnet, Resurs>();
        }


        private void DropImage(object sender, DragEventArgs e)
        {
            ImageSource image = e.Data.GetData(typeof(ImageSource)) as ImageSource;

            Image imageControl = new Image() { Width = 45, Height = 35, Source = image };
            imageControl.MouseLeftButtonDown += new MouseButtonEventHandler(DragImage);



            Canvas.SetLeft(imageControl, e.GetPosition(this.Canvas).X);
            Canvas.SetTop(imageControl, e.GetPosition(this.Canvas).Y);
            this.Canvas.Children.Add(imageControl);
            //ikonice.Remove(resursic);
            int index = listaIkonica.SelectedIndex;
            pocetak = e.GetPosition(this.Canvas);
            slika = imageControl;



            if (pomoc.Key != null)
            {
                if (pomoc.Key.slika != null)
                {
                    pomoc.Key.slika = slika;
                    pomoc.Key.koord = e.GetPosition(Canvas);
                    pomoc.Value.Lokacija = e.GetPosition(Canvas);

                    pomoc = new KeyValuePair<Magnet, Resurs>();

                }
            }



        }


        public void iscrtavanje()
        {
            ikonice = new ObservableCollection<Resurs>();
            foreach (Resurs resurs in MainWindow.resursi.Values)
            {
                try
                {
                    if (resurs.Lokacija.X != 0 && resurs.Lokacija.Y != 0)
                    {
                        Image image = new Image() { Width = 45, Height = 35, Source = new BitmapImage(new Uri(resurs.Ikonica)) };
                        image.MouseLeftButtonDown += new MouseButtonEventHandler(DragImage);
                        Canvas.SetLeft(image, resurs.Lokacija.X);
                        Canvas.SetTop(image, resurs.Lokacija.Y);
                        this.Canvas.Children.Add(image);
                        nalijepljene.Add(new Magnet(image, resurs.Lokacija), resurs);
                        ikonice.Add(resurs);
                    }
                    else
                    {
                        ikonice.Add(resurs);
                    }
                }
                catch (WebException e)
                {

                }
            }
        }


        public void inicijalizacijaIkonica()
        {

            foreach (Resurs resurs in MainWindow.resursi.Values)
            {
                ikonice.Add(resurs);
            }
        }

    }
}
