# Buildable Abstractions [![NuGet](https://img.shields.io/nuget/v/Pustalorc.Logging.API.svg)](https://www.nuget.org/packages/Pustalorc.BuildableAbstractions/)

Unturned Library to abstract all buildables (Barricades & Structures) in unturned.  
Download is available on [Github Releases](https://github.com/Pustalorc/BuildableAbstractions/releases/).  
For developers, you should use the [NuGet Package](https://www.nuget.org/packages/Pustalorc.BuildableAbstractions/).

Note that this Library can also be ran as a Plugin in order to utilize the following commands and their functionality.

---

## Commands

`/findbuilds [b | s | v] [player] [item] [radius]` - Finds and returns the count of buildables with the filters.  
`/removebuildable` - Removes the buildable that the player is currently looking at.  
`/teleporttobuild [b | s | v] [player] [item]` - Teleports to a random buildable that satisfies the filters.  
`/topbuilders [v]` - Lists the top 5 players that have the most amount of buildables on the map.  
`/wreck [b | s | v] [player] [item] [radius]` and `/wreck [abort | confirm]` - Wrecks all of the buildables that satisfy
the filters. Requires confirmation before fully wrecking them.  
`/wreckvehicle` - Wrecks all of the buildables without confirmation on the vehicle that you are facing.

### Explanation of arguments:

Arguments can be on any order, so doing: `/wreck b pusta birch 5.0` should be the same as `/wreck birch b 5.0 pusta`  
`[b | s | v]` specifies filters for all of the buildables. It can specify to filter JUST for barricades (`b`), JUST for
structures (`s`), or INCLUDE buildables on vehicles (`v`).  
`[player]` self-explanatory. Accepts Steam64ID as well as the name.  
`[item]` self-explanatory. Accepts item names (including just typing `birch`) as well as item IDs. Using item names will
select all results with that name, so be careful if you only write one letter!  
`[radius]` self-explanatory. Note that typing `5` will be considered an item if you do not specify an item by name or ID
before it! To prevent this, type `.0` at the end of the number, that will force it to be detected as a radius.  
`[abort | confirm]` - self-explanatory, aborts or confirms previous action

---

## Default Translations:

```xml
<?xml version="1.0" encoding="utf-8"?>
<Translations xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
  <Translation Id="not_available" Value="N/A" />
  <Translation Id="cannot_be_executed_from_console" Value="That command cannot be executed from console with those arguments!" />
  <Translation Id="build_count" Value="There are a total of {0} builds. Specific Item: {1}, Radius: {2}, Player: {3}, Planted Barricades Included: {4}, Filter by Barricades: {5}, Filter by Structures: {6}" />
  <Translation Id="not_looking_buildable" Value="Cannot get any info! Are you looking at a structure/barricade?" />
  <Translation Id="cannot_teleport_no_builds" Value="Cannot teleport anywhere, no buildables found with the following filters. Specific Item: {0}, Player: {1}, Planted Barricades Included: {2}, Filter by Barricades: {3}, Filter by Structures: {4}" />
  <Translation Id="cannot_teleport_builds_too_close" Value="Cannot teleport anywhere, all buildables with the specified filters are too close. Specific Item: {0}, Player: {1}, Planted Barricades Included: {2}, Filter by Barricades: {3}, Filter by Structures: {4}" />
  <Translation Id="top_builder_format" Value="At number {0}, {1} with {2} buildables!" />
</Translations>
```