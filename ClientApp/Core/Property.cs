public class Property
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public double Price { get; set; }
    public string Description { get; set; } = string.Empty;
    // Agent who listed the property (matches Agents.Id in DB)
    public int AgentId { get; set; }
    // Optional agent username (resolved when loading properties)
    public string AgentUsername { get; set; } = string.Empty;
}
