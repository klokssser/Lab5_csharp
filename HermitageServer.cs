namespace lab5_C_
{
    public class HermitageService
    {
        private List<Artist> _artists;
        private List<Style> _styles;
        private List<Painting> _paintings;
        private readonly FileProvider _fileProvider;

        public HermitageService()
        {
            _fileProvider = new FileProvider();
            _artists = new List<Artist>();
            _styles = new List<Style>();
            _paintings = new List<Painting>();
        }

        public void LoadDatabase()
        {
            var data = _fileProvider.LoadData();
            _artists = data.Item1;
            _styles = data.Item2;
            _paintings = data.Item3;
        }

        public void SaveDatabase()
        {
            _fileProvider.SaveData(_artists, _styles, _paintings);
        }

        public List<Painting> GetAllPaintings()
        {
            return _paintings;
        }

        public List<Artist> GetAllArtists()
        {
            return _artists;
        }

        public List<Style> GetAllStyles()
        {
            return _styles;
        }

        public void AddPainting(string name, int artistId, int styleId, int year, int part)
        {
            int newId = _paintings.Count > 0 ? _paintings.Max(p => p.Id) + 1 : 1;
            _paintings.Add(new Painting(newId, name, artistId, styleId, year, part));
        }

        public void AddArtist(string name)
        {
            int newId = _artists.Count > 0 ? _artists.Max(a => a.Id) + 1 : 1;
            _artists.Add(new Artist(newId, name));
        }

        public void AddStyle(string name)
        {
            int newId = _styles.Count > 0 ? _styles.Max(s => s.Id) + 1 : 1;
            _styles.Add(new Style(newId, name));
        }

        public bool RemovePainting(int id)
        {
            var item = _paintings.FirstOrDefault(p => p.Id == id);
            if (item != null)
            {
                _paintings.Remove(item);
                return true;
            }
            return false;
        }

        public bool ArtistExists(int id)
        {
            return _artists.Any(a => a.Id == id);
        }

        public bool StyleExists(int id)
        {
            return _styles.Any(s => s.Id == id);
        }

        public List<string> Query1_GetPaintingsInPart(int part)
        {
            return _paintings
                .Where(p => p.PartOfHermitage == part)
                .Select(p => p.ToString())
                .ToList();
        }

        public List<string> Query2_GetPaintingsByArtist(string artistName)
        {
            return _paintings
                .Join(_artists,
                      p => p.ArtistId,
                      a => a.Id,
                      (p, a) => new { Painting = p, Artist = a })
                .Where(x => x.Artist.Name.Contains(artistName, StringComparison.OrdinalIgnoreCase))
                .Select(x => x.Painting.ToString())
                .ToList();
        }

        public int Query3_CountArtistsWithPaintingsInPart(int minCount, int part)
        {
            return _artists
                .GroupJoin(_paintings,
                           a => a.Id,
                           p => p.ArtistId,
                           (a, paints) => new { Artist = a, Count = paints.Count(p => p.PartOfHermitage == part) })
                .Count(x => x.Count > minCount);
        }

        public string Query4_OldestPaintingInfo()
        {
            var data = _paintings
                .Join(_artists, p => p.ArtistId, a => a.Id, (p, a) => new { p, a })
                .Join(_styles, x => x.p.StyleId, s => s.Id, (x, s) => new { x.p, x.a, s })
                .OrderBy(x => x.p.Year)
                .FirstOrDefault();

            if (data == null) return "Нет данных.";

            return $"Самая старая картина: \"{data.p.Name}\" ({data.p.Year}), Художник: {data.a.Name}, Стиль: {data.s.Name}";
        }
    }
}
