# Tworzenie usług sieciowych w ASP.NET Core 8

## Wprowadzenie

Witaj w repozetorium z materiałami do szkolenia **Tworzenie usług sieciowych w ASP.NET Core 8**.

Do rozpoczęcia tego kursu potrzebujesz następujących rzeczy:

1. [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0).
2. Sklonuj repozytorium Git
```
git clone https://github.com/sulmar/sages-abb-aspnetcore8
```
3. Utwórz bazę danych
```
cd sql
sqlcmd -S (localdb)\MSSQLLocalDB -d master -E -i sql-server-sakila-schema.sql
```
4. Załadowuj przykładowane dane
```
sqlcmd -S (localdb)\MSSQLLocalDB -d sakila -E -i sql-server-sakila-insert-data.sql
```