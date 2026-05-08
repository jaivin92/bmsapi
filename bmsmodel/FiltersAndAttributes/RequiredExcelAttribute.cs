namespace bmsmodel.FiltersAndAttributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class RequiredExcelAttribute : Attribute
    {
        public string Message { get; }

        public RequiredExcelAttribute(string message = null)
        {
            Message = message;
        }
    }
}
