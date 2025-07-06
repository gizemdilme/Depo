using Depo.Models;

public class Fabric

{
    public int FabricId { get; set; }
    public string? Name { get; set; }
    public int Tone { get; set; }
    public int QualityId { get; set; }
    public Quality? Quality { get; set; }
    public int ColorId { get; set; }
    public Color? Color { get; set; }
    public int LocationId { get; set; }
    public Location? Location { get; set; }
}