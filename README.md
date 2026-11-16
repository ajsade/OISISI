# Sajam knjiga

Desktop aplikacija za referenta sajma knjiga. (C#,MVC,WPF)
Projekat iz predmeta Osnovi informacionih sistema i softverskog inzenjerstva.

## Struktura projekta
- `Core` - modeli, servisi, DAO, interfejsi i pomocne klase (zajednicko)
- `ConsoleClient` - konzolni klijent
- `WpfClient` - graficki klijent

## Podjela posla

### Vanja Samardzic

- Modeli :  `Posetilac`, `Kupovina`, `Izdavac`, `StatusPosetioca`
- Interfejsi i DAO: `IPosetilacDao`, `PosetilacDao`, `IIzdavacDao`, `IzdavacDao`
- Servisi: `PosetilacService`, `IzdavacService`
- Cuvanje podataka: `ISerializable`, `Serializer`, `Storage` (tekstualni format sa separatorom `|`, po uzoru na 3. vjezbe)
- Konzola: glavni meni, `PosetilacController`, `IzdavacController`, `Unos`
- Funkcionalnosti: dodavanje, izmjena, brisanje i prikaz posjetilaca i izdavaca,
  cuvanje i ucitavanje podataka iz tekstualnih fajlova


### Ajsa Demirovic

- Modeli: `Autor`, `Knjiga`, `Adresa`
- Interfejsi i DAO: `IAutorDao`, `AutorDao`, `IKnjigaDao`, `KnjigaDao`
- Servisi: `AutorService`, `KnjigaService`
- Cuvanje podataka: implementirane metode ToCSV i FromCSV
  za klase Autor i Knjiga, uz koriscenje postojecegsistema Serializer i Storage (tekstualni format sa separatorom |)
- Serijalizacija i deserijalizacija autora i knjiga
- Konzola: `AutorController`, `KnjigaController`
- Funkcionalnosti: dodavanje, izmena, brisanje i prikaz autora i knjiga, cuvanje i ucitavanje podataka iz tekstualnih datoteka


## Pokretanje

Postaviti `ConsoleClient` kao startup projekat i pokrenuti. Podaci se cuvaju u folderu `Data` u korijenu projekta (`posetioci.txt`, `izdavaci.txt`, `autori.txt`, `knjige.txt`).