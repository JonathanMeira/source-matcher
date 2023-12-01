using System.Collections.Immutable;
namespace SourceMatcher
{
    public class SingleCompiledSource<T1, T2> 
        where T1 : class
        where T2 : class
    {
        Dictionary<string, (T1 FirstSource, T2? SecondSource)> _compiledSource = new();

        public SingleCompiledSource(
            MaterializedSource<T1> firstMaterializedSource,
            MaterializedSource<T2> secondMaterializedSource
            )
        {
            _compiledSource = new();

            foreach (var item in firstMaterializedSource.ReadValues())
            {
                secondMaterializedSource.TryGetValue(item.Key, out T2? matchedItem);

                _compiledSource.TryAdd(item.Key,
                    (
                        FirstSource: item.Value,
                        SecondSource: matchedItem
                    ));
            }
        }

        public bool TryGetValue(string @in, out (T1 FirstSource, T2? SecondSource) compiledItem)
        {
            return _compiledSource.TryGetValue(@in, out compiledItem);
        }

        public ImmutableArray<(T1 FirstSource, T2? SecondSource)> GetValues()
        {
            return _compiledSource.Values.ToImmutableArray();
        }
    }
}
