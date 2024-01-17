using Autopark.Models;

namespace Autopark.Areas.Manager.Dto
{
    public class DriverDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Salary { get; set; }
        public int EnterpriseId { get; set; }
        public int VehicleId { get; set; }
        public IEnumerable<int> AssignedCarsId { get; set; } = new List<int>();
    }
}
