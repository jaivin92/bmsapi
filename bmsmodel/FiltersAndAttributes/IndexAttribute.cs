namespace bmsmodel.FiltersAndAttributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class IndexAttribute : Attribute
    {
        public int Position { get; }

        public IndexAttribute(int position)
        {
            Position = position;
        }
    }
}
