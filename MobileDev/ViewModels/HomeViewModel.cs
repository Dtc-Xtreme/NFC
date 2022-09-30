using banditoth.MAUI.Multilanguage.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Plugin.Fingerprint.Abstractions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace MobileDev.ViewModels
{
    public partial class HomeViewModel : BaseViewModel
    {       
        public HomeViewModel()
        {
            
        }

        [RelayCommand]
        private async void GetAPI()
        {
            //HttpClientHandler handler = new HttpClientHandler();
            //handler.ServerCertificateCustomValidationCallback = ServerCertificateCustomValidation;
            HttpClient client = new HttpClient();
            //HttpClient client = new HttpClient(handler);
            HttpResponseMessage response = await client.GetAsync("http://10.0.10.6:7163/Calendar");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
        }

        //private static bool ServerCertificateCustomValidation(HttpRequestMessage requestMessage, X509Certificate2 certificate, X509Chain chain, SslPolicyErrors sslErrors)
        //{
        //    // It is possible to inspect the certificate provided by the server.
        //    Console.WriteLine($"Requested URI: {requestMessage.RequestUri}");
        //    Console.WriteLine($"Effective date: {certificate.GetEffectiveDateString()}");
        //    Console.WriteLine($"Exp date: {certificate.GetExpirationDateString()}");
        //    Console.WriteLine($"Issuer: {certificate.Issuer}");
        //    Console.WriteLine($"Subject: {certificate.Subject}");

        //    // Based on the custom logic it is possible to decide whether the client considers certificate valid or not
        //    Console.WriteLine($"Errors: {sslErrors}");
        //    return sslErrors == SslPolicyErrors.None;
        //}

    }
}
