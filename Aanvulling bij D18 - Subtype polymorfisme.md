# Subtype polymorfisme

```csharp        
namespace SubtypePolymofisme.VoorbeeldZonderOvererving.Figuren {
    internal class Program {
        static void Main1() {
            Rechthoek r1 = new Rechthoek { Breedte = 4, Hoogte = 5, Kleur = "Blauw" };
            Rechthoek r2 = new Rechthoek { Breedte = 3, Hoogte = 6, Kleur = "Groen" };
            Cirkel c1 = new Cirkel { Straal = 2, Kleur = "Geel" };

            PrintDetails(r1); // Figuur met oppervlakte 20.00 en kleur Blauw.
            PrintDetails(r2); // Figuur met oppervlakte 18.00 en kleur Groen.
            PrintDetails(c1); // Figuur met oppervlakte 12.57 en kleur Geel.
            Console.WriteLine($"Totale oppervlakte: {TotaleOppervlakte(new Rechthoek[] { r1, r2 }, new Cirkel[] { c1 }):F2}\n");

            MaakRood(r1);
            MaakRood(c1);
            PrintDetails(r1); // Figuur met oppervlakte 20.00 en kleur Rood.
            PrintDetails(r2); // Figuur met oppervlakte 18.00 en kleur Groen.
            PrintDetails(c1); // Figuur met oppervlakte 12.57 en kleur Rood.
        }
        // Voorzien op het doorgeven van rechthoeken en cirkels
        static double TotaleOppervlakte(Rechthoek[] rechthoeken, Cirkel[] cirkels) {
            double totaleOppervlakte = 0;
            foreach (var rechthoek in rechthoeken) totaleOppervlakte += rechthoek.Oppervlakte();
            foreach (var cirkel in cirkels) totaleOppervlakte += cirkel.Oppervlakte();
            return totaleOppervlakte;
        }
        // Voor zowel rechthoeken als cirkels een MaakRood-methode voorhanden
        static void MaakRood(Rechthoek r) { r.Kleur = "Rood"; }
        static void MaakRood(Cirkel c) { c.Kleur = "Rood"; }
        // Voor zowel rechthoeken als cirkels een PrintDetails-methode voorhanden
        static void PrintDetails(Rechthoek r) {
            Console.WriteLine($"Vorm met oppervlakte {r.Oppervlakte():F2} en kleur {r.Kleur}.");
        }
        static void PrintDetails(Cirkel c) {
            Console.WriteLine($"Vorm met oppervlakte {c.Oppervlakte():F2} en kleur {c.Kleur}.");
        }
    }
    class Rechthoek {
        public double Breedte { get; set; }
        public double Hoogte { get; set; }
        public string Kleur { get; set; }
        public double Oppervlakte() { return Breedte * Hoogte; }
    }
    class Cirkel {
        public double Straal { get; set; }
        public string Kleur { get; set; }
        public double Oppervlakte() { return Math.PI * Straal * Straal; }
    }
}
namespace SubtypePolymofisme.VoorbeeldZonderOvererving.Werknemers {
    internal class Program {
        static void Main2() {
            Werknemer w1 = new Werknemer { Naam = "Jan", MaandSalaris = 3000 };
            Werknemer w2 = new Werknemer { Naam = "Klaas", MaandSalaris = 4000 };
            Manager m1 = new Manager { Naam = "Piet", MaandSalaris = 5000, Bedrijfswagen = new Bedrijfswagen { Nummerplaat = "1-ABC-123" } };
            PrintDetails(w1); // Jan verdient 36000 per jaar.
            PrintDetails(w2); // Klaas verdient 48000 per jaar.
            PrintDetails(m1); // Piet verdient 60000 per jaar.
            Werknemer[] werknemersDeel1 = new Werknemer[2] { w1, w2 };
            Manager[] werknemersDeel2 = new Manager[1] { m1 };
            Console.WriteLine($"Totaal salaris: {TotaleJaarSalaris(werknemersDeel1, werknemersDeel2)}"); // Totaal salaris: 144000
        }
        static void PrintDetails(Werknemer w) {
            Console.WriteLine($"{w.Naam} verdient {w.JaarSalaris()} per jaar.");
        }
        static void PrintDetails(Manager m) {
            Console.WriteLine($"{m.Naam} verdient {m.JaarSalaris()} per jaar.");
        }
        static decimal TotaleJaarSalaris(Werknemer[] werknemers, Manager[] managers) {
            decimal totaal = 0;
            foreach (var w in werknemers) totaal += w.JaarSalaris();
            foreach (var m in managers) totaal += m.JaarSalaris();
            return totaal;
        }
    }
    class Werknemer {
        public string Naam { get; set; }
        public decimal MaandSalaris { get; set; }
        public decimal JaarSalaris() { return MaandSalaris * 12; }
    }
    class Manager {
        public string Naam { get; set; }
        public decimal MaandSalaris { get; set; }
        public decimal JaarSalaris() { return (MaandSalaris * 12); }
        public Bedrijfswagen Bedrijfswagen { get; set; }
    }
    class Bedrijfswagen {
        public string Nummerplaat { get; set; }
    }
}
namespace SubtypePolymofisme.VoorbeeldMetOvererving.Werknemers {
    // In plaats van voor elke soort werknemer (gewone werknemers en managers) een aparte
    // benaderingsvorm te moeten voorzien (2 keer een PrintDetails-methode, 2 parameters in
    // TotaleJaarSalaris, ...), en in plaats van code te moeten dupliceren (Naam, MaandSalaris,
    // JaarSalaris in zowel Werknemer als Manager), kunnen we gebruik maken van overerving.
    class Werknemer {
        public string Naam { get; set; }
        public decimal MaandSalaris { get; set; }
        public decimal JaarSalaris() { return MaandSalaris * 12; }
    }
    class Manager : Werknemer {
        // let hier op de ": Werknemer" op bovenstaande regel => OVERERVING
        // Dit doen we als Manager een subtype/specialisatie is van Werknemer, of anders verwordt indien we 
        // Manager verder willen laten bouwen op Werknemer.
        // We spreken over de "basisklasse" ("parentclass" of "superklasse") Werknemer en anderzijds de
        // "afgeleide klasse" ("childclass" of "subklasse") Manager.
        public Bedrijfswagen Bedrijfswagen { get; set; }
        // OVERERVING (of INHERITANCE) is het mechanisme waarbij een nieuwe klasse (de afgeleide klasse)
        // eigenschappen en gedrag overneemt van een bestaande klasse (de basisklasse).  Alles wordt
        // overgenomen, behalve de constructoren.
        // Pas inheritance enkel en alleen toe wanneer voldaan is aan de...
        // - "100%" regel: alle members (met uitzondering van de constructoren) zinvol zijn voor de afgeleide klasse
        // - "IS_EEN" regel (specialisatie): wanneer het subtype een specialisatie is van het supertype, of je
        //   met andere woorden kan zeggen "een Manager IS EEN Werknemer"
    }
    class Bedrijfswagen {
        public string Nummerplaat { get; set; }
    }
    internal class Program {
        static void Main3() {
            Werknemer w1 = new Werknemer { Naam = "Jan", MaandSalaris = 3000 };
            Werknemer w2 = new Werknemer { Naam = "Klaas", MaandSalaris = 4000 };
            Manager m1 = new Manager { Naam = "Piet", MaandSalaris = 5000, Bedrijfswagen = new Bedrijfswagen { Nummerplaat = "1-ABC-123" } };
            PrintDetails(w1); // Jan verdient 36000 per jaar.
            PrintDetails(w2); // Klaas verdient 48000 per jaar.
            PrintDetails(m1); // Piet verdient 60000 per jaar.
            Werknemer[] alleWerknemers = new Werknemer[3];
            alleWerknemers[0] = w1;
            alleWerknemers[1] = w2;
            alleWerknemers[2] = m1; // een manager wordt hier in formaat werknemer opgeslagen
            Console.WriteLine($"Totaal salaris: {TotaleJaarSalaris(alleWerknemers)}"); // Totaal salaris: 144000

            // Een manager kan nu in de vorm van een manager of in de vorm van een werknemer worden
            // benaderd:
            Manager m2 = new Manager { Naam = "Els", MaandSalaris = 6000 };
            Werknemer w3 = m2;
            // Overal waar je een Werknemer verwacht, kan je ook een Manager doorgeven ("LSP" of Liskov Substitution Principle).
            // Dat houdt steek, want een manager "is een" werknemer.

            // Elke klasse die je definiëert is een subtype, indien niet expliciet gecodeerd
            // erf een klasse over van System.Object:
            object o1 = w3;

            // Een meer generale benaderingsvorm is niet persé voordelig (*), zo kan je in object vorm
            // aan geen enkel specifieke member van Werknemer (Naam, MaandSalaris, JaarSalaris of Bedrijfswagen):
            //Console.WriteLine(o1.Naam);           // compileerfout 'object does not contain a definition for Naam'
            //Console.WriteLine(o1.MaandSalaris);   // compileerfout 'object does not contain a definition for MaandSalaris'   
            //Console.WriteLine(o1.JaarSalaris());  // compileerfout 'object does not contain a definition for JaarSalaris'
            //Console.WriteLine(o1.Bedrijfswagen);  // compileerfout 'object does not contain a definition for Bedrijfswagen'
            // In Werknemer vorm kan je wel aan Naam, MaandSalaris en JaarSalaris, maar niet aan Bedrijfswagen:
            //Console.WriteLine(w3.Bedrijfswagen);  // compileerfout 'Werknemer does not contain a definition for Bedrijfswagen'
            Console.WriteLine(w3.Naam);             // OK
            Console.WriteLine(w3.MaandSalaris);     // OK
            Console.WriteLine(w3.JaarSalaris());    // OK

            // (*)
            // Het statisch type ("compiletimetype") van de expressie o1 is object, en van de expressie
            // w3 is Werknemer.
            // Dit statisch type is relevant voor de compileren, de compiler gebruikt dit om te bepalen
            // of de aanroep van de members geldig zijn.  De compiler zal immers verifiëren of in de
            // publieke interface (verzameling van publieke members) van object (o1) of Werknemer (w3)
            // members als Naam, MaandSalaris, JaarSalaris of Bedrijfswagen aanwezig zijn.

            // Het belangrijkste voordeel van inheritance is niet zozeer het hergebruik van code, 
            // maar wel de verschillende benaderingsvormen die ontstaan ("poly-morfisme" of vele vormen).
            // Je kan Manager's, of andere afgeleiden van Werknemer, zo ook doorgeven daar (bijvoorbeeld
            // in methodes) waar werknemers verwacht worden.
        }
        static void PrintDetails(Werknemer w) {
            Console.WriteLine($"{w.Naam} verdient {w.JaarSalaris()} per jaar.");
        }
        //static void PrintDetails(Manager m) { } // is niet meer nodig
        static decimal TotaleJaarSalaris(Werknemer[] werknemers) {
            decimal totaal = 0;
            foreach (var w in werknemers) totaal += w.JaarSalaris();
            //foreach (var m in managers) totaal += m.JaarSalaris();  OVERBODIG
            return totaal;
        }
    }
}
namespace SubtypePolymofisme.VoorbeeldMetHerdefinitie.Werknemers {
    internal class Program {
        static void Main4() {
            Werknemer w1 = new Werknemer { Naam = "Jan", MaandSalaris = 3000 };
            Werknemer w2 = new Werknemer { Naam = "Klaas", MaandSalaris = 4000 };
            Manager m1 = new Manager { Naam = "Piet", MaandSalaris = 5000, Bedrijfswagen = new Bedrijfswagen { Nummerplaat = "1-ABC-123" } };
            PrintDetails(w1); // Jan verdient 36000 per jaar.
            PrintDetails(w2); // Klaas verdient 48000 per jaar.
            PrintDetails(m1); // Piet verdient 60000 per jaar. >>>>>>>>>>> MOET WORDEN: 65000 per jaar. (13de maand)
            Console.WriteLine($"Totaal salaris: {TotaleJaarSalaris(new Werknemer[] { w1, w2, m1 })}"); // Totaal salaris: 144000

            Werknemer w3 = new Manager { Naam = "Els", MaandSalaris = 6000 };
            Console.WriteLine(w3.JaarSalaris());
            // Het statisch type ("compiletimetype") van de expressie w3 is Werknemer.
            // Dit statisch type is relevant voor de compileren, de compiler gebruikt dit om te bepalen
            // of de aanroep van de methode JaarSalaris() geldig is.  De compiler zal immers verifiëren
            // of in de publieke interface (verzameling van publieke members) van Werknemer een methode
            // JaarSalaris() bestaat.
            // Het dynamisch type ("runtimetype") van de expressie w3 is echter Manager (het werkelijke
            // datatype van de instantie waar w3 naar verwijst).  Dit dynamisch type is relevant voor
            // de method-binding.  Tijdens de uitvoering van het programma zal op basis van het dynamisch
            // type van w3 (namelijk Manager) de juiste implementatie van de methode JaarSalaris()
            // worden opgeroepen (namelijk die van Manager).

            // Methode binding is het proces waarbij wordt bepaald welke methode-implementatie moet
            // worden opgeroepen wanneer een methode wordt aangeroepen op een object.
            // Doorgaans gebeurt methodbinding tijdens compiletijd (statisch binden), dan gaat daar
            // geen tijd aan verloren at runtime.  In het geval dat de aangeroepen implementatie-member
            // echter virtual is gedefinieerd, zal de binding pas at runtime gebeuren.  Dit is wat
            // trager, maar 
        }
        static void PrintDetails(Werknemer w) {
            Console.WriteLine($"{w.Naam} verdient {w.JaarSalaris()} per jaar.");
        }
        static void PrintDetails(Manager m) {
            Console.WriteLine($"{m.Naam} verdient {m.JaarSalaris()} per jaar.");
        }
        static decimal TotaleJaarSalaris(Werknemer[] werknemers) {
            decimal totaal = 0;
            foreach (var w in werknemers) totaal += w.JaarSalaris();
            return totaal;
        }
    }
    class Werknemer {
        public string Naam { get; set; }
        public decimal MaandSalaris { get; set; }
        public virtual decimal JaarSalaris() { // OVERSCHRIJFBAAR maken a.h.v. virtual sleutelwoord
            return MaandSalaris * 12;
        }
    }
    class Manager : Werknemer {
        public Bedrijfswagen Bedrijfswagen { get; set; }
        public override decimal JaarSalaris() { // OVERSCHRIJVEN met override sleutelwoord
            return base.JaarSalaris() + MaandSalaris;
            // hier behavioural subtyping: het JaarSalaris van een manager wordt gebaseerd op dat
            // van een gewone werknemer
        }
        // HERDEFINITIE (of REDEFINITION) is het aanpassen van het gedrag dat gebonden wordt aan
        // een methode in een afgeleide klasse.
        //
        // Per implementatie-member erf je als het ware drie dinge over:
        // (1) de aanwezigheid in de interface, technisch: de signatuur (naam, parameters, returntype)
        // (2) de implementatie (de code in de body van de method)
        // (3) de binding van de aanwezige member (1) aan de implementatie (2)
        // Bij herdefinitie pas je (2) en (3) aan, maar behoud je (1).  Je kan zo bijvoorbeeld de
        // naam, parameters en returntype van JaarSalaris niet aanpassen in Manager.
    }
    class Bedrijfswagen {
        public string Nummerplaat { get; set; }
    }
}
namespace SubtypePolymofisme.VoorbeeldMetOvererving.Figuren {
    internal class Program {
        static void Main5() {
            Rechthoek r1 = new Rechthoek { Breedte = 4, Hoogte = 5, Kleur = "Blauw" };
            Rechthoek r2 = new Rechthoek { Breedte = 3, Hoogte = 6, Kleur = "Groen" };
            Cirkel c1 = new Cirkel { Straal = 2, Kleur = "Geel" };

            PrintDetails(r1); // Figuur met oppervlakte 20.00 en kleur Blauw.
            PrintDetails(r2); // Figuur met oppervlakte 18.00 en kleur Groen.
            PrintDetails(c1); // Figuur met oppervlakte 12.57 en kleur Geel.
            Console.WriteLine($"Totale oppervlakte: {TotaleOppervlakte(new Figuur[] { r1, r2, c1 }):F2}\n");

            MaakRood(r1);
            MaakRood(c1);
            PrintDetails(r1); // Figuur met oppervlakte 20.00 en kleur Rood.
            PrintDetails(r2); // Figuur met oppervlakte 18.00 en kleur Groen.
            PrintDetails(c1); // Figuur met oppervlakte 12.57 en kleur Rood.

            // Een object van het type Figuur hoeven we niet te kunnen aanmaken, omdat Figuur een
            // abstracte concept is.  Definieer je de klasse als 'abstract' kan kan je ze ook niet
            // mee instantiëren:
            //Figuur f1; // OK, een variabele van het abstracte type Figuur kan uiteraard, het blijft een immers een datatype
            //f1 = new Figuur(); // compileerfout 'cannot create an instance of the abstract class Figuur'
        }
        // Voorzien op het doorgeven van rechthoeken en cirkels (want beiden "zijn" figuren)
        static double TotaleOppervlakte(Figuur[] figuren) {
            double totaleOppervlakte = 0;
            foreach (Figuur f in figuren) totaleOppervlakte += f.Oppervlakte();
            return totaleOppervlakte;
        }
        // Voor zowel op rechthoeken als cirkels...
        static void MaakRood(Figuur v) { v.Kleur = "Rood"; }
        static void PrintDetails(Figuur v) {
            Console.WriteLine($"Figuur met oppervlakte {v.Oppervlakte():F2} en kleur {v.Kleur}.");
        }
    }
    abstract class Figuur {
        public string Kleur { get; set; }
        public abstract double Oppervlakte();
        // Het voordeel van een ABC (Abstract Base Class) is dat je er abstracte members in
        // kan definiëren. Dit zijn members zonder implementatie, die door elke concrete
        // afgeleide klasse (niet abstracte klasse) MOETEN worden overschreven (override).
        // Ze (de abstracte members) dwingen dus af dat iets aanwezig is, maar leggen niet op hoe
        // de implementatie er uitziet.
        // Dergelijke ABC met abstracte members vormen zo een soort contract.
    }
    class Rechthoek : Figuur {
        public double Breedte { get; set; }
        public double Hoogte { get; set; }
        public override double Oppervlakte() { return Breedte * Hoogte; }
    }
    class Cirkel : Figuur {
        public double Straal { get; set; }
        public override double Oppervlakte() { return Math.PI * Straal * Straal; }
    }
}
namespace SubtypePolymofisme.VoorbeeldMetInterface.Figuren {
    // Indien een ABC enkel abstracte members heeft kan deze ook gedefinieerd worden door een interface.
    // Men spreekt over het "implementeren" van een interface. Maar in wezen is dit niets anders dan
    // het overerveren van abstracte members.
    // Wederom zal bij het overerven van deze abstracte members (bij wijze van het "implementeren
    // van deze interface") er een verplichting ontstaan deze abstracte members aan een implementatie
    // te binden.
    interface IFiguur { // het is een gewoonte in .NET om interfaces te laten beginnen met een hoofdletter I
        public double Oppervlakte(); // geen body => geen implementatie of binding (op niveau van dit datatype) aan een implementatie
    }
    class Rechthoek : IFiguur { // Rechthoek "implementeert" de interface IFiguur
        public double Breedte { get; set; }
        public double Hoogte { get; set; }
        public double Oppervlakte() { return Breedte * Hoogte; }
    }
    class Cirkel : IFiguur { // Cirkel "implementeert" de interface IFiguur
        public double Straal { get; set; }
        public double Oppervlakte() { return Math.PI * Straal * Straal; }
    }
    internal class Program {
        static void Main() {
            Rechthoek r1 = new Rechthoek { Breedte = 4, Hoogte = 5 };
            Rechthoek r2 = new Rechthoek { Breedte = 3, Hoogte = 6 };
            Cirkel c1 = new Cirkel { Straal = 2 };

            Console.WriteLine($"Totale oppervlakte: {TotaleOppervlakte(new IFiguur[] { r1, r2, c1 }):F2}\n");

            // Een object van het type IFiguur kunnen we niet aanmaken...
            //IFiguur f1; // OK, een variabele van het abstracte type Figuur kan uiteraard, het blijft een immers een datatype
            //f1 = new IFiguur(); // compileerfout 'cannot create an instance of inteface IFiguur'
        }
        // Voorzien op het doorgeven van rechthoeken en cirkels (want beiden "zijn" figuren)
        static double TotaleOppervlakte(IFiguur[] figuren) {
            double totaleOppervlakte = 0;
            foreach (IFiguur f in figuren) totaleOppervlakte += f.Oppervlakte();
            return totaleOppervlakte;
        }
    }
}
```