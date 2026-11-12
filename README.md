# Print Track 

## 1. Prérequis
  - net10.0 [https://dotnet.microsoft.com/fr-fr/download/dotnet/10.0](https://dotnet.microsoft.com/fr-fr/download/dotnet/10.0)

## 2. Installation

```
# Cloner le dépôt
git clone https://github.com/VOTRE_COMPTE/PrintTrack

cd PrintTrack

#Compilation
dotnet build 
```

## 3. Lancement 

```
dotnet run
```

## 4. Construire un exécutable. 

### Linux 
```
dotnet publish -c Release -r linux-x64
```

### Windows 
```
dotnet publish -c Release -r win-x64
```

Le logiciel est sous license open source : [LICENSE.md](LICENSE.md)

Les OIDS utilisés par l'application sont issus de ce projet : [https://github.com/PaoloDSP/PrinterMonitor/blob/main/OIDS.md](https://github.com/PaoloDSP/PrinterMonitor/blob/main/OIDS.md)
