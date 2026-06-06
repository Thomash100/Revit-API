# Semantic Context For Revit-HLS-Exchange

## Purpose

This document records the shared semantic context for the Revit-HLS-Exchange workstream.
It complements Issue #1, which defines the first technical prototype, and Issue #2,
which keeps later Codex tasks, data checks, documentation, and analytics work consistent.

The core workflow is:

```text
Revit -> JSON Export -> Third-party validation -> JSON Import -> WriteBack to Revit
```

The first implementation is a local, file-based exchange prototype. It is not a full
IFC editor, web portal, cloud sync, user management system, REST service, or production
project-management platform.

## Controlling Sources

Use this precedence when project context differs across sources:

1. Implemented repository files, tests, sample files, and generated documentation.
2. GitHub Issue #1 for the initial technical prototype scope.
3. GitHub Issue #2 for semantic context, later Data Analytics, and consistency rules.
4. Local Codex semantic-layer files for working guidance.
5. Inferred Revit/API conventions, clearly marked as inferred.

## Scope

The semantic context covers:

- Revit project export metadata.
- Rooms.
- HLS/TGA and MEP elements.
- External validation results.
- WriteBack parameter updates.
- Summary counts and reporting concepts.
- Revit version scope.
- Acceptance and non-goal boundaries for future Codex tasks.

## Revit Version Scope

| Use | Version |
| --- | --- |
| Target implementation | Revit 2026 |
| Compatibility check | Revit 2025.3 |
| Migration watch | Revit 2027 |

Future reports and implementation notes should keep this version scope visible.

## Identity Rule

`UniqueId` is the leading reference for WriteBack and external result matching.

`ElementId` may be exported for diagnostics and local traceability, but it should not
become the primary external identity unless a later authoritative source changes this
rule.

## Separation Of Concerns

The project should keep these concerns separate:

| Concern | Expected Home |
| --- | --- |
| Revit add-in and API integration | `/RevitApiAddin` |
| DTOs, JSON contracts, serialization, import models, mapping logic | `/RevitExchange` |
| Example exports and imports | `/samples` |
| Exchange and WriteBack documentation | `/docs` |
| Tests that can run without Revit where possible | `/tests` |

This separation is important because DTOs, JSON serialization, import parsing, and
WriteBack mapping logic should remain testable even when a local Revit API build is
not available.

## Acceptance Context

Future Codex tasks and reviews should check that:

- The repository has a clear project structure.
- A Revit export command is prepared.
- Export data contains project info, summary counts, rooms, and elements.
- Rooms and HLS/TGA elements are handled separately.
- `UniqueId` is used as the leading WriteBack reference.
- Sample export and import files are available under `/samples`.
- WriteBack handles missing elements, missing parameters, and read-only parameters.
- Documentation explains export, import, WriteBack, `UniqueId`, scope boundaries, and Revit API references.
- A final report records implemented files, technical structure, open points, build/test status, Revit version notes, and the next recommended step.

## Open Semantic Questions

- Which shared parameters are final for WriteBack beyond the current sample names?
- Which room usage, equipment, and HLS/TGA parameters are required for real projects?
- How should broad `mepElements` counts avoid double counting more specific categories?
- Which official Revit API references should be cited for 2025.3, 2026, and 2027?
- Should future refresh checks watch GitHub Issues, `/docs`, `/samples`, and `/tests` automatically?

