using Microsoft.EntityFrameworkCore;
using ProductService.Application.Dto.DiscountDtos;
using ProductService.Application.Interfaces;
using ProductService.Domain.Entities;
using ProductService.Infrastructure.Data;

namespace ProductService.Application.Services;

public class DiscountService(ApplicationContext context) : IDiscountService
{
    public async Task<Discount> CreateDiscount(CreateDiscountDto dto)
    {
        var discount = new Discount(dto.Products, dto.Percent);
        await context.Discounts.AddAsync(discount);
        await context.SaveChangesAsync();
        return discount;
    }

    public async Task<bool> DeleteDiscount(Guid discountId)
    {
        var discount = await context.Discounts.FindAsync(discountId);
        if (discount is null)
        {
            return false;
        }

        context.Discounts.Remove(discount);
        await context.SaveChangesAsync();
        return true;
        
    }
    

    public async Task<Discount?> UpdateDiscount(UpdateDiscountDto dto, Guid discountId)
    {
        var discount = await context.Discounts.Include(d => d.Products)
            .FirstOrDefaultAsync(p => p.Id == discountId);
        if (discount is null)
        {
            return null;
        }
        discount.Products = dto.Products ?? discount.Products;
        discount.Percentage = dto.Percentage ?? discount.Percentage;
        await context.SaveChangesAsync();
        return discount;
    }

    public async Task<Discount?> GetDiscount(Guid discountId)
    {
        var discount = await context.Discounts.FindAsync(discountId);
        return discount;
    }
}