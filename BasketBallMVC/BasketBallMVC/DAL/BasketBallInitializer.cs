using BasketBallMVC.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace BasketBallMVC.DAL
{
    public class BasketBallInitializer : DropCreateDatabaseIfModelChanges<BasketBallContext> // DropCreateDatabaseIfModelChanges -- DropCreateDatabaseAlways
    {
        protected override void Seed(BasketBallContext context)
        {
            SeedBasketBallData(context);

            base.Seed(context);
        }

        private void SeedBasketBallData(BasketBallContext context)
        {

            var categories = new List<ShopCategory>
            {
                new ShopCategory() {ShopCategoryId = Guid.NewGuid(), Name = "Siła" },
                new ShopCategory() {ShopCategoryId = Guid.NewGuid(), Name = "Celność" },
                new ShopCategory() {ShopCategoryId = Guid.NewGuid(), Name = "Szybkość" },
                new ShopCategory() {ShopCategoryId = Guid.NewGuid(), Name = "Obrona" },
            };

            categories.ForEach(g => context.ShopCategories.AddOrUpdate(g));
            context.SaveChanges();

            var deviceCategories = new List<DeviceCategory>
            {
                new DeviceCategory() {ShopCategory = categories[0], Name ="Hantle",DeviceCategoryId  = Guid.NewGuid() },
                new DeviceCategory() {ShopCategory = categories[0], Name ="Sztanga",DeviceCategoryId  = Guid.NewGuid() },
                new DeviceCategory() {ShopCategory = categories[0], Name ="Lina",DeviceCategoryId  = Guid.NewGuid() },
                new DeviceCategory() {ShopCategory = categories[0], Name ="Ławeczka",DeviceCategoryId  = Guid.NewGuid() },
                new DeviceCategory() {ShopCategory = categories[1], Name ="Kosz",DeviceCategoryId  = Guid.NewGuid() },
                new DeviceCategory() {ShopCategory = categories[1], Name ="Oszczep",DeviceCategoryId  = Guid.NewGuid() },
                new DeviceCategory() {ShopCategory = categories[1], Name ="Łuk",DeviceCategoryId  = Guid.NewGuid() },
                new DeviceCategory() {ShopCategory = categories[1], Name ="Bramka",DeviceCategoryId  = Guid.NewGuid() },
                new DeviceCategory() {ShopCategory = categories[2], Name ="Bieżnia",DeviceCategoryId  = Guid.NewGuid() },
                new DeviceCategory() {ShopCategory = categories[2], Name ="Rower",DeviceCategoryId  = Guid.NewGuid() },
                new DeviceCategory() {ShopCategory = categories[2], Name ="Rower stacjonarny",DeviceCategoryId  = Guid.NewGuid() },
                new DeviceCategory() {ShopCategory = categories[2], Name ="Buty",DeviceCategoryId  = Guid.NewGuid() },
                new DeviceCategory() {ShopCategory = categories[3], Name ="Tarcza treningowa",DeviceCategoryId  = Guid.NewGuid() },
                new DeviceCategory() {ShopCategory = categories[3], Name ="Ochraniacze",DeviceCategoryId  = Guid.NewGuid() },
                new DeviceCategory() {ShopCategory = categories[3], Name ="Slalom",DeviceCategoryId  = Guid.NewGuid() },
                new DeviceCategory() {ShopCategory = categories[3], Name ="Mata",DeviceCategoryId  = Guid.NewGuid() }
            };
            deviceCategories.ForEach(x => context.DeviceCategories.AddOrUpdate(x));
            context.SaveChanges();

            var devices = new List<Device>
            {
                new Device() {Name="Złote Hantle", Price=850, DeviceCategory=deviceCategories[0],DeviceID=Guid.NewGuid() ,Description= "+80 siła" },
                new Device() {Name="Srebrne Hantle", Price=800, DeviceCategory=deviceCategories[0],DeviceID=Guid.NewGuid() ,Description= "+75 siła" },
                new Device() {Name="Brązowe Hantle", Price=750, DeviceCategory=deviceCategories[0],DeviceID=Guid.NewGuid() ,Description= "+70 siła" },
                new Device() {Name="Zwykłe Hantle", Price=700, DeviceCategory=deviceCategories[0],DeviceID=Guid.NewGuid() ,Description= "+65 siła" },


                new Device() {Name="Złota Sztanga", Price=650, DeviceCategory=deviceCategories[1],DeviceID=Guid.NewGuid() ,Description= "+60 siła" },
                new Device() {Name="Srebrna Sztanga", Price=600, DeviceCategory=deviceCategories[1],DeviceID=Guid.NewGuid() ,Description= "+55 siła" },
                new Device() {Name="Brązowa Sztanga", Price=550, DeviceCategory=deviceCategories[1],DeviceID=Guid.NewGuid() ,Description= "+50 siła" },
                new Device() {Name="Zwykła Sztanga", Price=500, DeviceCategory=deviceCategories[1],DeviceID=Guid.NewGuid() ,Description= "+45 siła" },


                new Device() {Name="Złota Lina", Price=450, DeviceCategory=deviceCategories[2],DeviceID=Guid.NewGuid() ,Description= "+40 siła" },
                new Device() {Name="Srebrna Lina", Price=400, DeviceCategory=deviceCategories[2],DeviceID=Guid.NewGuid() ,Description= "+35 siła" },
                new Device() {Name="Brązowa Lina", Price=350, DeviceCategory=deviceCategories[2],DeviceID=Guid.NewGuid() ,Description= "+30 siła" },
                new Device() {Name="Zwykła Lina", Price=300, DeviceCategory=deviceCategories[2],DeviceID=Guid.NewGuid() ,Description= "+25 siła" },

                new Device() {Name="Złota Ławeczka", Price=250, DeviceCategory=deviceCategories[3],DeviceID=Guid.NewGuid() ,Description= "+20 siła" },
                new Device() {Name="Srebrna Ławeczka", Price=200, DeviceCategory=deviceCategories[3],DeviceID=Guid.NewGuid() ,Description= "+15 siła" },
                new Device() {Name="Brązowa Ławeczka", Price=150, DeviceCategory=deviceCategories[3],DeviceID=Guid.NewGuid() ,Description= "+10 siła" },
                new Device() {Name="Zwykła Ławeczka", Price=100, DeviceCategory=deviceCategories[3],DeviceID=Guid.NewGuid() ,Description= "+5 siła" },


                new Device() {Name="Złoty Kosz", Price=850, DeviceCategory=deviceCategories[4],DeviceID=Guid.NewGuid() ,Description= "+80 celność" },
                new Device() {Name="Srebrny Kosz", Price=800, DeviceCategory=deviceCategories[4],DeviceID=Guid.NewGuid() ,Description= "+75 celność" },
                new Device() {Name="Brązowy Kosz", Price=750, DeviceCategory=deviceCategories[4],DeviceID=Guid.NewGuid() ,Description= "+70 celność" },
                new Device() {Name="Zwykły Kosz", Price=700, DeviceCategory=deviceCategories[4],DeviceID=Guid.NewGuid() ,Description= "+65 celność" },


                new Device() {Name="Złoty Oszczep", Price=650, DeviceCategory=deviceCategories[5],DeviceID=Guid.NewGuid() ,Description= "+60 celność" },
                new Device() {Name="Srebrny Oszczep", Price=600, DeviceCategory=deviceCategories[5],DeviceID=Guid.NewGuid() ,Description= "+55 celność" },
                new Device() {Name="Brązowy Oszczep", Price=550, DeviceCategory=deviceCategories[5],DeviceID=Guid.NewGuid() ,Description= "+50 celność" },
                new Device() {Name="Zwykły Oszczep", Price=500, DeviceCategory=deviceCategories[5],DeviceID=Guid.NewGuid() ,Description= "+45 celność" },


                new Device() {Name="Złoty Łuk", Price=450, DeviceCategory=deviceCategories[6],DeviceID=Guid.NewGuid() ,Description= "+40 celność" },
                new Device() {Name="Srebrny Łuk", Price=400, DeviceCategory=deviceCategories[6],DeviceID=Guid.NewGuid() ,Description= "+35 celność" },
                new Device() {Name="Brązowy Łuk", Price=350, DeviceCategory=deviceCategories[6],DeviceID=Guid.NewGuid() ,Description= "+30 celność" },
                new Device() {Name="Zwykły Łuk", Price=300, DeviceCategory=deviceCategories[6],DeviceID=Guid.NewGuid() ,Description= "+25 celność" },


                new Device() {Name="Złota Bramka", Price=250, DeviceCategory=deviceCategories[7],DeviceID=Guid.NewGuid() ,Description= "+20 celność" },
                new Device() {Name="Srebrna Bramka", Price=200, DeviceCategory=deviceCategories[7],DeviceID=Guid.NewGuid() ,Description= "+15 celność" },
                new Device() {Name="Brązowa Bramka", Price=150, DeviceCategory=deviceCategories[7],DeviceID=Guid.NewGuid() ,Description= "+10 celność" },
                new Device() {Name="Zwykła Bramka", Price=100, DeviceCategory=deviceCategories[7],DeviceID=Guid.NewGuid() ,Description= "+5 celność" },


                new Device() {Name="Złota Bieżnia", Price=850, DeviceCategory=deviceCategories[8],DeviceID=Guid.NewGuid() ,Description= "+80 szybkość" },
                new Device() {Name="Srebrna Bieżnia", Price=800, DeviceCategory=deviceCategories[8],DeviceID=Guid.NewGuid() ,Description= "+75  szybkość" },
                new Device() {Name="Brązowa Bieżnia", Price=750, DeviceCategory=deviceCategories[8],DeviceID=Guid.NewGuid() ,Description= "+70  szybkość" },
                new Device() {Name="Zwykła Bieżnia", Price=700, DeviceCategory=deviceCategories[8],DeviceID=Guid.NewGuid() ,Description= "+65  szybkość" },

                new Device() {Name="Złoty Rower", Price=650, DeviceCategory=deviceCategories[9],DeviceID=Guid.NewGuid() ,Description= "+60  szybkość" },
                new Device() {Name="Srebrny Rower", Price=600, DeviceCategory=deviceCategories[9],DeviceID=Guid.NewGuid() ,Description= "+55  szybkość" },
                new Device() {Name="Brązowy Rower", Price=550, DeviceCategory=deviceCategories[9],DeviceID=Guid.NewGuid() ,Description= "+50  szybkość" },
                new Device() {Name="Zwykły Rower", Price=500, DeviceCategory=deviceCategories[9],DeviceID=Guid.NewGuid() ,Description= "+45  szybkość" },


                new Device() {Name="Złoty Rower stacjonarny", Price=450, DeviceCategory=deviceCategories[10],DeviceID=Guid.NewGuid() ,Description= "+40  szybkość" },
                new Device() {Name="Srebrny Rower stacjonarny", Price=400, DeviceCategory=deviceCategories[10],DeviceID=Guid.NewGuid() ,Description= "+35  szybkość" },
                new Device() {Name="Brązowy Rower stacjonarny", Price=350, DeviceCategory=deviceCategories[10],DeviceID=Guid.NewGuid() ,Description= "+30  szybkość" },
                new Device() {Name="Zwykły Rower stacjonarny", Price=300, DeviceCategory=deviceCategories[10],DeviceID=Guid.NewGuid() ,Description= "+25  szybkość" },


                new Device() {Name="Złote Buty", Price=250, DeviceCategory=deviceCategories[11],DeviceID=Guid.NewGuid() ,Description= "+20  szybkość" },
                new Device() {Name="Srebrne Buty", Price=200, DeviceCategory=deviceCategories[11],DeviceID=Guid.NewGuid() ,Description= "+15  szybkość" },
                new Device() {Name="Brązowe Buty", Price=150, DeviceCategory=deviceCategories[11],DeviceID=Guid.NewGuid() ,Description= "+10  szybkość" },
                new Device() {Name="Zwykłe Buty", Price=100, DeviceCategory=deviceCategories[11],DeviceID=Guid.NewGuid() ,Description= "+5  szybkość" },


                new Device() {Name="Złota Tarcza treningowa", Price=850, DeviceCategory=deviceCategories[12],DeviceID=Guid.NewGuid() ,Description= "+80 obrona" },
                new Device() {Name="Srebrna Tarcza treningowa", Price=800, DeviceCategory=deviceCategories[12],DeviceID=Guid.NewGuid() ,Description= "+75 obrona" },
                new Device() {Name="Brązowa Tarcza treningowa", Price=750, DeviceCategory=deviceCategories[12],DeviceID=Guid.NewGuid() ,Description= "+70 obrona" },
                new Device() {Name="Zwykła Tarcza treningowa", Price=700, DeviceCategory=deviceCategories[12],DeviceID=Guid.NewGuid() ,Description= "+65 obrona" },


                new Device() {Name="Złote Ochraniacze", Price=650, DeviceCategory=deviceCategories[13],DeviceID=Guid.NewGuid() ,Description= "+60 obrona" },
                new Device() {Name="Srebrne Ochraniacze", Price=600, DeviceCategory=deviceCategories[13],DeviceID=Guid.NewGuid() ,Description= "+55 obrona" },
                new Device() {Name="Brązowe Ochraniacze", Price=550, DeviceCategory=deviceCategories[13],DeviceID=Guid.NewGuid() ,Description= "+50 obrona" },
                new Device() {Name="Zwykłe Ochraniacze", Price=500, DeviceCategory=deviceCategories[13],DeviceID=Guid.NewGuid() ,Description= "+45 obrona" },


                new Device() {Name="Złoty Slalom", Price=450, DeviceCategory=deviceCategories[14],DeviceID=Guid.NewGuid() ,Description= "+40 obrona" },
                new Device() {Name="Srebrny Slalom", Price=400, DeviceCategory=deviceCategories[14],DeviceID=Guid.NewGuid() ,Description= "+35 obrona" },
                new Device() {Name="Brązowy Slalom", Price=350, DeviceCategory=deviceCategories[14],DeviceID=Guid.NewGuid() ,Description= "+30 obrona" },
                new Device() {Name="Zwykły Slalom", Price=300, DeviceCategory=deviceCategories[14],DeviceID=Guid.NewGuid() ,Description= "+25 obrona" },


                new Device() {Name="Złota Mata", Price=250, DeviceCategory=deviceCategories[15],DeviceID=Guid.NewGuid() ,Description= "+20 obrona" },
                new Device() {Name="Srebrna Mata", Price=200, DeviceCategory=deviceCategories[15],DeviceID=Guid.NewGuid() ,Description= "+15 obrona" },
                new Device() {Name="Brązowa Mata", Price=150, DeviceCategory=deviceCategories[15],DeviceID=Guid.NewGuid() ,Description= "+10 obrona" },
                new Device() {Name="Zwykła Mata", Price=100, DeviceCategory=deviceCategories[15],DeviceID=Guid.NewGuid() ,Description= "+5 obrona" },
            };
            devices.ForEach(x => context.Devices.AddOrUpdate(x));
            context.SaveChanges();
        }
    }
}