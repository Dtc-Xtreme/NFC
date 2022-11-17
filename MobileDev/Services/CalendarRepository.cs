using PetanqueCL.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace MobileDev.Services
{
    public class CalendarRepository : ICalendarRepository<CalendarItem>
    {
        public async Task<List<CalendarItem>> GetAll()
        {
            try
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync("https://api.dtc-xtreme.net/Calendar");
                var code = response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<List<CalendarItem>>();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Errors: " + ex.Message);
            }
            return null;
        }
    }
}