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

namespace Fuhrpark
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        /*  Drei Säulen der objektorientierten Programmierung:
         *  - Kapselung ("Encapsulation")
         *       - Daten und Funktionen zusammen
         *       - privat/öffentlich
         *  - Vererbung ("Inheritance")
         *       - Kindklassen
         *       - Vererbung der Implementation
         *       - Vererbung der Schnittstelle ("Interface"),
         *            siehe: virtuelle Methoden (vgl. Polymorphie)
         *  - Polymorphie ("Polymorphism")
         */

        private void button_Click(object sender, RoutedEventArgs e)
        {
            PkwElektro auto1 = new PkwElektro("BI XY 123", 300.0);
            auto1.FahreStrecke(50.0);
            double r1 = auto1.SchätzeReichweite();
            auto1.FahreStrecke(100.0);
            r1 = auto1.SchätzeReichweite();
            auto1.Lade(20.0);
            r1 = auto1.SchätzeReichweite();

            Fahrzeug[] fuhrpark = new Fahrzeug[3];
            fuhrpark[0] = auto1;
            fuhrpark[1] = new HerkömmlichesFahrrad();
            fuhrpark[2] = new Lkw();
            for (int i = 0; i < fuhrpark.Length; i++)
            {
                fuhrpark[i].FahreStrecke(10.0);
            }
        }
    }
}
