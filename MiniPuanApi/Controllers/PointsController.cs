using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniPuanApi.Data;
using MiniPuanApi.Models;

namespace MiniPuanApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PointsController : ControllerBase
{
    private readonly AppDbContext _db;
    public PointsController(AppDbContext db) { _db = db; }

    [HttpPost("users")]
    public async Task<IActionResult> CreateUser([FromBody] UserDto dto)
    {
        var user = new User { Name = dto.Name };
        _db.Users.Add(user);
        await _db.SaveChangesAsync();
        return Ok(user);
    }

    [HttpPost("earn")]
    public async Task<IActionResult> Earn([FromBody] EarnDto dto)
    {
        var user = await _db.Users.FindAsync(dto.UserId);
        if (user == null) return NotFound("Kullanıcı yok");
        user.PointBalance += dto.Amount;
        _db.PointTransactions.Add(new PointTransaction { UserId = dto.UserId, Amount = dto.Amount, Type = "Earn", Description = dto.Description });
        await _db.SaveChangesAsync();
        return Ok(user);
    }

    [HttpPost("spend")]
    public async Task<IActionResult> Spend([FromBody] EarnDto dto)
    {
        var user = await _db.Users.FindAsync(dto.UserId);
        if (user == null) return NotFound("Kullanıcı yok");
        if (user.PointBalance < dto.Amount) return BadRequest($"Yetersiz bakiye! Mevcut: {user.PointBalance}");
        user.PointBalance -= dto.Amount;
        _db.PointTransactions.Add(new PointTransaction { UserId = dto.UserId, Amount = -dto.Amount, Type = "Spend", Description = dto.Description });
        await _db.SaveChangesAsync();
        return Ok(user);
    }

    [HttpGet("history/{userId}")]
    public async Task<IActionResult> History(int userId)
    {
        var list = await _db.PointTransactions.Where(x => x.UserId == userId).ToListAsync();
        return Ok(list);
    }
}
public record UserDto(string Name);
public record EarnDto(int UserId, int Amount, string Description);