# Data Domain Model

## Purpose

This document describes the early data domain for the Revit-HLS-Exchange prototype.
It is based on Issue #1 and Issue #2 and should be refined once DTOs, sample files,
and tests are implemented.

The contract layer now also prepares a generic BIM exchange model. TGA/HLS remains the
first compatible domain, while future consumers can add architecture, structural,
electrical, MSR, fire protection, landscape, and coordination data through the generic
model without renaming existing TGA contracts.

## Core Entities

| Entity | Meaning | Grain | Primary Identifiers |
| --- | --- | --- | --- |
| Project export | One JSON export from an active Revit model | Export run | Project name, export date, Revit version |
| Summary | Counts for key exported categories | Export run | Export file |
| Room | Revit room included in the export | One room | Room number, Revit identifiers when available |
| HLS/TGA element | Revit building-services component included in the export | One element | `UniqueId`, `ElementId` |
| Validation result | External checking result returned for an element | One result per checked element/rule combination unless later defined otherwise | `uniqueId`, `sourceSystem`, `ruleCode` |
| WriteBack map | Parameter updates proposed from a validation result | One parameter map per validation result | `uniqueId`, parameter names |

## Generic BIM Entities

| Entity | Meaning | Grain | Primary Identifiers |
| --- | --- | --- | --- |
| Discipline | Fachdisziplin such as architecture, structural, TGA, electrical, MSR, fire protection, landscape, or coordination | Enum value | Discipline name |
| BIM level | Generic building level or floor included in an exchange document | One level | Level id, name |
| ModelElement | Generic BIM element independent of Revit API types | One element | `UniqueId`, `ElementId`, optional `GlobalId` |
| ElementClassification | Classification assigned by a system such as DIN, IFC, or office standard | One classification per element context | Classification system and code |
| SourceApplication | Source system for element, parameter, issue, or result data | Enum value | Source name |
| ParameterValue | Single named parameter value with optional unit and data type | One parameter value | Parameter name |
| ParameterSet | Group of parameter values by source and discipline | One parameter set per source/discipline context | Parameter set name |
| ValidationIssue | Generic validation issue for any discipline | One issue per element/rule combination unless later defined otherwise | `uniqueId`, `ruleCode`, source |
| CoordinationIssue | Cross-discipline coordination issue | One coordination issue | Issue id, related `UniqueId` values |

## Multidisciplinary Sample

The current repository includes a checked sample at:

```text
samples/bim-exchange.multidisciplinary.sample.json
```

The sample contains one project, two levels, multiple elements per listed discipline,
typed parameter declarations, validation issues, and coordination issues. Unit tests
load this JSON file directly to keep the generic BIM contract practically verifiable.

## Minimum Export Shape

```json
{
  "project": {
    "name": "Projektname",
    "revitVersion": "2026",
    "exportDate": "2026-06-04T00:00:00",
    "source": "Revit-API"
  },
  "summary": {
    "pipes": 0,
    "pipeFittings": 0,
    "pipeAccessories": 0,
    "plumbingFixtures": 0,
    "mepElements": 0,
    "rooms": 0
  },
  "rooms": [],
  "elements": []
}
```

## Project Fields

| Field | Meaning | Notes |
| --- | --- | --- |
| `name` | Revit project name | Required by Issue #1 |
| `revitVersion` | Revit version used for export | Target is 2026 |
| `exportDate` | Export timestamp | Use ISO 8601; timezone handling should be explicit if needed |
| `source` | Source label | Initial value: `Revit-API` |

## Summary Fields

| Field | Meaning | Notes |
| --- | --- | --- |
| `pipes` | Count of exported pipe elements | Revit category mapping must be verified |
| `pipeFittings` | Count of exported pipe fittings | Revit category mapping must be verified |
| `pipeAccessories` | Count of exported pipe accessories | Revit category mapping must be verified |
| `plumbingFixtures` | Count of exported plumbing fixtures | Maps to `Sanitaerobjekte` in the German task text |
| `mepElements` | Count of general HLS/TGA or MEP elements | Define whether this overlaps with specific categories |
| `rooms` | Count of exported rooms | Should reconcile to `rooms.length` |

## Required Room Data

| Field Group | Meaning |
| --- | --- |
| Room number | Revit room number |
| Room name | Revit room name |
| Level | Associated Revit level |
| Area | Room area |
| Height | Room height |
| Volume | Room volume |
| Usage/equipment parameters | Relevant available usage or equipment parameters |

## Required Element Data

| Field | Meaning | Notes |
| --- | --- | --- |
| `ElementId` | Revit document/session element identifier | Diagnostic/local traceability |
| `UniqueId` | Leading stable identity for WriteBack | Required for external result matching |
| Category | Revit category | Required |
| Family | Revit family | Required |
| Type | Revit type | Required |
| Level | Associated level where available | Required where available |
| System name | MEP system name where available | Missing values may drive validation warnings |
| Selected parameters | Relevant project/domain parameters | Final list is open |

## Validation Import Shape

Expected sample path:

```text
/samples/validation-results.sample.json
```

Example structure:

```json
{
  "results": [
    {
      "uniqueId": "REVT-UNIQUE-ID",
      "status": "warning",
      "sourceSystem": "IFC-Editor",
      "ruleCode": "HLS-001",
      "message": "Bauteil ohne eindeutige Systemzuordnung.",
      "writeBack": {
        "SM_Pruefstatus": "Warnung",
        "SM_Pruefhinweis": "Bauteil ohne eindeutige Systemzuordnung.",
        "SM_Quelle": "IFC-Editor"
      }
    }
  ]
}
```

## Validation Result Fields

| Field | Meaning | Notes |
| --- | --- | --- |
| `uniqueId` | Revit `UniqueId` for element lookup | Required for WriteBack |
| `status` | External validation status | Allowed values not yet finalized |
| `sourceSystem` | External source system | Examples: IFC-Editor, Projektverwaltung, Pruefregel-Engine, Bauteilkatalog, Raumbuch |
| `ruleCode` | External validation rule code | Example: `HLS-001` |
| `message` | Human-readable validation message | Useful for display and hints |
| `writeBack` | Parameter name/value map | Must pass WriteBack safety rules |

## WriteBack Safety Rules

Write parameter values only when:

1. The element is found by `UniqueId`.
2. The parameter exists.
3. The parameter is not read-only.
4. The change runs inside a Revit transaction.
5. Failures are logged and do not crash the full import.

## Relationships

| From | To | Join Key | Notes |
| --- | --- | --- | --- |
| Validation result | Exported element | `validationResult.uniqueId = element.UniqueId` | Main external result matching path |
| WriteBack map | Revit element parameter | `uniqueId` plus parameter name | Requires Revit element lookup and parameter checks |
| Summary | Rooms/elements arrays | Category and array counts | Reconciliation rules must account for category overlap |

## Open Model Questions

- Should the export schema include a schema version?
- Should validation imports include run metadata, source export hash, or project identifiers?
- What are the canonical allowed values for validation `status`?
- Which parameters are mandatory versus optional for rooms and elements?
- How should duplicate validation results for the same `uniqueId` and `ruleCode` be handled?
