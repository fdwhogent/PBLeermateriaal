# Cultuurgebonden omzetting van en naar tekst vs. cultuurneutraal opslaan

## Inleiding over temporele datatypes en cultuur

Bij het werken met datums en tijden in C# heb je drie relevante datatypes:

| Datatype | Bevat | Typisch gebruik |
|---|---|---|
| `DateTime` | Datum én tijd | Wanneer je beide componenten hebt |
| `DateOnly` | Enkel datum | Louter in datum geïnteresserd, past ook beter bij het `DATE`-datatypes van databanken |
| `TimeOnly` | Enkel tijd | Louter in tijdstip geïnteresserd, past ook beter bij het `TIME`-datatypes van databanken |

`DateOnly` en `TimeOnly` bestaan sinds .NET 6 en maken je code **expressiever**: als je enkel een datum nodig hebt, hoef je geen tijdscomponent mee te slepen (en omgekeerd). `DateOnly` sluit beter aan bij het `DATE`-datatype van databanken (SQL Server, PostgreSQL, ...), die enkel een datumcomponent opslaan. Analoog voor `TimeOnly` en het `TIME`-datatype.

Een terugkerend probleem bij het werken met datums en tijden is dat **invoer van de gebruiker** (bijvoorbeeld a.h.v. console input ) **cultuurgebonden** is (de gebruiker werkt met `19/03/2026` voor 19 maart op een Belgisch systeem, maar `03/19/2026` op een Amerikaans systeem), terwijl je bij het **wegschrijven naar externe resources (bijvoorbeeld bestanden) een vast, cultuurneutraal formaat** wilt gebruiken. Zo voorkom je dat een bestand aangemaakt op het ene systeem onleesbaar wordt op het andere.

We bekijken hier:

- **gebruikersinvoer**: cultuurgebonden parsen (= respecteer de ingestelde cultuur van het systeem)
- **opslag in externe resources**: cultuurneutraal formatteren en parsen (= gebruik een vast formaat, typisch ISO 8601)
- **uitvoer op console**: cultuurgebonden formatteren (standaard bij `ToString()`)

---

## 1. Enkele voorbeelden met `DateOnly`

### Console-input parsen (cultuurgebonden)

Bij `TryParse` zonder expliciete `CultureInfo` wordt de **huidige systeemcultuur** gebruikt. Op een Belgisch systeem (`nl-BE`) wordt `19/03/2026` geïnterpreteerd als 19 maart 2026.

```csharp
Console.Write("Geef een datum (bv. 19/03/2026): ");
string invoer = Console.ReadLine();

if (DateOnly.TryParse(invoer, out DateOnly datum)) {
    Console.WriteLine($"Ingelezen datum: {datum}");                // Op nl-BE toont dit: 19/03/2026
    Console.WriteLine($"Ingelezen datum: {datum.ToString("m")}");  // Op nl-BE toont dit: 19 maart
}
else
    Console.WriteLine("Ongeldige datum.");
```

Bij het meegeven van de `"m"` formatstring aan `ToString()` wordt de datum getoond in een cultuurgebonden korte datumformaat. Op een Belgisch systeem zou dat `19 maart` opleveren.  `March 19` bij een Amerikaans systeem.

Ingevoerde waardes als "19/3", "19-3", "19 maart", "19 mrt", ... zouden allen succesvol omgezet kunnen worden naar een `DateOnly`.
Merk trouwens op dat indien de gebruiker enkel "19/3" invoert het huidig jaar wordt gebruikt.

Bij invoer van "hello" zou de omzetting mislukken, en zou "Ongeldige datum." worden getoond.  Het is de returnwaarde van de `TryParse` method die aangeeft of de omzetting gelukt is of niet.  Omzetten van een string naar een `DateOnly` kan dus mislukken, en dat is niet noodzakelijk een fout in de code, maar kan ook gewoon te wijten zijn aan onjuiste of onverwachte invoer van de gebruiker.  Indien je daarmee rekening wil houden is een `TryParse` optimaal, ben je zeker dat een tekst omzetbaar is naar een `DateOnly` dan kan je ook `Parse` gebruiken, maar die zal een exception gooien bij mislukte omzetting, en dat is doorgaans niet wenselijk bij gebruikersinvoer.

### Input parsen (cultuurneutraal of cultuurgebonden)

Indien de gebruiker op een Belgisch systeem "3/4" zou opgeven dan bedoelt hij daarmee allicht 3 april.  Wil je echter zeker zijn dat geen Amerikaan achter het klavier zat, die daarmee eerder dacht aan 4 maart, dan zou je een specifiek formaat kunnen afdwingen met de `TryParseExact` method...

```csharp
Console.Write("Date?: (use format day-month): ");
string invoer = Console.ReadLine();

if (DateOnly.TryParseExact(invoer, "d-M", out DateOnly datum)) {
    Console.WriteLine($"Date: {datum.ToString("m")}");
} else 
    Console.WriteLine("Invalid format.");
```

De tweede argumentwaarde `"d-M"` zorgt ervoor dat nu eerst de dag, en na een koppelteken de maand moet opgegeven worden.  "3-4" of "03-04" zou hier bijvoorbeeld als invoer worden omgezet naar een `DateOnly` die 3 april representeert.  Invoer "3/4" of "3 april" zouden nu niet meer worden aanvaard.
Zorg er uiteraard voor dat je in dergelijk scenario je verwachting expliciteert ("use format day-month" zou voor onze Amerikaan dit verzoek kunnen verhelderen).

Hier werken met een ISO 8601-formaat (format string-patroon `yyyy-MM-dd` of ook via de `"O"` formatstring) is misschien geen slecht idee, de meesten zijn hiermee vertrouwd...

```csharp
 Console.Write("Date?: (use ISO8601 format: YYYY-MM-DD): ");
string invoer = Console.ReadLine();

if (DateOnly.TryParseExact(invoer, "o", out DateOnly datum)) {
    Console.WriteLine($"Date: {datum.ToString("m")}");
} else 
    Console.WriteLine("Invalid format.");
```

Het gebruik van standaard format strings (voorgedefinieerde formaat-patronen) zoals `"m"` of `"O"` is handig, voor meer informatie over de beschikbare format strings, zie:
https://learn.microsoft.com/en-us/dotnet/standard/base-types/standard-date-and-time-format-strings

Stel je zelf een custom formaat samen, bijvoorbeeld met `"yyyy"` voor het jaar, `"MM"` voor de maand (met voorloopnul) en `"dd"` voor de dag (met voorloopnul), dan vindt je de beschikbare format specifiers hier:
https://learn.microsoft.com/en-us/dotnet/standard/base-types/custom-date-and-time-format-strings

Komt de inhoud hier uit een externe resource (als een bestand) waarvan je zeker weet dat het in een bepaald formaat zit (bijvoorbeeld ISO 8601-formaat), dan is ook de `ParseExact` methode gebruiken, en kan je ook een expliciete `CultureInfo.InvariantCulture` argumentwaarde meegeven...

```csharp
DateOnly datum = new DateOnly(2026, 3, 19);

string isoString = datum.ToString("O", CultureInfo.InvariantCulture); // "2026-03-19"
File.WriteAllText("datum.txt", isoString);
//...
string uitBestand = File.ReadAllText("datum.txt");
DateOnly teruggelezen = DateOnly.ParseExact(uitBestand, "O", CultureInfo.InvariantCulture);

Console.WriteLine(teruggelezen.ToString("m")); // toont cultuurgebonden weergave op console
```
Je kan een `DateOnly` instantie (indien je het jaar, maand en dag kent) aanmaken met een object initializer (`new DateOnly`) die jaar, maand en dag als argumenten neemt (de waardes tussen haakjes volgend op de object-initializer: `(2026, 3, 19)`). 

Benadrukken in de call dat hier cultuurneutraal wordt gewerkt (met de argumentwaarde )  is bij het "o" formaat (of yyyy-MM-dd formaat) niet strikt is omdat dat formaat altijd hetzelfde is, ongeacht de cultuur.  Maar het is wel een goede gewoonte om dit expliciet te maken, zodat er geen twijfel kan bestaan over de intentie van de code.  En het maakt ook duidelijk dat je bewust kiest voor een cultuuronafhankelijk formaat, wat belangrijk is bij het werken met externe resources zoals bestanden.

Bij decimalen (getallen met cijfers na de "komma") in de inputstring, kan je culture-info meegeven aan methods als `TryParse` (hier is er geen `TryParseExact`)...

```csharp
bool gelukt;
string getalString = "1,234.56"; // Amerikaanse notatie
gelukt = double.TryParse(getalString, NumberStyles.Number, null, out double getal); // null betekent dat de huidige systeemcultuur wordt gebruikt
Console.WriteLine(gelukt);  // False (indien systeemcultuur nl-BE is, want daar is de komma het decimaal scheidingsteken)

System.Globalization.CultureInfo enUs = new System.Globalization.CultureInfo("en-US");
gelukt = double.TryParse(getalString, NumberStyles.Number, enUs, out getal);
Console.WriteLine(gelukt);  // True
Console.WriteLine(getal);   // 1234 gehelen, en 56 hondersten
```

Het aantal opties om hier een specifiek formaat af te dwingen is beperkter, wel heb je de `NumberStyles` enumeratie die wat zaken mogelijk maakt:
- voorloop- en achterloopspaties toestaan (`AllowLeadingWhite`, `AllowTrailingWhite`)
- duizendtalscheidingstekens toestaan (`AllowThousands`)
- valutasymbolen ($ of €) toestaan (`AllowCurrencySymbol`)
- wetenschappelijke notatie (1.23E3) toestaan (`AllowExponent`)
- +/- teken toestaan (`AllowLeadingSign`)
- decimaal scheidingsteken toestaan (`AllowDecimalPoint`)
- niets toestaan (`None`)

```csharp
bool gelukt;
string getalString = "€1.234,56";

gelukt = double.TryParse(getalString, NumberStyles.Number | NumberStyles.AllowCurrencySymbol, null, out getal);
Console.WriteLine(gelukt);  // True

// NumberStyles.Number = Combinatie van AllowLeadingWhite, AllowTrailingWhite, AllowLeadingSign, AllowDecimalPoint, en AllowThousands
gelukt = double.TryParse(getalString, NumberStyles.Number, null, out getal);
Console.WriteLine(gelukt);  // False

gelukt = double.TryParse(getalString, NumberStyles.AllowCurrencySymbol, null, out getal);
Console.WriteLine(gelukt);  // False
```
Ook het `DateTime` datatype (waar je meer informatie over kunt vinden in het reguliere uitgeschreven cursusmateriaal, zie D12) heeft een `TryParseExact` methode die je toelaat om een specifiek formaat af te dwingen bij het parsen van een string naar een `DateTime` instantie.  De argumenten zijn vergelijkbaar als bij `DateOnly.TryParseExact`, maar er zijn ook nog extra argumenten mogelijk, zoals de `DateTimeStyles` enumeratie, waarmee je bijvoorbeeld kan aangeven dat je wilt dat de tijdzone-informatie in de string wordt genegeerd (`DateTimeStyles.AssumeUniversal`), of dat je wilt dat de datum en tijd worden geïnterpreteerd als lokale tijd (`DateTimeStyles.AssumeLocal`).

### Van DateOnly naar DateTime, en omgekeerd

Misschien zinvol omdat je via `DateTime` instanties aan `TimeSpan`s wil geraken...

```csharp
DateOnly do1 = new DateOnly(2026, 3, 12);
DateOnly do2 = new DateOnly(2026, 3, 15);

DateTime dt1 = do1.ToDateTime(TimeOnly.MinValue);
DateTime dt2 = do2.ToDateTime(TimeOnly.MinValue);

TimeSpan ts1 = dt2 - dt1;
Console.WriteLine(ts1.Days); // 3

// Terug naar DateTime met FromDateTime...
DateOnly otherDateOnly = DateOnly.FromDateTime(dt1);
```

## 2. Samenvatting: wanneer welke cultuur?

| Situatie | Cultuurinstelling | Voorbeeld |
|---|---|---|
| Console-input parsen | Systeemcultuur (standaard bij `TryParse`) | `DateOnly.TryParse(invoer, out datum)` |
| Console-output tonen | Systeemcultuur (standaard bij `ToString()`) | `Console.WriteLine(datum)` |
| Wegschrijven naar bestand | `CultureInfo.InvariantCulture` + vast formaat | `datum.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)` |
| Inlezen uit bestand | `CultureInfo.InvariantCulture` + vast formaat | `DateOnly.ParseExact(tekst, "yyyy-MM-dd", CultureInfo.InvariantCulture)` |

## 3. Bijlage: de `"O"` (round-trip) formatstring per datatype

| Datatype | `"O"` formaat | Voorbeeld |
|---|---|---|
| `DateOnly` | `yyyy-MM-dd` | `2026-03-19` |
| `TimeOnly` | `HH:mm:ss.fffffff` | `14:30:00.0000000` |
| `DateTime` | `yyyy-MM-ddTHH:mm:ss.fffffffK` | `2026-03-19T14:30:00.0000000` |

De `"O"` formatstring garandeert een **round-trip**: wat je wegschrijft, kun je identiek teruglezen. Het nadeel is dat het formaat vrij uitgebreid is (vooral bij `TimeOnly` en `DateTime`).
