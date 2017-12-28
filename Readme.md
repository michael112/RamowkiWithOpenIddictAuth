## Informacje podstawowe

Celem niniejszej aplikacji było zapoznanie się z techniką tworzenia API sieciowego w środowisku .NET, z zastosowaniem podstawowego uwierzytelniania użytkowników.

Aplikacja obsługuje uproszczone zarządzanie ramówkami radiowymi lub telewizyjnymi, z możliwością przygotowania ramówki cyklicznej - tygodniowej, oraz ramówek specjalnych, na konkretną datę.

## Wymagania techniczne

Aplikacja działa w środowisku .NET Core w wersji 2.0. SDK niezbędne do uruchomienia aplikacji można pobrać pod adresem: https://www.microsoft.com/net/download

## Obsługa aplikacji - autoryzacja

1. Wygenerowanie tokenu

Należy wysłać zapytanie HTTP POST pod adres /connect/token, w postaci x-www-form-urlencoded, z parametrem grant_type: password, a także parametrami username i password, przyjmującymi za wartości login i hasło użytkownika.

2. Dostęp do zabezpieczonych punktów dostępowych

W nagłówku zapytania musimy podać parametr Authorization, który przyjmuje wartość: Bearer <access_token>. Token pobieramy oczywiście w sposób podany w punkcie 1.

## Wykorzystane technologie:
- język programowania C#
- środowisko .NET Core 2.0
- biblioteka OpenIddict (https://github.com/openiddict/openiddict-core)

NIE używałem frameworku ASP.NET Core Identity, ponieważ klasa IdentityUser ma zbyt dużo niepotrzebnych pól, które zaśmiecałyby bazę danych.

## Obsługa aplikacji - działanie

1. Wprowadzenie do systemu nowej audycji.

Zapytanie HTTP POST pod adres /api/programme. Wymagane konto administratorskie.

Przykładowa zawartość JSON:
```javascript
{
	"Title": "Przykładowa audycja 1",
	"Description": "Opis audycji"
}
```

Pole Description jest opcjonalne.

2. Edycja istniejącej audycji.

Zapytanie HTTP PUT pod adres /api/programme/<programmeID>, gdzie za <programmeID> podajemy odpowiedni identyfikator. Wymagane konto administratorskie.

Przykładowa zawartość JSON - jak w pkt. 1.

3. Usuwanie audycji.

Zapytanie HTTP DELETE pod adres /api/programme/<programmeID>, gdzie za <programmeID> podajemy odpowiedni identyfikator. Wymagane konto administratorskie.

Zapytanie nie oczekuje JSON-a w zawartości.

4. Wyświetlanie listy wszystkich audycji w systemie.

Zapytanie HTTP GET pod adres /api/programme. Wymagane konto użytkownika z dowolnymi uprawnieniami.

Zapytanie nie oczekuje JSON-a w zawartości.

5. Wyświetlanie audycji o określonym identyfikatorze.

Zapytanie HTTP GET pod adres /api/programme, gdzie za <programmeID> podajemy odpowiedni identyfikator. Wymagane konto użytkownika z dowolnymi uprawnieniami.

Zapytanie nie oczekuje JSON-a w zawartości.

6. Jednoczesne wprowadzenie do systemu programu oraz jego pozycji w ramówce.

Zapytanie HTTP POST pod adres /api/schedule. Wymagane konto administratorskie.

Przykładowa zawartość JSON (jeśli ma zostać przypisany do ramówki specjalnej na konkretną datę):

```javascript
{
	"Programme": {
		"Title": "sample title",
		"Description": "sample description"
	},
	"Day": {
		"Date": "15-11-2016",
	}
	"BeginTime": {
		"Hours": 17,
		"Minutes": 0
	}
}
```

lub (przypisanie do ramówki cyklicznej na dany dzień tygodnia):

```javascript
{
	"Programme": {
		"Title": "sample title",
		"Description": "sample description"
	},
	"Day": {
		"WeekDay": 2,
	}
	"BeginTime": {
		"Hours": 17,
		"Minutes": 0
	}
}
```

Dni tygodnia numerujemy w sposób następujący:

0 - niedziela

1 - poniedziałek

...

6 - sobota

7 - niedziela

8 - poniedziałek

itp... (reszta z dzielenia nr dnia przez 7)

7. Wprowadzenie do systemu informacji o przypisaniu istniejącego programu do dnia i godziny w ramówce.

Zapytanie HTTP POST pod adres /api/schedule/?programme=<programmeID>, gdzie za <programmeID> podajemy odpowiedni identyfikator. Wymagane konto administratorskie.

Przykładowa zawartość JSON (jeśli ma zostać przypisany do ramówki specjalnej na konkretną datę):

```javascript
{
	"Day": {
		"Date": "15-11-2016",
	}
	"BeginTime": {
		"Hours": 17,
		"Minutes": 0
	}
}
```

lub (przypisanie do ramówki cyklicznej na dany dzień tygodnia):

```javascript
{
	"Day": {
		"WeekDay": 2,
	}
	"BeginTime": {
		"Hours": 17,
		"Minutes": 0
	}
}
```

Dni tygodnia numerujemy jak wyżej.

8. Przypisanie do istniejącej pozycji w ramówce innej audycji istniejącej w systemie.

Zapytanie HTTP PUT pod adres /api/schedule/<scheduleID>?programme=<programmeID>, gdzie za <scheduleID> podajemy identyfikator pozycji w ramówce, a za <programmeID> - identyfikator programu, który ma zostać do niej przypisany. Wymagane konto administratorskie.

Zapytanie NIE MOŻE zawierać JSON-a.

9. Edycja informacji o pozycji ramówkowej (dzień, godzina).

Zapytanie HTTP PUT pod adres /api/schedule/<scheduleID>, gdzie za <scheduleID> podajemy identyfikator pozycji w ramówce. Wymagane konto administratorskie.

Przykładowa zawartość JSON (jeśli ma zostać przypisany do ramówki specjalnej na konkretną datę):

```javascript
{
	"Day": {
		"Date": "15-11-2016",
	}
	"BeginTime": {
		"Hours": 17,
		"Minutes": 0
	}
}
```

lub (przypisanie do ramówki cyklicznej na dany dzień tygodnia):

```javascript
{
	"Day": {
		"WeekDay": 2,
	}
	"BeginTime": {
		"Hours": 17,
		"Minutes": 0
	}
}
```

10. Usuwanie pozycji ramówkowej (bez usunięcia samej audycji!).

Zapytanie HTTP DELETE pod adres /api/schedule/<scheduleID>, gdzie za <scheduleID> podajemy identyfikator pozycji w ramówce. Wymagane konto administratorskie.

Zapytanie nie oczekuje JSON-a w zawartości.

11. Wyszukanie informacji o pozycji ramówkowej na podstawie jej identyfikatora.

Zapytanie HTTP GET pod adres /api/schedule/<scheduleID>, gdzie za <scheduleID> podajemy identyfikator pozycji w ramówce. Wymagane konto użytkownika z dowolnymi uprawnieniami.

Zapytanie nie oczekuje JSON-a w zawartości.

12. Wyszukanie listy pozycji ramówkowych na dany dzień tygodnia.

Zapytanie HTTP GET pod adres /api/schedule?date=<date>, gdzie <date> jest datą w formacie <dd-mm-yyyy>. Wymagane konto użytkownika z dowolnymi uprawnieniami.

Zapytanie nie oczekuje JSON-a w zawartości.

13. Wyszukanie listy pozycji ramówkowych na daną datę (jeżeli brak, zostaną wyszukane programy na dzień tygodnia, w jaki przypada dana data).

Zapytanie HTTP GET pod adres /api/schedule?day=<dayNumber>, gdzie <dayNumber> jest numerem dnia tygodnia wg opisu wyżej. Wymagane konto użytkownika z dowolnymi uprawnieniami.

Zapytanie nie oczekuje JSON-a w zawartości.

## Przygotowanie aplikacji do działania

Informacje o użytkownikach zapisane są w bazie danych - MS SQL Server. Zapis hasła w bazie jest szyfrowany przy pomocy SHA-256.

1. Generowanie szkieletu bazy danych

Aby wygenerować bazę danych, musimy najpierw przygotować parametry łączenia z bazą w Twoim komputerze. Odpowiada za to parametr ConnectionStrings.DefaultConnection umieszczony w pliku appsettings.json.

Następnym etapem jest wygenerowanie migracji oraz zapisanie szkieletu bazy danych.
W tym celu, proszę wpisać do konsoli następujące polecenia:

dotnet ef migrations add Initial

gdzie słowo Initial jest nazwą migracji i może zostać dowolnie zmienione.
Następnie proszę wpisać:

dotnet ef database update

2. Przygotowanie użytkowników systemu.

a) Tworzenie użytkowników

W tym celu proszę - dla każdego użytkownika - wykonać w bazie następujące zapytanie SQL.

INSERT INTO Users VALUES ('<user_id>', '<user_email>', '<hashed_password>', '<user_name>');

Wartością parametru "user_id" powinien być ciąg znaków - tzw. GUID / UUID. Można go wygenerować np. na stronie:

https://www.guidgenerator.com/online-guid-generator.aspx

Wartością parametru "hashed_password" jest hasło PO ZASZYFROWANIU metodą SHA-256. Przykładowy generator online:

http://passwordsgenerator.net/sha256-hash-generator/

Parametry "user_email" oraz "user_name" ustawiamy według własnego życzenia.

b) Role użytkowników

Użytkownik może występować jako "user", "admin", lub mieć jednocześnie obie te role.

Musimy najpierw umieścić je w bazie danych:

INSERT INTO Roles VALUES('225baeb0-bed5-4a03-987d-082a272bd32f', 'User');

INSERT INTO Roles VALUES('85ec8be5-8108-44ed-b363f3df765db495', 'Admin');

c) Przydzielanie ról użytkownikom

Odpowiada za to tabela łącząca UserRoleJoin.

INSERT INTO UserRoleJoin VALUES('<join_id>', '<role_id>', '<user_id>');

Wartości parametrów "role_id" oraz "user_id" muszą być identyczne co identyfikatory odpowiednio roli i użytkownika, któremu chcemy daną rolę nadać.

Z kolei w miejscu "join_id" wpisujemy dowolny GUID (UUID). Istnienie tego identyfikatora jest pewnego rodzaju usterką systemu. Prawidłowym jego działaniem byłoby, gdyby identyfikatorem tabeli była kombinacja wartości "role_id" i "user_id". Poprawię to, jeśli znajdę czas.

## Uruchamianie aplikacji

Proszę wpisać do konsoli:

dotnet run

Można też skorzystać z IDE, np. JetBrains Rider czy MS Visual Studio.