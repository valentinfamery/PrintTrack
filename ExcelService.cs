using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using PrintTrack.Models;
using static DocumentFormat.OpenXml.Packaging.SpreadsheetDocument;

public class ExcelService{
    public ExcelService(){

    }
    public static async Task<List<Printer>> ReadCmdbSheet(string filePath)
    {
        List<Printer> printers = [];


        using (SpreadsheetDocument doc = SpreadsheetDocument.Open(filePath, false))
        {
            WorkbookPart? workbookPart = doc.WorkbookPart;
            if (workbookPart?.Workbook?.Sheets == null)
            {
                Console.WriteLine("Le fichier Excel ne contient aucune feuille.");
                return [];
            }

            // Recherche de la feuille par son nom exact "CMDB"
            Sheet? cmdbSheet = workbookPart.Workbook.Sheets
                .Elements<Sheet>()
                .FirstOrDefault(s => string.Equals(s.Name?.Value, "CMDB", StringComparison.OrdinalIgnoreCase));

            if (cmdbSheet?.Id?.Value == null)
            {
                Console.WriteLine("La feuille 'CMDB' n'a pas été trouvée.");
                return [];
            }

            WorksheetPart worksheetPart = (WorksheetPart)workbookPart.GetPartById(cmdbSheet.Id.Value);
            SheetData? sheetData = worksheetPart.Worksheet.Elements<SheetData>().FirstOrDefault();

            if (sheetData == null) return [];

            SharedStringTablePart? sstPart = workbookPart.SharedStringTablePart;
            SharedStringTable? sst = sstPart?.SharedStringTable;

            
            // Parcourt les lignes en ignorant la première (en-tête)
        foreach (Row row in sheetData.Elements<Row>().Skip(1))
        {
            // Récupération sous forme de dictionnaire ou tableau ordonné
            var cells = row.Elements<Cell>().ToList();
            if (cells.Count == 0) continue;

            // Exemple de mapping basé sur l'index des colonnes :
            // Colonne 0 (A) = Nom/IP, Colonne 1 (B) = Modèle, Colonne 2 (C) = Emplacement...
            string Site = GetCellValue(cells.ElementAtOrDefault(1), sst);
            string Categorie = GetCellValue(cells.ElementAtOrDefault(2), sst);
            string AdresseIP = GetCellValue(cells.ElementAtOrDefault(3), sst);
            string Numeroetiquette = GetCellValue(cells.ElementAtOrDefault(4), sst);
            string Marque = GetCellValue(cells.ElementAtOrDefault(5), sst);
            string Modele = GetCellValue(cells.ElementAtOrDefault(6), sst);
            string Numserie = GetCellValue(cells.ElementAtOrDefault(7), sst);
            string AdresseMAC = GetCellValue(cells.ElementAtOrDefault(8), sst);
            string ConcatenerLocation = GetCellValue(cells.ElementAtOrDefault(12), sst);

            // Ignore les lignes vides si nécessaire
            if (string.IsNullOrWhiteSpace(Site) && string.IsNullOrWhiteSpace(AdresseIP))
                continue;

            // Instanciation et ajout de l'imprimante
            Printer printer = new Printer(
                Site,
                Categorie,
                AdresseIP,
                Numeroetiquette,
                Marque,
                Modele,
                Numserie,
                AdresseMAC,
                ConcatenerLocation
            );

            Console.WriteLine(printer.Numeroetiquette.ToString());

            if (AdresseIP != "RETIRE"){

            printers.Add(printer);

            }

        }


            return printers;
        }

        
    }

    private static string GetCellValue(Cell? cell, SharedStringTable? sst)
{
    if (cell == null || cell.CellValue == null) 
        return string.Empty;

    string value = cell.CellValue.Text;

    if (cell.DataType != null && cell.DataType.Value == CellValues.SharedString)
    {
        if (sst != null && int.TryParse(value, out int index))
        {
            return sst.ChildElements[index].InnerText;
        }
    }

    return value;
}


}