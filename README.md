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
tests/
  RevitApi.Contracts.Tests/
docs/
  ARCHITECTURE.md
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
- Electrical
- MSR
- FireProtection
- Landscape
- Coordination

## Build und Tests

```powershell
dotnet build RevitApi.Contracts.sln
dotnet test tests/RevitApi.Contracts.Tests/RevitApi.Contracts.Tests.csproj
dotnet format RevitApi.Contracts.sln --verify-no-changes --no-restore
```

## Konsumenten

`Thomash100/Revit-TGA-Platform` soll diese Bibliothek als Contracts-Schicht referenzieren. Lokal kann das als ProjectReference erfolgen; spaeter ist ein NuGet-Paket sinnvoll.
