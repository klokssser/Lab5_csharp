namespace lab5_C_
{
    public class Painting
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ArtistId { get; set; }
        public int StyleId { get; set; }
        public int Year { get; set; }
        public int PartOfHermitage { get; set; }

        public Painting(int id, string name, int artistId, int styleId, int year, int partOfHermitage)
        {
            Id = id;
            Name = name;
            ArtistId = artistId;
            StyleId = styleId;
            Year = year;
            PartOfHermitage = partOfHermitage;
        }

        public override string ToString()
        {
            return $"ID: {Id} | Название: {Name} | Год: {Year} | Часть Эрмитажа: {PartOfHermitage}";
        }
    }
}