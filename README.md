# IT Toolkit

Ein schlankes Windows-Kommandozeilentool zur Systemdiagnose, entwickelt mit C# und .NET 10. Ermöglicht schnelle Systemprüfungen ohne externe Abhängigkeiten außer der .NET-Laufzeitumgebung.

## Funktionen

| Befehl | Beschreibung |
|--------|--------------|
| `CI` | Prüft die Internetverbindung via asynchronem HTTP-Request |
| `OS` | Zeigt CPU, RAM, Festplattennutzung, OS-Version und Architektur |

## Technologie-Stack

- **Sprache:** C# 13 / .NET 10
- **Zielplattform:** Windows (`net10.0-windows`)
- **Verwendete APIs:** `System.Management` (WMI), `System.Net.Http.HttpClient`, `System.Runtime.InteropServices`
- **Patterns:** `async/await`, Nullable Reference Types, WMI-Abfragen via `ManagementObjectSearcher`

## Erste Schritte

**Voraussetzungen:** .NET 10 SDK, Windows OS

```bash
# Klonen und bauen
git clone <repo-url>
cd csproj
dotnet build

# Ausführen
dotnet run
```

**Beispielsitzung:**
```
What do you want to check? (CI for internet connection, OS for system details): OS
CPU: Intel(R) Core(TM) i7-10750H CPU @ 2.60GHz
RAM: 15.83 GB
Disk C: (Windows): 120.4 GB free of 476.9 GB
OS: Microsoft Windows 10.0.22631
OS Architecture: X64
```

## Projektstruktur

```
csproj/
├── Program.cs              # Einstiegspunkt und Befehlsverarbeitung
├── Commands/
│   └── SystemInformation.cs  # WMI-basierte Hardware-/OS-Abfragen
└── Models/
    └── Status.cs
Services/
└── InternetServices.cs     # Asynchrone Verbindungsprüfung
```
