using AutoMapper;
using Domain.EF;
using WebMvc.Models.ModelView;

namespace WebMvc.Configurations
{
    public class MapperProfile
    {
        //create automaper config
        public static IMapper MapperConfig()
        {
            var mapperConfig = new AutoMapper.MapperConfiguration(config =>
            {
                config.CreateMap<Role, RoleDTO>();
                //category
                config.CreateMap<Category, CategoryDTO>();
                config.CreateMap<CategoryDTO, Category>();
                //donate
                config.CreateMap<Donate, DonateDTO>();
                config.CreateMap<DonateDTO, Donate>();
            });

            return mapperConfig.CreateMapper();
        }
    }
}