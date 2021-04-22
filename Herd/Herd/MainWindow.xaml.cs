using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Herd
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Elektroherd meinHerd = new Elektroherd(4);
            meinHerd.StellBackofenTemperatur(200);
            meinHerd.SchalteBackofenModus(Elektroherd.BackofenModus.UnterUndOberhitze);
            double last = meinHerd.AktuelleLast;
            meinHerd.SchaltePlatte(3, 7);
            meinHerd.SchaltePlatte(2, 3);
            last = meinHerd.AktuelleLast;
        }
    }

    class Platte
    {
        int stufe;

        public void Schalte(int stufe)
        {
            this.stufe = stufe;
        }

        public double AktuelleLast
        {
            get { return stufe / 9.0 * 750.0; }
        }
    }

    class Elektroherd
    {
        Platte[] platten;
        int temperatur;
        public enum BackofenModus { Aus, Unterhitze, Oberhitze, UnterUndOberhitze, Ventilator }
        BackofenModus modus;

        public Elektroherd(int zahlDerPlatten)
        {
            platten = new Platte[zahlDerPlatten];
            for (int i = 0; i < zahlDerPlatten; i++)
            {
                platten[i] = new Platte();
            }
        }

        // stufe == 0 heißt: ausschalten
        public void SchaltePlatte(int nummerDerPlatte, int stufe)
        {
            platten[nummerDerPlatte].Schalte(stufe);
        }
        public void StellBackofenTemperatur(int temperatur)
        {
            this.temperatur = temperatur;
        }
        public void SchalteBackofenModus(BackofenModus modus)
        {
            this.modus = modus;
        }
        public double AktuelleLast
        {
            get
            {
                return ((temperatur > 100 && modus != BackofenModus.Aus) ? 3000.0 : 0.0)
                       + platten.Sum(x => x.AktuelleLast);
            }
        }
    }
}
