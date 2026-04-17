namespace bmslib.Resource
{
    public static class MessageExtensions
    {
        public static string EditTitle(this string _str)
        {
            return string.Format(SystemMessages.Edit, _str);
        }

        public static string InsertedSuccessfully(this string _str)
        {
            return string.Format(SystemMessages.InsertedSuccessfully, _str);
        }

        public static string UpdatedSuccessfully(this string _str)
        {
            return string.Format(SystemMessages.UpdatedSuccessfully, _str);
        }

        public static string SavedSuccessfully(this string _str)
        {
            return string.Format(SystemMessages.SavedSuccessfully, _str);
        }

        public static string AlreadyExist(this string _str, List<string> duplidateValueField)
        {
            if (duplidateValueField.IsNotNull())
            {
                return _str.AlreadyExist(string.Join(", ", duplidateValueField.Select(t => t)));
            }
            return _str.AlreadyExist();
        }

        public static string AlreadyExist(this string _str, string duplidateValueField = null)
        {
            var _result = string.Format(SystemMessages.AlreadyExist, _str);
            if (!string.IsNullOrWhiteSpace(duplidateValueField))
            {
                _result += $" : {duplidateValueField.DuplicateValue()}";
            }
            return _result;
        }

        public static string DuplicateValue(this string _str)
        {
            return string.Format(SystemMessages.DuplicateValue, _str);
        }

        public static string PleaseSelect(this string _str)
        {
            return string.Format(SystemMessages.PleaseSelect, _str);
        }

        public static string Invalid(this string _str)
        {
            return string.Format(SystemMessages.Invalid, _str);
        }

        public static string DeleteSuccessfully(this string _str)
        {
            return string.Format(SystemMessages.DeleteSuccessfully, _str);
        }

        public static string IsNotFound(this string _str)
        {
            return string.Format(SystemMessages.IsNotFound, _str);
        }
    }
}
