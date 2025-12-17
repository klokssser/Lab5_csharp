namespace lab5_C_
{
    public class Style
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Style(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public override string ToString()
        {
            return $"ID: {Id} | Стиль: {Name}";
        }
    }
}
