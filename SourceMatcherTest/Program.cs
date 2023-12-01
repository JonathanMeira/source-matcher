using SourceMatcher;

var sourcerBuilder = MutilpleSourceMatcher<SampleSource01, SampleSource02>.Create();

sourcerBuilder.AddSourceOrigin(
    Source<SampleSource01>
    .Create()
    .FromOrigin(new()
    {
        new()
        {
            Name = "Sample"
        }
    })
    .MapUniqueIdentifier(sampleSource01 => sampleSource01.Name)
    );

sourcerBuilder.AddSourceOrigin(
    Source<SampleSource02>
    .Create()
    .FromOrigin(new()
    {
        new()
        {
            FullName = "Sample Name"
        }
    })
    .MapUniqueIdentifier(sampleSource02 => sampleSource02.GetName())
    );


var dasdasda = sourcerBuilder.JoinSources();


foreach (var item in dasdasda.GetValues())
{
    Console.WriteLine(
        $"The Source 01, has the value {item.FirstSource.Name} and Matches with the second source who has the value of {item.SecondSource?.FullName}"
        );
}


public class SampleSource01 
{
    public string Name { get; init; } = string.Empty;
}

public class SampleSource02 
{
    public string? FullName { get; init; } = string.Empty;

    public string GetName()
    {
        return FullName!.Split(" ")[0];
    }
}
