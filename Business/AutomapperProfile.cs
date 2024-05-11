using AutoMapper;
using Business.Models;
using Data.Entities;
using System.Linq;

namespace Business
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Receipt, ReceiptModel>()
                .ForMember(rm => rm.ReceiptDetailsIds, r => r.MapFrom(x => x.ReceiptDetails.Select(rd => rd.Id)))
                .ReverseMap();

            //TODO: Create mapping for Product and ProductModel
            CreateMap<Product, ProductModel>()
                .ForMember(pm => pm.ReceiptDetailIds, opt => opt.MapFrom(p => p.ReceiptDetails.Select(rd => rd.Id)))
                .ForMember(pm => pm.CategoryName, opt => opt.MapFrom(p => p.Category.CategoryName))
                .ReverseMap();

            //TODO: Create mapping for ReceiptDetail and ReceiptDetailModel
            CreateMap<ReceiptDetail, ReceiptDetailModel>().ReverseMap();

            //TODO: Create mapping that combines Customer and Person into CustomerModel
            CreateMap<Customer, CustomerModel>()
                .ForMember(cm => cm.ReceiptsIds, opt => opt.MapFrom(c => c.Receipts.Select(r => r.Id)))
                .ForMember(cm => cm.Name, opt => opt.MapFrom(c => c.Person.Name))
                .ForMember(cm => cm.Surname, opt => opt.MapFrom(c => c.Person.Surname))
                .ForMember(cm => cm.BirthDate, opt => opt.MapFrom(c => c.Person.BirthDate))
                .ReverseMap();

            //TODO: Create mapping for ProductCategory and ProductCategoryModel
            CreateMap<ProductCategory, ProductCategoryModel>()
                .ForMember(rm => rm.ProductIds, r => r.MapFrom(x => x.Products.Select(rd => rd.Id)))
                .ReverseMap();
        }
    }
}