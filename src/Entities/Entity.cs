using SFML.System;

namespace sfml_csharp.Entities;

public class Entity
{
    public Vector2f Velocity { get; set; } = new(0.0f, 0.0f);
}