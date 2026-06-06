# Revit API Contracts Architecture

## Ziel

`Thomash100/Revit-API` ist die neutrale Contracts-Schicht fuer den Revit-/BIM-Austausch. Das Repository definiert Datenformen und JSON-Vertraege, enthaelt aber keine Revit-App, keinen Ribbon-Code, keine Commands und keine produktiven Module.

TGA bleibt als erster bestehender Vertrag erhalten. Parallel dazu bereitet die generische BIM-Schicht eine spaetere gewerkeuebergreifende Nutzung fuer Architektur, Tragwerk, Elektro, MSR, Brandschutz, Landschaftsplanung und Koordination vor.

## Solution

```text
RevitApi.Contracts.sln
src/RevitApi.Contracts
tests/RevitApi.Contracts.Tests
```

## Verantwortlichkeiten

| Bereich | Inhalt |
| --- | --- |
| BIM | `Discipline`, `BimLevel`, `ModelElement`, `ElementClassification`, `SourceApplication`, `ParameterValue`, `ParameterSet`, `ValidationIssue`, `CoordinationIssue`, `BimExchangeDocument` |
| Domain | `Shaft`, `Register`, `Trade`, `PlanningStatus` |
| Export | Projektinfo, Summary, Raum-DTOs, Element-DTOs, Exportdokument |
| JSON | Einheitliche `System.Text.Json`-Optionen mit camelCase und String-Enums |
| Validation | Externe Validierungsergebnisse mit `uniqueId`, `sourceSystem`, `ruleCode`, `message`, `writeBack` |
| WriteBack | Revit-freier Vertrag fuer Parameterupdates und Ergebnisstatus |

## Abhaengigkeitsregeln

- Keine Referenz auf `RevitAPI.dll` oder `RevitAPIUI.dll`.
- Keine UI-, Ribbon- oder Command-Klassen.
- Keine App-Modulstruktur.
- Alle Modelle muessen in normalen .NET Unit Tests pruefbar bleiben.

## JSON-Vertrag

Der generische BIM-Vertrag besteht aus:

- `BimExchangeDocument`
- `BimLevel`
- `ModelElement`
- `ElementClassification`
- `ParameterSet`
- `ParameterValue`
- `ValidationIssue`
- `CoordinationIssue`

Der bestehende TGA-Exportvertrag bleibt kompatibel und besteht aus:

- `TgaExportDocument`
- `TgaProjectInfo`
- `TgaExportSummary`
- `TgaRoomExport`
- `TgaElementExport`

Der Validierungs- und WriteBack-Vertrag besteht aus:

- `TgaValidationImport`
- `TgaValidationResult`
- `WriteBackRequest`
- `WriteBackInstruction`
- `WriteBackOutcome`
- `WriteBackOutcomeStatus`

`UniqueId` ist die fuehrende Identitaet fuer externe Resultate und spaetere WriteBack-Ausfuehrung.

## Disziplinen

Die vorbereitete Fachlogik ist bewusst nur ein Vertrag, keine produktive Implementierung:

| Discipline | Bedeutung |
| --- | --- |
| Architecture | Architektur |
| Structural | Tragwerk |
| TechnicalBuildingEquipment | TGA |
| Heating | Heizung |
| Ventilation | Lueftung |
| Sanitary | Sanitaer |
| Electrical | Elektro |
| MSR | Mess-, Steuer- und Regelungstechnik |
| FireProtection | Brandschutz |
| Landscape | Landschaftsplanung |
| Coordination | Gewerkeuebergreifende Koordination |

## Nicht-Ziele

- Keine Revit-Modellabfrage.
- Kein Revit-Add-in.
- Kein Ribbon-Tab und kein Button.
- Keine Revit-Transaktion.
- Keine produktiven Fachfunktionen fuer Architektur, Tragwerk, Elektro, MSR, Brandschutz oder Landschaft.
- Kein IFC-Editor, Webportal, Cloud Sync oder Projektmanagement.

## Beispiele

Das Repository enthaelt unter `samples/bim-exchange.multidisciplinary.sample.json` einen multidisziplinaeren Beispielvertrag. Er dient als Contract-Fixture fuer Unit Tests und als fachliche Orientierung fuer spaetere Plattformintegration.

Der Beispielvertrag enthaelt:

- ein Projekt
- zwei Ebenen in `levels`
- mehrere `ModelElement`-Eintraege je Disziplin
- `UniqueId` und `ElementId` je Element
- `ElementClassification`
- `ParameterSets` mit typdeklarierten Stringwerten
- `ValidationIssues`
- `CoordinationIssues`

## Offene Punkte

- Paketierungsstrategie fuer Konsumenten festlegen.
- Schema-Versionierung und Breaking-Change-Regeln definieren.
- Beispiel-JSON unter `samples/` ergaenzen.
- Finale Shared-Parameter fuer WriteBack abstimmen.
- Namenskonventionen und Klassifikationssysteme fuer alle Disziplinen fachlich abstimmen.
