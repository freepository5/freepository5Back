using Freepository.Models;

namespace Freepository.Repositories.Promotions;

public interface IPromotionRepository
{
    Task<IEnumerable<Promotion>> GetAllPromotions();
    Task<Promotion> GetPromotionsById(int id);
    Task AddPromotion(Promotion promotion);
    Task DeletePromotion(int id); 
}