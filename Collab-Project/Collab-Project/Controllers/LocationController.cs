﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using Collab_Project.Models;
using Collab_Project.Models.ViewModels;
using System.Web.Script.Serialization;

namespace Collab_Project.Controllers
{
    public class LocationController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();
        static LocationController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44376/api/");
        }
        // ------------------------------- LIST FEATURES
        // GET: Location/ListLocations
        public ActionResult ListLocations()
        {
            //objective: communicate with feature data api to retrieve a list of features
            //curl https://localhost:44376/api/locationdata/listlocations

            string url = "locationdata/listlocations";
            HttpResponseMessage response = client.GetAsync(url).Result;

            IEnumerable<LocationDto> locations = response.Content.ReadAsAsync<IEnumerable<LocationDto>>().Result;


            return View(locations);
        }




        //---------------------------- LOCATION DETAILS
        //GET: Location/LocationDetails/5
        public ActionResult LocationDetails(int? id)
        {

            if (id == null)
            {
                // Handle the case where id is null
                // Return an appropriate response or redirect
                // For example:
                return RedirectToAction("Index");
            }

            LocationDetails ViewModel = new LocationDetails();
            string url = "locationdata/findlocation/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            LocationDto SelectedLocation = response.Content.ReadAsAsync<LocationDto>().Result;

            ViewModel.SelectedLocation = SelectedLocation;

            
            url = "traildata/ListTrailsForLocation/" + id;
            response = client.GetAsync(url).Result;
            IEnumerable<TrailDto> RelatedTrails = response.Content.ReadAsAsync<IEnumerable<TrailDto>>().Result;
           

            //Show info about trails related to a location
            ViewModel.RelatedTrails = RelatedTrails;

            if (User.Identity.IsAuthenticated && User.IsInRole("Admin")) ViewModel.IsAdmin = true;
            else ViewModel.IsAdmin = false; //  this code is set to always be false for a guest user


            return View(ViewModel);
        }


        // --------------------------------------- ADD LOCATION GET
        // GET: Location/Create
        public ActionResult New()
        {
            return View();
        }


        // ------------------------------------------- ADD FEATURE POST
        // POST: Location/Create
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(Location Location)
        {
            string url = "locationdata/addlocation";
            string jsonpayload = jss.Serialize(Location);
            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";
            
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            if(response.IsSuccessStatusCode)
            {
                return RedirectToAction("ListLocations");
            } else
            {
                return RedirectToAction("Error");
            }
        }


        // -------------------------------------- EDIT
        // GET: Feature/Edit/5
        public ActionResult Edit(int id)
        {
            string url = "locationdata/findlocation/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            LocationDto selectedLocation = response.Content.ReadAsAsync<LocationDto>().Result;
            return View(selectedLocation);
        }


        // ------------------------------------------ UPDATE LOCATION
        // POST: Location/Update/5
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Update(int id, Location Location)
        {
            string url = "locationdata/updatelocation/" + id;
            string jsonpayload = jss.Serialize(Location);
            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("ListLocations");
            } else
            {
                return RedirectToAction("Error");
            }
        }



        // --------------------------------------- DELETE LOCATION GET
        // GET: Location/Delete/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "locationdata/findlocation/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            LocationDto SelectedLocation = response.Content.ReadAsAsync<LocationDto>().Result;
            return View(SelectedLocation);
        }



        // ----------------------------------------- DELETE FEATURE POST
        // POST: Location/Delete/5
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id, FormCollection collection)
        {

            string url = "locationdata/deletelocation/" + id;
            HttpContent content = new StringContent("");
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            if(response.IsSuccessStatusCode)
            {
                return RedirectToAction("ListLocations");
            } else
            {
                return RedirectToAction("Error");
            }
        }
    }
}
