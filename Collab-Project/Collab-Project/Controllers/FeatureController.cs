﻿using System;
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
    public class FeatureController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();
        static FeatureController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44376/api/");
        }

        // ------------------------------------------------- LIST FEATURES
        // GET: Feature/ListFeatures
        public ActionResult ListFeatures()
        {
            //objective: communicate with feature data api to retrieve a list of features
            //curl https://localhost:44376/api/featuredata/listfeatures

          
            string url = "featuredata/listfeatures";
            HttpResponseMessage response = client.GetAsync(url).Result;

            IEnumerable<FeatureDto> features = response.Content.ReadAsAsync<IEnumerable<FeatureDto>>().Result;


            return View(features);
        }





        //----------------------------------------- FEATURE DETAILS
        //GET: Feature/Details/5
        public ActionResult Details(int id)
        {

            FeatureDetails ViewModel = new FeatureDetails();
                //curl https://localhost:44376/api/featuredata/findfeature/{id}

                string url = "featuredata/findfeature/"+id;
                HttpResponseMessage response = client.GetAsync(url).Result;

                FeatureDto SelectedFeature = response.Content.ReadAsAsync<FeatureDto>().Result;

            ViewModel.SelectedFeature = SelectedFeature;

            // Show all trails associated with a particular hiking feature
            url = "traildata/listtrailfeatures/" + id;
            response = client.GetAsync(url).Result;
            IEnumerable<TrailDto> TrailFeatures = response.Content.ReadAsAsync<IEnumerable<TrailDto>>().Result;

            ViewModel.TrailFeatures = TrailFeatures;

            if (User.Identity.IsAuthenticated && User.IsInRole("Admin")) ViewModel.IsAdmin = true;
            else ViewModel.IsAdmin = false; //  this code is set to always be false for a guest user


            return View(ViewModel);
         }


        public ActionResult Error()
        {
            return View();
        }
     
        


        // -------------------------------------- CREATE FEATURE GET
        // GET: Feature/NewFeature
        public ActionResult NewFeature()
        {
            return View();
        }



        // ---------------------------------------- CREATE FEATURE POST
        // POST: Feature/Create
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(Feature feature)
        {
            string url = "featuredata/addfeature";
         
            string jsonpayload = jss.Serialize(feature);
            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";

            HttpResponseMessage response = client.PostAsync(url, content).Result;
            if(response.IsSuccessStatusCode)
            {
                return RedirectToAction("ListFeatures");
            }
            else
            {
                return RedirectToAction("Error");
            }
          
        }



        // ------------------------------------------EDIT FEATURE GET
        // GET: Feature/Edit/5
        public ActionResult Edit(int id)

        {
            string url = "featuredata/findfeature/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            FeatureDto SelectedFeature = response.Content.ReadAsAsync<FeatureDto>().Result;
            return View(SelectedFeature);
        }


        // ------------------------------------------ EDIT FEATURE
        // POST: Feature/UpdateFeature/5
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult UpdateFeature(int id, Feature Feature)
        {
            string url = "featuredata/UpdateFeature/" + id;
            string jsonpayload = jss.Serialize(Feature);
            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("ListFeatures");
            } else
            {
                return RedirectToAction("Error");
            }
            
        }



        // ------------------------------------------ DELETE FEATURE GET
        // GET: Feature/Delete/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "featuredata/findfeature/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            FeatureDto SelectedFeature = response.Content.ReadAsAsync<FeatureDto>().Result;
            return View(SelectedFeature);
        }



        // ----------------------------------------- DELETE FEATURE POST
        // POST: Feature/Delete/5
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id, Feature Feature)
        {
            string url = "featuredata/deletefeature/" + id;
            HttpContent content = new StringContent("");
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("ListFeatures");

            } else
            {
                return RedirectToAction("Error");
            }
        }
    }
}
