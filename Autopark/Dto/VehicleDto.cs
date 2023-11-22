namespace Autopark.Dto
{
    public class VehicleDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public double Price { get; set; }
        public double ZeroToHundred { get; set; }
        public int Mileage { get; set; }
        public int Year { get; set; }
        public int HorsePower { get; set; }
        public int BrandId { get; set; }
    }
}
