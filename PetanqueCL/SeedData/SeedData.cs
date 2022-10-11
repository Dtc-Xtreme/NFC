using PetanqueCL.Models;

namespace Repositories.SeedData
{
    public class SeedData
    {
        public static void EnsurePopulated(PetanqueDbContext dbContext)
        {
            PetanqueDbContext context = dbContext;

            //------------//
            // Federation //
            //------------//
            Federation fed = null;

            if (!context.Federations.Any())
            {
                context.Federations.AddRange(
                    fed = new Federation
                    {
                        Name = "Petanque Federatie Vlaanderen",
                        Prefix = "PFV",
                        //Image = "pfv.png"
                    }
                );
                context.SaveChanges();
            }


            //-----------//
            // Provinces //
            //-----------//
            Province antw = null;
            Province limb = null;

            if (!context.Provinces.Any())
            {
                context.Provinces.AddRange(
                    new Province
                    {
                        Name = "Oost-Vlaanderen",
                        Federation = fed
                    },
                    antw = new Province
                    {
                        Name = "Antwerpen",
                        Federation = fed
                    },
                    limb = new Province
                    {
                        Name = "Limburg",
                        Federation = fed
                    },
                    new Province
                    {
                        Name = "Vlaams Brabant",
                        Federation = fed
                    },
                    new Province
                    {
                        Name = "West-Vlaanderen",
                        Federation = fed
                    }
                );
                context.SaveChanges();
            }


            //---------//
            // Genders //
            //---------//
            Gender male = null;
            Gender female = null;

            if (!context.Genders.Any())
            {
                context.Genders.AddRange(
                    male = new Gender
                    {
                        Name = "Male"
                    },
                    female = new Gender
                    {
                        Name = "Female"
                    }
                );
                context.SaveChanges();
            }


            //-------//
            // Clubs //
            //-------//
            Club boekt = null;
            Club lier = null;

            if (!context.Clubs.Any())
            {
                context.Clubs.AddRange(
                    boekt = new Club
                    {
                        Name = "PC Boekt",
                        Nr = 007,
                        Province = limb,
                        Image = "boekt.jpg"
                    },
                    lier = new Club
                    {
                        Name = "PC Lier",
                        Nr = 009,
                        Province = antw,
                        Image = "pclier.jpg"
                    }
                );
                context.SaveChanges();
            }


            //----------//
            // Licenses //
            //----------//
            License steK = null;
            License fraK = null;

            if (!context.Licenses.Any())
            {
                context.Licenses.AddRange(
                    steK = new License
                    {
                        Nr = 4077,
                        FirstName = "Steven",
                        LastName = "Kazmierczak",
                        BirthDate = DateTime.Parse("03/08/1989"),
                        Gender = male,
                        Club = boekt,
                        Disqualified = false,
                        Image = "stevenk.jpg",
                        Active = true
                    },
                    fraK = new License
                    {
                        Nr = 4088,
                        FirstName = "Frans",
                        LastName = "Kazmierczak",
                        BirthDate = DateTime.Parse("05/08/1955"),
                        Gender = male,
                        Club = boekt,
                        Disqualified = false,
                        Image = "fransk.jpg",
                        Active = false
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
