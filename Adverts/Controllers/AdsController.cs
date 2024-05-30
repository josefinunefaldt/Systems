using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Adverts.ViewModel;
using Newtonsoft.Json;
using Subscribers.Data.Entities;
using Adverts.Data.Entities;
using Adverts.Data;
using Microsoft.EntityFrameworkCore;
using freecurrencyapi;
using Newtonsoft.Json.Linq;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.DataProtection.Repositories;

namespace Advert.Controllers
{
    public class AdsController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly ApplicationDbContext _Context;
        private readonly string _apiKey = "fca_live_HbXSO2MFJEQrs6aX3jZcCzpnozQfIKCpEXBJfsXZ";

        public AdsController(IHttpClientFactory clientFactory, ApplicationDbContext dbContext)
        {
            _clientFactory = clientFactory ?? throw new ArgumentNullException(nameof(clientFactory));
            _Context = dbContext;
        }

        public IActionResult Index()
        {
            var subscriberInfo = TempData["SubscriberInfo"] as string;
            ViewData["SubscriberInfo"] = subscriberInfo;

            var convertedPrice = TempData["ConvertedPrice"] as string;
            ViewData["ConvertedPrice"] = convertedPrice;

            var viewModel = new AdvertViewModel();
            viewModel.AdsList = _Context.tbl_ads.ToList();
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CheckSubscriber(string subscriptionNumber)
        {
            if (string.IsNullOrEmpty(subscriptionNumber))
            {
                TempData["SubscriberInfo"] = "Please enter a subscription number.";
                return RedirectToAction("Index");
            }

            var client = _clientFactory.CreateClient("SubscriberAPI");
            try
            {
                var response = await client.GetAsync(subscriptionNumber);

                if (response.IsSuccessStatusCode)
                {
                    var subscriberInfo = await response.Content.ReadAsStringAsync();

                    var subscriber = JsonConvert.DeserializeObject<Sub>(subscriberInfo);
                    var advertViewModel = new AdvertViewModel
                    {
                        Subscriber = subscriber,
                        AdsList = new List<Ads>()
                    };

                    return View("Index", advertViewModel);
                }
                else
                {
                    TempData["SubscriberInfo"] = "Can't find the subscription number, please try again!";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["SubscriberInfo"] = "Error occurred while processing the request";
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateSubscriber(Sub subscriber)
        {
            var client = _clientFactory.CreateClient("SubscriberAPI");

            try
            {
                var jsonContent = JsonConvert.SerializeObject(subscriber);
                var content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");

                var response = await client.PutAsync($"{subscriber.sub_id}", content);

                if (response.IsSuccessStatusCode)
                {
                    var ad = new Ads { ad_advertismentPrice = 0 };
                    return View("CreateAd", ad);
                }
                else
                {
                    TempData["SubscriberInfo"] = "Subscriber update failed.";
                }
            }
            catch (Exception ex)
            {
                TempData["SubscriberInfo"] = "Error occurred while processing the request.";
            }
            return RedirectToAction("Index");
        }


        [HttpPost]
        public IActionResult AdvertiserForm(Advertisers advertiser)
        {

            if (ModelState.IsValid)
            {
                var existingAdvertiser = _Context.tbl_advertisers.FirstOrDefault(a => a.adv_name == advertiser.adv_name);

                if (existingAdvertiser != null)
                {
                    ModelState.AddModelError("advertiser.adv_name", "An advertiser with this name already exists.");
                    ViewData["ErrorMessage"] = "An advertiser with this name already exists.";
                    return View("Advertise");
                }

                _Context.tbl_advertisers.Add(advertiser);
                _Context.SaveChanges();

                return RedirectToAction("CreateAd", new { advertiserId = advertiser.adv_id });
            }
            else
            {
                TempData["SubscriberInfo"] = "Can't find the subscription number, please try again!";
                return RedirectToAction("Index");
            }
        }

        public IActionResult CreateAd(int advertiserId)
        {
            var advertiser = _Context.tbl_advertisers.Find(advertiserId);
            var ad = new Ads { ad_advertisers_id = advertiserId, ad_Advertisers = advertiser, ad_advertismentPrice = 40 };
            return View(ad);
        }

        [HttpPost]
        public IActionResult CreateAd(Ads ad)
        {
            _Context.tbl_ads.Add(ad);
            _Context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Advertise()
        {
            return View();
        }


        public async Task<IActionResult> ConvertProductPrice(double originalPrice, string targetCurrency)
        {
            string SEK = "SEK";
            try
            {
                var requestUrl = $"https://api.freecurrencyapi.com/v1/latest?apikey={_apiKey}&currencies={SEK},{targetCurrency}";

                using var client = new HttpClient();

                var response = await client.GetAsync(requestUrl);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var jsonResponse = JObject.Parse(content);

                    var rates = jsonResponse["data"];

                    var originalRate = (double)rates[SEK];
                    var targetRate = (double)rates[targetCurrency];

                    var convertedPrice = originalPrice * (targetRate / originalRate);
                    var RoundedUp = Math.Round(convertedPrice, 2);

                    var OriginalPrice = $"{originalPrice} {SEK}";
                    var convertedResult = $"{RoundedUp} {targetCurrency}";

                    TempData["ConvertedPrice"] = $"{OriginalPrice} = {convertedResult}";
                    return RedirectToAction("Index");

                }
                else
                {
                    return StatusCode((int)response.StatusCode, "Failed to fetch exchange rates.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error occurred: {ex.Message}");
            }
        }

        public async Task<IActionResult> ExportSubscribers()
        {

            var client = _clientFactory.CreateClient("SubscriberAPI");
            var response = await client.GetAsync("export/subscribers");

            if (response.IsSuccessStatusCode)
            {
                var fileContent = await response.Content.ReadAsByteArrayAsync();
                return File(fileContent, "application/xml", "subscribers.xml");
            }
            else
            {
                return View("Error");
            }
        }


        public async Task<IActionResult> ImportSubscribers(IFormFile file)
        {
            var client = _clientFactory.CreateClient("SubscriberAPI");

            using var formData = new MultipartFormDataContent();
            formData.Headers.ContentType.MediaType = "multipart/form-data";

            var fileContent = new StreamContent(file.OpenReadStream());
            formData.Add(fileContent, "file", file.FileName);

            var response = await client.PostAsync("import/subscribers", formData);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View("Error");
            }
        }


        public IActionResult Import()
        {
            return View();
        }


    }
}


















