* Wprowadzenie
	* .NET CLI
	* REST API (standardy)
	* Protokół HTTP
* Budowanie aplikacji webowej
	* Porównanie CreateBuilder, CreateSlimBuilder, CreateEmptyBuilder
* Konfiguracja
	* Pobieranie konfiguracji
	* Standardowi dostawcy konfiguracji
	* Konfiguracja zależna od środowiska
* Wstrzykiwanie zależności
 	* Rejestracja usług
 	* Porównanie cykli życia usługi
 	* Pobieranie usługi na podstawie typu
 	* Pobieranie usługi na podstawie klucza
* Minimal Api
	* Mapowanie akcji
	* Mapowanie tras
 	* Reguły tras
 	* Grupowanie tras  
 	* Przekazywanie parametrów 
 	* Mapowanie DTO (source generators)
 	* Zastosowanie atrybutów FromRoute, FromQuery, FromServices, FromHeader, AsParameters
 	* Wysyłanie danych
 	* Obsługa formularzy
 	* Walidacja danych
	* Formatowanie odpowiedzi
	* Pobieranie plików	
	* Zastosowanie filtrów
	* Request Short Circuiting
	* Dokumentowanie usługi (OpenApi, Swagger, uwierzytelnianie)
	* Wersjonowanie usług
* Przechwytywanie wyjątków
	* Własna implementacja
* Pamięć podręczna  
	* Przechowywanie odpowiedzi
* Utworzenie klienta usługi
	* Podstawowe użycie
	* Nazwani klienci
	* Silnie typowani klienci
	* Generowanie klienta (Refit)
	* Warstwa pośrednia komunikatów wychodzących
	* Ponawianie żądań (Poly)
* Warstwy pośrednie (Middleware)
	* Zasada działania
	* Utworzenie własnej warstwy pośredniej
	* Zastosowania
* Diagnostyka
	* Logowanie informacji
	* Zapisywanie własnych informacji
* Kontrola kondycji  
	* Rejestracja diagnostyki
	* Diagnostyki wbudowane
	* Utworzenie własnej diagnostyki  
	* Wizualizacja diagnostyki
* Aplikacje czasu rzeczywistego
	* Server Sent Events
 	* WebSockets
	* Signal-R
* Bezpieczeństwo
	* Uwierzytelnianie
	* Tokeny JWT
	* Użycie narzędzia user-jwts
	* Autoryzacja oparta o role
	* Autoryzacja oparta o poświadczenia
	* Własny magazyn danych
* Wdrożenie
	* Windows
	* Linux
	* Docker
