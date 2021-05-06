using System;

namespace Fuhrpark
{
    class Fahrzeug // später kommt: abstract
    {
        static int anzahlFahrzeuge;

        public Fahrzeug()
        {
            anzahlFahrzeuge++;
        }

        public virtual double SchätzeReichweite() // später kommt: abstract
        {
            return double.NaN;
        }

        public virtual void FahreStrecke(double entfernung) // später kommt: abstract
        {
        }
    }

    class Lkw : Fahrzeug
    {
        string kennzeichen;
    }

    class Pkw : Fahrzeug
    {
        string kennzeichen;
        public Pkw(string kennzeichen)
        {
            this.kennzeichen = kennzeichen;
        }
    }

    class PkwBenzin : Pkw
    {
        double tankfüllstand;
        public PkwBenzin(string kennzeichen)
            : base(kennzeichen)
        {
        }
    }

    class PkwElektro : Pkw
    {
        double stateOfChargeProzent;
        double stateOfHealthProzent;
        double maximaleReichweite;
        public PkwElektro(string kennzeichen, double maximaleReichweite)
            : base(kennzeichen)
        {
            this.maximaleReichweite = maximaleReichweite;
            stateOfHealthProzent = 100.0;
            stateOfChargeProzent = 100.0;
        }
        public void Lade(double ladung_kwh)
        {
            stateOfChargeProzent = Math.Min(100.0, stateOfChargeProzent + 100.0 * ladung_kwh / 50.0);
        }
        public override void FahreStrecke(double entfernung)
        {
            stateOfHealthProzent = stateOfHealthProzent * Math.Exp(-entfernung / 50000.0);
            // Schätzwert!!
            stateOfChargeProzent = Math.Max(0.0, stateOfChargeProzent - 100.0 * entfernung / (maximaleReichweite * stateOfHealthProzent / 100.0));
        }
        public override double SchätzeReichweite()
        {
            return maximaleReichweite * stateOfChargeProzent / 100.0 * stateOfHealthProzent / 100.0;
        }
    }

    class HerkömmlichesFahrrad : Fahrzeug
    {
        public override double SchätzeReichweite()
        {
            return 10.0;
        }
    }

    class Ladestation
    {
    }
}