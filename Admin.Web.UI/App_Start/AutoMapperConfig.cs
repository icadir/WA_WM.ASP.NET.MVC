using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Admin.Models.Entities;
using Admin.Models.ViewModels;
using AutoMapper;

namespace Admin.Web.UI.App_Start
{
    public class AutoMapperConfig
    {
        public static void RegisterMapping()
        {
            Mapper.Initialize(cfg =>
            {
                CategoryMapping(cfg);
                UserMapping(cfg);
            });
        }

        private static void CategoryMapping(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Product, ProductViewModel>()
                //aşagıdaki sadece göstermelik bir örnek. mesala sen categoryde product sayısınıda getirmek istiyorsun. Böyle yapmak isterdigimizde  gidip category view modelde bir alan açarız ve aşagıdaki gibi .dest.ProductCount opt.Product.ProductCount diyerek yapabiliz. Yani tabloda olmayan farklı bir tablodan gelen alanları bu şekilde yapabilirz. 
                //Projennin product kontrolunde Nasıl kullanıldıgını gösterdildi. 
                //Global ajax a yazıldı sonra.
                .ForMember(dest=>dest.SupProductId,opt=>opt.MapFrom(x=>x.Category.CategoryName))
                .ReverseMap();
        }

        private static void UserMapping(IMapperConfigurationExpression cfg)
        {
            throw new NotImplementedException();
        }
    }
}