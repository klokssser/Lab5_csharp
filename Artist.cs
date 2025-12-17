namespace lab5_C_
{
    public class Artist
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Artist(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public override string ToString()
        {
            return $"ID: {Id} | Художник: {Name}";
        }
    }
}
