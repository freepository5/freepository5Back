using AutoMapper;
using Freepository.DTO_s;
using Freepository.Models;
using Freepository.Repositories.Promotions;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Freepository.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class PromotionController : ControllerBase
    {
        private readonly IPromotionRepository _promotionRepository;
        private readonly IMapper _mapper;

        public PromotionController(IPromotionRepository promotionRepository, IMapper mapper)
        {
            _promotionRepository = promotionRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PromotionDTO>>> GetAllPromotions()
        {
            var promotions = await _promotionRepository.GetAllPromotions();
            var promotionsDto = _mapper.Map<IEnumerable<PromotionDTO>>(promotions);
            return Ok(promotionsDto);
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<PromotionDTO>> GetPromotionsById(int id)
        {
            var promotion = await _promotionRepository.GetPromotionsById(id);
            if (promotion == null)
            {
                return NotFound();
            }

            var promotionDTO = _mapper.Map<PromotionDTO>(promotion);
            return Ok(promotionDTO);
        }
        
        [HttpPost]
        public async Task<ActionResult<PromotionDTO>> CreatePromotion(PromotionDTO promotionDto)
        {
            var promotion = _mapper.Map<Promotion>(promotionDto);
            await _promotionRepository.AddPromotion(promotion);

            return CreatedAtAction(nameof(GetAllPromotions), new { id = promotion.Id }, promotionDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePromotion(int id)
        {
            await _promotionRepository.DeletePromotion(id);
            return NoContent();
        }
    }
}
