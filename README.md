Test Voor de rechten 

# 🔥 Het Vlammend Varken - Digitale Oplossing

Welkom bij het project van NBM Solutions voor Het Vlammend Varken – een duurzaam barbecue- en grillrestaurant. Dit systeem ondersteunt reserveringen, betalingen, voorraadoverzicht en meer, gericht op efficiëntie en duurzaamheid.

## 📌 Doel van het project

Een toekomstbestendige webapplicatie ontwikkelen die:
- Reserveringen digitaal afhandelt
- Bestellingen en betalingen vereenvoudigt
- Voorraad en dagmenu’s dynamisch beheert
- Verschillende gebruikersrollen ondersteunt (gast, ober, kok)

---

## 🛠️ Technische Stack

- **Frontend**: 
- **Backend**: C#
- **Database**: MySQL
- **Styling**: 
- **Authenticatie**: 

---

## 🌱 Duurzame Focus

- Gebruik van rest- en seizoensproducten
- Dagmenu’s op basis van actuele voorraad
- Inzichten in duurzame keuzes zichtbaar voor de gast

---

## 🧑‍💻 Rollen & Functionaliteiten

| Rol   | Functionaliteit                          |
|-------|------------------------------------------|
| Gast | Reserveren, menu bekijken, bestellen      |
| Ober | Bestellingen opnemen, betalingen beheren  |
| Kok  | Bestellingen volgen, dagmenu beheren      |

---

## 🌿 Branch Strategie

We gebruiken feature-based branching:

### 📁 Rollenbranches:
- `gast`
- `ober`
- `kok`

### ⚙️ Featurebranches:
- `feature/reserveringsysteem`
- `feature/betaalfunctionaliteit`

Werk aan functies in een aparte branch, en maak een pull request naar `main` als het klaar is.

---

## 🔧 Werkinstructie

```bash
# Clone het project
git clone https://github.com/jouw-gebruikernaam/jouw-projectnaam.git
cd jouw-projectnaam

# Start het project (eerste terminal)
dotnet watch --project .\Vlammend_Varken

# Start API project (tweede terminal)
cd .\Vlammend_Varken.API\
dotnet run

# Haal de laatste wijzigingen op
git pull origin main

# Bekijk alle beschikbare branches
git branch -a

# Checkout naar een branch
git checkout kok    # of een andere branch

# Als je de branch nog niet lokaal hebt, maak hem dan aan vanaf de remote
git checkout -b kok origin/kok
# Nu heb je een lokale branch 'kok' die verbonden is met de remote 'origin/kok'

# Na wijzigingen
git add .
git commit -m "Je commit message hier"
git push

# Merge wijzigingen naar main
git checkout main
git pull origin main      # eerst zeker zijn dat main up-to-date is
git merge kok
git push origin main
```
## 🛠 Als er conflicts zijn tijdens een merge of pull
```bash
# Sla je lokale werk even tijdelijk op (stash)
git stash

# Haal opnieuw de laatste wijzigingen op
git pull origin main

# Haal je lokale wijzigingen terug
git stash pop

### Korte samenvatting conflict oplossing:
bash
Copy
Edit
git stash            # bewaar je lokale werk tijdelijk
git pull origin main # update je branch
git stash pop        # breng je werk terug
# los eventuele conflicts op
git add .
git commit -m "Conflict opgelost"
git push
