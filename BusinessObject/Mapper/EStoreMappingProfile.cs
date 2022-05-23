using AutoMapper;
using BusinessObject.Dtos;

namespace BusinessObject.Mapper
{
    public class EStoreMappingProfile : Profile
    {
        public EStoreMappingProfile()
        {
            CreateMap<CategoryDto, Category>().ReverseMap();
            CreateMap<CreateCategoryDto, Category>();
            CreateMap<UpdateCategoryDto, Category>();
            CreateMap<CategoryDto, UpdateCategoryDto>();

            CreateMap<MemberDto, Member>().ReverseMap();
            CreateMap<CreateMemberDto, Member>();
            CreateMap<UpdateMemberDto, Member>();
            CreateMap<MemberDto, UpdateMemberDto>();

            CreateMap<ProductDto, Product>().ReverseMap();
            CreateMap<CreateProductDto, Product>();
            CreateMap<UpdateProductDto, Product>();
            CreateMap<ProductDto, UpdateProductDto>();

            CreateMap<OrderDto, Order>().ReverseMap();
            CreateMap<CreateOrderDto, Order>();

            CreateMap<OrderDetailDto, OrderDetail>().ReverseMap();
        }
    }
}
