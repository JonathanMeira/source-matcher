namespace SourceMatcher
{
    public class Source<T> where T : class
    {
        private static List<T> _source;
        private static Func<T, string> _keyValueSelector;

        public static Source<T> Create()
        {
            return new Source<T>();
        }

        public Source<T> FromOrigin(List<T> origin)
        {
            if (origin is null)
            {
                throw new ArgumentNullException(nameof(origin));
            }
            
            _source = origin;
            return this;
        }

        public Source<T> MapUniqueIdentifier(Func<T, string> keyValueSelector)
        {
            if (keyValueSelector is null)
            {
                throw new ArgumentNullException(nameof(keyValueSelector));
            }

            _keyValueSelector = keyValueSelector;
            return this;
        }

        public MaterializedSource<T> Materialize()
        {
            return new MaterializedSource<T>(_source, _keyValueSelector);
        }
    }
}