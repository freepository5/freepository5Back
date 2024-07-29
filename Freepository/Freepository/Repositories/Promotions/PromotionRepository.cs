using Freepository.Data;
using Freepository.Models;
using Microsoft.EntityFrameworkCore;

namespace Freepository.Repositories.Promotions;

public class PromotionRepository : IPromotionRepository
{
    private readonly ApplicationDbContext _context;

    public PromotionRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Promotion>> GetAllPromotions()
    {
        return await _context.Promotions.ToListAsync();
    }

    public async Task<Promotion> GetPromotionsById(int id)
    {
        return await _context.Promotions.FindAsync(id);
    }

    public async Task AddPromotion(Promotion promotion)
    {
        _context.Promotions.Add(promotion);
        await _context.SaveChangesAsync();
    }

    public async Task DeletePromotion(int id)
    {
        var promotion = await _context.Promotions.FindAsync(id);
        if (promotion != null)
        {
            _context.Promotions.Remove(promotion);
            await _context.SaveChangesAsync();
        }
    }

}