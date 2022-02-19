namespace Project.Domain.Entities;

public abstract class Basis
{ 
    public int Id { get; set; }
    public Guid Guid { get; set; }
    public string Name { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool Deleted { get; set; }
}