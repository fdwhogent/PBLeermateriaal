# Afrondingsregels in de praktijk

Wanneer je kortingen of btw-percentages toepast, werk je met de officiële munteenheid: de euro. 
Omdat de kleinste eenheid de cent is, vereist de Belgische btw- en boekhoudwetgeving dat bedragen op een factuur (zoals de maatstaf van heffing en het btw-bedrag zelf) worden afgerond op twee cijfers na de komma.

Hierbij gelden de standaard rekenkundige afrondingsregels:
- Eindigt het derde cijfer na de komma op 0 tot en met 4? Dan rond je af naar beneden (bijv. € 10,124 wordt € 10,12).
- Eindigt het derde cijfer op 5 tot en met 9? Dan rond je af naar boven (bijv. € 10,125 wordt € 10,13).

Per lijn of op het totaal?
De FOD Financiën staat toe dat je facturatiesoftware op twee manieren afrondt:
- Afronden per factuurlijn (horizontaal): Je berekent de btw of korting per artikel, rondt dit bedrag meteen af op twee decimalen en telt daarna alles op.
- Afronden op het totaal (verticaal): Je telt eerst alle bedragen exclusief btw per btw-tarief (6%, 12%, 21%) exact op, inclusief alle decimalen. Pas op dat totaal bereken je de btw en dat eindresultaat rond je af op twee decimalen.

Beide methodes zijn wettelijk in orde. Omdat de methodes wiskundig licht verschillen, kan er soms een verschil van enkele centen ontstaan op de eindfactuur. Zolang je consequent één methode toepast binnen je bedrijfsvoering, zal de fiscus dit aanvaarden.

```csharp
namespace AfrondenVolgensBelgischeWetgeving {
    internal class Program {
        static void Main(string[] args) {
            // ============================================================================
            // Belgische BTW-afrondingsregels — demonstratie
            // ============================================================================
            // Twee wettelijk toegestane methodes:
            //   1) Horizontaal: afronden per factuurlijn
            //   2) Verticaal:   afronden op het totaal per btw-tarief
            //
            // Belangrijk: C# gebruikt standaard "banker's rounding" (MidpointRounding.ToEven),
            // waarbij 0.125 wordt afgerond naar 0.12 in plaats van 0.13.
            // De Belgische rekenkundige afrondingsregel vereist MidpointRounding.AwayFromZero.
            // ============================================================================

            Console.OutputEncoding = System.Text.Encoding.UTF8; // correct Unicode-tekens (zoals accenten, speciale symbolen, box-drawing characters) weergeven

            // --- Factuurgegevens (parallel arrays) ---
            string[] omschrijvingen = {"Brood (tarwe)", "Verse melk 1L", "Fles rode wijn", "Biologische pasta", "Ambachtelijke kaas"};
            decimal[] eenheidsprijzen = { 2.75m, 1.89m, 8.45m, 3.29m, 6.15m };
            int[] aantallen = { 3, 2, 1, 4, 2 };
            decimal[] btwPercentages = { 6m, 6m, 21m, 6m, 6m };

            int aantalRegels = omschrijvingen.Length;

            // =====================
            //  Factuur afdrukken
            // =====================
            Console.WriteLine("╔══════════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                    FACTUUR — Delhaize Fictief                        ║");
            Console.WriteLine("╠══════════════════════════════════════════════════════════════════════╣");
            Console.WriteLine();

            ToonFactuurRegels(omschrijvingen, eenheidsprijzen, aantallen, btwPercentages, aantalRegels);

            Console.WriteLine();
            Console.WriteLine("────────────────────────────────────────────────────────────────────────");

            // =========================================
            //  Methode 1: Horizontaal (per factuurlijn)
            // =========================================
            decimal totaalExclHorizontaal = 0m;
            decimal totaalBtwHorizontaal = 0m;

            Console.WriteLine();
            Console.WriteLine("► METHODE 1 — Horizontaal (afronden per lijn)");
            Console.WriteLine();

            for (int i = 0; i < aantalRegels; i++) {
                decimal lijnExcl = eenheidsprijzen[i] * aantallen[i];
                decimal lijnBtwExact = lijnExcl * btwPercentages[i] / 100m;
                decimal lijnBtw = AfrondenBelgisch(lijnBtwExact);

                Console.WriteLine($"  {omschrijvingen[i],-22} excl: {lijnExcl,8:F2} €  " +
                                  $"btw {btwPercentages[i],2}%: {lijnBtwExact,10:F4} € → afgerond: {lijnBtw,7:F2} €");

                totaalExclHorizontaal += lijnExcl;
                totaalBtwHorizontaal += lijnBtw;       // al afgerond per lijn
            }

            decimal totaalInclHorizontaal = totaalExclHorizontaal + totaalBtwHorizontaal;

            Console.WriteLine();
            Console.WriteLine($"  Totaal excl. btw:  {totaalExclHorizontaal,10:F2} €");
            Console.WriteLine($"  Totaal btw:        {totaalBtwHorizontaal,10:F2} €");
            Console.WriteLine($"  Totaal incl. btw:  {totaalInclHorizontaal,10:F2} €");

            // =========================================
            //  Methode 2: Verticaal (per btw-tarief)
            // =========================================
            Console.WriteLine();
            Console.WriteLine("────────────────────────────────────────────────────────────────────────");
            Console.WriteLine();
            Console.WriteLine("► METHODE 2 — Verticaal (afronden op totaal per btw-tarief)");
            Console.WriteLine();

            // Verzamel totalen per btw-tarief (we kennen onze tarieven: 6% en 21%)
            decimal[] bekendeTarieven = { 6m, 21m };
            decimal[] totaalExclPerTarief = new decimal[bekendeTarieven.Length]; // exact, niet afgerond

            for (int i = 0; i < aantalRegels; i++) {
                decimal lijnExcl = eenheidsprijzen[i] * aantallen[i];
                int tariefIndex = ZoekTariefIndex(bekendeTarieven, btwPercentages[i]);
                totaalExclPerTarief[tariefIndex] += lijnExcl;
            }

            decimal totaalExclVerticaal = 0m;
            decimal totaalBtwVerticaal = 0m;

            for (int t = 0; t < bekendeTarieven.Length; t++) {
                if (totaalExclPerTarief[t] == 0m) continue;

                decimal btwExact = totaalExclPerTarief[t] * bekendeTarieven[t] / 100m;
                decimal btwAfgerond = AfrondenBelgisch(btwExact);

                Console.WriteLine($"  Tarief {bekendeTarieven[t],2}%:  basis {totaalExclPerTarief[t],10:F2} €  " +
                                  $"btw exact: {btwExact,10:F4} € → afgerond: {btwAfgerond,7:F2} €");

                totaalExclVerticaal += totaalExclPerTarief[t];
                totaalBtwVerticaal += btwAfgerond;
            }

            decimal totaalInclVerticaal = totaalExclVerticaal + totaalBtwVerticaal;

            Console.WriteLine();
            Console.WriteLine($"  Totaal excl. btw:  {totaalExclVerticaal,10:F2} €");
            Console.WriteLine($"  Totaal btw:        {totaalBtwVerticaal,10:F2} €");
            Console.WriteLine($"  Totaal incl. btw:  {totaalInclVerticaal,10:F2} €");

            // =========================================
            //  Vergelijking
            // =========================================
            Console.WriteLine();
            Console.WriteLine("════════════════════════════════════════════════════════════════════════");
            decimal verschil = totaalInclHorizontaal - totaalInclVerticaal;
            Console.WriteLine($"  Verschil tussen beide methodes: {verschil:F2} €");

            if (verschil == 0m)
                Console.WriteLine("  → Geen verschil bij deze factuur.");
            else
                Console.WriteLine("  → Klein afrondingsverschil — beide methodes zijn wettelijk in orde.");

            Console.WriteLine("════════════════════════════════════════════════════════════════════════");

            // =========================================
            //  Let op: MidpointRounding demonstratie
            // =========================================
            Console.WriteLine();
            Console.WriteLine("────────────────────────────────────────────────────────────────────────");
            Console.WriteLine("⚠  OPGELET: verschil tussen C#-standaard en Belgische afronding");
            Console.WriteLine("────────────────────────────────────────────────────────────────────────");
            Console.WriteLine();

            decimal testBedrag = 10.125m;
            decimal standaard = Math.Round(testBedrag, 2);                                  // banker's rounding
            decimal belgisch = Math.Round(testBedrag, 2, MidpointRounding.AwayFromZero);   // rekenkundig

            Console.WriteLine($"  Bedrag: {testBedrag} €");
            Console.WriteLine($"  Math.Round(10.125, 2)                                → {standaard:F2} €  (banker's rounding: naar even)");
            Console.WriteLine($"  Math.Round(10.125, 2, MidpointRounding.AwayFromZero) → {belgisch:F2} €  (Belgische regel: ≥5 naar boven)");
            Console.WriteLine();
        }
        // ============================================================================
        //  Methodes
        // ============================================================================

        /// <summary>
        /// Rondt een bedrag af op 2 decimalen volgens de Belgische rekenkundige regel:
        /// derde cijfer 0-4 → naar beneden, 5-9 → naar boven.
        /// </summary>
        static decimal AfrondenBelgisch(decimal bedrag) {
            return Math.Round(bedrag, 2, MidpointRounding.AwayFromZero);
        }

        /// <summary>
        /// Zoekt de index van een btw-tarief in de array van bekende tarieven.
        /// Geeft -1 terug als het tarief niet gevonden wordt.
        /// </summary>
        static int ZoekTariefIndex(decimal[] tarieven, decimal gezocht) {
            for (int i = 0; i < tarieven.Length; i++) {
                if (tarieven[i] == gezocht)
                    return i;
            }
            return -1;
        }

        /// <summary>
        /// Drukt de factuurregels overzichtelijk af.
        /// </summary>
        static void ToonFactuurRegels(string[] omschrijvingen, decimal[] prijzen,
                                       int[] aantallen, decimal[] btwPercentages, int aantal) {
            Console.WriteLine($"  {"Artikel",-22} {"Prijs", 8}   {"Aantal", 6}   {"Subtotaal", 10}   {"BTW%", 4}");
            Console.WriteLine($"  {new string('─', 60)}");

            for (int i = 0; i < aantal; i++) {
                decimal subtotaal = prijzen[i] * aantallen[i];
                Console.WriteLine($"  {omschrijvingen[i],-22} {prijzen[i],8:F2} €   {aantallen[i],5}   {subtotaal,10:F2} €   {btwPercentages[i],3}%");
            }
        }

    }
}
```