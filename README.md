# Cappario
Il Cappario è un software sviluppato per automatizzare un task che il nostro supporto deve compiere internamente ogni giorno e che manualmente richiederebbe ore di lavoro.

## Cosa fa?
In breve il Cappario controlla che contratti a partire da una data abbiano la giusta filiale fiscale assegnata in base al CAP del cantiere; per ogni contratto la cui filiale fiscale non sia giusta il Cappario provvede, a seconda della sua configurazione, a:

- Modificare automaticamente i contratti
- Riportare nel file Results.txt quali contratti dovrebbero essere modificati

## Quali contratti controlla?
Il Cappario prende tutti i contratti “Approved” e “Running” a partire da una data nel passato fornita nel file di configurazione. I dati vengono ottenuti chiamando la v5/GET/integration/contracts.

## Come decide quali contratti modificare?
Il Cappario, quando ha l’elenco di contratti che deve controllare, ne prende il CAP del cantiere e guarda che la filiale fiscale assegnata combaci con quella fornita nel file di configurazione Cappario.xlsx, se il CAP non viene trovato nel file il contratto viene saltato dalla modifica. 

## Come modifica i contratti?
Il Cappario, quando ha l’elenco di contratti per i quali il CAP del cantiere e la filiale fiscale assegnata su CDRS non combaciano con quella riportata nel file Cappario.xlsx, apre una sessione di Chrome e va in ogni contratto ad impostare la filiale fiscale come riportata nel file Cappario.xlsx

## Come verificare il risultato?
Aprite il file Results.txt per vedere eventuali:

- CAP mancanti nel file Cappario.xlsx ma trovati nei cantieri dei contratti
- Contratti modificati con successo O contratti da modificare (a seconda della configurazione ModifyContract)
- Contratti per i quali il backend non ha permesso la modifica

## Come lanciarlo?
### Fornire le configurazioni necessarie

Prima di avviare il Cappario è necessario inserire delle configurazioni obbligatorie che è possibile trovare in Cappario.dll.config

Il file può essere aperto con un qualsiasi editor di testo, anche Notepad.

### Parametri
ModifyContract 
bool
Se impostato a true il Cappario modificherà da solo i contratti, su false invece riporta i contratti da modificare nel file Results.txt

HeadlessBrowser
bool
Se impostato a true il browser lanciato dal Cappario non sarà visualizzato a schermo permettendo di continuare a lavorare indisturbati

BackendUrl
string
URL del backend a cui il Cappario si collegherà per modificare i contratti

ApiBaseUrl
string
URL delle API a cui il Cappario si collegherà per ottenere informazioni sui contratti

ClientId
string
Client id a cui il Cappario si collegherà per ottenere informazioni sui contratti

ClientSecret
string
Secret del client a cui il Cappario si collegherà per ottenere informazioni sui contratti. Eventuali caratteri speciali devono essere convertiti in HTML, potete utilizzare questo tool https://www.web2generators.com/html-based-tools/online-html-entities-encoder-and-decoder 

BackendUsername
string
Email dell’utente backend che il Cappario utilizzerà per modificare i contratti

BackendPassword
string
Password dell’utente backend che il Cappario utilizzerà per modificare i contratti

NumberOfDaysInThePast
int
	
Numero di giorni tolti dalla data di oggi che il Cappario utilizza per filtrare i contratti da controllare (data di creazione)

### File con CAP e filiali collegate
Nella stessa cartella dove c'è il file Cappario.exe ci deve essere un file excel chiamato Cappario.xlsx con tutti i codici postali e la loro filiale collegata.

### File con filiali ed il loro id
Nella stessa cartella dove c'è il file Cappario.exe ci deve essere un file excel chiamato Branches.xlsx con tutte le branch ed i loro id di CDRS in ordine crescente

### ChromeDriver
Nella stessa cartella dove c'è il file Cappario.exe ci deve essere anche l’eseguibile Chromedriver.exe. Il file può essere scaricato a questo indirizzo https://chromedriver.chromium.org/downloads 

### Avviare il programma
Cliccate su Cappario.exe, si aprirà il Command Prompt di Windows. Lasciatelo aperto fino a quando non apparirà un messaggio specificando che il programma ha finito e che è possibile chiudere la console.

All’avvio del Cappario.exe vi apparirà un messaggio di avvertimento che l’autore dell’applicazione non è conosciuto, dovete confermare di voler procedere ugualmente.
