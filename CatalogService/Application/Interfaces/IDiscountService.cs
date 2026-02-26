using ProductService.Application.Dto.DiscountDtos;
using ProductService.Domain.Entities;

namespace ProductService.Application.Interfaces;

public interface IDiscountService
{
    public Task<Discount> CreateDiscount(CreateDiscountDto dto);
    public Task<bool> DeleteDiscount(Guid discountId);

    public Task<Discount?> UpdateDiscount(UpdateDiscountDto dto, Guid discountId);
    public Task<Discount?> GetDiscount(Guid discountId);

}