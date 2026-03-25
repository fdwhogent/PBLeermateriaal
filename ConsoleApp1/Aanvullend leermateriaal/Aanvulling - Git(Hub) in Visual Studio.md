# Git & GitHub met Visual Studio 2026

**Programmeren Basis**

---

## 1. Wat is versiebeheer?

Als programmeur werk je voortdurend aan code: je schrijft nieuwe functies, je past bestaande code aan, je lost bugs op. Soms werkt een aanpassing niet zoals verwacht en wil je terug naar een vorige versie. Of je wilt samenwerken met iemand anders, code uitwisselen en nagaan wie juist welke aanpassing heeft verricht. Precies hiervoor bestaat versiebeheer.

Versiebeheer (Engels: *version control*) is een systeem dat elke wijziging aan je bestanden bijhoudt. Het biedt twee grote voordelen:

- **Geschiedenis bijhouden:** elke wijziging wordt opgeslagen als een snapshot. Je kunt altijd terugkijken wat je wanneer veranderd hebt, en als iets niet werkt, kun je terugkeren naar een eerdere versie. Het is als een oneindig undo-systeem.
- **Code uitwisselen:** via een online kopie van je project (een zogenaamde *remote repository*) kun je je code delen met anderen. Een lector kan oefeningen klaarzetten die je kunt ophalen, en jij kunt je eigen werk uploaden zodat iemand anders het kan bekijken of er feedback op kan geven.

Versiebeheer is standaard in de professionele softwarewereld. Vrijwel elk bedrijf dat software ontwikkelt, gebruikt het dagelijks. Het is dus een vaardigheid die je sowieso nodig zult hebben.

---

## 2. Git

Git is niet het enige, maar wel het populairste versiebeheersysteem.

Git is gratis, open source, en werkt lokaal op je eigen computer. Dat betekent dat je geen internetverbinding nodig hebt om je versies bij te houden. Git slaat je volledige projectgeschiedenis op in een verborgen map (`.git`) binnen je projectfolder.

> **Belangrijk:** Git is de motor onder de motorkap. Het is de software die het eigenlijke versiebeheer doet. Git zelf heeft geen website of grafische interface — het is een programma dat je via commando's kunt aansturen (zie hiervoor het opleidingsonderdeel Digitale Werkomgeving 1/2), maar dat hoeft niet: Visual Studio doet dat voor jou.

---

## 3. GitHub

GitHub ([github.com](https://github.com)) is een online platform waar je je Git-repositories kunt opslaan en delen. Het is eigendom van Microsoft (overgenomen in 2018). GitHub voegt een webinterface, samenwerking en extra tools toe bovenop Git.

Je kunt GitHub vergelijken met OneDrive of Google Drive, maar dan specifiek ontworpen voor code. Het verschil: GitHub houdt niet gewoon bestanden bij, maar de volledige geschiedenis van elke wijziging.

Er bestaan ook alternatieven voor GitHub, zoals GitLab en Bitbucket. Ze doen vergelijkbare dingen, maar in deze cursus gebruiken we GitHub.

---

## 4. Wat is een repository?

Een repository (vaak afgekort tot *repo*) is de "container" voor je project. Een repository bevat al je bestanden én de volledige geschiedenis van alle wijzigingen die ooit zijn aangebracht.

Er zijn twee soorten:

- **Lokale repository:** de versie op jouw eigen computer (in de verborgen `.git`-map binnen je project).
- **Remote repository:** de versie op GitHub (online). Dit is de gedeelde versie die anderen kunnen bekijken of downloaden.

Een repository kan twee zichtbaarheden hebben:

- **Public:** iedereen op het internet kan de code bekijken en clonen. Maar niet iedereen kan er wijzigingen naar uploaden — dat kunnen alleen mensen die als collaborator zijn toegevoegd. Een public repo betekent dus niet dat iedereen zomaar je code kan aanpassen.
- **Private:** alleen jij en de mensen die je expliciet toegang geeft (collaborators) kunnen de repository zien. Voor opdrachten gebruiken we private repositories zodat je code niet publiek zichtbaar is.

---

## 5. Clone en fork

Er zijn twee manieren om een kopie te maken van een repository. Ze lijken op elkaar, maar er is een belangrijk verschil.

### Clone

Een clone kopieert een repository van GitHub naar je lokale computer. Je krijgt alle bestanden en de volledige geschiedenis. Na het clonen kun je lokaal werken: code bekijken, aanpassen, committen. Als je collaborator bent op de originele repo, kun je ook pushen (wijzigingen terugduwen naar de online repo). Als je dat niet bent, kun je alleen aan je lokale versie werken.

Clone is een Git-commando. Het werkt met elke Git-repository, ongeacht waar die gehost is.

### Fork

Een fork kopieert een repository van iemand anders naar jouw eigen GitHub-account. Je krijgt een volledig onafhankelijke repo op GitHub die je zelf beheert. Je kunt die vervolgens clonen naar je lokale computer om er effectief aan te werken.

Fork is geen Git-concept maar een GitHub-functie. Git zelf kent het woord "fork" niet. Het is specifiek bedoeld om met andermans code aan de slag te gaan zonder dat het origineel geraakt wordt.

### Vergelijking

|                          | Clone                              | Fork                                                      |
| ------------------------ | ---------------------------------- | --------------------------------------------------------- |
| **Wat gebeurt er?**      | Kopie van GitHub naar je pc        | Kopie van GitHub naar je eigen GitHub-account              |
| **Richting**             | Remote → lokaal                    | Remote → remote (daarna clone je naar lokaal)              |
| **Eigen repo op GitHub?**| Nee                                | Ja                                                        |
| **Kun je pushen?**       | Alleen als je collaborator bent    | Ja, naar je eigen fork                                    |


---

## 6. Belangrijke termen

Hieronder vind je de belangrijkste Git-termen die je in deze cursus tegenkomt. Je hoeft ze niet uit het hoofd te leren — je leert ze door ze te gebruiken.

| Term             | Betekenis                                                                                                                                                                                   |
| ---------------- | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| **clone**        | Een kopie maken van een remote repository (op GitHub) naar je lokale computer.                                                                                                              |
| **fork**         | Een kopie maken van iemand anders' repository naar je eigen GitHub-account. Je krijgt een eigen, onafhankelijke repo die je zelf beheert.                                                   |
| **commit**       | Een snapshot (momentopname) maken van je huidige code. Dit is als "opslaan met een beschrijving". Een commit bevat altijd een boodschap (*commit message*) die beschrijft wat je veranderd hebt. |
| **push**         | Je lokale commits uploaden naar de remote repository op GitHub. Pas na een push zijn je wijzigingen online zichtbaar voor anderen.                                                           |
| **pull**         | De nieuwste wijzigingen van de remote repository ophalen en samenvoegen met je lokale versie. Dit gebruik je als iemand anders (of jijzelf op een andere computer) iets heeft gewijzigd.     |
| **repository**   | De container voor je project: alle bestanden plus hun volledige wijzigingsgeschiedenis. Bestaat lokaal ("op je pc") en remote ("op GitHub").                                                  |
| **remote**       | De online versie van je repository.                                                                                                                                                         |
| **collaborator** | Iemand die je expliciet toevoegt aan je repository, waardoor die persoon ook kan pushen naar jouw repo.                                                                                     |
| **.gitignore**   | Een bestand dat aangeeft welke bestanden Git moet negeren (bv. gegenereerde bestanden, tijdelijke bestanden). Visual Studio maakt dit automatisch aan.                                      |
| **issue**        | Een ticket of discussiepunt op GitHub, gekoppeld aan een repository. Handig voor feedback, vragen of te melden problemen.                                                                   |
| **merge conflict** | Ontstaat wanneer twee personen (of jijzelf op twee plekken) dezelfde regels in hetzelfde bestand hebben gewijzigd. Git kan dan niet automatisch kiezen welke versie juist is.              |

---

## 7. De basisworkflow

De dagelijkse Git-workflow is eigenlijk heel eenvoudig en bestaat uit drie stappen:

1. **Werk aan je code** — schrijf, test, debug zoals je gewend bent.
2. **Commit** — maak een snapshot met een beschrijving van wat je gedaan hebt.
3. **Push** — upload je commits naar GitHub.

> **Onthoud:** Je moet altijd eerst committen vóór je kunt pushen. Een push zonder commit doet niets. Commit = lokaal opslaan van een snapshot. Push = dat snapshot uploaden naar GitHub.

---

## 8. Voorbereidingen (eenmalig)

### 8.1 GitHub-account aanmaken

1. Ga naar [github.com](https://github.com) en klik op "Sign up".
2. Kies een professionele gebruikersnaam (bv. voornaam-achternaam). Dit is zichtbaar voor anderen.
3. Gebruik bij voorkeur je school-e-mailadres.
4. Bevestig je account via de verificatie-e-mail.

Een gratis GitHub-account volstaat. Je kunt daarmee onbeperkt repositories aanmaken, zowel public als private.

Op [github.com/education/students](https://github.com/education/students) kan je registreren voor GitHub Education (GitHub Student Developer Pack), deze levert je een GitHub Copilot Student licentie op die meer opties biedt dan de free tier van GitHub Copilot (intensiever gebruik van Copilot mogelijk maakt).

### 8.2 GitHub koppelen aan Visual Studio 2026

1. Open Visual Studio 2026.
2. Ga naar **Help → Register Visual Studio**.
3. Add je GitHub-account.

Je hoeft dit maar één keer te doen. Visual Studio onthoudt je account.

### 8.3 Notificaties

GitHub stuurt standaard e-mailnotificaties wanneer iemand je toevoegt als collaborator, je tagt in een issue (`@gebruikersnaam`), of reageert op een issue waar je bij betrokken bent. Je hoeft hier niets voor te configureren — het werkt automatisch zolang je e-mailadres geverifieerd is (wat onderdeel is van de registratie).

> **Tip:** Controleer af en toe je spam-folder als je geen meldingen lijkt te ontvangen. E-mails van GitHub (`noreply@github.com`) worden soms als spam gefilterd.

---

## 9. Code ophalen van de lector of iemand anders (clonen)

De lector heeft een repository op GitHub klaargezet met oefeningen of voorbeeldcode. Jij maakt hiervan een lokale kopie via clone.

### Stappen

1. De lector geeft je een URL, kopieer deze: `https://github.com/fdwhogent/Hello-Git.git`
2. In Visual Studio: **Git → Clone Repository**.
3. Plak de URL in het veld "Repository location".
4. Kies een lokale map waar je het project wilt bewaren.
5. Klik op **Clone**. Visual Studio downloadt alle bestanden en opent het project.

Je hebt nu een lokale kopie. Je kunt de code bekijken, aanpassen en ermee oefenen. Je wijzigingen blijven op jouw computer — je kunt niets kapotmaken aan de repository van de lector.

### Updates ophalen

Als de lector nieuwe code toevoegt, kun je die ophalen via **Git → Pull**. Dit haalt de nieuwste versie op en voegt die samen met je lokale bestanden.

> **Let op:** Als je zelf bestanden hebt aangepast die de lector ook heeft gewijzigd, kan er een merge conflict ontstaan. De veiligste aanpak: bewaar je eigen werk in aparte bestanden of mappen, en wijzig de originele oefeningen niet.

### Ter info: hoe de lector de online repository aanmaakte

Met de solution (die gedeeld wordt) open in Visual Studio:

1. **Git → Create Git Repository...**
2. In het dialoogvenster kies je **GitHub** (niet "Local only").
3. (Log in met je GitHub-account als dat nog niet gekoppeld is.)
4. Stel in:
   - **Repository name** — wordt de naam op GitHub
   - **Visibility:** Public
   - **.gitignore template:** Visual Studio (zou standaard aanstaan)
5. Klik **Create and Push**.

Op het GitHub-platform kan je makkelijk de link terugvinden die je kan delen.

---

## 10. Een eigen kopie maken (forken)

Soms wil je niet alleen code ophalen, maar ook je eigen versie bijhouden op GitHub — zodat je kunt pushen, anderen kunt toevoegen als collaborator, en de lector feedback kan geven. Dan gebruik je een fork.

### Stappen

1. Ga naar de repository van de lector op [github.com](https://github.com).
2. Klik rechtsboven op de knop **Fork**.
3. GitHub maakt een kopie aan op jouw eigen account. Je wordt automatisch doorgestuurd naar je eigen versie (je ziet je eigen gebruikersnaam in de URL, bv. `github.com/jouwusername/programmeren-basis-2526`).
4. Clone deze fork naar je lokale computer via **Git → Clone Repository** in Visual Studio (gebruik de URL van **jouw fork**, niet die van de lector).

Je hebt nu een eigen repository op GitHub én een lokale werkkopie. Als je commit en pusht, gaan je wijzigingen naar jouw fork — niet naar de repository van de lector.

> **Wanneer clone, wanneer fork?** Gebruik clone als je alleen code wilt ophalen om mee te oefenen (bv. voorbeeldcode bekijken). Gebruik fork als je een eigen versie nodig hebt op GitHub waar je naartoe kunt pushen en waar anderen aan kunnen samenwerken.

---

## 11. Je eigen repository aanmaken

Naast forken kun je ook een volledig nieuwe repository aanmaken voor je eigen projecten.

### 11.1 Repository aanmaken op GitHub

1. Ga naar [github.com](https://github.com) en klik rechtsboven op **+ → New repository**.
2. Geef je repository een duidelijke naam (bv. `progbasis-achternaam-voornaam`).
3. Kies **Private** (zodat alleen jij en de lector de code kunnen zien).
4. Vink **Add a README file** aan.
5. Kies bij "Add .gitignore" de template **Visual Studio**. Dit zorgt ervoor dat gegenereerde bestanden (`bin/`, `obj/`) niet in je repository terechtkomen.
6. Klik op **Create repository**.

### 11.2 Collaborators toevoegen

1. Ga naar je repository op [github.com](https://github.com).
2. Klik op **Settings** (tabblad bovenaan).
3. Klik links op **Collaborators**.
4. Klik op **Add people** en voer de GitHub-gebruikersnaam in van de persoon die je wilt toevoegen.
5. Die persoon krijgt automatisch een e-mail met een uitnodiging. Pas na het accepteren van de uitnodiging heeft de persoon toegang.

### 11.3 Repository clonen en gebruiken

1. Kopieer de URL van je repository op GitHub (groene knop **"Code"** → kopieer de HTTPS-URL).
2. In Visual Studio: **Git → Clone Repository** → plak de URL → kies een lokale map.
3. Klik op **Clone**.

### 11.4 Code committen en pushen

Nadat je aan je code hebt gewerkt:

1. Open het **Git Changes**-venster (**View → Git Changes**).
2. Je ziet een lijst van alle bestanden die je hebt gewijzigd, toegevoegd of verwijderd.
3. Schrijf een **commit message** — een korte beschrijving van wat je gedaan hebt (bv. `Oefening 3 afgewerkt - lussen`).
4. Klik op **Commit All**. Dit maakt een lokale snapshot.
5. Klik op de **Push**-knop (pijl omhoog, ↑) of ga naar **Git → Push**. Nu staat je code op GitHub.

> **Goede gewoonte:** Commit regelmatig en met duidelijke berichten. Niet één grote commit op het einde van de week met "alles" als bericht, maar meerdere kleine commits zoals `Oefening 1 afgewerkt`, `Bug in oefening 2 opgelost`. Dit helpt je om terug te kijken naar je werk en maakt feedback door de lector eenvoudiger.

---

## 12. Terugkijken naar oude versies

Een van de grootste voordelen van Git is dat je altijd kunt terugkijken naar eerdere versies van je code. Elke commit die je hebt gemaakt is bewaard en kun je opnieuw bekijken.

### Commitgeschiedenis bekijken

1. In Visual Studio: ga naar **Git → View Branch History**.
2. Je ziet een lijst van alle commits, met de datum, de auteur en de commit message.
3. Klik op een commit om te zien welke bestanden er gewijzigd werden.
4. Dubbelklik op een bestand om precies te zien wat er veranderd is: links de oude versie, rechts de nieuwe. Toegevoegde regels staan in het groen, verwijderde in het rood.

Dit is ook de reden waarom goede commit messages zo belangrijk zijn: ze helpen je om snel terug te vinden wanneer je iets veranderd hebt.

### Code terughalen uit een oude versie

Stel dat je iets hebt gewijzigd of verwijderd en je wilt de oude versie van een bestand terughalen. Dat kan:

1. Open de Branch History (**Git → View Branch History**).
2. Zoek de commit waarin het bestand nog correct was.
3. Klik op die commit en zoek het bestand in de lijst van gewijzigde bestanden.
4. Rechtermuisklik op het bestand → **Open**. Je ziet nu de oude versie.
5. Kopieer wat je nodig hebt naar je huidige versie van het bestand, of sla het bestand op om het te vervangen.

Dit is de eenvoudigste en veiligste manier om oude code terug te halen. Er bestaan ook geavanceerdere manieren om volledige commits ongedaan te maken (zoals *Revert*), maar die bewaren we voor later.

> **Geruststelling:** Git gooit nooit zomaar iets weg. Zelfs als je per ongeluk code verwijdert en dat commit, staat de oude versie nog in je geschiedenis. Je kunt altijd terugkijken en terughalen.

---

## 13. Feedback via Issues

De lector kan feedback geven via Issues op GitHub. Een issue is een soort ticket of notitie, gekoppeld aan je repository. Hieronder lees je hoe je ermee werkt.

### Issues bekijken

1. Ga naar je repository op [github.com](https://github.com).
2. Klik op het tabblad **Issues** (bovenaan de pagina).
3. Je ziet een lijst van alle open issues. Een getal naast het tabblad geeft aan hoeveel open issues er zijn.
4. Klik op een issue om de volledige feedback te lezen.

### Reageren op een issue

Onderaan elk issue vind je een tekstveld waar je een reactie kunt schrijven. Gebruik dit om vragen te stellen of om aan te geven dat je de feedback hebt verwerkt. Klik daarna op de groene knop om je reactie te plaatsen.

### Een issue sluiten

Als je de feedback hebt verwerkt en het probleem is opgelost, kun je het issue sluiten. Dit doe je onderaan het issue via de knop **Close issue** (of **Close with comment** als je er meteen een reactie bij wilt schrijven). Gesloten issues verdwijnen uit de standaardlijst, maar zijn niet verwijderd — je kunt ze altijd terugvinden door op "Closed" te klikken in het Issues-overzicht.

> **Tip:** Behandel issues als een to-do-lijst. Nieuwe feedback van de lector komt binnen als open issue. Na het verwerken sluit je het. Zo houd je een duidelijk overzicht van wat nog te doen is.

---

## 14. Samenvatting

| Wat je wilt doen                     | Hoe                                                                   |
| ------------------------------------ | --------------------------------------------------------------------- |
| Een repository clonen                | VS: **Git → Clone Repository** → URL plakken → Clone                  |
| Een repository forken                | GitHub: repo openen → **Fork** → dan clonen naar je pc                |
| Je wijzigingen opslaan (commit)      | VS: **Git Changes** → bericht schrijven → **Commit All**              |
| Je commits uploaden (push)           | VS: **Git Changes** → **Push** (↑) of **Git → Push**                 |
| Updates ophalen (pull)               | VS: **Git → Pull**                                                    |
| Oude versies bekijken                | VS: **Git → View Branch History** → commit aanklikken                 |
| Oude code terughalen                 | VS: Branch History → commit → bestand → **Open**                      |
| Collaborator toevoegen               | GitHub: repo → **Settings → Collaborators → Add people**              |
| Feedback bekijken                    | GitHub: je repo → tabblad **Issues**                                  |
| Issue sluiten                        | GitHub: issue openen → **Close issue**                                |
