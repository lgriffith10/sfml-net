using SFML.Graphics;

namespace sfml_csharp;

public sealed class TextureHolder : ResourceHolder<Texture, TextureIdEnum>
{
    public void Load(TextureIdEnum id, string filename)
    {
        base.Load<string>(
            id,
            (file, _) => new Texture(file),
            filename,
            null
        );
    }
}