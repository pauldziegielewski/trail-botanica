using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using Collab_Project.Models;
using System.Web.Script.Serialization;
using Collab_Project.Models.ViewModel;

namespace Collab_Project.Controllers
{
    public class PlantController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();
        static PlantController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44376/api/");
        }

        // ------------------------------------------------- LIST Plants
        // GET: Plant/ListPlants
        public ActionResult ListPlants()
        {
            //objective: communicate with Plant data api to retrieve a list of Plants
            //curl https://localhost:44376/api/plantdata/listplants


            string url = "PlantData/ListPlants";
            HttpResponseMessage response = client.GetAsync(url).Result;

            IEnumerable<PlantDto> plants = response.Content.ReadAsAsync<IEnumerable<PlantDto>>().Result;


            return View(plants);
        }





        //----------------------------------------- PLANTS DETAILS
        //GET: Plant/Details/5
        public ActionResult Details(int id)
        {

            PlantDetails ViewModel = new PlantDetails();
            //curl https://localhost:44376/api/plantdata/findplant/{id}

            if (User.Identity.IsAuthenticated && User.IsInRole("Admin")) ViewModel.IsAdmin = true;
            else ViewModel.IsAdmin = false; //  this code is set to always be false for a guest user


            string url = "plantdata/findplant/" + id;
                HttpResponseMessage response = client.GetAsync(url).Result;

            PlantDto SelectedPlant = response.Content.ReadAsAsync<PlantDto>().Result;

            ViewModel.SelectedPlant = SelectedPlant;

            // Show all trails associated with a particular plant
            url = "traildata/listtrailplants/" + id;
            response = client.GetAsync(url).Result;
            IEnumerable<TrailDto> TrailPlants = response.Content.ReadAsAsync<IEnumerable<TrailDto>>().Result;

            ViewModel.TrailPlants = TrailPlants;

                return View(ViewModel);
         }


        public ActionResult Error()
        {
            return View();
        }




        // -------------------------------------- CREATE PLANT GET
        // GET: Plant/NewPlant
        public ActionResult NewPlant()
        {
            return View();
        }



        // ---------------------------------------- CREATE PLANT POST
        // POST: Plant/Create
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(Plant plant)
        {
            string url = "plantdata/addplant";
         
            string jsonpayload = jss.Serialize(plant);
            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";

            HttpResponseMessage response = client.PostAsync(url, content).Result;
            if(response.IsSuccessStatusCode)
            {
                return RedirectToAction("ListPlants");
            }
            else
            {
                return RedirectToAction("Error");
            }
          
        }



        // ------------------------------------------EDIT FEATURE GET
        // GET: Plant/Edit/5
        public ActionResult Edit(int id)

        {
            string url = "plantdata/findplant/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            PlantDto SelectedPlant = response.Content.ReadAsAsync<PlantDto>().Result;
            return View(SelectedPlant);
        }


        // ------------------------------------------ EDIT FEATURE
        // POST: Plant/UpdatePlant/5
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult UpdatePlant(int id, Plant Plant)
        {
            string url = "plantdata/UpdatePlant/" + id;
            string jsonpayload = jss.Serialize(Plant);
            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("ListPlants");
            } else
            {
                return RedirectToAction("Error");
            }
            
        }



        // ------------------------------------------ DELETE PLANT GET
        // GET: Plant/Delete/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "plantdata/FindPlant/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            PlantDto SelectedPlant = response.Content.ReadAsAsync<PlantDto>().Result;
            return View(SelectedPlant);
        }



        // ----------------------------------------- DELETE PLANT POST
        // POST: Plant/Delete/5
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id, Plant Plant)
        {
            string url = "Plantdata/DeletePlant/" + id;
            HttpContent content = new StringContent("");
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("ListPlants");

            } else
            {
                return RedirectToAction("Error");
            }
        }
    }
}
