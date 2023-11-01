using ClothBazar.Database;
using ClothBazar.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothBazar.Services
{
    public class ConfigurationsService
    {
        //public static ConfigurationsService Instance { get; set; }
        //private static ConfigurationsService instance { get; set; }

        //private ConfigurationsService()
        //{

        //}

        public Config GetConfig (string Key)
        {
            using (var context = new AppDbContext())
            {
                return context.Configurations.Find(Key);
            }
        }
    }
}
