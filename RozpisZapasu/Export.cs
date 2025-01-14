﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

namespace RozpisZapasu
{
    public static class Export
    {
        public static int VitezneSety { get; set; }
        /// <summary>
        /// Export dokumentu ve formátu MS Excel
        /// </summary>
        /// <param name="nazev">nazev dokumentu</param>
        /// <param name="makra">podpora maker (*.xlsm)</param>
        /// <param name="skupinyTymy">vstupní seznam skupin a týmů v nich obsažených</param>
        /// <param name="hristeZapasy">zápasy na hřištích</param>
        /// <param name="skupinyZapasy">zápasy ve skupinách</param>
        public static void UlozitExcel(string nazev, bool makra, List<(string, string)> skupinyTymy, List<(string, string)> hristeZapasy, List<(string, string)> skupinyZapasy)
        {
            byte[] byteArray;

            if (makra == true)
            {
                byteArray = File.ReadAllBytes("sablonyExcel\\sablona.xltm");
                using (MemoryStream stream = new MemoryStream())
                {
                    stream.Write(byteArray, 0, (int)byteArray.Length);
                    using (SpreadsheetDocument doc = SpreadsheetDocument.Open(stream, true))
                    {
                        //změna typu dokumentu
                        doc.ChangeDocumentType(SpreadsheetDocumentType.MacroEnabledWorkbook);
                        VytvoritObsah(doc, skupinyTymy, hristeZapasy, skupinyZapasy);
                    }
                    File.WriteAllBytes(nazev, stream.ToArray());
                }
            }
            else
            {
                byteArray = File.ReadAllBytes("sablonyExcel\\sablona.xltx");
                using (MemoryStream stream = new MemoryStream())
                {
                    stream.Write(byteArray, 0, (int)byteArray.Length);
                    using (SpreadsheetDocument doc = SpreadsheetDocument.Open(stream, true))
                    {
                        //změna typu dokumentu
                        doc.ChangeDocumentType(SpreadsheetDocumentType.Workbook);
                        VytvoritObsah(doc, skupinyTymy, hristeZapasy, skupinyZapasy);
                    }
                    File.WriteAllBytes(nazev, stream.ToArray());
                }
            }
        }

        private static void VytvoritObsah(SpreadsheetDocument doc, List<(string, string)> skupinyTymy, List<(string, string)> hristeZapasy, List<(string, string)> skupinyZapasy)
        {
            WorkbookPart wbPart = doc.WorkbookPart;

            //obsah listů
            WorksheetPart wsPart1 = (WorksheetPart)wbPart.GetPartById(doc.WorkbookPart.Workbook.Descendants<Sheet>().First(s => s.Name.Equals("Křížová tabulka")).Id);
            KrizovaTabulka(wsPart1, skupinyTymy);

            WorksheetPart wsPart2 = (WorksheetPart)wbPart.GetPartById(doc.WorkbookPart.Workbook.Descendants<Sheet>().First(s => s.Name.Equals("Klasická tabulka")).Id);
            KlasickaTabulka(wsPart2, skupinyTymy);

            WorksheetPart wsPart3 = (WorksheetPart)wbPart.GetPartById(doc.WorkbookPart.Workbook.Descendants<Sheet>().First(s => s.Name.Equals("Každý s každým")).Id);
            ZapasyHriste(wsPart3, hristeZapasy);

            WorksheetPart wsPart4 = (WorksheetPart)wbPart.GetPartById(doc.WorkbookPart.Workbook.Descendants<Sheet>().First(s => s.Name.Equals("Skupinový turnaj")).Id);
            ZapasySkupina(wsPart4, skupinyZapasy, skupinyTymy);

            //Přidání stylu
            WorkbookStylesPart stylePart = doc.WorkbookPart.WorkbookStylesPart;
            stylePart.Stylesheet = GenerateStylesheet();
            stylePart.Stylesheet.Save();
        }

        /// <summary>
        /// Provede zápis do listu Křížová tabulka
        /// </summary>
        /// <param name="wsPart">část list</param>
        /// <param name="skupinyTymy">vstupní seznam skupin a týmů v nich obsažených</param>
        private static void KrizovaTabulka(WorksheetPart wsPart, List<(string, string)> skupinyTymy)
        {
            string[] hlavicka = new string[] { "Míče", "Sety", "Body" };
            
            List<string> tymy = ListNazvu(1, skupinyTymy, null);
            List<string> skupiny = ListNazvu(3, skupinyTymy, null);

            Worksheet ws = wsPart.Worksheet;
            SheetData sd = ws.GetFirstChild<SheetData>();

            Columns cols = ws.InsertBefore(new Columns(), sd);
            Column col = new Column() { Min = (UInt32Value)1U, Max = (UInt32Value)1U, Width = tymy.Max(tym => tym.Length), CustomWidth = true };
            cols.Append(col);

            //počátek tabulky
            int offset = 0;

            for (int i = 0; i < skupiny.Count; i++)
            {
                //vyčištění seznamu týmů a zápis týmů dané skupiny
                tymy.Clear();
                tymy = ListNazvu(2, skupinyTymy, skupiny[i]);
                //zápis tabulky
                for (int radek = 0; radek < tymy.Count + 1; radek++)
                {
                    for (int sloupec = 0; sloupec < tymy.Count + hlavicka.Length + 1; sloupec++)
                    {
                        Row row = new Row { RowIndex = (UInt32)(radek + offset + 1) };
                        Cell cell;

                        //vybarvení
                        if (radek == sloupec)
                        {
                            cell = new Cell();
                            cell.DataType = CellValues.String;
                            cell.CellReference = SloupecNaZnak(sloupec + 1) + (radek + offset + 1).ToString();
                            cell.StyleIndex = 2;
                            row.Append(cell);
                        }
                        else
                        {
                            //vyplnění týmů
                            if (sloupec < tymy.Count + 1)
                            {
                                //týmy v řádku
                                if (radek == 0 && sloupec > 0)
                                {
                                    cell = new Cell();
                                    cell.CellValue = new CellValue(tymy[sloupec - 1]);
                                    cell.CellReference = SloupecNaZnak(sloupec + 1) + (radek + offset + 1).ToString();
                                    cell.DataType = CellValues.String;
                                    cell.StyleIndex = 4;
                                    row.Append(cell);
                                }
                                //týmy ve sloupci
                                else if (radek > 0 && sloupec == 0)
                                {
                                    cell = new Cell();
                                    cell.CellValue = new CellValue(tymy[radek - 1]);
                                    cell.CellReference = SloupecNaZnak(sloupec + 1) + (radek + offset + 1).ToString();
                                    cell.DataType = CellValues.String;
                                    cell.StyleIndex = 2;
                                    row.Append(cell);
                                }
                                //ohraničení zbytku v tabulce
                                else if (radek > 0 & sloupec > 0)
                                {
                                    cell = new Cell();
                                    cell.CellReference = SloupecNaZnak(sloupec + 1) + (radek + offset + 1).ToString();
                                    cell.StyleIndex = 1;
                                    row.Append(cell);
                                }
                            }
                        }
                        //vyplnění hlavičky vedle týmů
                        if (sloupec >= tymy.Count + 1)
                        {
                            if (radek == 0)
                            {
                                cell = new Cell();
                                cell.CellValue = new CellValue(hlavicka[sloupec - (tymy.Count + 1)]);
                                cell.CellReference = SloupecNaZnak(sloupec + 1) + (radek + offset + 1).ToString();
                                cell.DataType = CellValues.String;
                                cell.StyleIndex = 5;
                                row.Append(cell);
                            }
                            //ohraničení zbytku pod hlavičkou
                            else if (radek > 0)
                            {
                                cell = new Cell();
                                cell.CellReference = SloupecNaZnak(sloupec + 1) + (radek + offset + 1).ToString();
                                cell.StyleIndex = 1;
                                row.Append(cell);
                            }
                        }
                        sd.Append(row);
                    }
                }
                //přičtení offsetu
                offset += tymy.Count + 3;
            }
        }
        /// <summary>
        /// Provede zápis do listu Klasická tabulka
        /// </summary>
        /// <param name="wsPart">část list</param>
        /// <param name="skupinyTymy">vstupní seznam skupin a týmů v nich obsažených</param>
        private static void KlasickaTabulka(WorksheetPart wsPart, List<(string, string)> skupinyTymy)
        {
            string[] hlavicka = new string[] { "Pořadí", "Tým", "Výhry", "Prohry", "Míče", "Sety", "Body" };
            List<string> tymy = ListNazvu(1, skupinyTymy, null);
            List<string> skupiny = ListNazvu(3, skupinyTymy, null);

            Worksheet ws = wsPart.Worksheet;
            SheetData sd = ws.GetFirstChild<SheetData>();

            Columns cols = ws.InsertBefore(new Columns(), sd);
            Column col = new Column() { Min = (UInt32Value)2U, Max = (UInt32Value)2U, Width = tymy.Max(tym => tym.Length), CustomWidth = true };
            cols.Append(col);

            //počátek tabulky
            int offset = 0;
            
            for (int i = 0; i < skupiny.Count; i++)
            {
                //vyčištění seznamu týmů a zápis týmů dané skupiny
                tymy.Clear();
                tymy = ListNazvu(2, skupinyTymy, skupiny[i]);
                //zápis tabulky
                for (int radek = 0; radek < tymy.Count + 1; radek++)
                {
                    for (int sloupec = 0; sloupec < hlavicka.Length; sloupec++)
                    {
                        Row row = new Row { RowIndex = (UInt32)(radek + offset + 1) };
                        Cell cell;

                        //vyplnění hlavičky
                        if (radek == 0)
                        {
                            cell = new Cell();
                            cell.CellValue = new CellValue(hlavicka[sloupec]);
                            cell.CellReference = SloupecNaZnak(sloupec + 1) + (radek + offset + 1).ToString();
                            cell.DataType = CellValues.String;
                            cell.StyleIndex = 2;
                            row.Append(cell);
                        }
                        else if (radek > 0)
                        {
                            //vyplnění pořadí
                            if (sloupec == 0)
                            {
                                cell = new Cell();
                                cell.CellValue = new CellValue(radek);
                                cell.CellReference = SloupecNaZnak(sloupec + 1) + (radek + offset + 1).ToString();
                                cell.DataType = CellValues.Number;
                                cell.StyleIndex = 1;
                                row.Append(cell);
                            }
                            //vyplnění týmů
                            else if (sloupec == 1)
                            {
                                cell = new Cell();
                                cell.CellValue = new CellValue(tymy[radek - 1]);
                                cell.CellReference = SloupecNaZnak(sloupec + 1) + (radek + offset + 1).ToString();
                                cell.DataType = CellValues.String;
                                cell.StyleIndex = 1;
                                row.Append(cell);
                            }
                            //ohraničení zbytku
                            else if (sloupec > 1)
                            {
                                cell = new Cell();
                                cell.CellReference = SloupecNaZnak(sloupec + 1) + (radek + offset + 1).ToString();
                                cell.StyleIndex = 1;
                                row.Append(cell);
                            }
                        }
                        sd.Append(row);
                    }
                }
                offset += tymy.Count + 3;
            }
        }
        /// <summary>
        /// Provede zápis do listu Skupinový turnaj
        /// </summary>
        /// <param name="wsPart">část list</param>
        /// <param name="skupinyZapasy">zápasy ve skupinách</param>
        /// <param name="skupinyTymy">vstupní seznam skupin a týmů v nich obsažených</param>
        private static void ZapasySkupina(WorksheetPart wsPart, List<(string, string)> skupinyZapasy, List<(string, string)> skupinyTymy)
        {
            string[] hlavicka = new string[] { "Kolo", "Zápas", "Skóre" };
            List<string> skupiny = ListNazvu(3, skupinyTymy, null);
            List<(string, string)> zapasySkupiny;

            Worksheet ws = wsPart.Worksheet;
            SheetData sd = ws.GetFirstChild<SheetData>();

            Columns cols = ws.InsertBefore(new Columns(), sd);
            Column col = new Column() { Min = (UInt32Value)2U, Max = (UInt32Value)2U, Width = NejdelsiRetezec(skupinyZapasy, 1), CustomWidth = true };
            cols.Append(col);

            //počátek tabulky
            int offset = 0;

            for (int i = 0; i < skupiny.Count; i++)
            {
                //zápis zápasů dané skupiny
                zapasySkupiny = ListZapasuSkupin(skupinyZapasy, skupiny[i]);
                //zápis tabulky
                for (int radek = 0; radek < zapasySkupiny.Count + 2; radek++)
                {
                    for (int sloupec = 0; sloupec < hlavicka.Length; sloupec++)
                    {
                        Row row = new Row { RowIndex = (UInt32)(radek + offset + 1) };
                        Cell cell;

                        //skupina
                        if (radek == 0)
                        {
                            cell = new Cell();
                            cell.CellValue = new CellValue("Skupina: " + skupiny[i]);
                            cell.DataType = CellValues.String;
                            row.Append(cell);
                        }
                        //vyplnění hlavičky
                        else if (radek == 1)
                        {
                            cell = new Cell();
                            cell.CellValue = new CellValue(hlavicka[sloupec]);
                            cell.CellReference = SloupecNaZnak(sloupec + 1) + (radek + offset + 1).ToString();
                            cell.DataType = CellValues.String;
                            cell.StyleIndex = 2;
                            row.Append(cell);
                        }
                        //vyplnění zbytku
                        else if (radek > 1)
                        {
                            cell = new Cell();
                            cell.CellValue = new CellValue(radek - 1);
                            cell.CellReference = SloupecNaZnak(1) + (radek + offset + 1).ToString();
                            cell.DataType = CellValues.Number;
                            cell.StyleIndex = 1;
                            row.Append(cell);

                            cell = new Cell();
                            cell.CellValue = new CellValue(zapasySkupiny[radek - 2].Item1);
                            cell.CellReference = SloupecNaZnak(2) + (radek + offset + 1).ToString();
                            cell.DataType = CellValues.String;
                            cell.StyleIndex = 1;
                            row.Append(cell);

                            cell = new Cell();
                            cell.CellReference = SloupecNaZnak(3) + (radek + offset + 1).ToString();
                            cell.StyleIndex = 1;
                            row.Append(cell);
                        }
                        sd.Append(row);
                    }
                }
                offset += zapasySkupiny.Count + 4;

                //vyčištění seznamu zápasů dané skupiny
                zapasySkupiny.Clear();
            }
        }
        /// <summary>
        /// Provede zápis do listu Každý s každým
        /// </summary>
        /// <param name="wsPart">část list</param>
        /// <param name="hristeZapasy">zápasy na hřištích</param>
        /// <param name="pocetSetu">počet setů</param>
        private static void ZapasyHriste(WorksheetPart wsPart, List<(string, string)> hristeZapasy)
        {
            string[] hlavicka = new string[] { "Kolo", "Zápas", "Hřiště", "Skóre" };
            //pole setů
            string[] sety = new string[(VitezneSety * 2) - 1];
            for (int i = 0; i < sety.Length; i++)
                sety[i] = i + 1 + ". set";

            Worksheet ws = wsPart.Worksheet;
            SheetData sd = ws.GetFirstChild<SheetData>();

            Columns cols = ws.InsertBefore(new Columns(), sd);
            Column col = new Column() { Min = (UInt32Value)2U, Max = (UInt32Value)2U, Width = NejdelsiRetezec(hristeZapasy, 1), CustomWidth = true };
            Column col1 = new Column() { Min = (UInt32Value)3U, Max = (UInt32Value)3U, Width = NejdelsiRetezec(hristeZapasy, 2), CustomWidth = true };
            cols.Append(col);
            cols.Append(col1);

            MergeCells mergeCells = new MergeCells();
            MergeCell mergeCell = new MergeCell();

            for (int radek = 0; radek < hristeZapasy.Count + 2; radek++)
            {
                for (int sloupec = 0; sloupec < hlavicka.Length + sety.Length + 1; sloupec++)
                {
                    Row row = new Row { RowIndex = (UInt32)(radek + 1) };
                    Cell cell;

                    //perioda
                    if (radek == 0)
                    {
                        cell = new Cell();
                        cell.DataType = CellValues.String;
                        cell.StyleIndex = 2;
                        //sloučení buňek
                        mergeCell.Reference = new StringValue(SloupecNaZnak(1) + (radek + 1) + ":" + SloupecNaZnak(hlavicka.Length + sety.Length + 1) + (radek + 1));
                        row.Append(cell);
                    }
                    //vyplnění hlavičky
                    else if (radek == 1)
                    {
                        if (sloupec < hlavicka.Length)
                        {
                            cell = new Cell();
                            cell.CellValue = new CellValue(hlavicka[sloupec]);
                            cell.CellReference = SloupecNaZnak(sloupec + 1) + (radek + 1).ToString();
                            cell.DataType = CellValues.String;
                            cell.StyleIndex = 2;
                            row.Append(cell);
                        }
                        else if (sloupec >= hlavicka.Length && sloupec < hlavicka.Length + sety.Length)
                        {
                            cell = new Cell();
                            cell.CellValue = new CellValue(sety[sloupec - hlavicka.Length]);
                            cell.CellReference = SloupecNaZnak(sloupec + 1) + (radek + 1).ToString();
                            cell.DataType = CellValues.String;
                            cell.StyleIndex = 2;
                            row.Append(cell);
                        }
                        else if (sloupec >= hlavicka.Length + sety.Length)
                        {
                            cell = new Cell();
                            cell.CellValue = new CellValue("Počet míčů");
                            cell.CellReference = SloupecNaZnak(sloupec + 1) + (radek + 1).ToString();
                            cell.DataType = CellValues.String;
                            cell.StyleIndex = 2;
                            row.Append(cell);
                        }
                    }
                    //vyplnění zbytku
                    else if (radek > 1)
                    {
                        cell = new Cell();
                        cell.CellValue = new CellValue(radek - 1);
                        cell.CellReference = SloupecNaZnak(1) + (radek + 1).ToString();
                        cell.DataType = CellValues.Number;
                        cell.StyleIndex = 1;
                        row.Append(cell);

                        cell = new Cell();
                        cell.CellValue = new CellValue(hristeZapasy[radek - 2].Item1);
                        cell.CellReference = SloupecNaZnak(2) + (radek + 1).ToString();
                        cell.DataType = CellValues.String;
                        cell.StyleIndex = 1;
                        row.Append(cell);

                        cell = new Cell();
                        cell.CellValue = new CellValue(hristeZapasy[radek - 2].Item2);
                        cell.CellReference = SloupecNaZnak(3) + (radek + 1).ToString();
                        cell.DataType = CellValues.String;
                        cell.StyleIndex = 1;
                        row.Append(cell);

                        //ohraničení zbytku
                        if (sloupec > 2)
                        {
                            cell = new Cell();
                            cell.CellReference = SloupecNaZnak(sloupec + 1) + (radek + 1).ToString();
                            cell.DataType = CellValues.String;
                            cell.StyleIndex = 1;
                            row.Append(cell);
                        }
                    }
                    sd.Append(row);
                }
            }

            ws.InsertAfter(mergeCells, ws.Elements<SheetData>().First());
            mergeCells.Append(mergeCell);
            ws.Save();
        }
        /// <summary>
        /// vytvoří styly, které lze aplikovat do listů
        /// </summary>
        /// <returns>Vrátí styly</returns>
        private static Stylesheet GenerateStylesheet()
        {
            Stylesheet styleSheet = null;

            Fonts fonts = new Fonts(
                new Font( // Index 0 - výchozí
                    new FontSize() { Val = 10 }

                ),
                new Font( // Index 1 - tučné
                    new FontSize() { Val = 10 },
                    new Bold()));

            Fills fills = new Fills(
                new Fill(new PatternFill() { PatternType = PatternValues.None }), // Index 0 - výchozí
                new Fill(new PatternFill() { PatternType = PatternValues.Gray125 }), // Index 1 - výchozí
                new Fill(new PatternFill(new ForegroundColor
                {
                    Rgb = new HexBinaryValue()
                    {
                        Value = ZpracovaniPrehledu.Barva.R.ToString("X2") + ZpracovaniPrehledu.Barva.G.ToString("X2") + ZpracovaniPrehledu.Barva.B.ToString("X2")
                    }
                })
                { PatternType = PatternValues.Solid })); // Index 2 - výplň

            Borders borders = new Borders(
                    new Border(), // Index 0 - výchozí
                    new Border( // Index 1 - černé tenké ohraničení
                        new LeftBorder(new Color() { Auto = true }) { Style = BorderStyleValues.Thin },
                        new RightBorder(new Color() { Auto = true }) { Style = BorderStyleValues.Thin },
                        new TopBorder(new Color() { Auto = true }) { Style = BorderStyleValues.Thin },
                        new BottomBorder(new Color() { Auto = true }) { Style = BorderStyleValues.Thin },
                        new DiagonalBorder()),
                    new Border( // Index 2 - černé tlusté ohraničení
                        new LeftBorder(new Color() { Auto = true }) { Style = BorderStyleValues.Thick },
                        new RightBorder(new Color() { Auto = true }) { Style = BorderStyleValues.Thick },
                        new TopBorder(new Color() { Auto = true }) { Style = BorderStyleValues.Thick },
                        new BottomBorder(new Color() { Auto = true }) { Style = BorderStyleValues.Thick },
                        new DiagonalBorder()));

            CellFormats cellFormats = new CellFormats(
                new CellFormat(), // Index 0 - výchozí
                new CellFormat
                {
                    FontId = 0,
                    FillId = 0,
                    BorderId = 1,
                    ApplyBorder = true
                }, // Index 1 - normální písmo a tenké ohraničení
                new CellFormat
                {
                    FontId = 1,
                    FillId = 2,
                    BorderId = 1,
                    ApplyFill = true,
                }, // Index 2 - tučné písmo, tenké ohraničení a výplň
                new CellFormat
                {
                    FontId = 0,
                    FillId = 0,
                    BorderId = 2,
                    ApplyFill = true
                }, // Index 3 - normální písmo a tlusté ohraničení
                new CellFormat
                {
                    FontId = 1,
                    FillId = 2,
                    BorderId = 1,
                    ApplyFill = true,
                    Alignment = new Alignment { TextRotation = (UInt32Value)90U }
                }, // Index 4 - tučné písmo, tenké ohraničení, orientace textu 90 stupňů a výplň
                new CellFormat
                {
                    FontId = 0,
                    FillId = 0,
                    BorderId = 1,
                    ApplyBorder = true,
                    Alignment = new Alignment { TextRotation = (UInt32Value)90U }
                } // Index 5 - normální písmo, orientace textu 90 stupňů a tenké ohraničení
                );

            styleSheet = new Stylesheet(fonts, fills, borders, cellFormats);

            return styleSheet;
        }
        /// <summary>
        /// Převede sloupec na znak
        /// </summary>
        /// <param name="sloupec">číslo sloupce</param>
        /// <returns>Vrátí znak sloupce (např. pro 1 vrátí A)</returns>
        private static string SloupecNaZnak(int sloupec)
        {
            int div = sloupec;
            string znakSloupce = String.Empty;
            int mod = 0;

            while (div > 0)
            {
                mod = (div - 1) % 26;
                znakSloupce = (char)(65 + mod) + znakSloupce;
                div = (int)((div - mod) / 26);
            }
            return znakSloupce;
        }
        /// <summary>
        /// Nalezne nejdelší řetězec v poli na základě vybraného pořadí v kolekci
        /// </summary>
        /// <param name="kolekce">vstupní kolekce</param>
        /// <param name="volbaPoradi">volba pořadí položky v kolekci</param>
        /// <returns>vrací písmeno odpovídající číslu sloupce</returns>
        private static int NejdelsiRetezec(List<(string, string)> kolekce, int volbaPoradi)
        {
            List<string> pole = new List<string>();
            for (int i = 0; i < kolekce.Count; i++)
            {
                if (volbaPoradi == 1)
                    pole.Add(kolekce[i].Item1);
                else if (volbaPoradi == 2)
                    pole.Add(kolekce[i].Item2); 
            }
            return pole.Max(polozka => polozka.Length);
        }

        private static List<string> ListNazvu(int volba, List<(string, string)> skupinyTymy, string skupina)
        {
            List<string> list = new List<string>();
            for (int i = 0; i < skupinyTymy.Count; i++)
            {
                //všechny týmy
                if (volba == 1)
                {
                    list.Add(skupinyTymy[i].Item1);
                }
                //týmy vybrané skupiny
                else if (volba == 2)
                {
                    if (skupinyTymy[i].Item2.Contains(skupina))
                    {
                        list.Add(skupinyTymy[i].Item1);
                    }
                }
                //všechny skupiny
                else if (volba == 3)
                {
                    list.Add(skupinyTymy[i].Item2);
                    list = list.Distinct().ToList();
                }
            }
            
            return list;
        }

        private static List<(string, string)> ListZapasuSkupin(List<(string, string)> skupinyZapasy, string skupina)
        {
            List<(string, string)> listZapasuSkupin = new List<(string, string)>();

            //zápasy vybrané skupiny
            for (int i = 0; i < skupinyZapasy.Count; i++)
            {
                if (skupinyZapasy[i].Item2.Contains(skupina))
                {
                    listZapasuSkupin.Add((skupinyZapasy[i].Item1, skupinyZapasy[i].Item2));
                }
            }

            return listZapasuSkupin;
        }
    }
}
