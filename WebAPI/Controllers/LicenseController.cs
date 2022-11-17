using Microsoft.AspNetCore.Mvc;
using PetanqueCL.Models;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Text.Json;
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
            },
            new License
            {
                Id = 11,
                Nr = 6000,
                FirstName = "Sandra",
                LastName = "Bartoli",
                BirthDate = DateTime.Parse("01/09/1980"),
                Disqualified = false,
                Image = ImageToByteArray("Images/11.jpg"),
                Active = true,
                Club= pcboekt,
                Gender = female
            },
            new License
            {
                Id = 12,
                Nr = 6005,
                FirstName = "Geofry",
                LastName = "Goodman",
                BirthDate = DateTime.Parse("05/08/1983"),
                Disqualified = false,
                Image = ImageToByteArray("Images/12.jpg"),
                Active = true,
                Club= pcboekt,
                Gender = male
            },
            new License
            {
                Id = 13,
                Nr = 6010,
                FirstName = "Inne",
                LastName = "Poelmans",
                BirthDate = DateTime.Parse("09/04/1950"),
                Disqualified = false,
                Image = ImageToByteArray("Images/13.jpg"),
                Active = true,
                Club= pcboekt,
                Gender = female
            },
            new License
            {
                Id = 14,
                Nr = 6015,
                FirstName = "Alisa",
                LastName = "Van de Winkel",
                BirthDate = DateTime.Parse("05/09/1969"),
                Disqualified = false,
                Image = ImageToByteArray("Images/14.jpg"),
                Active = true,
                Club= pcboekt,
                Gender = female
            },
            new License
            {
                Id = 15,
                Nr = 6020,
                FirstName = "Tim",
                LastName = "Derrijke",
                BirthDate = DateTime.Parse("02/02/1987"),
                Disqualified = false,
                Image = ImageToByteArray("Images/15.jpg"),
                Active = true,
                Club= pcboekt,
                Gender = male
            },
            new License
            {
                Id = 16,
                Nr = 7000,
                FirstName = "Deborah",
                LastName = "Eroi",
                BirthDate = DateTime.Parse("05/08/1981"),
                Disqualified = false,
                Image = ImageToByteArray("Images/16.jpg"),
                Active = false,
                Club= pclier,
                Gender = female
            },
            new License
            {
                Id = 17,
                Nr = 7005,
                FirstName = "Erik",
                LastName = "Chanu",
                BirthDate = DateTime.Parse("09/01/1985"),
                Disqualified = false,
                Image = ImageToByteArray("Images/17.jpg"),
                Active = false,
                Club= pclier,
                Gender = male
            },
            new License
            {
                Id = 18,
                Nr = 7010,
                FirstName = "Chiane",
                LastName = "Chenu",
                BirthDate = DateTime.Parse("06/08/1991"),
                Disqualified = false,
                Image = ImageToByteArray("Images/18.jpg"),
                Active = false,
                Club= pclier,
                Gender = female
            },
            new License
            {
                Id = 19,
                Nr = 7015,
                FirstName = "Daisy",
                LastName = "Albrecht",
                BirthDate = DateTime.Parse("01/02/1982"),
                Disqualified = false,
                Image = ImageToByteArray("Images/19.jpg"),
                Active = false,
                Club= pclier,
                Gender = female
            },
            new License
            {
                Id = 20,
                Nr = 7020,
                FirstName = "Klarisa",
                LastName = "Poelmans",
                BirthDate = DateTime.Parse("03/03/1993"),
                Disqualified = false,
                Image = ImageToByteArray("Images/20.jpg"),
                Active = false,
                Club= pclier,
                Gender = female
            },
            new License
            {
                Id = 21,
                Nr = 8000,
                FirstName = "Jasper",
                LastName = "Chonia",
                BirthDate = DateTime.Parse("01/09/1978"),
                Disqualified = false,
                Image = ImageToByteArray("Images/21.jpg"),
                Active = true,
                Club= pcboekt,
                Gender = male
            },
            new License
            {
                Id = 22,
                Nr = 8005,
                FirstName = "Tom",
                LastName = "Chonia",
                BirthDate = DateTime.Parse("04/04/1975"),
                Disqualified = false,
                Image = ImageToByteArray("Images/22.jpg"),
                Active = true,
                Club= pcboekt,
                Gender = male
            },
            new License
            {
                Id = 23,
                Nr = 8010,
                FirstName = "Dirk",
                LastName = "Gabriel",
                BirthDate = DateTime.Parse("09/08/1980"),
                Disqualified = false,
                Image = ImageToByteArray("Images/23.jpg"),
                Active = true,
                Club= pcboekt,
                Gender = male
            },
            new License
            {
                Id = 24,
                Nr = 8015,
                FirstName = "Lina",
                LastName = "Van de Winkel",
                BirthDate = DateTime.Parse("07/09/1962"),
                Disqualified = false,
                Image = ImageToByteArray("Images/24.jpg"),
                Active = true,
                Club= pcboekt,
                Gender = female
            },
            new License
            {
                Id = 25,
                Nr = 8020,
                FirstName = "Sara",
                LastName = "Derrijke",
                BirthDate = DateTime.Parse("08/08/1981"),
                Disqualified = false,
                Image = ImageToByteArray("Images/25.jpg"),
                Active = true,
                Club= pcboekt,
                Gender = female
            },
            new License
            {
                Id = 26,
                Nr = 9000,
                FirstName = "Melani",
                LastName = "Eroi",
                BirthDate = DateTime.Parse("05/08/2000"),
                Disqualified = false,
                Image = ImageToByteArray("Images/26.jpg"),
                Active = false,
                Club= pclier,
                Gender = female
            },
            new License
            {
                Id = 27,
                Nr = 9005,
                FirstName = "Roderika",
                LastName = "Espanino",
                BirthDate = DateTime.Parse("09/05/1980"),
                Disqualified = false,
                Image = ImageToByteArray("Images/27.jpg"),
                Active = false,
                Club= pclier,
                Gender = male
            },
            new License
            {
                Id = 28,
                Nr = 9010,
                FirstName = "Manu",
                LastName = "Mordona",
                BirthDate = DateTime.Parse("09/01/1988"),
                Disqualified = false,
                Image = ImageToByteArray("Images/28.jpg"),
                Active = false,
                Club= pclier,
                Gender = female
            },
            new License
            {
                Id = 29,
                Nr = 9015,
                FirstName = "Moradiz",
                LastName = "Espanino",
                BirthDate = DateTime.Parse("05/09/1984"),
                Disqualified = false,
                Image = ImageToByteArray("Images/29.jpg"),
                Active = false,
                Club= pclier,
                Gender = male
            },
            new License
            {
                Id = 30,
                Nr = 9020,
                FirstName = "Inna",
                LastName = "Poelmans",
                BirthDate = DateTime.Parse("03/07/1990"),
                Disqualified = false,
                Image = ImageToByteArray("Images/30.jpg"),
                Active = false,
                Club= pclier,
                Gender = female
            }
        };

        [HttpGet]
        public IActionResult GetAll()
        {
            if (Licenses.Length != 0 && Licenses != null)
            {
                return Ok(Licenses.ToList());

            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("id/{id}")]
        public IActionResult GetLicenceById(int id)
        {
            License? lic = Licenses.FirstOrDefault(c => c.Id == id);
            if (lic != null)
            {
                return Ok(lic);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("search/{arg}")]
        public IActionResult Find(string arg)
        {
            List<License>? result = new List<License>();
            int numeric;
            bool isNumeric = int.TryParse(arg, out numeric);
            if (isNumeric)
            {
               result.Add(Licenses.FirstOrDefault(c => c.Nr == numeric));
            }
            else
            {
                result = Licenses.Where(c => c.FirstName.ToLower().Contains(arg.ToLower()) || c.LastName.ToLower().Contains(arg.ToLower())).ToList();
            }

            if(result.Count > 0)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        private static byte[] ImageToByteArray(string fileName)
        {
            return System.IO.File.ReadAllBytes(fileName);
        }
    }
}