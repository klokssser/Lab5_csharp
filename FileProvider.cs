using ClosedXML.Excel;

namespace lab5_C_
{
    public class FileProvider
    {
        private const string FilePath = "LR5-var11.xlsx";

        public void SaveData(List<Artist> artists, List<Style> styles, List<Painting> paintings)
        {
            using var workbook = new XLWorkbook();

            var artistsSheet = workbook.Worksheets.Add("Artists");
            artistsSheet.Cell(1, 1).Value = "ID";
            artistsSheet.Cell(1, 2).Value = "Name";
            for (int i = 0; i < artists.Count; i++)
            {
                artistsSheet.Cell(i + 2, 1).Value = artists[i].Id;
                artistsSheet.Cell(i + 2, 2).Value = artists[i].Name;
            }

            var stylesSheet = workbook.Worksheets.Add("Styles");
            stylesSheet.Cell(1, 1).Value = "ID";
            stylesSheet.Cell(1, 2).Value = "Name";
            for (int i = 0; i < styles.Count; i++)
            {
                stylesSheet.Cell(i + 2, 1).Value = styles[i].Id;
                stylesSheet.Cell(i + 2, 2).Value = styles[i].Name;
            }

            var paintingsSheet = workbook.Worksheets.Add("Paintings");
            paintingsSheet.Cell(1, 1).Value = "ID";
            paintingsSheet.Cell(1, 2).Value = "Name";
            paintingsSheet.Cell(1, 3).Value = "ArtistId";
            paintingsSheet.Cell(1, 4).Value = "StyleId";
            paintingsSheet.Cell(1, 5).Value = "Year";
            paintingsSheet.Cell(1, 6).Value = "PartOfHermitage";
            for (int i = 0; i < paintings.Count; i++)
            {
                paintingsSheet.Cell(i + 2, 1).Value = paintings[i].Id;
                paintingsSheet.Cell(i + 2, 2).Value = paintings[i].Name;
                paintingsSheet.Cell(i + 2, 3).Value = paintings[i].ArtistId;
                paintingsSheet.Cell(i + 2, 4).Value = paintings[i].StyleId;
                paintingsSheet.Cell(i + 2, 5).Value = paintings[i].Year;
                paintingsSheet.Cell(i + 2, 6).Value = paintings[i].PartOfHermitage;
            }

            workbook.SaveAs(FilePath);
        }

        public (List<Artist>, List<Style>, List<Painting>) LoadData()
        {
            if (!File.Exists(FilePath))
            {
                return (new List<Artist>(), new List<Style>(), new List<Painting>());
            }

            using var workbook = new XLWorkbook(FilePath);
            var artists = LoadArtists(workbook);
            var styles = LoadStyles(workbook);
            var paintings = LoadPaintings(workbook);

            return (artists, styles, paintings);
        }

        private List<Artist> LoadArtists(XLWorkbook workbook)
        {
            var list = new List<Artist>();
            if (workbook.TryGetWorksheet("Artists", out var sheet))
            {
                var rows = sheet.RowsUsed().Skip(1);
                foreach (var row in rows)
                {
                    if (int.TryParse(row.Cell(1).GetValue<string>(), out int id))
                    {
                        list.Add(new Artist(id, row.Cell(2).GetValue<string>()));
                    }
                }
            }
            return list;
        }

        private List<Style> LoadStyles(XLWorkbook workbook)
        {
            var list = new List<Style>();
            if (workbook.TryGetWorksheet("Styles", out var sheet))
            {
                var rows = sheet.RowsUsed().Skip(1);
                foreach (var row in rows)
                {
                    if (int.TryParse(row.Cell(1).GetValue<string>(), out int id))
                    {
                        list.Add(new Style(id, row.Cell(2).GetValue<string>()));
                    }
                }
            }
            return list;
        }

        private List<Painting> LoadPaintings(XLWorkbook workbook)
        {
            var list = new List<Painting>();
            if (workbook.TryGetWorksheet("Paintings", out var sheet))
            {
                var rows = sheet.RowsUsed().Skip(1);
                foreach (var row in rows)
                {
                    if (int.TryParse(row.Cell(1).GetValue<string>(), out int id) &&
                        int.TryParse(row.Cell(3).GetValue<string>(), out int artistId) &&
                        int.TryParse(row.Cell(4).GetValue<string>(), out int styleId) &&
                        int.TryParse(row.Cell(5).GetValue<string>(), out int year) &&
                        int.TryParse(row.Cell(6).GetValue<string>(), out int part))
                    {
                        list.Add(new Painting(id, row.Cell(2).GetValue<string>(), artistId, styleId, year, part));
                    }
                }
            }
            return list;
        }
    }
}
