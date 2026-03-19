# Iteratie statements (for, do while en while) - Voordelen en verschillen

```csharp        
// 1. FOR - Voordeel: Teller zit ingebouwd, aantal iteraties bekend
// Gebruik wanneer je in de body van je herhaling een numeriek gegeven
// hebt die van een specifieke startwaarde met een specifieke stap gaat 
// evolueren
Console.WriteLine("=== FOR VOORBEELD ===\nToon de datum van de eerste dag van elke maand....");
for (int maand = 1; maand <= 12; maand++) 
    Console.Write($"1/{maand} ");
Console.WriteLine();

// 2. WHILE - Voordeel: Conditie vooraf checken, onbekend aantal iteraties
// Gebruik bij input validatie of wanneer je niet weet hoeveel iteraties nodig zijn
Console.WriteLine("\n=== WHILE VOORBEELD ===\nBlijf een waarde inlezen tot het een getal (int) is in het bereik 1 t.e.m. 12");
int m;
Console.Write("Geef een getal tussen 1 en 12?: ");
while (!int.TryParse(Console.ReadLine(), out m) || m < 1 || m > 12) 
    Console.Write("Ongeldig! Probeer opnieuw (1-12)?: ");
Console.WriteLine($"De eerste van deze maand is: 1/{m}");

// 3. DO-WHILE - Voordeel: Garantie van minstens 1 uitvoering
// Gebruik voor menu's of wanneer je sowieso eerst iets moet doen
Console.WriteLine("\n=== DO-WHILE VOORBEELD ===");
string keuze;
do {
    Console.WriteLine("\n--- MENU ---");
    Console.WriteLine("A. Optie A");
    Console.WriteLine("B. Optie B");
    Console.Write("Keuze?: "); keuze = Console.ReadLine();

    // Menu MOET minstens 1x getoond worden
    // Bij while zou je eerst moeten initialiseren
} while (keuze != "A" && keuze != "B");
Console.WriteLine($"Je koos voor optie {keuze}.");
```