# TDD - Intro

## Kommandoer

- Nytt løsning
  ```sh
  dotnet new sln --name <solution-name>
  ```
- Ny test module
  ```sh
  dotnet new xunit --name <module-name> --output <path/folder-for-module>
  ```
- Link test module til løsning
  ```sh
  dotnet sln add <path/folder-for-module>
  ```
- Kjør testene fra terminalen (kan og kjøre de via C# DevKit extensionen)
  ```sh
  dotnet test
  ```
- Sett opp et nytt klasse bibliotek
  ```sh
  dotnet new classlib --name <module-name> --output <path/folder-for-module>
  ```
- Link klasse bibliotek til løsning
  ```sh
  dotnet sln add <path/folder-for-module>
  ```
- Link klasse bibliotek til test module
  ```sh
  dotnet add <path/folder-for-test-module> reference <path/folder-for-class-module>
  ```

## Libraries

- xUnit: Veldig vanlig bibliotek for å skrive tester

## Sratches

![Skratches](/docs/tdd-skratches.png)
