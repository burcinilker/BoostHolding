

using System.ComponentModel.DataAnnotations;

namespace BoostHolding.Entities.Identity
{
    public class Role
    {
        public int Id { get; set; }
        [Required,MaxLength(100)]
        public string RoleName { get; set; }
        public List<User> Users { get; set; }
    }
}
