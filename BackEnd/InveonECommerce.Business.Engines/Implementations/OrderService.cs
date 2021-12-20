using AutoMapper;
using InveonECommerce.Business.Engines.Constants.Messages;
using InveonECommerce.Business.Engines.DTOs.Order;
using InveonECommerce.Business.Engines.Interfaces;
using InveonECommerce.Data.DAL.Repository;
using InveonECommerce.Data.DAL.UnitOfWork;
using InveonECommerce.Data.Entities;
using Microsoft.AspNetCore.Identity;
using SharedLibrary.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InveonECommerce.Business.Engines.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly IUnitOfWork _uof;
        private readonly IRepository<OrderEntity> _orderRepo;
        private readonly IRepository<AddressInfoEntity> _addressRepo;
        private readonly IRepository<BasketEntity> _basketRepo;
        private readonly IRepository<ProductEntity> _productRepo;
        private readonly IMapper _mapper;

        public OrderService(IUnitOfWork uof,IMapper mapper, UserManager<UserEntity> userManager)
        {
            _userManager = userManager;
            _uof = uof;
            _orderRepo = uof.GetRepository<OrderEntity>();
            _addressRepo = uof.GetRepository<AddressInfoEntity>();
            _basketRepo = uof.GetRepository<BasketEntity>();
            _productRepo = uof.GetRepository<ProductEntity>();
            _mapper = mapper;
        }

        public async Task<Response<AddOrderDTO>> AddOrderAsync(AddOrderDTO orderDto,string username)
        {
            //Add address to memory with EF Tracker
            AddressInfoEntity address = _mapper.Map<AddressInfoEntity>(orderDto.AddressInfo);
            await _addressRepo.AddAsync(address);

            //Add basket to memory with EF Tracker 
            BasketEntity basket = new BasketEntity();
            await _basketRepo.AddAsync(basket);

            //Creating Order
            OrderEntity orderEntity = new OrderEntity();
            orderEntity.AddressInfo = address;

            //Adding basketItemEntity for each product
            decimal totalPrice = 0;
            foreach (int itemId in orderDto.ItemsIds)
            {
                ProductEntity product = await _productRepo.GetByIdAsync(itemId);
                basket.BasketItems.Add(new BasketItemEntity
                {
                    Product = product,
                    ProductQty = 1,
                    Basket = basket,
                    ProductPrice = product.Price,
                    SubTotal = product.Price,
                });
                totalPrice += product.Price;
            }
            

            orderEntity.OrderTotal = totalPrice;
            orderEntity.Basket = basket;
            orderEntity.OrderNo = Guid.NewGuid().ToString();
            orderEntity.OrderedDate = DateTime.Now;

            //Getting user and add relation to order
            var user = await _userManager.FindByNameAsync(username);
            orderEntity.User = user;
            orderEntity.UserId = user.Id;
            
            
            await _orderRepo.AddAsync(orderEntity);
            var result = await _uof.CommitAsync();

            if (result > 0)
            {
                return Response<AddOrderDTO>.Success(200);
            }
            else
            {
                return Response<AddOrderDTO>.Fail(OrderMessages.FailAdded, 400, false);
            }


        }

        

        public async Task<Response<List<ListOrderDTO>>> GetAllAsync()
        {
            List<OrderEntity> orderList = await _orderRepo.GetAllAsync();
            List<ListOrderDTO> listorderDto = _mapper.Map<List<ListOrderDTO>>(orderList);


            return Response<List<ListOrderDTO>>.Success(listorderDto, 200);
        }

        
        public async Task<Response<OrderDetailDTO>> GetByIdAsync(int id)
        {

            OrderEntity existOrder = await _orderRepo.GetByIdAsync(id);

            if (existOrder != null)
            {
                OrderDetailDTO orderDto = _mapper.Map<OrderDetailDTO>(existOrder);
                return Response<OrderDetailDTO>.Success(orderDto, 200);
            }
            else
            {
                return Response<OrderDetailDTO>.Fail(OrderMessages.NotFound, 400, false);
            }
        }

        public async Task<Response<OrderDetailDTO>> GetOrderDetails(int id)
        {
            
            OrderEntity orderEntity = await _orderRepo.GetByIdAsync(id);

            if(orderEntity != null)
            {
                OrderDetailDTO orderDetailDto = _mapper.Map<OrderDetailDTO>(orderEntity);

                // Estimated delivery date is calculated by adding 3 days to "OrderedData"
                orderDetailDto.EstimatedDeliveryDate = orderDetailDto.OrderedDate.AddDays(3);

                return Response<OrderDetailDTO>.Success(orderDetailDto,200);
            }
            else
            {
                return Response<OrderDetailDTO>.Fail(OrderMessages.NotFound, 400, true);
            }
        }
    }
}
