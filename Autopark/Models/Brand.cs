using System.ComponentModel.DataAnnotations;

namespace Autopark.Models
{
    public class Brand
    {
        public enum Types
        {
            MICRO,
            SEDAN,
            SPORT_CAR,
            SUV,
            CROSSOVER,
            VAN,
            BUS,
            LIMOUSINE,
            TRUCK
        }

        public enum Segments
        {
            DAILY,
            BUSINESS,
            LEASING
        }

        public enum Categories
        {
            A,
            B,
            C,
            D,
            E,
            M
        }

        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        public Types Type { get; set; }
        public int SeatsAmount { get; set; }
        public Segments Segment { get; set; }
        public Categories RequiredDriverCategory { get; set; }
    }
}
