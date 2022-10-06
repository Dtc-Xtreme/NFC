using Microsoft.AspNetCore.Mvc;
using System;
using System.Globalization;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("Calendar")]
    public class CalendarController : Controller
    {
        private static CultureInfo provider = new CultureInfo("nl-BE");
        private static string format = "dd/mm/yyyy";

        private static readonly CalendarItem[] CalenderItems = new[]
        {
            new CalendarItem(DateTime.ParseExact("01/10/2022", format, provider), "Competitie", "Boekt A VS Boekt B"),
            new CalendarItem(DateTime.ParseExact("08/10/2022", format, provider), "Competitie", "Boekt A VS Kelchteren A"),
            new CalendarItem(DateTime.ParseExact("15/10/2022", format, provider), "Competitie", "Boekt A VS Genenbos B"),
            new CalendarItem(DateTime.ParseExact("22/10/2022", format, provider), "Competitie", "Boekt A VS Sparrendaal B"),
            new CalendarItem(DateTime.ParseExact("29/10/2022", format, provider), "Competitie", "Boekt A VS Maasmechelen B"),
        };

        [HttpGet]
        public IEnumerable<CalendarItem> Get()
        {
            return CalenderItems.ToList();
        }

    }
}
