namespace MiniPuanApi.Models;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public int PointBalance { get; set; } = 0;
}

public class PointTransaction
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int Amount { get; set; }
    public string Type { get; set; } = "";
    public string Description { get; set; } = "";
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}