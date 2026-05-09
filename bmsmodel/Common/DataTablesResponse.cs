using System.Collections;

namespace bmsmodel.Common
{
    public class DataTablesResponse
    {
        public IEnumerable Data { get; set; }
        public int TotalRecord { get; set; }

        public bool DynamicGrid { get; set; }
    }
}
