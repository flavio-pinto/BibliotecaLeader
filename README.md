# 📖 BibliotecaLeader

**BibliotecaLeader** è un'applicazione console scritta in **C# e .NET**, progettata per gestire una biblioteca, permettendo la gestione di **libri, utenti e prestiti**. Utilizza la libreria **Spectre.Console** per un'interfaccia a riga di comando migliorata e **Entity Framework Core** per la gestione del database.

**Per un'esperienza di utilizzo migliore, suggerisco di utilizzare il terminale a schermo intero.**

---

## ✨ Funzionalità Principali

### 📚 **Gestione Libri**
- **Visualizza libri** con possibilità di filtrarli per:
  - **Autore**
  - **Titolo**
  - **Anno di pubblicazione**
- **Aggiungi un nuovo libro**
- **Modifica un libro esistente**
- **Elimina un libro**

### 👥 **Gestione Utenti**
- **Visualizza utenti** con filtri disponibili:
  - **Nome e Cognome**
  - **Codice Fiscale**
  - **Email**
- **Aggiungi un nuovo utente**
- **Modifica i dati di un utente**
- **Elimina un utente**

### 🔄 **Gestione Prestiti**
- **Visualizza prestiti attivi e passati**, con possibilità di filtrare per:
  - **ID utente**
  - **Prestiti attivi/non restituiti**
- **Aggiungi un nuovo prestito**, con verifica della disponibilità del libro
- **Modifica un prestito**
- **Terminare un prestito**, con gestione automatica di:
  - **Penale** (2€/giorno di ritardo)
  - **Aggiornamento disponibilità del libro**
- **Eliminare un prestito**, con controllo della disponibilità del libro

---

## 🛠️ Installazione

### Prerequisiti
- **.NET SDK 6.0 o superiore**
- **SQL Server** installato e configurato
- **Visual Studio Code / Visual Studio / Rider** (opzionale per sviluppo)

### Passaggi

1. **Clona il repository**
   ```sh
   git clone https://github.com/flavio-pinto/BibliotecaLeader.git
   cd BibliotecaLeader
   ```

2. **Installa le dipendenze**
   ```sh
   dotnet restore
   ```

3. **Applica le migrazioni al database**
   ```sh
   dotnet ef database update
   ```

4. **Avvia l'applicazione**
   ```sh
   dotnet run
   ```

---

## 📌 Struttura del Progetto

```
BibliotecaLeader/
│-- Controllers/
│   ├── BooksController.cs
│   ├── UsersController.cs
│   ├── LoansController.cs
│-- Models/
│   ├── Book.cs
│   ├── User.cs
│   ├── Loan.cs
│-- Data/
│   ├── BibliotecaContext.cs
│-- UserInterface.cs
│-- Program.cs
│-- Enums.cs
│-- README.md
│-- appsettings.json
```

- **Controllers/**: Gestisce la logica di business per libri, utenti e prestiti.
- **Models/**: Contiene le classi rappresentative dei dati (Libri, Utenti, Prestiti).
- **Data/**: Contiene il contesto di Entity Framework Core (`BibliotecaContext.cs`).
- **UserInterface.cs**: Gestisce l'interazione con l'utente tramite menu testuali.
- **Program.cs**: Punto di ingresso dell'applicazione.
- **Enums.cs**: Definisce le enumerazioni per i menu.
- **appsettings.json**: Contiene la configurazione dell'applicazione, inclusa la stringa di connessione al database.

---

## ⌨️ Utilizzo

1. **Avvia l'applicazione** con `dotnet run`
2. **Naviga nei menu** utilizzando le frecce direzionali e premi `Invio`
3. **Effettua operazioni** sui libri, utenti e prestiti
4. **Per uscire**, seleziona l'opzione "❌ Esci"

---

## 🔧 Tecnologie Utilizzate

- **C#**
- **.NET 6+**
- **SQL Server**
- **Entity Framework Core**
- **Spectre.Console** (per un'interfaccia CLI avanzata)

---

## 🐜 Licenza

Questo progetto è distribuito sotto licenza **MIT**.

📌 **Creato da Flavio** 🚀

