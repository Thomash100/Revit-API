# Examples

## Multidisciplinary BIM Exchange

The repository includes this checked contract fixture:

```text
samples/bim-exchange.multidisciplinary.sample.json
```

It represents a `BimExchangeDocument` with:

- one project
- two levels: `EG` and `OG01`
- multiple elements for Architecture, Structural, Heating, Ventilation, Sanitary, Electrical, MSR, FireProtection, and Landscape
- `UniqueId` and `ElementId` on every model element
- `ElementClassification` on every model element
- `ParameterSets` with declared `dataType` and optional `unit`
- `ValidationIssues`
- `CoordinationIssues`

The example is intentionally a contract fixture, not a productive Revit export. Tests in `RevitApi.Contracts.Tests` deserialize it and verify the required shape.

## Network Graph Schema Preview

The repository includes this checked contract fixture:

```text
samples/network-graph.schema-preview.sample.json
```

It represents a `NetworkGraphDocument` with:

- one ventilation graph
- graph nodes for start equipment, distribution segment, and terminal
- directed connector edges between nodes
- start-point based numbering preview
- preview assignments for `TGA_SchemaStrangNr`

The preview assignments are read-only contract data. They do not represent a WriteBack request and do not modify a Revit model.

## Parameter Values

`ParameterValue.Value` is stored as a string so the JSON contract stays simple and independent from Revit storage types. Type stability is represented by `dataType` and `unit`, for example:

```json
{
  "name": "Diameter",
  "value": "25",
  "dataType": "integer",
  "unit": "mm",
  "sourceApplication": "revit"
}
```
