using Depo.Models;

public class FabricExit
{
    public int ExitId { get; set; }
    public int FabricId { get; set; }
    public Fabric Fabric { get; set; }
    public DateTime Date { get; set; }
    public int Quantity { get; set; }
    public int LocationId { get; set; }
    public Location Location { get; set; }
}