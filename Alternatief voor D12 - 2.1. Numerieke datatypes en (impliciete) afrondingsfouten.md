# Het juiste numerieke datatype kiezen in C#

## Gehele getallen: begin bij `int`

C# biedt een hele reeks datatypes voor gehele getallen:

|Type|Bereik|Grootte|
|-|-|-|
|`byte`|0 tot 255|1 byte|
|`sbyte`|−128 tot 127|1 byte|
|`short`|−32.768 tot 32.767|2 bytes|
|`ushort`|0 tot 65.535|2 bytes|
|`int`|−2.147.483.648 tot 2.147.483.647 (≈ ±2 miljard)|4 bytes|
|`uint`|0 tot 4.294.967.295|4 bytes|
|`long`|≈ ±9,2 × 10¹⁸|8 bytes|
|`ulong`|0 tot ≈ 1,8 × 10¹⁹|8 bytes|

**Vuistregel: kies standaard `int`.**

Waarom?

* Het bereik van `int` (circa ±2 miljard) is ruim genoeg voor de overgrote meerderheid van toepassingen: tellers, indexen, aantallen, leeftijden, scores, ...
* `int` is het "natuurlijke" geheel-getaltype in C#. Veel methodes in de standaardbibliotheek verwachten of retourneren een `int`. Als je een ander type gebruikt, moet je vaak expliciet converteren/casten, wat je code rommeliger maakt én een bron van bugs is.
* De 4 bytes die een `int` inneemt zijn op moderne hardware verwaarloosbaar. Je bespaart niets merkbaars door een `short` of `byte` te gebruiken voor losse variabelen.

### Wanneer wijk je af van `int`?

* **Te klein bereik**: moet je de wereldbevolking bijhouden (~8 miljard)? Dan past dat niet in een `int`. Kies `long`.
* **Geheugen kritisch**: werk je met enorme arrays van miljoenen waarden waar je zeker weet dat elke waarde tussen 0 en 255 ligt (bijvoorbeeld pixelwaarden van een afbeelding)? Dan kan `byte` zinvol zijn. Maar dit is een optimalisatie die je pas doet als het nodig is.
* **Externe vereiste**: een protocol, bestandsformaat of API schrijft een specifiek type voor.

### Voorbeeld: waarom niet zomaar `short` kiezen

```csharp
short aantalStudenten = 120;
short aantalGroepen = 4;

// Dit compileert NIET:
// short studentenPerGroep = aantalStudenten / aantalGroepen;

// C# promoveert short automatisch naar int bij berekeningen.
// Je moet expliciet casten:
short studentenPerGroep = (short)(aantalStudenten / aantalGroepen);
```

Had je gewoon `int` gebruikt, dan was er geen probleem:

```csharp
int aantalStudenten = 120;
int aantalGroepen = 4;
int studentenPerGroep = aantalStudenten / aantalGroepen; // werkt prima
```

Die verplichte cast bij `short` is niet alleen vervelend — het is ook gevaarlijk. Stel dat de berekening een waarde oplevert die niet in een `short` past: de cast kapt het resultaat stilzwijgend af zonder foutmelding.

---

## Kommagetallen: begin bij `double`

|Type|Precisie|Grootte|Bereik|
|-|-|-|-|
|`float`|~6–7 significante cijfers|4 bytes|±3,4 × 10³⁸|
|`double`|~15–16 significante cijfers|8 bytes|±1,7 × 10³⁰⁸|
|`decimal`|28–29 significante cijfers|16 bytes|±7,9 × 10²⁸|

**Vuistregel: kies standaard `double`.**

* `double` is het standaard kommagetal-type in C#. Een literal zoals `3.14` is automatisch een `double`.
* Wiskundige functies (`Math.Sqrt`, `Math.Sin`, ...) werken met `double`.
* De precisie van ~15 cijfers is voor de meeste toepassingen meer dan voldoende.

### Wanneer wijk je af van `double`?

* **Financiële berekeningen → `decimal`**: als je met geld werkt en exacte centen-nauwkeurigheid nodig hebt, gebruik je `decimal`. Meer hierover verderop.
* **Geheugen kritisch → `float`**: bij game-ontwikkeling of grafische toepassingen met miljoenen coördinaten kan `float` zinvol zijn. In de meeste andere situaties: gewoon `double`.

---

## Het verraderlijke probleem: benaderingsfouten bij `double`

Dit is het deel waar het écht interessant (en gevaarlijk) wordt.

### Hoe slaat een computer kommagetallen op?

Computers werken binair. Het getal `0.1` lijkt simpel in ons decimale stelsel, maar in binair is het een **oneindige periodieke breuk** — vergelijkbaar met hoe `1/3 = 0.33333...` oneindig is in decimaal.

Omdat `double` maar 64 bits heeft, moet die oneindige reeks ergens worden afgekapt. Het resultaat is een *benadering* die héél dicht bij `0.1` ligt, maar het niet exact is.

### Demonstratie: 0.1 + 0.2 ≠ 0.3

```csharp
double a = 0.1;
double b = 0.2;
double som = a + b;

Console.WriteLine(som);           // 0.30000000000000004
Console.WriteLine(som == 0.3);    // False!
```

Lees die output goed: `0.1 + 0.2` geeft **niet** exact `0.3`, maar `0.30000000000000004`. Dit is geen bug in C# — dit is een fundamentele eigenschap van hoe floating-point werkt in élke programmeertaal.

### Waarom is dit een probleem?

Die minuscule afwijking lijkt onschuldig, maar kan serieuze gevolgen hebben zodra je gaat **vergelijken**:

```csharp
double prijs = 0.1 + 0.1 + 0.1;

if (prijs == 0.3)
{
    Console.WriteLine("Klopt!");
}
else
{
    Console.WriteLine("Klopt NIET!");  // ← dit wordt uitgevoerd
}
```

Of stel dat je een lus hebt die tot een bepaalde waarde moet tellen:

```csharp
double waarde = 0.0;

while (waarde != 1.0)  // GEVAARLIJK: oneindig doorlopen!
{
    waarde += 0.1;
    Console.WriteLine(waarde);
}
```

Deze lus stopt **nooit**, omdat `waarde` door afrondingsfouten nooit exact `1.0` wordt.

### Nog een voorbeeld: volgorde van bewerkingen

Benaderingsfouten kunnen zich ook opstapelen, en de volgorde van berekeningen kan het resultaat beïnvloeden:

```csharp
double x = (0.1 + 0.2) + 0.3;
double y = 0.1 + (0.2 + 0.3);

Console.WriteLine(x == y);  // Kan False zijn!
Console.WriteLine($"x = {x:R}");
Console.WriteLine($"y = {y:R}");
```

In de wiskunde geldt `(a + b) + c = a + (b + c)` altijd. Bij floating-point is dat **niet gegarandeerd**.

---

## Oplossing 1: vergelijken met een tolerantie (epsilon)

Omdat je `double`-waarden niet betrouwbaar met `==` kunt vergelijken, werk je met een **aanvaardbaar verschil**:

```csharp
double a = 0.1 + 0.2;
double b = 0.3;

double tolerantie = 0.0001; // aanvaardbaar verschil

if (Math.Abs(a - b) < tolerantie)
{
    Console.WriteLine("Ongeveer gelijk!");  // ← dit wordt nu uitgevoerd
}
```

**Hoe werkt dit?**

In plaats van te vragen "zijn `a` en `b` exact gelijk?", vraag je: "liggen `a` en `b` dicht genoeg bij elkaar?". Je berekent het absolute verschil (`Math.Abs(a - b)`) en controleert of dat kleiner is dan een drempelwaarde die je zelf bepaalt.

De keuze van de tolerantie hangt af van je toepassing:

* Voor het vergelijken van prijzen: `0.01` (cent-nauwkeurig)
* Voor wetenschappelijke berekeningen: mogelijks lagere toleraties, afhankelijk van doelstelling en de schaal van je getallen.

### De lus veilig herschrijven

De eerder getoonde gevaarlijke lus los je op met `<` in plaats van `!=`:

```csharp
double waarde = 0.0;

while (waarde < 1.0)  // VEILIG: < is robuust tegen kleine afwijkingen
{
    waarde += 0.1;
    Console.WriteLine(waarde);
}
```

Maar beter nog: **gebruik een geheel-getallige teller** als je een exact aantal iteraties nodig hebt:

```csharp
for (int i = 1; i <= 10; i++)
{
    double waarde = i * 0.1;
    Console.WriteLine(waarde);
}
```

Hier bepaalt een `int` het aantal iteraties (exact en betrouwbaar), en bereken je de `double` enkel waar nodig.
Je kan dat bijvoorbeeld ook door met andere eenheden te werken, in plaats van 2,35meter kan je ook werken met 235cm, dan heb je geen kommagetallen nodig en kan je gewoon met `int` werken.  Moet je uiteindelijk toch ergens een kommagetal opleveren (bijvoorbeeld afdrukken) op het scherm, doe dat dan zo laat mogelijk.

---

## Oplossing 2: `decimal` voor exacte decimale berekeningen

Als exactheid bij decimale waarden cruciaal is — met name bij **financiële berekeningen** — is `decimal` het juiste type:

```csharp
decimal a = 0.1m;
decimal b = 0.2m;
decimal som = a + b;

Console.WriteLine(som);           // 0.3
Console.WriteLine(som == 0.3m);   // True!
```

`decimal` slaat getallen op in **basis 10** in plaats van basis 2, waardoor waarden als `0.1` en `0.3` wél exact worden opgeslagen.

### Waarom dan niet altijd `decimal`?

* `decimal` is **aanzienlijk trager** dan `double` (tot ~20× voor intensieve berekeningen).
* `decimal` heeft een **kleiner bereik** dan `double` (10²⁸ vs. 10³⁰⁸).
* Wiskundige functies zoals `Math.Sin()` en `Math.Sqrt()` werken niet met `decimal`.
* Voor wetenschappelijke en technische berekeningen waar je met zeer grote of zeer kleine getallen werkt, is `double` geschikter.

**Kortom**: `decimal` is dé keuze voor geld en financiën, maar niet de standaardkeuze voor al je kommagetallen.

---

## Samenvatting: het keuzeproces

```
Heb ik cijfers na de komma nodig?
├── Nee → int
│         └── Past het bereik niet? → long
│         └── Gebruik ik teveel bits (werkgeheugen)? → byte/short
│
└── Ja → Gaat het over geld / moet het exact decimaal zijn?
         ├── Ja  → decimal
         └── Nee → Gebruik ik teveel bits (werkgeheugen)? 
                   └── Nee → double
                   └── Ja → float
```

Ofwel de essentie in één zin: **`int` voor gehele getallen, `double` voor kommagetallen, `decimal` voor geld.** 

---

## Belangrijkste lessen

1. **Kies het standaardtype** (`int` of `double`) tenzij je een concrete reden hebt om ervan af te wijken. "Het zou misschien ietsje zuiniger zijn" is geen concrete reden.
2. **Vergelijk `double`-waarden nooit met `==`**. Gebruik een tolerantie, of herformuleer je logica zodat je exacte vergelijkingen vermijdt (bijvoorbeeld `<` of `>` in plaats van `==` of `!=`).
3. **Gebruik gehele getallen voor lus-tellers**, zelfs als je in de lus met kommagetallen werkt. Dit voorkomt dat afrondingsfouten je luslogica verstoren.
4. **Gebruik `decimal` voor geld.** De tragere snelheid is een kleine prijs voor correcte eurocenten.