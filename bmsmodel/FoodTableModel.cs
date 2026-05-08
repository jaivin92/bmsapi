using System.ComponentModel.DataAnnotations;
using bmslib.Enmus;

namespace bmsmodel.Common
{
    public class FoodTableModel : BaseModel
    {
        public FoodTableType TableStatus { get; set; } = FoodTableType.Available;

        public DateTime BookTime { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
