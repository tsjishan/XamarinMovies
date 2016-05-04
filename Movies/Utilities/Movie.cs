using SQLite;

namespace Movies.Utilities
{
    public class Movie
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string movieID { get; set; }
        public string movieImageLink { get; set; }
    }
}