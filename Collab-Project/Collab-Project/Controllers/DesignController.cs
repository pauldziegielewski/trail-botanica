using System;
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
    public class DesignController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();
        static DesignController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44376/api/");
        }
        // ------------------------------- LIST DESIGNS
        // GET: Design/ListDesigns
        public ActionResult ListDesigns()
        {
            //objective: communicate with feature data api to retrieve a list of features
            //curl https://localhost:44376/api/locationdata/listlocations

            string url = "designdata/listdesigns";
            HttpResponseMessage response = client.GetAsync(url).Result;

            IEnumerable<DesignDto> designs = response.Content.ReadAsAsync<IEnumerable<DesignDto>>().Result;


            return View(designs);
        }




        //---------------------------- DESIGN DETAILS
        //GET: Design/DesignDetails/5
        public ActionResult DesignDetails(int? id)
        {

            if (id == null)
            {
                // Handle the case where id is null
                // Return an appropriate response or redirect
                // For example:
                return RedirectToAction("Index");
            }

            DesignDetails ViewModel = new DesignDetails();
            string url = "Designdata/findDesign/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            DesignDto SelectedDesign = response.Content.ReadAsAsync<DesignDto>().Result;

            ViewModel.SelectedDesign = SelectedDesign;

            
          //  url = "traildata/ListTrailsForDesign/" + id;
          //  response = client.GetAsync(url).Result;
          //  IEnumerable<TrailDto> RelatedTrails = response.Content.ReadAsAsync<IEnumerable<TrailDto>>().Result;
           

            //Show info about trails related to a design
          //  ViewModel.RelatedTrails = RelatedTrails;


            return View(ViewModel);
        }


        // --------------------------------------- ADD DESIGN GET
        // GET: Design/Create
        public ActionResult New()
        {
            return View();
        }


        // ------------------------------------------- ADD DESIGN POST
        // POST:Design/Create
        [HttpPost]
        public ActionResult Create(Design Design)
        {
            string url = "Designdata/addDesign";
            string jsonpayload = jss.Serialize(Design);
            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";
            
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            if(response.IsSuccessStatusCode)
            {
                return RedirectToAction("ListDesigns");
            } else
            {
                return RedirectToAction("Error");
            }
        }


        // -------------------------------------- EDIT
        // GET: Design/Edit/5
        public ActionResult Edit(int id)
        {
            string url = "Designdata/findDesign/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            DesignDto selectedDesign = response.Content.ReadAsAsync<DesignDto>().Result;
            return View(selectedDesign);
        }


        // ------------------------------------------ UPDATE DESIGN
        // POST: Design/Update/5
        [HttpPost]
        public ActionResult Update(int id, Design Design)
        {
            string url = "Designdata/updateDesign/" + id;
            string jsonpayload = jss.Serialize(Design);
            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("ListDesigns");
            } else
            {
                return RedirectToAction("Error");
            }
        }



        // --------------------------------------- DELETE DESIGN GET
        // GET: Design/Delete/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "Designdata/findDesign/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            DesignDto SelectedDesign = response.Content.ReadAsAsync<DesignDto>().Result;
            return View(SelectedDesign);
        }



        // ----------------------------------------- DELETE DESIGN POST
        // POST: Design/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {

            string url = "Designdata/deleteDesign/" + id;
            HttpContent content = new StringContent("");
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            if(response.IsSuccessStatusCode)
            {
                return RedirectToAction("ListDesign");
            } else
            {
                return RedirectToAction("Error");
            }
        }
    }
}
