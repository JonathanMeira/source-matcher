using System.Collections.Immutable;

namespace SourceMatcher
{
    public class MaterializedSource<T> where T : class
    {
        private readonly Dictionary<string, T> _sourceMaterialized;


        public MaterializedSource(
            List<T> source, 
            Func<T, string> keyValueSelector
            )
        {
            _sourceMaterialized = new();

            foreach (var sourceItem in source)
            {
                _ = _sourceMaterialized.TryAdd(keyValueSelector(sourceItem), sourceItem);
            }
        }

        public ImmutableDictionary<string, T> ReadValues()
        {
            return _sourceMaterialized.ToImmutableDictionary();
        }

        public bool TryGetValue(string @in, out T? sourceItem)
        {
            return _sourceMaterialized.TryGetValue(@in, out sourceItem);
        }
    }
}
