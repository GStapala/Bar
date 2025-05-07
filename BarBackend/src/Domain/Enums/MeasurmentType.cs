namespace BarBackend.Domain.Enums;

public record MeasurementType(int Id, string Name)
{
    public static MeasurementType Item = new(1, "Item");
    public static MeasurementType Gram = new(2, "Gram");
    public static MeasurementType Milliliter = new(3, "Milliliter");
    public static MeasurementType Teaspoon = new(4, "Teaspoon");
    public static MeasurementType Twist = new(5, "Twist");
    public static MeasurementType Slice = new(6, "Slice");
    public static MeasurementType Piece = new(7, "Piece");
    public static MeasurementType Drop = new(8, "Drop");
    public static MeasurementType Peel = new(9, "Peel");
    public static MeasurementType Cube = new(10, "Cube");
    public static MeasurementType Pinches = new(11, "Pinches");
    public static MeasurementType Sprigs = new(12, "Sprigs");
    public static MeasurementType Cup = new(13, "Cup");
    public static MeasurementType Rinse = new(14, "Rinse");
    
    public override string ToString() => Name;
}
