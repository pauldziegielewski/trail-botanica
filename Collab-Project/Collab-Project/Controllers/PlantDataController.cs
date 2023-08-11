using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Collab_Project.Models;

namespace Collab_Project.Controllers
{
    public class PlantDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();






        /// <summary>
        /// Returns all plants in the system.
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: all plants in the database, including their associated trail.
        /// </returns>
        /// <example>
        // GET: api/PlantData/ListPlants
        [HttpGet]
        public IEnumerable<PlantDto> ListPlants()
        {
            List<Plant> Plants = db.Plants.ToList();
            List<PlantDto> PlantDtos = new List<PlantDto>();

            Plants.ForEach(a => PlantDtos.Add(new PlantDto()
            {
                Plant_Id =a.Plant_Id,
                Plant_Name = a.Plant_Name,
                Plant_Image = a.Plant_Image,
                Plant_Type = a.Plant_Type
            }));
            return PlantDtos;
        }

        // ----------------------LIST PLANTS FOR TRAIL

        // GET: api/PlantData/ListPlantsForTrail
        [HttpGet]
        [ResponseType(typeof(PlantDto))]
        public IHttpActionResult ListPlantsForTrail(int id)
        {
            List<Plant> Plants = db.Plants.Where(
                p => p.Trails.Any(
                    t => t.TrailID == id)
                ).ToList();

            List<PlantDto> PlantDtos = new List<PlantDto>();

            Plants.ForEach(p => PlantDtos.Add(new PlantDto()

            {
                Plant_Id = p.Plant_Id,
                Plant_Name = p.Plant_Name,
                Plant_Type = p.Plant_Type,
                Plant_Image = p.Plant_Image

            }));

            return Ok(PlantDtos);
        }
        /// GET: api/PlantData/ListPlantsNotInTrial/1
        /// </example>
        [HttpGet]
        [ResponseType(typeof(PlantDto))]
        public IHttpActionResult ListPlantsNotInTrial(int id)
        {
            List<Plant> Plants = db.Plants.Where(
                p => !p.Trails.Any(
                    a => a.TrailID == id)
                ).ToList();
            List<PlantDto> PlantDtos = new List<PlantDto>();

            Plants.ForEach(p => PlantDtos.Add(new PlantDto()
            {
                Plant_Id = p.Plant_Id,
                Plant_Name = p.Plant_Name,
                Plant_Image = p.Plant_Image,
                Plant_Type = p.Plant_Type
            }));

            return Ok(PlantDtos);
        }

        // GET: api/PlantData/FindPlant/5
        [ResponseType(typeof(Plant))]
        [HttpGet]
        public IHttpActionResult FindPlant(int id)
        {
            Plant plant = db.Plants.Find(id);
            if (plant == null)
            {
                return NotFound();
            }

            return Ok(plant);
        }

        // POST: api/PlantData/FindPlant/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult FindPlant(int id, Plant plant)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != plant.Plant_Id)
            {
                return BadRequest();
            }

            db.Entry(plant).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlantExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }




       
        // POST: api/UpdatePlant
        [ResponseType(typeof(Plant))]
        [HttpPost]
        public IHttpActionResult UpdatePlant(int id, Plant Plant)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if(id != Plant.Plant_Id)
            {
                return BadRequest();
            }
            db.Entry(Plant).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlantExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return StatusCode(HttpStatusCode.NoContent);
        }

        // DELETE: api/PlantData/5
        [ResponseType(typeof(Plant))]
        public IHttpActionResult DeletePlant(int id)
        {
            Plant plant = db.Plants.Find(id);
            if (plant == null)
            {
                return NotFound();
            }

            db.Plants.Remove(plant);
            db.SaveChanges();

            return Ok(plant);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PlantExists(int id)
        {
            return db.Plants.Count(e => e.Plant_Id == id) > 0;
        }

        //---------ADD PLANT
        /// <summary>
        /// Adds an Plant to the system
        /// </summary>
        /// <param name="Plant">JSON FORM DATA of an Plant</param>
        /// <returns>
        /// HEADER: 201 (Created)
        /// CONTENT: Plant_Id, Plant Data
        /// or
        /// HEADER: 400 (Bad Request)
        /// </returns>
        /// <example>
        /// POST: api/PlantData/AddPlant
        /// FORM DATA: Plant JSON Object
        /// </example>
        /// 
        [ResponseType(typeof(Plant))]
        [HttpPost]
        public IHttpActionResult AddPlant(Plant Plant)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Plants.Add(Plant);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = Plant.Plant_Id }, Plant);
        }
    }
}