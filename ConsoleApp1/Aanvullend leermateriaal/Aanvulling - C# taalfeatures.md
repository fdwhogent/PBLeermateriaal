# C# Taalfeatures

## Inleiding

C# is een taal die continu evolueert. Elke nieuwe versie brengt features die het schrijven van code eenvoudiger, leesbaarder of performanter maken. Dit document bespreekt een reeks taalfeatures die in verschillende C#-versies zijn geïntroduceerd, van C# 2.0 tot en met C# 14.

Voor elke feature vind je een korte uitleg, voorbeeldcode en een verwijzing naar de officiële Microsoft-documentatie.

## Overzicht per C#-versie

Volgende onderwerpen komen aan bod...

| C# versie | .NET versie | Belangrijke features |
| --- | --- | --- |
| C# 2.0 | .NET Framework 2.0 | Partial classes, Static classes |
| C# 3.0 | .NET Framework 3.5 | LINQ, Lambda expressions, Anonymous types, var, Extension methods |
| C# 4.0 | .NET Framework 4.0 | Dynamic binding, Named/optional arguments |
| C# 6.0 | .NET Framework 4.6 | String interpolation, Expression-bodied members |
| C# 7.0-7.3 | .NET Framework 4.7 / .NET Core 2.x | Tuples, Pattern matching, Local functions, Ref returns, Default literal |
| C# 8.0 | .NET Core 3.x | Using declaration, Indices/ranges, Default interface methods |
| C# 9.0 | .NET 5 | Records, Top-level statements, Target-typed new, Pattern combinators (and, or, not) |
| C# 10 | .NET 6 | Global usings, File-scoped namespaces, Record structs, Const interpolated strings |
| C# 11 | .NET 7 | Raw string literals, Required members, Generic math, List patterns, UTF-8 literals |
| C# 12 | .NET 8 | Primary constructors, Collection expressions, Inline arrays, Alias any type |
| C# 13 | .NET 9 | Params collections,`\e` escape, Partial properties |
| C# 14 | .NET 10 | Field keyword, Extension members, Partial constructors/events |

## Partial Classes (C# 2.0)

**Beschikbaar sinds:** C# 2.0 / .NET Framework 2.0 (2005)

Met het `partial` keyword kan je de definitie van een klasse, struct of interface over meerdere bestanden verspreiden. De compiler combineert alle delen tot één geheel.

Dit is vooral nuttig bij:

- Door tools gegenereerde code (zoals Windows Forms of Entity Framework)
- Grote klassen opsplitsen voor betere leesbaarheid
- Meerdere ontwikkelaars aan dezelfde klasse laten werken

**Voorbeeld van partial classes**

**Bestand: Persoon.cs**
```csharp
public partial class Persoon
{
    public string Voornaam { get; set; }
    public string Achternaam { get; set; }
}
```

**Bestand: Persoon.Methods.cs**
```csharp
public partial class Persoon
{
    public string VolledigeNaam()
    {
        return $"{Voornaam} {Achternaam}";
    }
}
```

**Gebruik**
```csharp
Persoon p = new Persoon { Voornaam = "Jan", Achternaam = "Janssen" };
Console.WriteLine(p.VolledigeNaam());  // Output: Jan Janssen
```

> ❗ **Belangrijk**
>
> Alle partial-delen moeten hetzelfde `partial` keyword gebruiken en zich in dezelfde namespace en assembly bevinden.

> 💡 Meer info: [Partial Classes and Methods - Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/partial-classes-and-methods)

## Static Classes (C# 2.0)

**Beschikbaar sinds:** C# 2.0 / .NET Framework 2.0 (2005)

Een **static class** is een klasse die enkel statische leden bevat. Je kan geen instantie aanmaken van een static class.

Static classes zijn ideaal voor:

- Utility-functies (zoals `Math` of `Console`)
- Extension methods (zie verder)
- Groeperen van gerelateerde functies die geen objectstatus nodig hebben

**Voorbeeld van een static class**

```csharp
public static class Rekenmachine
{
    public static int Optellen(int a, int b)
    {
        return a + b;
    }
    
    public static int Vermenigvuldigen(int a, int b)
    {
        return a * b;
    }
}
```

```csharp
// Gebruik - geen instantie nodig
int som = Rekenmachine.Optellen(5, 3);
int product = Rekenmachine.Vermenigvuldigen(4, 7);

Console.WriteLine($"Som: {som}");        // Output: Som: 8
Console.WriteLine($"Product: {product}"); // Output: Product: 28
```

> ℹ️ **Opmerking**
>
> Een static class:
>
> - Kan niet geïnstantieerd worden (`new` is niet mogelijk)
> - Kan niet overerven of overgeërfd worden
> - Bevat enkel statische members
> - Wordt automatisch `sealed`

> 💡 Meer info: [Static Classes - Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/static-classes-and-static-class-members)

## LINQ (C# 3.0)

**Beschikbaar sinds:** C# 3.0 / .NET Framework 3.5 (2007)

**LINQ** (Language Integrated Query) laat je toe om op een uniforme manier data te bevragen, ongeacht de databron (arrays, lijsten, XML, databases, ...).

LINQ biedt twee syntaxvormen:

- **Query syntax**: lijkt op SQL
- **Method syntax**: ketent methodes aan elkaar

**Voorbeeld van LINQ met query syntax**

```csharp
int[] getallen = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

var evenGetallen = from g in getallen
                   where g % 2 == 0
                   select g;

foreach (int getal in evenGetallen)
{
    Console.WriteLine(getal);  // Output: 2, 4, 6, 8, 10
}
```
**1.** `from` definieert de databron en een range variable.
**2.** `where` filtert de elementen.
**3.** `select` bepaalt wat wordt teruggegeven.

**Hetzelfde met method syntax**

```csharp
int[] getallen = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

var evenGetallen = getallen.Where(g => g % 2 == 0);

foreach (int getal in evenGetallen)
{
    Console.WriteLine(getal);
}
```

**LINQ met objecten**

```csharp
List<Student> studenten = new List<Student>
{
    new Student { Naam = "Jan", Score = 85 },
    new Student { Naam = "Piet", Score = 72 },
    new Student { Naam = "Marie", Score = 91 }
};

var topStudenten = from s in studenten
                   where s.Score >= 80
                   orderby s.Score descending
                   select s;

foreach (var student in topStudenten)
{
    Console.WriteLine($"{student.Naam}: {student.Score}");
}
// Output:
// Marie: 91
// Jan: 85
```

> 💡 Meer info: [LINQ - Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/csharp/linq/)

## Lambda Expressions (C# 3.0)

**Beschikbaar sinds:** C# 3.0 / .NET Framework 3.5 (2007)

Een **lambda expression** is een compacte manier om een anonieme functie te schrijven. De `=>`-operator (de "goes to" operator of "pijl") scheidt de parameters van de body.

**Basisvoorbeelden van lambda expressions**

```csharp
// Lambda zonder parameters
Func<int> geefVijf = () => 5;
Console.WriteLine(geefVijf());  // Output: 5

// Lambda met één parameter (haakjes optioneel)
Func<int, int> verdubbel = x => x * 2;
Console.WriteLine(verdubbel(7));  // Output: 14

// Lambda met meerdere parameters
Func<int, int, int> optellen = (a, b) => a + b;
Console.WriteLine(optellen(3, 4));  // Output: 7
```

**Lambda met statement body**

Voor complexere logica gebruik je accolades:

```csharp
Func<int, int, int> berekenMax = (a, b) =>
{
    if (a > b)
        return a;
    else
        return b;
};

Console.WriteLine(berekenMax(10, 20));  // Output: 20
```

**Lambda's met LINQ**

```csharp
string[] namen = { "Jan", "Piet", "Klaas", "Marie" };

var langeNamen = namen.Where(n => n.Length > 3)
                      .OrderBy(n => n);

foreach (string naam in langeNamen)
{
    Console.WriteLine(naam);  // Output: Klaas, Marie, Piet
}
```

> 💡 Meer info: [Lambda expressions - Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/lambda-expressions)

## Anonymous Types (C# 3.0)

**Beschikbaar sinds:** C# 3.0 / .NET Framework 3.5 (2007)

**Anonymous types** laten toe om objecten te maken zonder expliciet een klasse te definiëren. De compiler genereert een klasse op de achtergrond.

Ze zijn vooral handig bij LINQ-queries waar je tijdelijke resultaten wil opslaan.

**Voorbeeld van anonymous types**

```csharp
var persoon = new { Voornaam = "Jan", Leeftijd = 30 };

Console.WriteLine(persoon.Voornaam);  // Output: Jan
Console.WriteLine(persoon.Leeftijd);  // Output: 30
```
**1.** Het type wordt door de compiler gegenereerd.

**Anonymous types in LINQ**

```csharp
List<Product> producten = new List<Product>
{
    new Product { Naam = "Laptop", Prijs = 999.99m },
    new Product { Naam = "Muis", Prijs = 29.99m }
};

var productInfo = from p in producten
                  select new { p.Naam, PrijsMetBTW = p.Prijs * 1.21m };

foreach (var item in productInfo)
{
    Console.WriteLine($"{item.Naam}: €{item.PrijsMetBTW:F2}");
}
```

> ❗ **Belangrijk**
>
> Anonymous types zijn:
>
> - Immutable (read-only properties)
> - Beperkt tot de scope waarin ze zijn gedeclareerd
> - Niet bruikbaar als return type van een methode

> 💡 Meer info: [Anonymous Types - Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/types/anonymous-types)

## Implicitly Typed Variables - var (C# 3.0)

**Beschikbaar sinds:** C# 3.0 / .NET Framework 3.5 (2007)

Met het `var` keyword kan de compiler zelf het type afleiden uit de toegekende waarde. Dit wordt **type inference** genoemd.

**Voorbeelden van var**

```csharp
var naam = "Jan";           // Compiler leidt af: string
var leeftijd = 25;          // Compiler leidt af: int
var prijs = 19.99m;         // Compiler leidt af: decimal
var actief = true;          // Compiler leidt af: bool

var getallen = new List<int> { 1, 2, 3 };  // List<int>
```

> ℹ️ **Opmerking**
>
> `var` is *niet* hetzelfde als `dynamic`. Bij `var` wordt het type bepaald tijdens compilatie en blijft daarna vast. De variabele is volledig strongly typed.
>
> ```csharp
> var x = 10;
> x = "tekst";  // Compilatiefout! x is een int.
> ```

**Wanneer var gebruiken?**

`var` is vooral nuttig wanneer:

- Het type duidelijk is uit de rechterkant
- Het type lang of complex is (generics, anonymous types)

```csharp
// Duidelijk - var is prima
var klant = new Klant();
var resultaten = producten.Where(p => p.Prijs > 100);

// Onduidelijk - beter expliciet type
var x = GetData();  // Wat is het type?
```

> 💡 Meer info: [Implicitly typed local variables - Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/implicitly-typed-local-variables)

## Dynamic Binding (C# 4.0)

**Beschikbaar sinds:** C# 4.0 / .NET Framework 4.0 (2010)

Het `dynamic` keyword schakelt type checking uit tijdens compilatie. Het type wordt pas tijdens runtime bepaald.

Dit is nuttig bij:

- Interoperabiliteit met COM-objecten
- Werken met dynamic languages (Python, JavaScript)
- Reflection-scenario's

**Voorbeeld van dynamic**

```csharp
dynamic waarde = 10;
Console.WriteLine(waarde);  // Output: 10

waarde = "Nu ben ik een string";
Console.WriteLine(waarde);  // Output: Nu ben ik een string

waarde = new List<int> { 1, 2, 3 };
Console.WriteLine(waarde.Count);  // Output: 3
```

> ⚠️ **Waarschuwing**
>
> Met `dynamic` verlies je IntelliSense en compile-time checking. Fouten worden pas zichtbaar tijdens runtime.
>
> ```csharp
> dynamic obj = "test";
> obj.NietBestaandeMethode();  // Compileert, maar crasht runtime!
> ```

> 💡 Meer info: [Using type dynamic - Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/csharp/advanced-topics/interop/using-type-dynamic)

## Tuples en Deconstruction (C# 7.0)

**Beschikbaar sinds:** C# 7.0 / .NET Framework 4.7 / .NET Core 2.0 (2017)

**Tuples** laten toe om meerdere waarden te groeperen zonder een aparte klasse te definiëren. Met **deconstruction** kan je tuple-elementen eenvoudig uitpakken naar individuele variabelen.

**Tuple aanmaken en gebruiken**

```csharp
// Tuple met benoemde elementen
(string naam, int leeftijd) persoon = ("Jan", 30);
Console.WriteLine($"{persoon.naam} is {persoon.leeftijd} jaar.");

// Tuple zonder namen (Item1, Item2, ...)
var punt = (10, 20);
Console.WriteLine($"X: {punt.Item1}, Y: {punt.Item2}");
```

**Tuple als returnwaarde**

```csharp
(int min, int max) BerekenMinMax(int[] getallen)
{
    return (getallen.Min(), getallen.Max());
}

int[] data = { 5, 2, 9, 1, 7 };
var resultaat = BerekenMinMax(data);
Console.WriteLine($"Min: {resultaat.min}, Max: {resultaat.max}");
```

**Deconstruction**

```csharp
var (minimum, maximum) = BerekenMinMax(data);
Console.WriteLine($"Min: {minimum}, Max: {maximum}");

// Negeren van waarden met discard
var (_, maxWaarde) = BerekenMinMax(data);
```
**1.** De tuple wordt "uitgepakt" in twee variabelen.
**2.** De underscore (`_`) negeert een waarde.

> 💡 Meer info: [Tuple types - Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/value-tuples)

## Pattern Matching - is en switch (C# 7.0+)

**Beschikbaar sinds:** C# 7.0 / .NET Framework 4.7 (2017), uitgebreid in C# 8.0 en C# 9.0

**Pattern matching** laat toe om te testen of een waarde overeenkomt met een bepaald patroon en tegelijk data te extraheren.

**Type pattern met is**

```csharp
object obj = "Hallo wereld";

if (obj is string tekst)
{
    Console.WriteLine($"Het is een string met lengte {tekst.Length}");
}
```
**1.** Als `obj` een `string` is, wordt het meteen toegekend aan `tekst`.

**Switch expression (C# 8.0)**

```csharp
string BeschrijfGetal(int getal) => getal switch
{
    0 => "nul",
    > 0 => "positief",
    < 0 => "negatief"
};

Console.WriteLine(BeschrijfGetal(5));   // Output: positief
Console.WriteLine(BeschrijfGetal(-3));  // Output: negatief
```

**Pattern combinators: and, or, not (C# 9.0)**

```csharp
string Classificeer(int score) => score switch
{
    >= 90 and <= 100 => "Uitstekend",
    >= 70 and < 90 => "Goed",
    >= 50 and < 70 => "Voldoende",
    < 50 and >= 0 => "Onvoldoende",
    _ => "Ongeldige score"
};

Console.WriteLine(Classificeer(85));  // Output: Goed
```
**1.** `and` combineert twee voorwaarden.
**2.** De underscore (`_`) is de default case.

```csharp
bool IsWeekend(DayOfWeek dag) => dag is DayOfWeek.Saturday or DayOfWeek.Sunday;

bool IsGeldig(int? waarde) => waarde is not null and > 0;
```

> 💡 Meer info: [Pattern matching - Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/functional/pattern-matching)

## Local Functions (C# 7.0)

**Beschikbaar sinds:** C# 7.0 / .NET Framework 4.7 / .NET Core 2.0 (2017)

**Local functions** zijn functies die je definieert **binnen** een andere methode. Ze zijn alleen zichtbaar binnen die methode.

**Voorbeeld van local function**

```csharp
int BerekenFactoriaal(int n)
{
    return Faculteit(n);
    
    int Faculteit(int getal)
    {
        if (getal <= 1) return 1;
        return getal * Faculteit(getal - 1);
    }
}

Console.WriteLine(BerekenFactoriaal(5));  // Output: 120
```
**1.** `Faculteit` is een local function, alleen toegankelijk binnen `BerekenFactoriaal`.

> ℹ️ **Opmerking**
>
> Voordelen van local functions:
>
> - Encapsulatie van helperfuncties
> - Toegang tot variabelen van de omringende methode
> - Betere performance dan lambda's in sommige gevallen

> 💡 Meer info: [Local functions - Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/local-functions)

## Out Variables Inline (C# 7.0)

**Beschikbaar sinds:** C# 7.0 / .NET Framework 4.7 / .NET Core 2.0 (2017)

Je kan `out`-variabelen nu declareren op de plaats waar je ze gebruikt, in plaats van vooraf.

**Vergelijking oud vs nieuw**

```csharp
// Vóór C# 7.0
int resultaat;
if (int.TryParse("123", out resultaat))
{
    Console.WriteLine(resultaat);
}

// Vanaf C# 7.0
if (int.TryParse("123", out int getal))
{
    Console.WriteLine(getal);
}

// Met var
if (int.TryParse("456", out var nummer))
{
    Console.WriteLine(nummer);
}
```
**1.** De variabele wordt inline gedeclareerd.

> 💡 Meer info: [out parameter modifier - Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/out-parameter-modifier)

## Ref Returns en Locals (C# 7.0)

**Beschikbaar sinds:** C# 7.0 / .NET Framework 4.7 / .NET Core 2.0 (2017)

Je kan een referentie naar een variabele teruggeven vanuit een methode, in plaats van een kopie van de waarde.

**Voorbeeld van ref return**

```csharp
int[] getallen = { 1, 2, 3, 4, 5 };

ref int VindEersteGroterDan(int[] array, int drempel)
{
    for (int i = 0; i < array.Length; i++)
    {
        if (array[i] > drempel)
            return ref array[i];
    }
    throw new InvalidOperationException("Niet gevonden");
}

ref int gevonden = ref VindEersteGroterDan(getallen, 3);
gevonden = 100;  // Wijzigt direct in de array!

Console.WriteLine(string.Join(", ", getallen));  // Output: 1, 2, 3, 100, 5
```
**1.** Geeft een referentie terug naar het array-element.
**2.** De referentie wordt opgeslagen in een `ref`-variabele.

> ⚠️ **Waarschuwing**
>
> Gebruik `ref` returns voorzichtig. Ze kunnen leiden tot onverwachte side effects als je niet oplet.

> 💡 Meer info: [Ref returns - Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/statements/jump-statements#ref-returns)

## Default Literal (C# 7.1)

**Beschikbaar sinds:** C# 7.1 / .NET Core 2.0 (2017)

Het `default` keyword kan gebruikt worden zonder het type expliciet te vermelden wanneer de compiler het type kan afleiden.

**Voorbeelden van default literal**

```csharp
// Vóór C# 7.1
int getal = default(int);        // 0
string tekst = default(string);  // null

// Vanaf C# 7.1
int getal = default;             // 0
string tekst = default;          // null
bool vlag = default;             // false

// Bij methode-parameters
void Methode(int waarde = default)
{
    Console.WriteLine(waarde);  // Output: 0
}

// Bij generics
T GeefDefault<T>() => default;
```

> 💡 Meer info: [default operator - Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/default)

## Using Declaration (C# 8.0)

**Beschikbaar sinds:** C# 8.0 / .NET Core 3.0 (2019)

Een `using` declaratie (zonder blok) zorgt ervoor dat een disposable object automatisch wordt opgeruimd aan het einde van de scope.

**Vergelijking using statement vs declaration**

```csharp
// Klassieke using statement (met blok)
using (StreamReader reader = new StreamReader("bestand.txt"))
{
    string inhoud = reader.ReadToEnd();
    Console.WriteLine(inhoud);
}  // reader.Dispose() wordt hier aangeroepen

// Using declaration (vanaf C# 8.0)
using StreamReader reader = new StreamReader("bestand.txt");
string inhoud = reader.ReadToEnd();
Console.WriteLine(inhoud);
// reader.Dispose() wordt aangeroepen aan het einde van de methode/scope
```

> ℹ️ **Opmerking**
>
> De using declaration is compacter en vermindert nesting, maar het object blijft langer bestaan (tot het einde van de scope).

> 💡 Meer info: [using statement - Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/statements/using)

## Indices en Ranges (C# 8.0)

**Beschikbaar sinds:** C# 8.0 / .NET Core 3.0 (2019)

Met de `^` operator (index from end) en `..` operator (range) kan je eenvoudig elementen vanaf het einde aanspreken en slices maken.

**Index from end met ^**

```csharp
string[] dagen = { "ma", "di", "wo", "do", "vr", "za", "zo" };

Console.WriteLine(dagen[^1]);  // Output: zo (laatste element)
Console.WriteLine(dagen[^2]);  // Output: za (voorlaatste)

Index laatsteIndex = ^1;
Console.WriteLine(dagen[laatsteIndex]);  // Output: zo
```

**Ranges met ..**

```csharp
string[] dagen = { "ma", "di", "wo", "do", "vr", "za", "zo" };

string[] werkdagen = dagen[0..5];     // ma, di, wo, do, vr
string[] weekend = dagen[5..];        // za, zo
string[] midden = dagen[2..5];        // wo, do, vr
string[] laatsteDrie = dagen[^3..];   // vr, za, zo

// Ook op strings
string tekst = "Hallo wereld";
string sub = tekst[0..5];  // "Hallo"
```

> 💡 Meer info: [Indices and ranges - Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/member-access-operators#range-operator-)

## Default Interface Methods (C# 8.0)

**Beschikbaar sinds:** C# 8.0 / .NET Core 3.0 (2019)

Interfaces kunnen nu standaardimplementaties bevatten. Klassen die de interface implementeren hoeven deze methodes niet te overschrijven.

**Voorbeeld van default interface method**

```csharp
public interface ILogger
{
    void Log(string bericht);
    
    void LogError(string bericht)
    {
        Log($"ERROR: {bericht}");
    }
}

public class ConsoleLogger : ILogger
{
    public void Log(string bericht)
    {
        Console.WriteLine(bericht);
    }
    // LogError hoeft niet geïmplementeerd te worden
}
```
**1.** Standaardimplementatie in de interface.

```csharp
ILogger logger = new ConsoleLogger();
logger.Log("Normaal bericht");
logger.LogError("Er ging iets mis");  // Gebruikt de default implementatie
```

> 💡 Meer info: [Default interface members - Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/interface#default-interface-members)

## Records (C# 9.0)

**Beschikbaar sinds:** C# 9.0 / .NET 5 (2020)

Een `record` is een reference type dat speciaal ontworpen is voor immutable data. Het biedt automatisch: equality by value, `ToString()`, deconstruction en meer.

**Eenvoudige record declaratie**

```csharp
public record Persoon(string Voornaam, string Achternaam);
```
**1.** Deze ene regel genereert een complete klasse met constructor, properties, Equals, GetHashCode, ToString, etc.

```csharp
var p1 = new Persoon("Jan", "Janssen");
var p2 = new Persoon("Jan", "Janssen");

Console.WriteLine(p1);           // Output: Persoon { Voornaam = Jan, Achternaam = Janssen }
Console.WriteLine(p1 == p2);     // Output: True (value equality!)
```

**With-expression voor kopieën**

```csharp
var p1 = new Persoon("Jan", "Janssen");
var p2 = p1 with { Voornaam = "Piet" };

Console.WriteLine(p2);  // Output: Persoon { Voornaam = Piet, Achternaam = Janssen }
```
**1.** Maakt een kopie met aangepaste waarde(s).

> 💡 Meer info: [Records - Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/record)

## Top-Level Statements (C# 9.0)

**Beschikbaar sinds:** C# 9.0 / .NET 5 (2020)

Je kan een programma schrijven zonder expliciet een `Main`-methode te definiëren. De code op het hoogste niveau vormt automatisch het entry point.

**Klassiek C#-programma**

```csharp
using System;

namespace MijnApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hallo wereld!");
        }
    }
}
```

**Met top-level statements**

```csharp
Console.WriteLine("Hallo wereld!");
```

Dat is het volledige programma! De compiler genereert de rest automatisch.

> ℹ️ **Opmerking**
>
> Top-level statements zijn ideaal voor kleine programma's, scripts en leermateriaal. Voor grotere applicaties blijft de klassieke structuur overzichtelijker.

> 💡 Meer info: [Top-level statements - Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/program-structure/top-level-statements)

## Target-Typed new (C# 9.0)

**Beschikbaar sinds:** C# 9.0 / .NET 5 (2020)

Als het type duidelijk is uit de context, kan je `new()` gebruiken zonder het type te herhalen.

**Voorbeelden van target-typed new**

```csharp
// Het type is duidelijk uit de declaratie
List<string> namen = new();
Dictionary<int, string> lookup = new();

// Bij field initialisatie
private List<int> _getallen = new();

// Als parameter
void Verwerk(List<int> data) { }
Verwerk(new() { 1, 2, 3 });

// Vergelijking
List<string> oud = new List<string>();         // Oudere stijl
List<string> nieuw = new();                    // C# 9.0+ stijl
```
**1.** De compiler weet dat `new()` een `List<string>` moet zijn.

> 💡 Meer info: [Target-typed new - Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/new-operator#target-typed-new)

## Global Usings (C# 10)

**Beschikbaar sinds:** C# 10 / .NET 6 (2021)

Met `global using` declareer je een using directive die geldt voor alle bestanden in het project.

**Voorbeeld van global usings**

**Bestand: GlobalUsings.cs**
```csharp
global using System;
global using System.Collections.Generic;
global using System.Linq;
global using System.Threading.Tasks;
```

Nu hoef je deze namespaces niet meer in elk bestand te importeren.

> ℹ️ **Opmerking**
>
> .NET 6+ projecten hebben standaard __implicit usings__ ingeschakeld, wat automatisch veelgebruikte namespaces importeert. Dit kan je uitschakelen in het project-bestand met `<ImplicitUsings>disable</ImplicitUsings>`.

> 💡 Meer info: [Global using directive - Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/using-directive#global-modifier)

## File-Scoped Namespaces (C# 10)

**Beschikbaar sinds:** C# 10 / .NET 6 (2021)

In plaats van namespace met accolades, kan je een file-scoped namespace gebruiken. Dit bespaart een niveau van inspringing.

**Vergelijking**

```csharp
// Klassiek (met accolades)
namespace MijnApp.Models
{
    public class Klant
    {
        public string Naam { get; set; }
    }
}

// File-scoped (C# 10)
namespace MijnApp.Models;

public class Klant
{
    public string Naam { get; set; }
}
```

> 💡 Meer info: [File-scoped namespace - Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/namespace)

## Record Structs (C# 10)

**Beschikbaar sinds:** C# 10 / .NET 6 (2021)

Naast `record` (reference type) kan je nu ook `record struct` gebruiken voor value types met dezelfde voordelen.

**Voorbeeld van record struct**

```csharp
public record struct Punt(int X, int Y);

var p1 = new Punt(3, 4);
var p2 = new Punt(3, 4);

Console.WriteLine(p1);           // Output: Punt { X = 3, Y = 4 }
Console.WriteLine(p1 == p2);     // Output: True

// Record struct is standaard mutable
p1.X = 10;
Console.WriteLine(p1);           // Output: Punt { X = 10, Y = 4 }
```

> ℹ️ **Opmerking**
>
> Gebruik `readonly record struct` voor immutable value types:
> ```csharp
> public readonly record struct Punt(int X, int Y);
> ```

> 💡 Meer info: [Record struct - Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/record#record-struct)

## Verbeterde String Interpolation (C# 10)

**Beschikbaar sinds:** C# 10 / .NET 6 (2021)

String interpolation is geoptimaliseerd voor betere performance. Interpolated string handlers maken het mogelijk om strings efficiënter samen te stellen.

**Voorbeeld**

```csharp
int x = 10;
int y = 20;

// Newlines in interpolation expressions
string bericht = $"De berekening: {x
    + y
    + 100}";

Console.WriteLine(bericht);  // Output: De berekening: 130
```

> 💡 Meer info: [String interpolation - Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/tokens/interpolated)

## Const Interpolated Strings (C# 10)

**Beschikbaar sinds:** C# 10 / .NET 6 (2021)

Interpolated strings kunnen nu `const` zijn, mits alle onderdelen ook constanten zijn.

**Voorbeeld**

```csharp
const string Voornaam = "Jan";
const string Achternaam = "Janssen";
const string VolledigeNaam = $"{Voornaam} {Achternaam}";

Console.WriteLine(VolledigeNaam);  // Output: Jan Janssen
```
**1.** Toegestaan omdat zowel `Voornaam` als `Achternaam` const zijn.

> 💡 Meer info: [Interpolated strings - Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/tokens/interpolated)

## Raw String Literals (C# 11)

**Beschikbaar sinds:** C# 11 / .NET 7 (2022)

Met drie of meer aanhalingstekens (`"""`) maak je **raw string literals**. Hierbij hoef je speciale tekens niet te escapen.

**Voorbeeld van raw strings**

```csharp
string json = """
    {
        "naam": "Jan",
        "leeftijd": 30,
        "actief": true
    }
    """;

Console.WriteLine(json);
```

De inspringing van de sluitende `"""` bepaalt hoeveel witruimte wordt verwijderd.

**Raw string met interpolation**

```csharp
string naam = "Jan";
int leeftijd = 30;

string json = $$"""
    {
        "naam": "{{naam}}",
        "leeftijd": {{leeftijd}}
    }
    """;
```

Het aantal `$`-tekens bepaalt hoeveel accolades nodig zijn voor interpolatie.

> 💡 Meer info: [Raw string literals - Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/tokens/raw-string)

## Required Members (C# 11)

**Beschikbaar sinds:** C# 11 / .NET 7 (2022)

Met het `required` keyword dwing je af dat bepaalde properties geïnitialiseerd moeten worden bij het aanmaken van een object.

**Voorbeeld van required members**

```csharp
public class Persoon
{
    public required string Voornaam { get; set; }
    public required string Achternaam { get; set; }
    public int? Leeftijd { get; set; }             // Optioneel
}
```
**1.** `required` dwingt initialisatie af.

```csharp
// Dit werkt
var p1 = new Persoon 
{ 
    Voornaam = "Jan", 
    Achternaam = "Janssen" 
};

// Dit geeft een compilatiefout
var p2 = new Persoon { Voornaam = "Jan" };  // Error: Achternaam is required
```

> 💡 Meer info: [required modifier - Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/required)

## Generic Math / Static Abstract Interface Members (C# 11)

**Beschikbaar sinds:** C# 11 / .NET 7 (2022)

Interfaces kunnen nu statische abstracte leden definiëren. Dit maakt **generic math** mogelijk: generieke methodes die werken met numerieke operaties.

**Voorbeeld van generic math**

```csharp
using System.Numerics;

T Som<T>(T[] getallen) where T : INumber<T>
{
    T totaal = T.Zero;
    foreach (T getal in getallen)
    {
        totaal += getal;
    }
    return totaal;
}

int[] integers = { 1, 2, 3, 4, 5 };
double[] doubles = { 1.5, 2.5, 3.5 };

Console.WriteLine(Som(integers));  // Output: 15
Console.WriteLine(Som(doubles));   // Output: 7.5
```
**1.** `INumber<T>` constraint garandeert dat `T` een numeriek type is.
**2.** `T.Zero` is een statisch abstract lid.
**3.** De `+` operator werkt op alle numerieke types.

> 💡 Meer info: [Generic math - Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/tutorials/static-virtual-interface-members)

## List Patterns (C# 11)

**Beschikbaar sinds:** C# 11 / .NET 7 (2022)

Met list patterns kan je patronen matchen op arrays en lijsten.

**Voorbeelden van list patterns**

```csharp
int[] getallen = { 1, 2, 3, 4, 5 };

// Exacte match
if (getallen is [1, 2, 3, 4, 5])
    Console.WriteLine("Exacte match!");

// Met variabelen
if (getallen is [var eerste, .., var laatste])
    Console.WriteLine($"Eerste: {eerste}, Laatste: {laatste}");

// In switch
string Beschrijf(int[] arr) => arr switch
{
    [] => "Leeg",
    [var x] => $"Eén element: {x}",
    [var x, var y] => $"Twee elementen: {x} en {y}",
    [var x, .., var z] => $"Van {x} tot {z}",
    _ => "Anders"
};
```
**1.** `..` matcht nul of meer elementen.

> 💡 Meer info: [List patterns - Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/patterns#list-patterns)

## UTF-8 String Literals (C# 11)

**Beschikbaar sinds:** C# 11 / .NET 7 (2022)

Met het `u8` suffix maak je een `ReadOnlySpan<byte>` met UTF-8 gecodeerde tekst. Dit is efficiënter voor netwerk- en bestandsoperaties.

**Voorbeeld**

```csharp
ReadOnlySpan<byte> utf8Data = "Hallo wereld"u8;

// Vergelijking
string tekst = "Hallo";              // UTF-16 encoded
ReadOnlySpan<byte> bytes = "Hallo"u8; // UTF-8 encoded, geen allocatie
```
**1.** Het `u8` suffix maakt een UTF-8 byte span.

> 💡 Meer info: [UTF-8 string literals - Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/reference-types#utf-8-string-literals)

## Primary Constructors voor Classes en Structs (C# 12)

**Beschikbaar sinds:** C# 12 / .NET 8 (2023)

Classes en structs kunnen nu primary constructors hebben (eerder alleen beschikbaar voor records).

**Voorbeeld van primary constructor**

```csharp
public class Persoon(string voornaam, string achternaam)
{
    public string Voornaam { get; } = voornaam;
    public string Achternaam { get; } = achternaam;
    
    public string VolledigeNaam => $"{voornaam} {achternaam}";
}
```
**1.** De constructor-parameters staan direct bij de klassenaam.
**2.** Parameters zijn beschikbaar in de hele klasse.

```csharp
var p = new Persoon("Jan", "Janssen");
Console.WriteLine(p.VolledigeNaam);  // Output: Jan Janssen
```

> 💡 Meer info: [Primary constructors - Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/tutorials/primary-constructors)

## Collection Expressions (C# 12)

**Beschikbaar sinds:** C# 12 / .NET 8 (2023)

Een uniforme syntax voor het initialiseren van collecties met vierkante haken `[]`.

**Voorbeelden**

```csharp
// Arrays
int[] getallen = [1, 2, 3, 4, 5];

// Lists
List<string> namen = ["Jan", "Piet", "Klaas"];

// Spans
Span<int> span = [10, 20, 30];

// Spread operator
int[] eerste = [1, 2, 3];
int[] tweede = [4, 5, 6];
int[] gecombineerd = [..eerste, ..tweede];
Console.WriteLine(string.Join(", ", gecombineerd));  // 1, 2, 3, 4, 5, 6

// Lege collectie
List<int> leeg = [];
```
**1.** De spread operator `..` voegt elementen samen.

> 💡 Meer info: [Collection expressions - Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/collection-expressions)

## Inline Arrays (C# 12)

**Beschikbaar sinds:** C# 12 / .NET 8 (2023)

Inline arrays zijn fixed-size arrays in structs, handig voor high-performance scenario's zonder heap allocaties.

**Voorbeeld**

```csharp
[System.Runtime.CompilerServices.InlineArray(5)]
public struct Buffer5
{
    private int _element;
}

Buffer5 buffer = new();
buffer[0] = 10;
buffer[1] = 20;
buffer[2] = 30;

foreach (int waarde in buffer)
{
    Console.WriteLine(waarde);
}
```
**1.** Slechts één veld declareren; de array wordt gegenereerd.

> 💡 Meer info: [Inline arrays - Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/struct#inline-arrays)

## Alias Any Type (C# 12)

**Beschikbaar sinds:** C# 12 / .NET 8 (2023)

De `using` alias directive kan nu elk type aliassen, inclusief tuples, pointers en arrays.

**Voorbeelden**

```csharp
using Punt = (int X, int Y);
using Naam = string;
using GetalllenLijst = System.Collections.Generic.List<int>;

Punt locatie = (10, 20);
Console.WriteLine($"X: {locatie.X}, Y: {locatie.Y}");

Naam voornaam = "Jan";
```
**1.** Een tuple type met een alias.

> 💡 Meer info: [Using alias - Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/using-directive#using-alias)

## Params voor Collections (C# 13)

**Beschikbaar sinds:** C# 13 / .NET 9 (2024)

Het `params` keyword werkt nu niet alleen met arrays, maar ook met andere collectietypes zoals `Span<T>`, `List<T>` en `IEnumerable<T>`.

**Voorbeeld**

```csharp
void ToonAlles(params IEnumerable<string> items)
{
    foreach (var item in items)
    {
        Console.WriteLine(item);
    }
}

ToonAlles("een", "twee", "drie");

void SomSpan(params ReadOnlySpan<int> getallen)
{
    int totaal = 0;
    foreach (int g in getallen)
        totaal += g;
    Console.WriteLine($"Som: {totaal}");
}

SomSpan(1, 2, 3, 4, 5);  // Output: Som: 15
```
**1.** `params` met `IEnumerable<T>` in plaats van alleen arrays.

> 💡 Meer info: [params keyword - Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/params)

## Lock Object Type (C# 13)

**Beschikbaar sinds:** C# 13 / .NET 9 (2024)

Het nieuwe `System.Threading.Lock` type biedt een efficiëntere manier voor synchronisatie dan `lock` op een `object`.

**Voorbeeld**

```csharp
using System.Threading;

public class TellerService
{
    private readonly Lock _lock = new();
    private int _teller = 0;

    public void Verhoog()
    {
        lock (_lock)
        {
            _teller++;
        }
    }

    public int Waarde => _teller;
}
```
**1.** Het nieuwe `Lock` type.
**2.** `lock` herkent dit type en gebruikt een geoptimaliseerd pad.

> 💡 Meer info: [Lock object - Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-13#new-lock-object)

## \e Escape Sequence (C# 13)

**Beschikbaar sinds:** C# 13 / .NET 9 (2024)

De `\e` escape sequence staat voor het ESCAPE-karakter (Unicode U+001B), vaak gebruikt voor ANSI terminal kleuren.

**Voorbeeld**

```csharp
// ANSI escape codes voor terminal kleuren
string rood = "\e[31m";
string reset = "\e[0m";

Console.WriteLine($"{rood}Dit is rode tekst{reset}");

// Vóór C# 13 moest je schrijven:
string oudRood = "\u001b[31m";  // Of: "\x1b[31m"
```

> 💡 Meer info: [Escape character - Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-13#escape-character)

## Implicit Indexer Access in Object Initializers (C# 13)

**Beschikbaar sinds:** C# 13 / .NET 9 (2024)

In object initializers kan je nu de `^` (from end) operator gebruiken zonder expliciet de collectie te noemen.

**Voorbeeld**

```csharp
public class Buffer
{
    public int[] Data { get; set; } = new int[10];
}

var buffer = new Buffer
{
    Data =
    {
        [0] = 1,
        [1] = 2,
        [^1] = 99
    }
};

Console.WriteLine(buffer.Data[^1]);  // Output: 99
```
**1.** `^1` wijst naar het laatste element.

> 💡 Meer info: [Implicit indexer access - Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-13#implicit-indexer-access-in-object-initializers)

## Partial Properties (C# 13)

**Beschikbaar sinds:** C# 13 / .NET 9 (2024)

Properties kunnen nu `partial` zijn, vergelijkbaar met partial methods. Dit is vooral nuttig bij source generators.

**Voorbeeld**

```csharp
public partial class DataModel
{
    public partial string Naam { get; set; }
}

public partial class DataModel
{
    private string _naam = "";
    
    public partial string Naam
    {
        get => _naam;
        set => _naam = value ?? "";
    }
}
```
**1.** Declaratie van de partial property.
**2.** Implementatie van de partial property.

> 💡 Meer info: [Partial properties - Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-13#partial-properties)

## Field Keyword (C# 14)

**Beschikbaar sinds:** C# 14 / .NET 10 (2025)

Het contextuele `field` keyword geeft toegang tot het automatisch gegenereerde backing field van een property, zonder dat je het zelf moet declareren.

**Vergelijking zonder en met field keyword**

```csharp
// Vóór C# 14: expliciet backing field nodig
public class Persoon
{
    private string _naam;
    
    public string Naam
    {
        get => _naam;
        set => _naam = value ?? throw new ArgumentNullException(nameof(value));
    }
}

// Met C# 14: field keyword
public class Persoon
{
    public string Naam
    {
        get;
        set => field = value ?? throw new ArgumentNullException(nameof(value));
    }
}
```
**1.** `field` verwijst naar het compiler-gegenereerde backing field.

**Lazy initialization met field**

```csharp
public class ConfigReader
{
    public string FilePath
    {
        get => field ??= "data/config.json";
        set => field = value ?? throw new ArgumentNullException(nameof(value));
    }
}
```
**1.** Combinatie van `field` met null-coalescing assignment.

> ℹ️ **Opmerking**
>
> Als je al een variabele genaamd `field` hebt, gebruik dan `@field` of `this.field` om disambiguatie te forceren.

> 💡 Meer info: [Field keyword - Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-14#field-backed-properties)

## Extension Members (C# 14)

**Beschikbaar sinds:** C# 14 / .NET 10 (2025)

Naast extension methods kan je nu ook extension properties, operators en statische members definiëren via **extension blocks**.

**Klassieke extension method**

```csharp
public static class StringExtensions
{
    public static int WoordTelling(this string tekst)
    {
        return tekst.Split([' ', '.', '?'], StringSplitOptions.RemoveEmptyEntries).Length;
    }
}
```

**Extension block (C# 14)**

```csharp
public static class StringExtensions
{
    extension(string tekst)
    {
        public int WoordAantal => tekst.Split([' ', '.', '?'],
            StringSplitOptions.RemoveEmptyEntries).Length;
            
        public bool IsLeegOfWitruimte => string.IsNullOrWhiteSpace(tekst);
        
        public string Afkappen(int maxLengte)
        {
            return tekst.Length <= maxLengte ? tekst : tekst[..maxLengte] + "...";
        }
    }
}
```
**1.** De `extension` block definieert voor welk type de members gelden.
**2.** Extension property - geen `this` keyword nodig.
**3.** Extension method binnen de block.

```csharp
string tekst = "Dit is een lange testzin.";

Console.WriteLine(tekst.WoordAantal);        // Output: 5
Console.WriteLine(tekst.IsLeegOfWitruimte);  // Output: False
Console.WriteLine(tekst.Afkappen(10));       // Output: Dit is een...
```

**Static extension members**

```csharp
public static class IntExtensions
{
    extension(int)
    {
        public static int MaxWaarde => int.MaxValue;
        public static bool IsPriem(int getal) { /* ... */ }
    }
}

// Gebruik als static member van int
Console.WriteLine(int.MaxWaarde);  // Via extension
```
**1.** Zonder parameternaam krijg je static extensions.

> 💡 Meer info: [Extension members - Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-14#extension-members)

## Partial Constructors (C# 14)

**Beschikbaar sinds:** C# 14 / .NET 10 (2025)

Constructors kunnen nu `partial` zijn, vergelijkbaar met partial methods en properties. Dit is bijzonder nuttig bij source generators.

**Voorbeeld**

```csharp
// Bestand 1: Declaratie
public partial class Gebruiker
{
    public partial Gebruiker(string naam);
}

// Bestand 2: Implementatie
public partial class Gebruiker
{
    public string Naam { get; }
    
    public partial Gebruiker(string naam)
    {
        Naam = naam ?? throw new ArgumentNullException(nameof(naam));
    }
}
```
**1.** Declarerende partial constructor.
**2.** Implementerende partial constructor.

> ℹ️ **Opmerking**
>
> - Alleen de implementerende declaratie mag een constructor initializer (`: this()` of `: base()`) bevatten.
> - Slechts één partial type declaratie mag de primary constructor syntax gebruiken.

> 💡 Meer info: [Partial constructors - Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-14#partial-events-and-constructors)

## Partial Events (C# 14)

**Beschikbaar sinds:** C# 14 / .NET 10 (2025)

Events kunnen nu ook `partial` zijn. De implementerende declaratie moet zowel `add` als `remove` accessors bevatten.

**Voorbeeld**

```csharp
// Bestand 1: Declaratie
public partial class Downloader
{
    public partial event Action<string> DownloadVoltooid;
}

// Bestand 2: Implementatie
public partial class Downloader
{
    private Action<string>? _downloadHandler;
    
    public partial event Action<string> DownloadVoltooid
    {
        add => _downloadHandler += value;
        remove => _downloadHandler -= value;
    }
    
    public void StartDownload()
    {
        // ... download logica ...
        _downloadHandler?.Invoke("bestand.txt");
    }
}
```
**1.** Field-like event declaratie.

> 💡 Meer info: [Partial events - Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-14#partial-events-and-constructors)

## Referenties

- [What's new in C# - Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/)
- [C# Language Reference - Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/)
- [C# Programming Guide - Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/)
