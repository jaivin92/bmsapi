using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using bmsmodel.FiltersAndAttributes;

namespace bmsmodel.Common
{
    public class BaseModel
    {
        List<string> list = new List<string>();

        public long Id { get; set; }

        public bool IsActive { get; set; }

        [NotMapped, IgnoreInSwagger, JsonIgnore]
        public List<string> ErrorMessage { get; set; }

        [NotMapped]
        public int TotalRecord { get; set; }

        [NotMapped]
        public List<long> fIds { get; set; } = new List<long>();

        [NotMapped]
        public string FreeTextSearch { get; set; } = string.Empty;

        [NotMapped, IgnoreInSwagger, JsonIgnore]
        public DataTableRequestModel DataTableRequestModel { get; set; } = new DataTableRequestModel();

        [NotMapped, JsonIgnore]
        public string[] IgnoreOnUpdate
        {
            get
            {
                return list.ToArray();
            }
        }

        public void AddIgnore(string name)
        {
            list.Add(name);
        }

        public void RemoveIgnore(string name)
        {
            list.RemoveAll(t => t == name);
        }
    }
}
