using AutoMapper;
using RestaurantAPI10.DTOs.CustomerDTO;
using RestaurantAPI10.DTOs.DishDTO;
using RestaurantAPI10.DTOs.OrderDTO;
using RestaurantAPI10.Models;

namespace RestaurantAPI10.Profiles
{
    /// <summary>
    /// Профиль маппинга AutoMapper
    /// </summary>
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Order, OrderReadDto>()
                .ForMember(dest => dest.CustomerName,
                    opt => opt.MapFrom(src => src.Customer != null ? src.Customer.Name : string.Empty))
                .ForMember(dest => dest.OrderItems,
                    opt => opt.MapFrom(src => src.OrderItems));

            CreateMap<OrderCreateDto, Order>()
                .ForMember(dest => dest.OrderDate,
                    opt => opt.MapFrom(src => src.OrderDate))
                .ForMember(dest => dest.Status,
                    opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.TotalAmount,
                    opt => opt.Ignore()) 
                .ForMember(dest => dest.CustomerId,
                    opt => opt.MapFrom(src => src.CustomerId));

            CreateMap<OrderUpdateDto, Order>()
                .ForMember(dest => dest.CustomerId,
                    opt => opt.Condition(src => src.CustomerId.HasValue))
                .ForMember(dest => dest.CustomerId,
                    opt => opt.MapFrom(src => src.CustomerId.Value))
                .ForMember(dest => dest.Status,
                    opt => opt.Condition(src => !string.IsNullOrEmpty(src.Status)))
                .ForMember(dest => dest.Status,
                    opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.TotalAmount,
                    opt => opt.Condition(src => src.TotalAmount.HasValue))
                .ForMember(dest => dest.TotalAmount,
                    opt => opt.MapFrom(src => src.TotalAmount.Value))
                .ForMember(dest => dest.OrderDate,
                    opt => opt.Ignore()) 
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<Customer, CustomerReadDto>()
                .ForMember(dest => dest.Name,
                    opt => opt.MapFrom(src => src.Name));

            CreateMap<CustomerCreateDto, Customer>()
                .ForMember(dest => dest.Name,
                    opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Email,
                    opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Phone,
                    opt => opt.MapFrom(src => src.Phone));

            CreateMap<Dish, DishReadDto>()
                .ForMember(dest => dest.Name,
                    opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description,
                    opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Price,
                    opt => opt.MapFrom(src => src.Price));

            CreateMap<DishCreateDto, Dish>()
                .ForMember(dest => dest.Name,
                    opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description,
                    opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Price,
                    opt => opt.MapFrom(src => src.Price));

            CreateMap<OrderItem, OrderItemReadDto>()
                .ForMember(dest => dest.DishId,
                    opt => opt.MapFrom(src => src.DishId))
                .ForMember(dest => dest.DishName,
                    opt => opt.MapFrom(src => src.Dish != null ? src.Dish.Name : string.Empty))
                .ForMember(dest => dest.Quantity,
                    opt => opt.MapFrom(src => src.Quantity))
                .ForMember(dest => dest.Price,
                    opt => opt.MapFrom(src => src.Price));

            CreateMap<OrderItemCreateDto, OrderItem>()
                .ForMember(dest => dest.DishId,
                    opt => opt.MapFrom(src => src.DishId))
                .ForMember(dest => dest.Quantity,
                    opt => opt.MapFrom(src => src.Quantity))
                .ForMember(dest => dest.Price,
                    opt => opt.Ignore())
                .ForMember(dest => dest.Dish,
                    opt => opt.Ignore());
        }
    }
}