# Null ?!?

## Het probleem zonder nullable datatypes

Je weet al dat **value types** zoals `int`, `double`, `bool` en `DateTime` altijd een waarde hebben. Een `int` die je declareert zonder toekenning krijgt standaard de waarde `0`, een `bool` wordt `false`, enzovoort.

Maar soms wil je uitdrukken dat er **geen waarde** is. Denk aan:

- Een klant die geen telefoonnummer heeft opgegeven.
- Een student waarvan het eindcijfer nog niet bekend is.
- Een product zonder vervaldatum.

Bij reference types (`string`, arrays, objecten) kan je `null` gebruiken om "geen waarde" aan te geven. Bij value types kan dat standaard **niet**:

```csharp
int cijfer = null; // FOUT: compilerfout
```

Daar komen **nullable value types** te hulp.

## Een nullable type declareren

Je voegt een `?` toe achter het type:

```csharp
int? cijfer = null;       // OK: geen cijfer bekend
double? gewicht = 72.5;   // OK: wel een waarde
bool? akkoord = null;     // OK: nog niet beslist
DateTime? geboortedatum = null; // OK: niet ingevuld
```

`int?` is eigenlijk een verkorte schrijfwijze voor `Nullable<int>`. In de praktijk gebruik je altijd de korte notatie met `?`.

## Werken met nullable waarden

### Controleren of er een waarde is

Je kan op twee manieren controleren of een nullable variabele een waarde bevat:

```csharp
int? cijfer = null;

// Manier 1: vergelijken met null
if (cijfer != null)
    Console.WriteLine($"Het cijfer is {cijfer}.");

// Manier 2: HasValue
if (cijfer.HasValue)
    Console.WriteLine($"Het cijfer is {cijfer.Value}.");
```

Beide manieren zijn gelijkwaardig. De eerste is het meest gebruikelijk.

> **Let op:** als je `.Value` opvraagt terwijl de waarde `null` is, krijg je een `InvalidOperationException`. Controleer dus altijd eerst!

### Null-conditional operator `?.`

De **null-conditional operator** `?.` roept een lid (property, methode) alleen aan als het object niet `null` is. Is het object wel `null`, dan is het resultaat `null` — zonder dat er een fout optreedt.

Dit werkt met reference types of nullable value types:

```csharp
string naam;
            
naam = null;                    // eigenlijk overbodig (naam is van type string, dus vertrekt van null)
int? lengte = naam?.Length;     // toch geen NullReferenceException
Console.WriteLine(lengte);      // (geen tekst, null wordt bij een ToString naar de lege string omgezet)

naam = "Priya";
lengte = naam?.Length; 
Console.WriteLine(lengte);      // 5
```

Het gaat trouwens ook bij indexers, let op het `?` voor de vierkante haken:

```csharp
string[] woorden = null;
int? lengte = woorden?[0]?.Length;
```

`lengte` is hier `null`, maar er treedt geen fout op.

### Nullable waarden vergelijken

Nullable types gedragen zich intuïtief bij vergelijkingen, maar het kan wat vreemd overkomen...

```csharp
int? a = 10;
Console.WriteLine(a > 5);     // True

int? b = null;
Console.WriteLine(b > 5);     // False
Console.WriteLine(b <= 5);    // False
Console.WriteLine(b == null); // True
```

Als één kant `null` is, geeft elke vergelijking met `>`, `<`, `>=`, `<=` het resultaat `false`. Dat kan verrassend zijn: `b` is hier niet groter dan 5, maar ook niet kleiner of gelijk aan 5.

# Een standaardwaarde voorzien met null-coalescing operator `??`

De **null-coalescing operator** `??` laat je een alternatieve waarde opgeven voor het geval de waarde `null` is:

```csharp
int? cijfer = null;
int resultaat = cijfer ?? 0; // resultaat = 0, want cijfer is null

int? anderCijfer = 15;
int resultaat2 = anderCijfer ?? 0; // resultaat2 = 15, want anderCijfer heeft een waarde
```

Lees `??` als: "gebruik de waarde links, tenzij die `null` is — gebruik dan de waarde rechts."

Je zou de operator in het Nederlands ietwat geforceerd **null-samenvoegingsoperator** kunnen noemen.

Dit is bijzonder handig bij het tonen van output:

```csharp
int? aantalPogingen = null;
Console.WriteLine($"Aantal pogingen: {aantalPogingen ?? 0}");
```
Uiteraard is dit gewoon een compactere manier dan werken met een **conditional operator** `?:` (ook wel eens *ternary operator* genoemd):

```csharp
int? aantalPogingen = null;
string pogingen = (aantalPogingen.HasValue ? aantalPogingen.Value : 0).ToString();
Console.WriteLine($"Aantal pogingen: {pogingen}");
```

Of met een klassieke `if`:

```csharp
int? aantalPogingen = null;
string output;
if (aantalPogingen.HasValue)
    output = aantalPogingen.Value.ToString();
else
    output = "geen";
Console.WriteLine($"Aantal pogingen: {output}");
```

Merk op dat het volgende niet kan...

```csharp
int? aantalPogingen = null;
Console.WriteLine($"Aantal pogingen: {aantalPogingen ?? "geen"}");  // kan niet: compileerfout
```

Tussen de accolades staat hier een expressie: `aantalPogingen ?? "geen"`.
Elke expressie (ook deze) is altijd van één bepaald type. In dit geval is de subexpressie `aantalPogingen` een `int?`, en de subexpressie `"geen"` is een `string`. 
De compiler kan deze twee types niet combineren: `"Operator ?? cannot be applied to operands of type int? and string."`.

Een oplossing zou zijn...

```csharp
int? aantalPogingen = null;
Console.WriteLine($"Aantal pogingen: {aantalPogingen?.ToString() ?? "geen"}");  // geen
```

## De null-coalescing assignment operator (`??=`)

De **null-coalescing assignment operator** kent een waarde toe aan een variabele, **maar alleen als die variabele op dat moment `null` is**. Als de variabele al een waarde heeft (niet `null`), gebeurt er niets.

```csharp
string? begroeting = null;
begroeting ??= "Hallo!";
Console.WriteLine(begroeting); // Hallo!

begroeting ??= "Goeiedag!";
Console.WriteLine(begroeting); // Hallo!  (niet overschreven, want niet null)
```

De `??=` variant is een verkorte schrijfwijze van de **null-coalescing operator** `??`:

```csharp
string naam = null;
// Deze twee regels doen hetzelfde:
naam = naam ?? "onbekend 1";
naam ??= "onbekend 2";
Console.WriteLine(naam); // onbekend 1
```

## Nullable reference types

Sinds C# 8 bestaat er ook een systeem van **nullable reference types**.  In recente projecten staat dit standaard aan (via `<Nullable>enable</Nullable>` in het `.csproj`-bestand).

Dit werkt anders dan bij value types. Het is een mechanisme waarmee je de compiler van meer informatie voorzien om `null`-gerelateerde waarschuwingen te geven.
Je vormt een nullable reference type (net zoals een nullable value type) door na de naam van het reference type type (bijvoorbeeld `string`) een `?` te vermelden (bijvoorbeeld `string?`).

- `string` signaleer dat er normaal gezien niet met `null` wordt gewerkt.
- `string?` geeft aan dat net wel _mogelijks_ met `null` wordt gewerkt zijn.

Je zal een waarschuwing krijgen bij het toekennen van `null` waardes op plaatsen waar je/men had aangegeven een `string` te verwachten. Deze `string` signaleert immers dat je verderop niet persé robuustheid hebt op dat vlak (rekening houdt met de _mogelijkheid_ dat het gaat over `null` gaat).
Net géén waarschuwing indien je met `null` werkt op plaatsen waar je/men had aangegeven een `string?` te verwachten.  Deze `string?` signaleert immers dat de _mogelijkheid_ er is dat met `null` wordt gewerkt.

Je zal een waarschuwing krijgen bij het uitvoeren van een handeling met een `string?` expressie indien deze handeling kan leiden tot een `NullReferenceException`, het datatype `string?` signaleert immers dat deze expressie _mogelijks_ naar `null` evalueert.
Net géén waarschuwing indien dezelfde handeling zou uitvoeren op een `string` expressie, want daarmee had je gesignaleerd aan te geven dat deze expressie _normaal gezien_ niet naar `null` zou evalueren.

Dit zijn waarschuwingen, geen fouten. Je programma compileert nog steeds, maar de waarschuwingen helpen je om `NullReferenceException`s te voorkomen.

### Voor parameters

Bij methods met parameters is dit het meest tastbaar. Het idee is dat je met `string` vs `string?` in een method-signatuur expliciet communiceert of een parameter (of return-waarde) `null` mag zijn.
Zonder nullable reference types is elke string-parameter impliciet "misschien null", en moet je als ontwikkelaar maar raden (of de documentatie lezen) of je er null aan mag meegeven:

```csharp
public string Initialen(string voornaam, string achternaam, string tussenvoegsel) { ... }
```

Wat mag hier bij het aanroepen van deze method null zijn?  Geen idee zonder de code of documentatie te lezen...

Met nullable reference types enabled wordt dat:

```csharp
static void Main() {
    string voornaam = "Jan";
    string achternaam = "Janssens";
    string tussenvoegsel = "Karel";
            
    string initialen = Initialen(voornaam, achternaam, tussenvoegsel);
    Console.WriteLine(initialen);

    achternaam = null;
    tussenvoegsel = null;
    initialen = Initialen(voornaam, achternaam, tussenvoegsel); // waarschuwt dat achternaam null kan zijn
    // terechte waarschuwing want bij uitvoer zal de Initialen method een exception opleveren, vanwege achternaam[0]
    // geen waarschuwing bij tussenvoegsel, daar zou het ook geen probleem mogen zijn
}
static string Initialen(string voornaam, string achternaam, string? tussenvoegsel) {
    char initiaal1 = voornaam[0];
    //char initiaal2 = tussenvoegsel[0];            // zou een waarschuwing geven: tussenvoegsel kan immers null zijn
    //                                                 "Dereference of a possibly null reference."      
    // maak er dus beter iets van als:
    char initiaal2 = tussenvoegsel?[0] ?? default;  // default zorgt voor default char value ('/0' of null character)
    char initiaal3 = achternaam[0];
            
    return $"{initiaal1}. {(initiaal2 != '\0' ? $"{initiaal2}. " : "")}{initiaal3}.";
}
```
Nu is het contract duidelijk: `voornaam` en `achternaam` zijn verplicht, `tussenvoegsel` mag null zijn. 
En de compiler helpt je aan beide kanten:

- Bij de aanroeper: als je `null` meegeeft voor `voornaam` of `achternaam`, krijg je een waarschuwing, bij `tussenvoegsel` net niet.
- In de method zelf: als je `tussenvoegsel.Length` schrijft zonder null-check, krijg je ook een waarschuwing.

Hetzelfde geldt voor return-types. string als return-type belooft dat er altijd een waarde terugkomt; `string?` zegt eerlijk dat het `null` kan zijn, en de aanroeper wordt gewaarschuwd als die dat niet afhandelt.

```csharp
static string? Lijn(int[] getallen) {
    if (getallen != null && getallen.Length > 0) {
        int[] copy = new int[getallen.Length];
        Array.Copy(getallen, copy, getallen.Length);
        Array.Sort(copy);
        return string.Join(" > ", copy);
    }
    return null;
}
static void Main() {
    int[] lottocijfers = { 10, 2, 38, 23, 16, 27 };
    //string alsLijn = Lijn(lottocijfers);  // warning: "Converting null literal or possible null value to non-nullable type."
    string? alsLijn = Lijn(lottocijfers);   // Geen warning door string?, dit ontheeft je niet van een null-check, maar geeft
                                            // aan dat de begrijpt dat de waarde null kan zijn.
    Console.WriteLine(alsLijn ?? "Geen cijfers opgegeven."); // 2 > 10 > 16 > 23 > 27 > 38
}
```

Het blijft een compiler-analyse, geen runtime-garantie. Je kunt nog steeds null meegeven aan een string-parameter zonder dat je programma crasht op dat moment — het is een waarschuwing, geen fout. Dat maakt het anders dan `int?` vs `int`, waar het type-systeem het echt afdwingt. 

### De null-forgiving operator (`!`)

De **null-forgiving operator** (ook wel *null-suppression operator* genoemd) plaats je achter een expressie om aan de compiler te zeggen: *"Ik weet dat dit niet `null` is, geef me geen waarschuwing."*

```csharp
string? naam = Console.ReadLine();  // kan null zijn
Console.WriteLine(naam!.ToUpper()); // "vertrouw me, dit is niet null"
```

Belangrijk om te weten...

- De `!`-operator heeft **geen effect tijdens het uitvoeren** van je programma. Hij verandert niets aan de waarde of het gedrag van je code.
- Hij onderdrukt enkel de **compilerwaarschuwing** die je anders zou krijgen bij nullable reference types.
- Als de waarde tóch `null` blijkt te zijn, krijg je gewoon een `NullReferenceException` — de `!` beschermt je daar niet tegen.

## Samenvatting

| Concept | Syntax | Betekenis |
|---|---|---|
| Conditional operator | conditie `?` x `:` y | Levert x op indien conditie `true` anders y |
| Nullable value type | `int?`, `double?`, ... | Value type dat ook `null` kan zijn |
| Null-checks... | x `!= null` of x`.HasValue` | Controleert of x verschillend is van `null` |
| Null-conditional operator | x`?.`Lid | Evalueert naar x.Lid indien x niet `null` is, anders evalueert naar `null` |
| Null-coalescing operator | x `??` y | Evalueert naar x als x niet `null` is, anders y |
| Null-coalescing assignment operator | x `??=` y | Indien x nog `null` is, wordt y aan x toegekend |
| Nullable reference type | `string?` | Geeft aan dat referentie `null` mag zijn |
| Null-forgiving | x`!` |  Onderdrukt compilerwaarschuwing over mogelijke `null`-waarde   |