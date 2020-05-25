using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
            });

            return mapperConfig.CreateMapper();
        }
    }
}