namespace bmsmodel.Common
{
    public static class DataTableExtensions
    {
        public static string GetPagination(this DataTableRequestModel dtRequest, string defaultSorting = "", bool appendFourceDefaultSorting = false, string alias = "", bool withPagination = true, bool defaultSortingRequired = false)
        {
            if (dtRequest == null)
            {
                return string.Empty;
            }

            string strFilter = string.Empty;

            if (!string.IsNullOrEmpty(dtRequest.OrderBy))
            {
                strFilter += " Order by " + dtRequest.OrderBy + " " + (!string.IsNullOrEmpty(dtRequest.OrderDir) ? dtRequest.OrderDir : " ASC ") + (defaultSortingRequired ? ", " + defaultSorting : "");
            }
            else
            {
                strFilter += " Order by " + defaultSorting;
            }

            if (dtRequest.PageSize > 0 && dtRequest.Limit > 0)
                strFilter += " LIMIT " + dtRequest.Limit + " OFFSET  " + (dtRequest.Offset * dtRequest.PageSize);
            //strFilter += " OFFSET " + (dtRequest.Offset * dtRequest.PageSize) + " ROWS FETCH NEXT " + dtRequest.Limit + " ROWS ONLY";
            return strFilter;
        }
    }
}
