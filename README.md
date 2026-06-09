# Revit API Contracts

Reine Schnittstellenbibliothek fuer den Revit-/BIM-Datenaustausch.

Dieses Repository enthaelt nur Contracts, DTOs, JSON-Serialisierung, Validation-Modelle, Coordination-Modelle und den WriteBack-Vertrag. Revit-Add-in-Code, Ribbon, Commands und App-Module gehoeren in `Thomash100/Revit-TGA-Platform`.

TGA bleibt als bestehender Vertrag kompatibel. Die generische BIM-Schicht bereitet zusaetzliche Fachdisziplinen wie Architektur, Tragwerk, Elektro, MSR, Brandschutz und Landschaftsplanung vor.

## Inhalt

```text
src/RevitApi.Contracts/
  Bim/          Generische BIM-Contracts fuer Disziplinen, Elemente, Parameter und Koordination
  Domain/       Bestehende TGA-Contracts: Shaft, Register, Trade, PlanningStatus
  Export/       TGA-kompatible JSON-Export-DTOs fuer Projekt, Summary, Raeume und Elemente
  Json/         Gemeinsame System.Text.Json-Optionen
  Validation/   Bestehende TGA-Validierungsergebnisse
  WriteBack/    Revit-freier WriteBack-Vertrag
samples/
  bim-exchange.multidisciplinary.sample.json
tests/
  RevitApi.Contracts.Tests/
docs/
  ARCHITECTURE.md
  examples.md
  packaging.md
  PROJECT_SUMMARY.md
```

## Vertragsregeln

- `UniqueId` ist die fuehrende externe Referenz fuer Validation und WriteBack.
- `ElementId` bleibt nur diagnostische Revit-Kontextinformation.
- DTOs und JSON-Vertraege bleiben Revit-frei und ohne Abhaengigkeit auf `RevitAPI.dll`.
- WriteBack beschreibt nur Parameterupdates und Ergebnisstatus, keine Revit-Transaktionen.
- `BimExchangeDocument` ist fuer multidisziplinaere Austauschdaten vorbereitet.
- Bestehende `Tga*` DTOs werden nicht umbenannt und bleiben als TGA-kompatibler Vertrag erhalten.

## Vorbereitete Disziplinen

- Architecture
- Structural
- TechnicalBuildingEquipment
- Heating
- Ventilation
- Sanitary
- Electrical
- MSR
- FireProtection
- Landscape
- Coordination

## Beispiel-JSON

`samples/bim-exchange.multidisciplinary.sample.json` enthaelt einen pruefbaren multidisziplinaeren `BimExchangeDocument`-Schnappschuss mit Projekt, zwei Ebenen, mehreren Elementen je Disziplin, ParameterSets, ValidationIssues und CoordinationIssues.

Die Tests unter `RevitApi.Contracts.Tests` laden dieses Beispiel direkt und pruefen Deserialisierung, Pflichtfelder, Disziplinen, Parametertypen sowie Validation-/Coordination-Daten.

## Build und Tests

```powershell
dotnet build RevitApi.Contracts.sln
dotnet test tests/RevitApi.Contracts.Tests/RevitApi.Contracts.Tests.csproj
dotnet format RevitApi.Contracts.sln --verify-no-changes --no-restore
```

## Paketierung

`RevitApi.Contracts` ist als NuGet-/GitHub-Package vorbereitet:

```powershell
dotnet pack src/RevitApi.Contracts/RevitApi.Contracts.csproj --configuration Release --no-build -p:PackageVersion=0.1.1 --output artifacts/packages
```

Die GitHub-Actions-Workflow-Datei `.github/workflows/package-contracts.yml` kann das Paket per Tag oder manuellem Start nach GitHub Packages veroeffentlichen. Details stehen in `docs/packaging.md`.

## Konsumenten

`Thomash100/Revit-TGA-Platform` soll diese Bibliothek als Contracts-Schicht referenzieren. Lokal kann das als ProjectReference erfolgen; spaeter ist ein NuGet-Paket sinnvoll.
