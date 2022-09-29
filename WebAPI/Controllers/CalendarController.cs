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
        private static readonly CalendarItem[] CalenderItems = new[]
        {
            new CalendarItem(DateTime.Parse("01/10/2022"), "Competitie", "Boekt A VS Boekt B"),
            new CalendarItem(DateTime.Parse("08/10/2022"), "Competitie", "Boekt A VS Kelchteren A"),
            new CalendarItem(DateTime.Parse("15/10/2022"), "Competitie", "Boekt A VS Genenbos B"),
            new CalendarItem(DateTime.Parse("22/10/2022"), "Competitie", "Boekt A VS Sparrendaal B"),
            new CalendarItem(DateTime.Parse("29/10/2022"), "Competitie", "Boekt A VS Maasmechelen B"),
        };

        [HttpGet(Name = "GetCalendar")]
        public IEnumerable<CalendarItem> Get()
        {
            return CalenderItems.ToList();
        }

    }
}
