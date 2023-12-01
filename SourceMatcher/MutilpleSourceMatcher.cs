namespace SourceMatcher
{
    public class MutilpleSourceMatcher<T1, T2>
        where T1 : class
        where T2: class
    {
        private Source<T1> _firstSource;
        private Source<T2> _secondSource;


        public static MutilpleSourceMatcher<T1, T2> Create()
        {
            if(typeof(T1) == typeof(T2))
            {
                throw new ArgumentException("Types must be different");
            }

            return new MutilpleSourceMatcher<T1, T2>();
        }

        public void AddSourceOrigin(Source<T1> origin)
        {
            if (origin is null)
            {
                throw new ArgumentNullException(nameof(origin));
            }
            
            _firstSource = origin;
        }

        public void AddSourceOrigin(Source<T2> origin)
        {
            if (origin is null)
            {
                throw new ArgumentNullException(nameof(origin));
            }
            _secondSource = origin;
        }

        public SingleCompiledSource<T1, T2> JoinSources()
        {
            var firstSource = _firstSource.Materialize();
            var secondSource = _secondSource.Materialize();

            return new SingleCompiledSource<T1, T2>(firstSource, secondSource);
        }


        //TODO: Do better
        public void Compare()
        {
            var firstSource = _firstSource.Materialize();
            var secondSource = _secondSource.Materialize();

            foreach(var firstSourceValue in firstSource.ReadValues())
            {
                if (secondSource.TryGetValue(firstSourceValue.Key, out T2? matchedItem))
                {
                    Console.WriteLine(firstSourceValue.Key);
                    Console.WriteLine(firstSourceValue.Value);
                    Console.WriteLine(matchedItem?.ToString());
                }
            }
        }

    }

}
