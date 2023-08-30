namespace CinemaMVC.Models
{
    public class Film
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] Pictures { get; set; }
        public string AgeRating { get; set; }
        public string Price { get; set; }
        public int Year { get; set; }
        public string Director { get; set; }
        public string Language { get; set; }
        public string Genre { get; set; }
        public string Duration { get; set; }
        public string ProductionCountry { get; set; }
        public string Studio { get; set; }
        public string MainActors { get; set; }
        public string Description { get; set; }
    }
}
