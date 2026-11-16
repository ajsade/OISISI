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

- (dopunjava koleginica)

## Pokretanje

Postaviti `ConsoleClient` kao startup projekat i pokrenuti. Podaci se cuvaju u folderu `Data` u korijenu projekta (`posetioci.txt`, `izdavaci.txt`).