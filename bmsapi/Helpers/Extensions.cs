using bmsmodel.Common;

namespace bmsapi.Helpers
{
    public static class Extensions
    {
        #region Datatable
        public static DataTablesResponse Datatable<T>(this List<T> listOfSelected)
        {
            var _model = listOfSelected.FirstOrDefault();
            if (_model == null)
            {
                return new DataTablesResponse() { Data = listOfSelected, TotalRecord = 0 };
            }
            bool isExistTotalRecord = _model.GetType().GetProperties().Any(x => x.Name == "TotalRecord");
            var TotalRecord = _model.GetType().GetProperty("TotalRecord");
            var countTotalRecord = listOfSelected.Select(x => TotalRecord.GetValue(x, null)).FirstOrDefault();

            return new DataTablesResponse() { Data = listOfSelected, TotalRecord = Convert.ToInt32(countTotalRecord) };
        }
        #endregion
    }
}
