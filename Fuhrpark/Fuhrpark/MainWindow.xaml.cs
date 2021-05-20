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

        // Fingerübungen zu Ableitungen und Polymorphie
        private void button_Click(object sender, RoutedEventArgs e)
        {
            PkwElektro auto1 = new PkwElektro("BI XY 123", 300.0);
            auto1.FahreStrecke(50.0);
            double r1 = auto1.SchätzeReichweite();
            auto1.FahreStrecke(100.0);
            r1 = auto1.SchätzeReichweite();
            auto1.Lade(20.0);
            r1 = auto1.SchätzeReichweite();

            //Erinnerung an casting
            double x = 3.14;
            int a = (int) x;

            Fahrzeug[] fuhrpark = new Fahrzeug[3];
            fuhrpark[0] = (Fahrzeug) auto1; // expliziter Upcast
            fuhrpark[1] = new HerkömmlichesFahrrad(); // impliziter Upcast
            fuhrpark[2] = new Lkw(); // impliziter Upcast
            for (int i = 0; i < fuhrpark.Length; i++)
            {
                fuhrpark[i].FahreStrecke(10.0);
            }

            // Downcasts sind gefährlich
            ((PkwElektro)fuhrpark[0]).Lade(23.0);
            // ((PkwElektro)fuhrpark[1]).Lade(23.0); // Fehler bei Ausführung!
            if (fuhrpark[1] is PkwElektro)
            {
                //...
            }
            // oder mit "as"
        }

        private void buttonElektro_Click(object sender, RoutedEventArgs e)
        {
            listBoxFahrzeuge.Items.Add(new PkwElektro("BI XY 987", 500.0));
        }

        private void buttonFahrrad_Click(object sender, RoutedEventArgs e)
        {
            listBoxFahrzeuge.Items.Add(new HerkömmlichesFahrrad());
        }

        private void buttonLaden_Click(object sender, RoutedEventArgs e)
        {
            // Lösung ohne Ausgrauen:
            //if (listBoxFahrzeuge.SelectedItem is PkwElektro)
            //{
            //    ((PkwElektro)listBoxFahrzeuge.SelectedItem).Lade(50.0);
            //    MessageBox.Show("Das Aufladen war erfolgreich!");
            //}
            ((PkwElektro)listBoxFahrzeuge.SelectedItem).Lade(10.0);
            listBoxFahrzeuge.Items.Refresh();
        }

        private void listBoxFahrzeuge_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            buttonFahren.IsEnabled = true;
            // lang:
            //if (listBoxFahrzeuge.SelectedItem is PkwElektro)
            //{
            //    buttonLaden.IsEnabled = true;
            //}
            //else
            //{
            //    buttonLaden.IsEnabled = false;
            //}
            // kurz:
            buttonLaden.IsEnabled = listBoxFahrzeuge.SelectedItem is PkwElektro;
        }

        private void buttonFahren_Click(object sender, RoutedEventArgs e)
        {
            ((Fahrzeug)listBoxFahrzeuge.SelectedItem).FahreStrecke(10.0);
            listBoxFahrzeuge.Items.Refresh();
        }

        private void buttonSortiere_Click(object sender, RoutedEventArgs e)
        {
            List<Fahrzeug> fahrzeuge = new List<Fahrzeug>();
            foreach (var item in listBoxFahrzeuge.Items)
            {
                fahrzeuge.Add((Fahrzeug)item);
            }
            var sortierteFahrzeuge = fahrzeuge.OrderByDescending(f => f is PkwElektro ? ((PkwElektro)f).SchätzeReichweite() : 10.0);
            listBoxFahrzeuge.Items.Clear();
            foreach (var item in sortierteFahrzeuge)
            {
                listBoxFahrzeuge.Items.Add(item);
            }
        }
    }
}
