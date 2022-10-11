using BoostHolding.Entities.Data;

namespace BoostHolding.Web.Models
{
    public class DemandViewModel
    {
        public int Id { get; set; }
        public DateTime DateOfStart { get; set; }
        public DateTime DateOfEnd { get; set; }
        public DateTime DateOfDemand { get; set; }
        public Status Status { get; set; }
        public List<DemandViewModel> Demands { get; set; } = new();
    }
}
