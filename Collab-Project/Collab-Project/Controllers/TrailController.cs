using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Diagnostics;
using Collab_Project.Models;
using Collab_Project.Models.ViewModels;
using System.Web.Script.Serialization;


namespace Collab_Project.Controllers
{
   
    public class TrailController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();
        static TrailController()
        {
            HttpClientHandler handler = new HttpClientHandler()
            {
                AllowAutoRedirect = false,
                UseCookies = false
            };
          

        //https://localhost:44376/
            client = new HttpClient(handler);
            client.BaseAddress = new Uri("https://localhost:44376/api/");
        }

        // ******************************************

        /// <summary>
        /// Grabs the authentication cookie sent to this controller.
        /// For proper WebAPI authentication, you can send a post request with login credentials to the WebAPI and log the access token from the response. The controller already knows this token, so we're just passing it up the chain.
        /// 
        /// Here is a descriptive article which walks through the process of setting up authorization/authentication directly.
        /// https://docs.microsoft.com/en-us/aspnet/web-api/overview/security/individual-accounts-in-web-api
        /// </summary>
        private void GetApplicationCookie()
        {
            string token = "";
            //HTTP client is set up to be reused, otherwise it will exhaust server resources.
            //This is a bit dangerous because a previously authenticated cookie could be cached for
            //a follow-up request from someone else. Reset cookies in HTTP client before grabbing a new one.
            client.DefaultRequestHeaders.Remove("Cookie");
            if (!User.Identity.IsAuthenticated) return;

            HttpCookie cookie = System.Web.HttpContext.Current.Request.Cookies.Get(".AspNet.ApplicationCookie");
            if (cookie != null) token = cookie.Value;

            //collect token as it is submitted to the controller
            //use it to pass along to the WebAPI.
            Debug.WriteLine("Token Submitted is : " + token);
            if (token != "") client.DefaultRequestHeaders.Add("Cookie", ".AspNet.ApplicationCookie=" + token);

            return;
        }


            // ------------------------ TRAIL LIST
            // GET: Trail/List
            public ActionResult List()
        {
            //objective: communicate with our animal data api to retrieve a list of trails
            //curl https://localhost:44376/api/traildata/listtrails

            //client is anything that's accessing any information from server
            // HttpClient client = new HttpClient() { }; ==> this line of code was in use before (client = new HttpClient();) was added on line 17
            string url = "traildata/listtrails";
            HttpResponseMessage response = client.GetAsync(url).Result;

            //Debug.WriteLine(response.StatusCode);
            //Debug.WriteLine("The response code is");

            //using Collab_Project.Models; is needed for <TrailDto> to link up with this TrailController.cs
            IEnumerable<TrailDto> trails = response.Content.ReadAsAsync<IEnumerable<TrailDto>>().Result;

            //Debug.WriteLine("Number of trails received: ");
            //Debug.WriteLine(trails.Count());

            return View(trails);
        }


        // ------------------------------ TRAIL DETAILS
        // GET: Trail/Show/5
        public ActionResult Show(int id)
        {
            TrailDetails ViewModel = new TrailDetails();

            if (User.Identity.IsAuthenticated && User.IsInRole("Admin")) ViewModel.IsAdmin = true;
            else ViewModel.IsAdmin = false; //  this code is set to always be false for a guest user

            //objective: communicate with our animal data api to retrieve one trail by ID
            //curl https://localhost:44376/api/traildata/findtrail/{id}

            //client is anything that's accessing any information from server
            //HttpClient client = new HttpClient() { };
            // url here is the end point for the full url string
            string url = "traildata/findtrail/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            //using Collab_Project.Models; is needed for <TrailDto> to link up with this TrailController.cs
            TrailDto SelectedTrail = response.Content.ReadAsAsync<TrailDto>().Result;

            ViewModel.SelectedTrail = SelectedTrail;



            url = "featuredata/listfeaturesfortrail/" + id;
            response = client.GetAsync(url).Result;

            IEnumerable<FeatureDto> AvailableFeatures = response.Content.ReadAsAsync<IEnumerable<FeatureDto>>().Result;

      ViewModel.AvailableFeatures = AvailableFeatures;


            url = "FeatureData/ListFeaturesNotInTrail/" + id;

            response = client.GetAsync(url).Result;
            IEnumerable<FeatureDto> AFeatures = response.Content.ReadAsAsync<IEnumerable<FeatureDto>>().Result;

            ViewModel.AFeatures = AFeatures;


            //Lst of plants in that trial 
            url = "plantdata/ListPlantsForTrail/" + id;
            response = client.GetAsync(url).Result;

            IEnumerable<PlantDto> AvailablePlants = response.Content.ReadAsAsync<IEnumerable<PlantDto>>().Result;

            ViewModel.AvailablePlants = AvailablePlants;

          //  url = "PlantData/ListPlantsNotInTrial" + id;

          //  response = client.GetAsync(url).Result;
          //  IEnumerable<PlantDto> APlants = response.Content.ReadAsAsync<IEnumerable<PlantDto>>().Result;

          //  ViewModel.APlants = APlants;


            return View(ViewModel);
        }




        // ---------------------------------- ERROR PAGE
        public ActionResult Error()
        {
            return View();
        }




        // -------------------------- NEW TRAIL GET
        // GET: Trail/NewTrail
        public ActionResult NewTrail()
        {
            //Information about all locations in the system
            //GET api/locationdata/listlocations
            string url = "locationdata/listlocations";
            HttpResponseMessage response = client.GetAsync(url).Result;
            IEnumerable<LocationDto> LocationOptions = response.Content.ReadAsAsync<IEnumerable<LocationDto>>().Result;
            return View(LocationOptions);
        }


        // -----------------------PostNewTrail
        // POST: Trail/PostNewTrail
        [HttpPost]
        [Authorize(Roles = "Admin")]

        public ActionResult PostNewTrail(Trail trail)
        {
            Debug.WriteLine("The json payload is: ");
            Debug.WriteLine(trail.TrailName);
            //objective: add a new trail into our system using API

            //curl -d @animal.json -H "Content-Type:application/json" https://localhost:44376/api/traildata/addtrail
            string url = "traildata/addtrail";

            JavaScriptSerializer jss = new JavaScriptSerializer();
            
            string JsonPayload = jss.Serialize(trail);

            Debug.WriteLine(JsonPayload);

            HttpContent content = new StringContent(JsonPayload);
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            } else
            {
                return RedirectToAction("ERROR");
            }

            //HttpContent content = new StringContent(jsonpayload);
            //content.Headers.ContentType.MediaType = "application/json";

            //return RedirectToAction("List");
        }


        // ----------------------------------- EDIT
        // GET: Trail/Edit/5
        public ActionResult Edit(int id)
        {

            UpdateTrail ViewModel = new UpdateTrail();

          // The existing trail information
            string url = "traildata/findtrail/"+ id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            TrailDto SelectedTrail = response.Content.ReadAsAsync<TrailDto>().Result;
            ViewModel.SelectedTrail = SelectedTrail;

            url = "locationdata/listlocations/";
            response = client.GetAsync(url).Result;
            IEnumerable<LocationDto> LocationOptions = response.Content.ReadAsAsync<IEnumerable<LocationDto>>().Result;

            ViewModel.LocationOptions = LocationOptions;

            // FEATURE TO BE INCLUDED HERE?

            return View(ViewModel);
        }


        //---------------------------------- UPDATE
        // POST: Trail/Update/5
        [HttpPost]
        [Authorize(Roles = "Admin")]

        public ActionResult Update(int id, Trail Trail, HttpPostedFileBase TrailPic)
        {
            GetApplicationCookie();
            string url = "traildata/updatetrail/" + id;
            string jsonPayload = jss.Serialize(Trail);
            HttpContent content = new StringContent(jsonPayload);
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;
            
            if (response.IsSuccessStatusCode && TrailPic != null )
            {
                url = "TrailData/UploadTrailPic/" + id;

                MultipartFormDataContent requestcontent = new MultipartFormDataContent();
                HttpContent imagecontent = new StreamContent(TrailPic.InputStream);
                requestcontent.Add(imagecontent, "TrailPic", TrailPic.FileName);
                response = client.PostAsync(url, requestcontent).Result;
                return RedirectToAction("Show/"+id);
            } 
            
            else if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Show/"+id);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }



        //--------------------DELETE CONFIRM TRAIL
        // GET: Trail/Delete/5
   
        public ActionResult DeleteConfirm(int id)
        {
            string url = "traildata/findtrail/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            TrailDto selectedTrail = response.Content.ReadAsAsync<TrailDto>().Result;

            return View(selectedTrail);
        }

        // POST: Trail/Delete/5
        [HttpPost]
        [Authorize(Roles = "Admin")]

        public ActionResult Delete(int id)
        {
            string url = "traildata/deletetrail/" + id;
            HttpContent content = new StringContent("");
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }





    }
}
