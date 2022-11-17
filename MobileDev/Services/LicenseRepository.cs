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
    public class LicenseRepository : ILicenseRepository<License>
    {
        private IAlertService alertService;

        public LicenseRepository(IAlertService alertService)
        {
            this.alertService = alertService;
        }
        public async Task<List<License>> Find(string arg)
        {
            try
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync("https://api.dtc-xtreme.net/License/search/"+arg);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<List<License>>();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Errors: " + ex.Message);
                alertService.ShowAlert("Error!", ex.Message);

            }
            return new List<License>();
        }

        public async Task<License> FindById(int id)
        {
            try
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync("https://api.dtc-xtreme.net/License/id/"+id);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<License>();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Errors: " + ex.Message);
                alertService.ShowAlert("Error!", ex.Message);
            }
            return null;
        }

        public async Task<List<License>> GetAll()
        {
            try
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync("https://api.dtc-xtreme.net/License");
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<List<License>>();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Errors: " + ex.Message);
                alertService.ShowAlert("Error!", ex.Message);
            }
            return new List<License>();
        }
    }
}