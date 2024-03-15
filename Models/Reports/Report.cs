using System.ComponentModel.DataAnnotations;

namespace Autopark.Models.Reports
{
    public abstract class Report
    {
        public required string Name { get; set; }
        public required Interval Interval { get; set; }
        public required DateTime Start { get; set; }
        public required DateTime End { get; set; }
    }
}
