# 📚 BibliotecaLeader (Aggiornato)

**BibliotecaLeader** è un'applicazione console scritta in **C# e .NET**, progettata per gestire una biblioteca, permettendo la gestione di **libri, utenti e prestiti**.  
Utilizza **Entity Framework Core** per la gestione del database **SQL Server** e **Spectre.Console** per un'interfaccia CLI migliorata.

💡 **Consiglio**: Per un'esperienza ottimale, utilizza il terminale a **schermo intero**.

## ✨ Funzionalità Principali

### 📚 **Gestione Libri**
✔ **Visualizza tutti i libri** con filtri per:  
   - **Autore**  
   - **Titolo**  
   - **Anno di pubblicazione**  
✔ **Aggiungi un nuovo libro**  
✔ **Modifica un libro esistente**  
✔ **Elimina un libro**  

### 👥 **Gestione Utenti**
✔ **Visualizza utenti** con filtri per:  
   - **Nome e Cognome**  
   - **Codice Fiscale**  
   - **Email**  
✔ **Aggiungi un nuovo utente**  
✔ **Modifica i dati di un utente**  
✔ **Elimina un utente**  

### 🔄 **Gestione Prestiti**
✔ **Visualizza i prestiti attivi e passati**, con filtri per:  
   - **ID utente**  
   - **Prestiti attivi/non restituiti**  
✔ **Aggiungi un nuovo prestito**, con verifica della disponibilità del libro  
✔ **Modifica un prestito**  
✔ **Termina un prestito**, con aggiornamento automatico di:  
   - **Penale** (2€/giorno di ritardo)  
   - **Disponibilità del libro**  
✔ **Elimina un prestito**, con gestione automatica della disponibilità del libro  

---

## 🛠️ Installazione

### 📌 Prerequisiti
- **.NET SDK 6.0 o superiore**
- **Microsoft SQL Server** (installato e configurato)
- **Visual Studio Code / Visual Studio** (opzionale)

### 📌 Configurazione del Database
1. **Crea il database SQL Server** con il nome `biblioteca_leader`.  
   Se utilizzi SQL Server Management Studio (SSMS), esegui:
   ```sql
   CREATE DATABASE biblioteca_leader;
   ```
2. **Aggiorna la stringa di connessione** nel file `appsettings.json` (se lo utilizzi) o direttamente in `BibliotecaContext.cs`:
   ```csharp
   protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
   {
       if (!optionsBuilder.IsConfigured)
       {
           optionsBuilder.UseSqlServer("Server=localhost;Database=biblioteca_leader;Trusted_Connection=True;TrustServerCertificate=True;");
       }
   }
   ```

3. **Esegui le migrazioni e aggiorna il database**:
   ```sh
   dotnet ef migrations add InitialCreate
   dotnet ef database update
   ```

---

## 🚀 Avvio dell'Applicazione

1. **Clona il repository**
   ```sh
   git clone https://github.com/flavio-pinto/BibliotecaLeader.git
   cd BibliotecaLeader
   ```

2. **Installa le dipendenze**
   ```sh
   dotnet restore
   ```

3. **Avvia l'applicazione**
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
```

- **Controllers/**: Gestisce la logica di business per libri, utenti e prestiti.
- **Models/**: Contiene le classi rappresentative dei dati (**Libri, Utenti, Prestiti**).
- **Data/**: Contiene il **DbContext**, che connette l'app al database SQL Server.
- **UserInterface.cs**: Gestisce l'interazione con l'utente tramite menu testuali.
- **Program.cs**: Punto di ingresso dell'applicazione.
- **Enums.cs**: Definisce le enumerazioni per i menu.

---

## ⌘ Utilizzo

1. **Avvia l'applicazione** con `dotnet run`
2. **Naviga nei menu** usando le **frecce direzionali** e premi **Invio**
3. **Esegui operazioni** su **libri, utenti e prestiti**
4. **Per uscire**, seleziona "❌ Esci"

---

## 🔧 Tecnologie Utilizzate

- **C# e .NET 6**
- **Entity Framework Core** (Gestione database SQL Server)
- **Spectre.Console** (Interfaccia CLI avanzata)
- **SQL Server**

---

## 🐜 Licenza

Questo progetto è distribuito sotto licenza **MIT**.

📌 **Creato da Flavio** 🚀

