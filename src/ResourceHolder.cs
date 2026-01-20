namespace sfml_csharp;

public abstract class ResourceHolder<TResource, TIdentifier>
    where TIdentifier : notnull
{
    private readonly Dictionary<TIdentifier, TResource> _resources = new();

    protected void Load<TParam>(
        TIdentifier id,
        Func<string, TParam?, TResource> factory,
        string filename,
        TParam? param
    )
    {
        if (_resources.ContainsKey(id))
            throw new ArgumentException($"Resource {id} already exists.");

        var resource = factory(filename, param);
        _resources.Add(id, resource);
        Console.WriteLine($"{typeof(TResource).Name}: Resource {id} created.");
    }


    public TResource Get(TIdentifier id)
    {
        return _resources.TryGetValue(id, out var resource)
            ? resource
            : throw new KeyNotFoundException($"No resource with id {id}");
    }
}