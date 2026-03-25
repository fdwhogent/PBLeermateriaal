# GitHub Copilot in Visual Studio

## 1. Wat is GitHub Copilot?

GitHub Copilot is een AI-gestuurde programmeerassistent die rechtstreeks in Visual Studio werkt. Het helpt je onder meer bij het 
- schrijven van code; 
- begrijpen van bestaande code; 
- opsporen van fouten. 
 
⚠️ Copilot is geen vervanging voor je eigen programmeerkennis — het is een hulpmiddel dat je productiever maakt.  

## 2. Copilot Chat: het chatpaneel

### 2.1 Openen en gebruiken

Het chatpaneel is het zijvenster waarin je vragen kunt stellen aan Copilot. Je opent het via:

- **View > GitHub Copilot Chat**
- Of via het **Copilot-icoontje** rechtsboven in Visual Studio

Typ je vraag (_Ask_ mode) of opdracht (_Agent_ mode) in gewone taal (bijvoorbeeld Nederlands of Engels) en druk op Enter.  Copilot antwoordt met uitleg, codevoorbeelden, of suggesties.

### 2.2 Threads en context

Elke conversatie in het chatpaneel is een **thread**. Copilot onthoudt wat je eerder in dezelfde thread hebt gevraagd en gebruikt die geschiedenis als context voor volgende antwoorden. Je kunt dus follow-up vragen stellen zoals "en hoe zit het met negatieve getallen?" zonder alles opnieuw uit te leggen.

> **💡 Tip: Nieuwe thread starten**
>
> - Klik op **New Thread** (of `Ctrl+N`) om een nieuwe conversatie te starten.
> - Doe dit wanneer je aan een nieuw onderwerp begint. Een lange thread met gemengde onderwerpen maakt de antwoorden minder relevant (het _context window_ geraakt vervuild).
> - Verwijder eventueel vragen die niet het gewenste resultaat gaven, ook deze vervuilden de context.

**Let op:** chatgeschiedenis is vluchtig. Als je Visual Studio sluit, kan de thread verdwenen zijn bij het heropenen. Kopieer nuttige antwoorden dus altijd naar je code of notities.

### 2.3 Context meegeven via references

Copilot geeft betere antwoorden als het weet waar je het over hebt. Standaard ziet Copilot (lees: _neemt mee in het context window_) ondermeer je _Active document_, _Selection_ en _Solution_.  Maar je kunt extra context toevoegen via de **+ Reference** knop onderaan het chatvenster:

| Reference | Wat doet het? |
|-----------|---------------|
| **Files** | Laat je een specifiek bestand uit je solution kiezen |
| **Methods** | Laat je een specifieke method selecteren als context |
| **Output Window logs** | Stuurt build/debug output mee — mogelijks een goed idee bij foutmeldingen |
| **Upload Image** | Laat je een screenshot meesturen (bv. van een probleem/foutmelding) |

*Tip: gebruik in je prompt `#` gevolgd door een bestandsnaam (bv. `#Program.cs`) of code element (bv.`#Program` of `#Main` om vlot aan een bestand of code element te refereren.*

### 2.4 References-lijst: controleren wat Copilot heeft gebruikt

Bij elk antwoord dat Copilot geeft, kun je onderaan een **References**-sectie bekijken (soms als een inklapbaar lijstje). Hierin toont Copilot welke bronnen het heeft gebruikt om zijn antwoord samen te stellen — denk aan bestanden uit je solution, open tabs, en ook je custom instructions-bestand (zie verderop).

Dit is nuttig om te verifiëren:

- Of Copilot inderdaad naar het juiste bestand heeft gekeken
- Of je `copilot-instructions.md` (zie verderop) is meegestuurd (als dat bestand in de References staat, zijn de instructies actief)
- Welke context Copilot heeft meegenomen — als een belangrijk bestand ontbreekt, voeg het dan handmatig toe via `#` of de `+ Reference` knop

## 3. Code completions en Next Edit Suggestions

Naast de chatfunctionaliteit biedt Copilot ook twee vormen van **inline suggesties** die verschijnen terwijl je typt. Dit zijn geen chatberichten, maar suggesties die direct in je code-editor verschijnen.

### 3.1 Code completions (ghost text)

Dit is de meest zichtbare Copilot-functie: terwijl je typt, verschijnt er **grijze tekst** (ghost text) die voorspelt wat je als volgende wilt schrijven. Dit kan een halve regel zijn, maar ook een heel codeblok.

- **Accepteren:** druk op `Tab` om de suggestie over te nemen
- **Negeren:** typ gewoon verder, de suggestie verdwijnt
- **Gedeeltelijk accepteren:** gebruik `Ctrl+Rechter pijl` om woord per woord te accepteren, of `Ctrl+Pijl omlaag` om regel per regel te accepteren
- **Handmatig oproepen:** druk op `Alt+.` of `Alt+,` om suggesties op te vragen als ze niet automatisch verschijnen

Code completions werken alleen op de positie van je cursor — ze vullen aan waar je nu typt.

### 3.2 Next Edit Suggestions (NES)

Next Edit Suggestions gaan een stap verder dan code completions. NES voorspelt niet alleen wat je op je cursorpositie wilt typen, maar ook **welke wijzigingen je elders in je bestand** wilt aanbrengen op basis van de bewerkingen die je net hebt gedaan.

**Hoe herken je NES?** Een **pijltje in de kantlijn** (gutter) geeft aan dat er een suggestie beschikbaar is op een andere locatie in je bestand. De suggestie wordt getoond als een diff-weergave (rood = oud, groen = nieuw).

**Hoe werkt het?**

1. Je maakt een wijziging (bv. je hernoemt een variabele)
2. NES detecteert dat dezelfde variabele elders in het bestand ook aangepast moet worden
3. Er verschijnt een pijltje in de kantlijn
4. Druk op `Tab` om naar de suggestie te navigeren
5. Druk nogmaals op `Tab` om de suggestie te accepteren

**Typische scenario's:**

- Je hernoemt een variabele → NES stelt voor om dezelfde naam overal in het bestand aan te passen
- Je voegt een parameter toe aan een method → NES past de aanroepen van die method aan
- Je wijzigt de logica van een conditie → NES past gerelateerde code aan
- Je maakt een typfout → NES vangt die op en stelt een correctie voor

### 3.3 Het verschil in een oogopslag

| | Code completions | Next Edit Suggestions (NES) |
|-|-------------------|------------------------------|
| **Waar?** | Alleen op je cursorpositie | Overal in het bestand |
| **Wat?** | Vult aan wat je nu typt | Voorspelt de volgende logische wijziging |
| **Wanneer?** | Terwijl je typt | Na een bewerking die gevolgen heeft elders |
| **Visueel** | Grijze ghost text | Pijltje in de kantlijn + diff-weergave |
| **Accepteren** | `Tab` | `Tab` (navigeren) + `Tab` (accepteren) |

## 4. Inline Chat: AI in je code-editor

Naast het chatpaneel kun je Copilot ook rechtstreeks in je code aanspreken. Dit heet **inline chat**.

### 4.1 Hoe activeren?

1. Zet je cursor op een regel (waar je code wil invoegen) of selecteer een blok code (die je wil bijsturen
2. Druk op `Alt + /` (standaard sneltoets in Visual Studio)
3. Er verschijnt een invoerveld ter plekke in je code

### 4.2 Verschil met het chatpaneel

Inline chat is specifiek ontworpen om code te **wijzigen of toe te voegen**. Het toont het resultaat als een diff-weergave: je ziet direct wat er zou veranderen (groen = toegevoegd, rood = verwijderd). Je klikt dan **Accept** of **Cancel**.

Het chatpaneel is beter geschikt voor algemene vragen en uitleg.

### 4.3 Voorbeeldprompts voor inline chat

- _"leg mij uit wat deze code doet"_
- _"voeg input validatie toe voor negatieve getallen"_
- _"voeg commentaar toe dat uitlegt wat deze loop doet"_
- _"hernoem de variabelen naar duidelijkere namen"_

## 5. Copilot Actions en Quick Actions

Visual Studio biedt meerdere manieren om Copilot snel aan te roepen zonder zelf een prompt te typen.

### 5.1 Copilot Actions (contextmenu)

Rechtsklik op je code om het contextmenu te openen. Onder het **Copilot-submenu** vind je voorgeconfigureerde acties:

- **Explain** — legt de geselecteerde code uit in het chatpaneel
- **Generate Comments** — genereert commentaar voor je code
- **Optimize Selection** — stelt optimalisaties voor als inline diff

*Opmerking: de beschikbare acties kunnen verschillen naargelang je Visual Studio-versie.*

**Hoe gebruiken?**

1. Selecteer een stuk code in de editor
2. Rechtsklik → zoek het Copilot-submenu
3. Kies een actie (bv. Explain)

> **💡 Tip: Explain als leertool**
>
> Selecteer code die je niet begrijpt en gebruik **Explain**. Dit is een uitstekende manier om te leren wat bestaande code doet, zonder de oplossing van je opdracht weg te geven.

### 5.2 Quick Actions (gloeilampje)

Naast het contextmenu toont Visual Studio soms een **gloeilampje** (lightbulb) naast je code — dat ken je al van de standaard Quick Actions en Refactorings. Copilot voegt hier extra opties aan toe:

- **Fix with Copilot** — verschijnt bij compile-fouten of waarschuwingen. Copilot analyseert de fout en stelt een oplossing voor via inline chat.
- **Implement with Copilot** — verschijnt wanneer je een method stub of interface moet implementeren. Copilot genereert de method body.

Deze acties verschijnen niet altijd — alleen wanneer Visual Studio detecteert dat Copilot kan bijspringen.

### 5.3 Andere Copilot-integraties in de editor

- **`///` voor documentatie** — typ `///` boven een method of klasse, en Copilot vult automatisch een XML-documentatiecommentaar aan op basis van de code.
- **Describe with Copilot** — zweef met je muis over een symbool (method, variabele) en klik op "Describe with Copilot" in de QuickInfo-popup voor een korte uitleg.

## 6. Slash commands en # references

In het chatpaneel kun je speciale commando's gebruiken die beginnen met `/` (slash commands) of `#` (references).

### 6.1 Slash commands

Typ `/` in het invoerveld om de lijst te zien. Enkele nuttige:

| Command | Wat doet het? |
|---------|---------------|
| `/explain` | Legt de geselecteerde code of het actieve bestand uit |
| `/fix` | Stelt een oplossing voor bij een fout |
| `/doc` | Genereert documentatie/commentaar |
| `/tests` | Genereert unit tests |
| `/help` | Toont een overzicht van beschikbare commando's |

### 6.2 # References

Zoals eerder vermeld, gebruik `#` om specifieke context mee te geven in je prompt:

- `#Program.cs` — verwijst naar een specifiek bestand
- `#CalculateBmi` — verwijst naar een specifieke method of klasse
- `@workspace` — verwijst naar je volledige solution

*Voorbeeld: "Waarom geeft `#Program.cs` een fout op regel 12?"*

## 7. Modelselectie

Copilot biedt meerdere AI-modellen aan. Elk model heeft eigen sterktes en een **kostenmultiplier** die bepaalt hoeveel van je maandelijks budget ("premium requests") het verbruikt.

### 7.1 Model wisselen

Onderaan het chatvenster zie je de naam van het huidige model (bv. "GPT-4.1"). Klik erop om een ander model te kiezen. Via **Manage Models** kun je modellen aan- of uitzetten om de lijst overzichtelijk te houden.

### 7.2 Hoe de multiplier werkt

Elk Copilot-plan geeft je een aantal **premium requests** per maand. De multiplier naast elk model bepaalt hoeveel er per interactie van je tegoed afgaat:

- **0x** = gratis, telt niet mee tegen je budget
- **0.25x–0.33x** = goedkoper dan standaard
- **1x** = standaardtarief (één premium request per interactie)
- **3x** = duur (drie premium requests per interactie)

Als je premium requests op zijn, val je automatisch terug op een van de gratis standaardmodellen.

### 7.3 Overzicht van modellen

Overzicht van enkel modellen...

#### Standaardmodellen (0x — gratis)

| Model | Sterktes | Wanneer gebruiken? |
|-------|----------|---------------------|
| **GPT-4.1** | Snelle, betrouwbare allrounder | Dagelijks gebruik, standaardvragen, code-uitleg |
| **GPT-4o** | Goed met afbeeldingen (multimodaal) | Als je een screenshot/afbeelding wilt meesturen |
| **GPT-5 mini** | Zeer snel, lichtgewicht | Triviale vragen, snelle autocompletions |

#### Premium modellen (1x)

| Model | Sterktes | Wanneer gebruiken? |
|-------|----------|---------------------|
| **Claude Sonnet 4 / 4.6** | Sterke uitleg, volgt instructies nauwgezet, goed Nederlands | Complexere uitleg, betere opvolging van custom instructions |
| **Gemini 2.5 Pro** | Enorm context window, goed in grote codebases | Analyse van grotere projecten |
| **GPT-5.2-Codex** | Geoptimaliseerd voor code-editing | Agent/edit mode taken |

#### Zware modellen (3x+)

| Model | Sterktes | Wanneer gebruiken? |
|-------|----------|---------------------|
| **Claude Opus 4.5 / 4.6** | Diep redeneren, complexe architectuur, subtiele bugs | Alleen bij echt complexe problemen — verbrandt snel je budget |

#### Auto-modus

**Auto** laat Copilot zelf het beste model kiezen op basis van beschikbaarheid. Het routeert alleen naar modellen met een multiplier van 0x tot 1x, dus het kiest nooit een duur 3x-model. Op een betaald plan krijg je bovendien 10% korting op de multiplier bij gebruik van Auto.

**Advies:** gebruik **Auto** of **GPT-4.1** als standaard. Schakel alleen over naar een premium model als je echt betere antwoorden nodig hebt.

### 7.4 Je usage controleren

Je kunt je verbruik van premium requests monitoren:

- Klik op de **Copilot-badge** rechtsboven in Visual Studio → kies **Copilot Usage** om te zien hoeveel premium requests je deze maand al hebt gebruikt
- Online: ga naar je [GitHub Copilot-instellingen](https://github.com/settings/copilot) en bekijk je verbruik onder **Billing**
- Als je premium requests op zijn, kun je nog steeds Copilot gebruiken met de standaardmodellen (0x) voor de rest van de maand

## 8. Custom instructions

### 8.1 Wat zijn custom instructions?

In de root van je repo neem je in een `.github` foler een  `copilot-instructions.md` markdown bestand op. Dit bestand bevat instructies die **automatisch** worden meegestuurd bij elke vraag die je aan Copilot stelt — zowel in het chatpaneel als bij inline chat.

De bestandsnaam en locatie zijn hard gecodeerd: het **moet** exact `.github/copilot-instructions.md` zijn in de root van je repository. Een bestand met een andere naam of op een andere locatie wordt genegeerd.

### 8.2 Wat staat erin voor dit vak?

Bij die custom instructions zou je kunnen opnemen:

- Dat Copilot in het **Nederlands** moet antwoorden
- Welke C#-concepten je al kent (en welke **niet** — zoals OOP, LINQ, exceptions)
- Dat Copilot **geen volledige oplossingen** mag geven, maar hints en gerichte vragen
- De **codeerstijl en naamgevingsconventies** die we hanteren (camelCase voor variabelen, PascalCase voor methods, K&R-accolades)

> ** Ter zijde: accolades**
>
> In de C#-wereld is Allman (openingsaccolade op nieuwe regel, op hetzelfde inspringniveau als de bijbehorende instructie) de standaardconventie — Visual Studio genereert by default code in die stijl, en de .NET-coderichtlijnen van Microsoft volgen die ook. JavaScript en TypeScript gebruiken daarentegen bijna universeel K&R (Kernighan & Ritchie), waarbij de openingsaccolade op dezelfde regel staat:. 

### 8.3 Verifiëren dat de instructies actief zijn

Custom instructions zijn niet zichtbaar in het chatvenster zelf. Om te controleren of ze actief zijn:

1. Stel een vraag aan Copilot in het chatpaneel
2. Bekijk de **References-lijst** onderaan het antwoord
3. Als `copilot-instructions.md` daar vermeld staat, zijn de instructies meegestuurd

**Als het bestand niet vermeld staat:**

- Controleer of het bestand op de juiste plek staat in je repository (`.github/copilot-instructions.md`)
- Ga naar **Tools > Options** > zoek "custom instructions"
- Vink aan: **Enable custom instructions to be loaded from .github/copilot-instructions.md files and added to requests**

## 9. Het Output Window als hulpbron

Visual Studio's Output Window bevat waardevolle informatie bij build errors en debug-sessies. Copilot kan deze informatie lezen en interpreteren.

### 9.1 Hoe gebruiken bij fouten?

1. Bouw je project (`Ctrl+Shift+B`) en bekijk de output
2. Selecteer de foutmelding(en) in het Output Window
3. Rechtsklik → _Explain with Copilot_ 
4. Of: gebruik de **+ Reference** knop in het chatpaneel en kies **Output Window logs**

Copilot analyseert dan de foutmeldingen en kan uitleggen wat er misgaat en hoe je het kunt oplossen.

Dit werkt niet alleen voor build output, maar ook voor debug-, test-, source control- en package manager-output.

## 10. Copilot-instellingen

### 10.1 Waar vind je de instellingen?

Er zijn twee wegen:

- **Via het Copilot-menu:** klik op de Copilot-badge rechtsboven → **Settings** → kies een submenu
- **Via Tools > Options:** navigeer naar **GitHub > Copilot** in het linkerpaneel

### 10.2 Belangrijke instellingen

#### Copilot Chat

| Instelling | Wat doet het? |
|------------|---------------|
| **Enable Agent mode in the chat pane** | Schakelt agent mode in — Copilot kan dan autonoom meerdere stappen uitvoeren, bestanden aanpassen, en commando's draaien |
| **Enable MCP server integration** | Laat Copilot verbinden met externe tools via het Model Context Protocol |
| **Enable Planning** | Copilot kan plannen maken en de voortgang bijhouden bij complexe taken in agent mode |
| **Enable View Plan Execution** | Toont een dashboard van wat Copilot van plan is te doen in agent mode |
| **Enable referencing your #solution** | Maakt `#solution` beschikbaar in het chatvenster, waarmee Copilot relevante delen van je solution kan doorzoeken |
| **Auto-Attach Active Document** | Stuurt automatisch je huidige bestand mee als context bij elke vraag (zonder dat je het handmatig moet toevoegen) |
| **Enable Copilot Coding agent (Preview)** | Schakelt de cloud-gebaseerde coding agent in die zelfstandig taken kan uitvoeren |

#### Copilot Completions (via Copilot-badge → Settings)

| Instelling | Wat doet het? |
|------------|---------------|
| **Enable Copilot Completions** | Schakelt de automatische ghost text-suggesties in/uit |
| **Enable Next Edit Suggestions** | Schakelt NES in/uit |


## 11. Praktische tips

### 11.1 Effectief prompts schrijven

- **Wees specifiek:** _"Waarom krijg ik een IndexOutOfRangeException in mijn for-loop?"_ is beter dan _"Mijn code werkt niet"_
- **Geef context mee:** gebruik `#` references naar je bestanden
- **Stel follow-up vragen:** _"En wat als de gebruiker niets invoert?"_
- **Vraag om uitleg, niet om oplossingen:** _"Leg uit wat er fout gaat"_ in plaats van _"Fix mijn code"_

### 11.2 Copilot als leerpartner

- Gebruik **Explain** om bestaande code te begrijpen
- Vraag Copilot om analogieën: _"Leg arrays uit alsof je het aan een niet-programmeur uitlegt"_
- Laat Copilot je code reviewen: _"Wat kan ik verbeteren aan deze method?"_
- Gebruik Copilot om te debuggen: stel de foutmelding als vraag en laat Copilot je in de juiste richting wijzen

### 11.3 Veelgemaakte fouten

- Copilot-antwoorden blindelings overnemen zonder ze te begrijpen
- Te vage prompts sturen (_"help"_, _"dit werkt niet"_)
- Vergeten om een nieuwe thread te starten bij een nieuw onderwerp
- Niet controleren welk model actief is (een duur model verbrandt onnodig budget)
- Niet nakijken in de References-lijst of Copilot de juiste bestanden heeft gebruikt

## 12. Sneltoetsen

| Sneltoets | Actie |
|-----------|-------|
| `Alt + /` | Inline chat openen (in de editor) |
| `Tab` | Code completion of NES-suggestie accepteren |
| `Esc` | Suggestie afwijzen |
| `Ctrl + Rechter pijl` | Completion woord per woord accepteren |
| `Ctrl + Pijl omlaag` | Completion regel per regel accepteren |
| `Alt + .` / `Alt + ,` | Handmatig completion opvragen / door suggesties bladeren |
| `/` (in chatpaneel) | Lijst van slash commands tonen |
| `#` (in chatpaneel) | Lijst van references/bestanden tonen |
