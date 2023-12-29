using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Autopark.Models
{
    [PrimaryKey(nameof(UserId), nameof(ManagedEnterpriseId))]
    public class EnterpriseManager
    {
        public string UserId { get; set; } = null!;
        [ForeignKey("UserId")]
        public AppUser Manager { get; set; } = null!;
        public int ManagedEnterpriseId { get; set; }
        [ForeignKey("ManagedEnterpriseId")]
        public Enterprise Enterprise { get; set; } = null!;
    }
}
