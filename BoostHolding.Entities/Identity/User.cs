

using System.ComponentModel.DataAnnotations;

namespace BoostHolding.Entities.Identity
{
    public class User
    {
        public int Id { get; set; }
        [Required,MaxLength(200)]
        public string UserName { get; set; }
        [Required, MaxLength(300)]
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public List<Role> Roles { get; set; }
        public DateTime SignedInTime { get; set; } 
    }
}
