# DateTime Behavior Modifier for Dataverse

An XrmToolBox plugin designed to bulk update the **Behavior** and **Format** of custom Date/Time fields across Dataverse tables without manual XML editing.

---

## Features

- **Automated Metadata Filtering**: Loads customizable tables and isolates custom Date/Time fields, excluding system-managed columns like `createdon` and `modifiedon`.
- **Intelligent Constraint Logic**:
  - Selecting **DateAndTime** format restricts options strictly to `UserLocal` and `TimeZoneIndependent`.
  - Selecting **DateOnly** format unlocks `DateOnly`, `UserLocal`, and `TimeZoneIndependent`.
- **Direct API Execution**: Employs Microsoft SDK `UpdateAttributeRequest` directly for rapid bulk updates.
- **Asynchronous Execution**: Background threading (`WorkAsync`) prevents UI freezes during heavy metadata retrieval and bulk updating operations.

---

## Installation

### Via XrmToolBox Tool Library
1. Open **XrmToolBox**.
2. Navigate to **Configuration** > **Tool Library**.
3. Search for `DateTime Behavior Modifier`.
4. Click **Install**.



