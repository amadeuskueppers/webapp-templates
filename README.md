# webapp-templates
Template für Web Apps - fürs Frontend  &amp; Backend

## Der Inhalt des Repos
- API mit C# und netcore 5.0
- UI mit angular 11 + Material design Components

## Sytem Voraussetzungen & Empfohlene Tools
- [Node.js](https://nodejs.org/en/ "Optionaler Linktitel")
- [Angular](https://angular.io/guide/setup-local)
- [Dotnet Core 5](https://dotnet.microsoft.com/download/dotnet/5.0)
- [Docker Desktop](https://www.docker.com/products/docker-desktop)
- [Visual Studio Code](https://code.visualstudio.com/)
- [Visual Studio](https://visualstudio.microsoft.com/)

## Getting Started
1. Tools installieren
2. Repository clonen
3. Solution in Visual Studio öffnen
   1. Pakete wiederherstellen
   2. Mit Docker starten / alternativ im IIS Express laufen lassen
4. angular Ordner in VS Code öffnen
   1. Terminal öffnen (Strg + Ö)
   2. "npm i" ausfüren um die npm Pakete wiederherzustellen
   3. "environment.ts" anpassen, es muss der Port des Backends eingetragen werden. (Die Datei findet sich unter "src/UI/angular/src/environments")
   4. "ng serve --open" ausführen um das Angular Projekt laufen zu lassen (es öffnet sich der Browser mit localhost:4200)