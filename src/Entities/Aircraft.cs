namespace sfml_csharp.Entities;

public class Aircraft(AircraftTypeEnum type) : Entity
{
    public AircraftTypeEnum Type { get; set; } = type;
}