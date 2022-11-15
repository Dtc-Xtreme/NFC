using Microsoft.AspNetCore.Mvc;
using PetanqueCL.Models;
using System.Globalization;
using static System.Net.Mime.MediaTypeNames;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("License")]
    public class LicenseController : Controller
    {
        private static CultureInfo provider = new CultureInfo("nl-BE");
        private static string format = "dd/mm/yyyy";

        private static readonly Gender male = new Gender
        {
            Id = 1,
            Name = "Male"
        };

        private static readonly Gender female = new Gender
        {
            Id = 2,
            Name = "Female"
        };

        private static readonly Federation pfv = new Federation
        {
            Id = 1,
            Name = "Petanque Federatie Vlaanderen",
            Image = "pfv.png",
            Prefix = "PFV"
        };

        private static readonly Province limburg = new Province
        {
            Id = 1,
            Name = "Limburg",
            Federation = pfv
        };

        private static readonly Province antwerpen = new Province
        {
            Id = 2,
            Name = "Antwerpen",
            Federation = pfv
        };


        private static readonly Club pcboekt = new Club
        {
            Id = 1,
            Image = "boekt.jpg",
            Name = "PC Boekt",
            Nr = 1,
            Province = limburg
        };

        private static readonly Club pclier = new Club
        {
            Id = 2,
            Image = "pclier.png",
            Name = "PC Lier",
            Nr = 6,
            Province = antwerpen
        };

        private static readonly License[] Licenses = new[]
        {
            new License
            {
                Id = 1,
                Nr = 4000,
                FirstName = "Izy",
                LastName = "Daniels",
                BirthDate = DateTime.Parse("03/08/1989"),
                Disqualified = false,
                Image = ImageToByteArray("Images/1.jpg"),
                Active = true,
                Club= pcboekt,
                Gender = female
            },
            new License
            {
                Id = 2,
                Nr = 4005,
                FirstName = "Veronica",
                LastName = "Verheyen",
                BirthDate = DateTime.Parse("05/08/1980"),
                Disqualified = false,
                Image = ImageToByteArray("Images/2.jpg"),
                Active = true,
                Club= pcboekt,
                Gender = female
            },
            new License
            {   
                Id = 3,
                Nr = 4010,
                FirstName = "Danny",
                LastName = "Poelmans",
                BirthDate = DateTime.Parse("09/01/1975"),
                Disqualified = false,
                Image = ImageToByteArray("Images/3.jpg"),
                Active = true,
                Club= pcboekt,
                Gender = male
            },
            new License
            {   
                Id = 4,
                Nr = 4015,
                FirstName = "Eddy",
                LastName = "Van de Winkel",
                BirthDate = DateTime.Parse("05/09/1955"),
                Disqualified = false,
                Image = ImageToByteArray("Images/4.jpg"),
                Active = true,
                Club= pcboekt,
                Gender = male
            },
            new License
            {   
                Id = 5,
                Nr = 4020,
                FirstName = "Frans",
                LastName = "Marien",
                BirthDate = DateTime.Parse("01/01/1950"),
                Disqualified = false,
                Image = ImageToByteArray("Images/5.jpg"),
                Active = true,
                Club= pcboekt,
                Gender = male
            },
            new License
            { 
                Id = 6,
                Nr = 5000,
                FirstName = "Frans",
                LastName = "Kazmierczak",
                BirthDate = DateTime.Parse("05/08/1955"),
                Disqualified = false,
                Image = ImageToByteArray("Images/6.jpg"),
                Active = false,
                Club= pclier,
                Gender = male
            },
            new License
            {
                Id = 7,
                Nr = 5005,
                FirstName = "Natachsa",
                LastName = "Poels",
                BirthDate = DateTime.Parse("09/01/1991"),
                Disqualified = false,
                Image = ImageToByteArray("Images/7.jpg"),
                Active = false,
                Club= pclier,
                Gender = female
            },
            new License
            {
                Id = 8,
                Nr = 5010,
                FirstName = "Bart",
                LastName = "Soons",
                BirthDate = DateTime.Parse("06/08/1988"),
                Disqualified = false,
                Image = ImageToByteArray("Images/8.jpg"),
                Active = false,
                Club= pclier,
                Gender = male
            },
            new License
            {
                Id = 9,
                Nr = 5015,
                FirstName = "Nathalie",
                LastName = "Poelmans",
                BirthDate = DateTime.Parse("09/02/1992"),
                Disqualified = false,
                Image = ImageToByteArray("Images/9.jpg"),
                Active = false,
                Club= pclier,
                Gender = female
            },
            new License
            {
                Id = 10,
                Nr = 5020,
                FirstName = "Cindy",
                LastName = "Kazmierczak",
                BirthDate = DateTime.Parse("01/02/1975"),
                Disqualified = false,
                Image = ImageToByteArray("Images/10.jpg"),
                Active = false,
                Club= pclier,
                Gender = female
            }
        };

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(Licenses.ToList());
        }

        private static byte[] ImageToByteArray(string fileName)
        {
            return System.IO.File.ReadAllBytes(fileName);
        }
    }
}