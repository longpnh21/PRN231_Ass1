using AutoMapper;
using BusinessObject;
using BusinessObject.Dtos;

namespace eStoreAPI.Mapper
{
    public class EStoreMappingProfile : Profile
    {
        public EStoreMappingProfile()
        {
            CreateMap<CategoryDto, Category>().ReverseMap();
            CreateMap<CreateCategoryDto, Category>();
            CreateMap<UpdateCategoryDto, Category>();

            CreateMap<MemberDto, Member>().ReverseMap();
            CreateMap<CreateMemberDto, Member>();
            CreateMap<UpdateMemberDto, Member>();

            CreateMap<ProductDto, Product>().ReverseMap();
            CreateMap<CreateProductDto, Product>();
            CreateMap<UpdateProductDto, Product>();

            CreateMap<OrderDto, Order>().ReverseMap();
            CreateMap<CreateOrderDto, Order>();

            CreateMap<OrderDetailDto, OrderDetail>().ReverseMap();
        }
    }
}
