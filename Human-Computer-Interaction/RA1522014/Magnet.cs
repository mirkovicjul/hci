using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;

namespace RA1522014
{
    [Serializable]
    public class Magnet
    {
        public Image slika = null;
        public Point koord;

        public Magnet(Image i, Point p)
        {
            slika = i;
            koord = p;
        }
    }
}
