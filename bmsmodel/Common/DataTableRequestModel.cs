using System.ComponentModel.DataAnnotations.Schema;

namespace bmsmodel.Common
{
    public class DataTableRequestModel
    {
        [NotMapped]
        public int PageSize { get; set; }

        [NotMapped]
        public int Limit { get; set; }

        [NotMapped]
        public int Offset { get; set; }

        [NotMapped]
        public string OrderDir { get; set; } = string.Empty;

        private string _orderBy { get; set; }

        [NotMapped]
        public string? OrderBy
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(_orderBy) && _orderBy.Contains(','))
                {
                    string _result = string.Join(" " + OrderDir + ", ", _orderBy.Split(','));
                    return _result;
                }
                return _orderBy;
            }
            set { _orderBy = value ?? string.Empty; }
        }

        [NotMapped]
        public string Filter { get; set; } = string.Empty;

        [NotMapped]
        public Dictionary<string, object> FilterObj { get; set; } = [];

        [NotMapped]
        public bool IsShowNoData { get; set; } = true;

        public bool ResponseForDataTable { get; set; } = true;

        [NotMapped]
        public bool GetSetCache { get; set; }
    }
}
