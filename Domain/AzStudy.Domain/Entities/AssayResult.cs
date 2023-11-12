namespace AzStudy.Domain.Entities;

public class AssayResult : Entity
{
    public double AssayScore { get; set; }
    public double StudentScore { get; set; }
    public bool Passed => StudentScore >= AssayScore * 0.6;
    
}