# Projektzusammenfassung

## Umgesetzt

- `Revit-API` auf eine reine Contracts-/DTO-Bibliothek reduziert.
- App-Projekte, Ribbon, Commands und Fachmodule entfernt.
- `RevitApi.Contracts.sln` mit `src/RevitApi.Contracts` und `tests/RevitApi.Contracts.Tests` erstellt.
- Domainmodelle `Shaft`, `Register`, `Trade`, `PlanningStatus` erhalten.
- JSON-Exportvertrag, Validation-Import und WriteBack-Vertrag vorbereitet.
- README, Architektur und Versionierung auf Schnittstellenbibliothek angepasst.
- Generische BIM-Contracts fuer Disziplinen, Modellelemente, Klassifikation, Parameter, Validation und Coordination vorbereitet.
- `BimExchangeDocument` als multidisziplinaerer JSON-Vertrag ergaenzt, ohne bestehende `Tga*` DTOs umzubenennen.
- Multidisziplinaeres Beispiel-JSON unter `samples/bim-exchange.multidisciplinary.sample.json` ergaenzt.
- Contract-Tests lesen das Beispiel-JSON und pruefen Pflichtfelder, Disziplinen, Parameterwerte, ValidationIssues und CoordinationIssues.
- NetworkGraph-Contracts und startpunktbasierte Schema-Nummerierungs-Vorschau fuer `TGA_SchemaStrangNr` vorbereitet.
- Beispiel-JSON `samples/network-graph.schema-preview.sample.json` und Tests fuer Graph, Kanten, Startpunkte und Preview-Zuordnungen ergaenzt.
- Schema-Regelsets, Flow-Richtungsmetadaten und explizite WriteBack-Kandidaten als Preview-Vertrag ergaenzt.
- NuGet-/GitHub-Package-Metadaten fuer `RevitApi.Contracts` vorbereitet.
- GitHub-Actions-Workflow zum Packen und Publizieren nach GitHub Packages ergaenzt.
- Packaging-Dokumentation unter `docs/packaging.md` erstellt.
- Lokale Dateisystem-Feed-Erzeugung ueber `scripts/publish-local-contracts-package.ps1` vorbereitet, damit Paketmodus-Tests ohne GitHub Packages moeglich sind.
- Tag `v0.1.3` wurde fuer den aktuellen Contract-Stand erstellt und nach GitHub gepusht.
- GitHub-Actions-Lauf `27422850699` fuer `v0.1.3` hat Restore, Build, Tests und Pack erfolgreich ausgefuehrt; der Schritt `Publish to GitHub Packages` ist fehlgeschlagen.
- Package-Workflow nutzt jetzt bevorzugt `CONTRACTS_PACKAGES_TOKEN` fuer GitHub-Packages-Publish und faellt nur ohne Secret auf `GITHUB_TOKEN` zurueck.

## Offene Punkte

- Schema-Versionierung und Kompatibilitaetsregeln dokumentieren.
- Finale WriteBack-Parameterliste fachlich bestaetigen.
- Fachliche Klassifikations- und Parameterkonventionen fuer Architektur, Tragwerk, Elektro, MSR, Brandschutz und Landschaft definieren.
- Weitere Beispielvarianten fuer TGA-Export, Validation-Import und WriteBack ergaenzen.
- GitHub-Packages-Publish fuer `v0.1.3` reparieren und Konsumentenbuild erneut pruefen.
- Repository-Secret `CONTRACTS_PACKAGES_TOKEN` mit `write:packages` und `read:packages` setzen und Workflow manuell fuer Package-Version `0.1.3` starten.
- GitHub-Packages-Auth-/Package-Zugriff bleibt offen; lokale Pakettests laufen ueber Dateisystem-Feed.

## Naechste Issue-Vorschlaege

1. JSON-Beispieldateien fuer TGA-Export, Validation und WriteBack anlegen.
2. Schema-Versionierungsregeln fuer Contracts definieren.
3. GitHub-Packages-Publish fuer `RevitApi.Contracts` `0.1.3` reparieren und Paketnutzung in `Revit-TGA-Platform` gegen GitHub Packages pruefen.
4. WriteBack-Parameterkatalog fachlich abstimmen.
5. Contract-Tests gegen Beispiel-JSON-Dateien ergaenzen.
6. Multidisziplinaeres Beispiel gegen reale Projektparameter spiegeln.
