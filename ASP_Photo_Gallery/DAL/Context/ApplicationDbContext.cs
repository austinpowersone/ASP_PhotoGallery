using System;
using System.Data.Entity;
using ASP_Photo_Gallery.Core;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ASP_Photo_Gallery.DAL.Context
{
    public class ApplicationDbContext : IdentityDbContext
    {
        static ApplicationDbContext()
        {
            Database.SetInitializer(new ApplicationDbInitializer());
        }

        public IDbSet<Image> Images { get; set; }
    }

    public class ApplicationDbInitializer : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            //Images
            var image1 = new Image
            {
                Path = @"1.jpg",
                Description = "Computer scines",
                InsertDate = DateTime.Now,
                UpdateDate = DateTime.Now,
                Name = "Computer"
            };
            context.Images.Add(image1);
            var image2 = new Image
            {
                Path = @"2.jpg",
                Description = "USA Army",
                InsertDate = DateTime.Now,
                UpdateDate = DateTime.Now,
                Name = "USA"
            };
            context.Images.Add(image2);
            var image3 = new Image
            {
                Path = @"3.jpg",
                Description = "Sun",
                InsertDate = DateTime.Now,
                UpdateDate = DateTime.Now,
                Name = "Story"
            };
            context.Images.Add(image3);
            var image4 = new Image
            {
                Path = @"4.jpg",
                Description = "Snow",
                InsertDate = DateTime.Now,
                UpdateDate = DateTime.Now,
                Name = "House"
            };
            context.Images.Add(image4);
            var image5 = new Image
            {
                Path = @"5.jpg",
                Description = "Mountains",
                InsertDate = DateTime.Now,
                UpdateDate = DateTime.Now,
                Name = "Tourism"
            };
            context.Images.Add(image5);

            //Save
            context.SaveChanges();
        }
    }
}