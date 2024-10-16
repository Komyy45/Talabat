using AutoMapper;
using Linkdev.Talabat.Core.Application.Abstraction.Contracts.Basket;
using Linkdev.Talabat.Core.Application.Abstraction.Models.Basket;
using Linkdev.Talabat.Core.Application.Exceptions;
using Linkdev.Talabat.Core.Domain.Contracts.Infrastructure;
using Linkdev.Talabat.Core.Domain.Entities.Basket;
using Microsoft.Extensions.Configuration;

namespace Linkdev.Talabat.Core.Application.Services.Basket
{
    internal class BasketService(IBasketRepository basketRepository, IMapper mapper, IConfiguration configuration) : IBasketService
    {
        public async Task<CustomerBasketDto> GetCustomerBasket(string id)
        {
             var basket = await basketRepository.GetAsync(id);

            if (basket is null)
                throw new NotFoundException(nameof(CustomerBasket), id);

            return mapper.Map<CustomerBasketDto>(basket);
        }

        public async Task<CustomerBasketDto> UpdateBasketAsync(CustomerBasketDto basket)
        {
            var mappedBasket = mapper.Map<CustomerBasket>(basket);

            var updatedBasket = await basketRepository.UpdateAsync(mappedBasket,TimeSpan.FromDays( double.Parse(configuration.GetSection("RedisSettings")["timeToLiveInDays"])));

            if (updatedBasket is null)
                throw new BadRequestException("A problem has been Occured while updating your cart");

            return basket;
        }

        public async Task DeleteCustomerBasket(string id)
        {
            var deleted = await basketRepository.DeleteAsync(id);

            if (deleted) throw new BadRequestException("A problem has been Occured while deleting your cart");
        }

    }
}
