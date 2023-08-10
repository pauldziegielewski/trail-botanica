using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Collab_Project.Models;
using Collab_Project.Models.ViewModels;

namespace Collab_Project.Controllers
{
    public class DesignDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        // ------------------------------ LIST DESIGNS

        /// <summary>
        /// Returns all Designs in the system.
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: all Designs in the database, including their associated hiking trails.
        /// </returns>
        /// <example>
        /// GET: api/DesignData/ListDesigns
        /// </example>

        [HttpGet]
        [ResponseType(typeof(DesignDto))]
        public IHttpActionResult ListDesigns()
        {
            List<Design> designs = db.Designs.ToList();
            List<DesignDto> designDtos = new List<DesignDto>();

            foreach (Design design in designs)
            {
                DesignDto designDto = new DesignDto()
                {
                    Design_Id = design.Design_Id,
                    Architect_Name = design.Architect_Name,
                    Description = design.Description ,
                    Drawing_Plan = design.Drawing_Plan,
                  //  TrailName = design.TrailID,
                };

                designDtos.Add(designDto);
            }

            return Ok(designDtos);
        }



        // ------------------------------------------ FIND DESIGN
        /// <summary>
        /// Returns a particular design for a design_Id
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: A Design in the database matching a particular ID
        /// HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <param name="id">The primary key of a design</param>
        /// <example>
        /// GET: api/DesignData/FindDesign/9
        /// </example>

        [HttpGet]
        [ResponseType(typeof(DesignDto))]
        public IHttpActionResult FindDesign(int id)
        {
            Design design = db.Designs.Find(id);
            DesignDto DesignDto = new DesignDto()
            {
                Design_Id = design.Design_Id,
                Architect_Name = design.Architect_Name,
                Description = design.Description,
                Drawing_Plan = design.Drawing_Plan,
            };
            if (design == null)
            {
                return NotFound();
            }

            return Ok(DesignDto);
        }

        // ------------------------------------------ UPDATE DESIGN
        /// <summary>
        /// Updates a design in the database with Post data input
        /// </summary>
        /// <param name="id">Represents the Design_Id primary key</param>
        /// <param name="Design">JSON FORM DATA of an design</param>
        /// <returns>
        /// HEADER: 204 (Success, No Content Response)
        /// or
        /// HEADER: 400 (Bad Request)
        /// or
        /// HEADER: 404 (Not Found)
        /// </returns>
        /// <example>
        /// POST: api/DesignData/UpdateDesign/10
        /// FORM DATA: Design JSON Object
        /// </example>
        // PUT: api/DesignData/UpdateDesign/10
        [HttpPost]
        [ResponseType(typeof(void))]
        public IHttpActionResult UpdateDesign(int id, Design design)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != design.Design_Id)
            {
                return BadRequest();
            }

            db.Entry(design).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DesignExists(id))
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


        // -------------------------------------------- ADD DESIGN
        // POST: api/DesignData/AddDesign
        /// <summary>
        /// Adds a design to the system
        /// </summary>
        /// <param name="Design">JSON FORM DATA of an Design</param>
        /// <returns>
        /// HEADER: 201 (Created)
        /// CONTENT: Design_Id, Design Data
        /// or
        /// HEADER: 400 (Bad Request)
        /// </returns>
        /// <example>
        /// POST: api/DesignData/AddDesign
        /// FORM DATA: Design JSON Object
        /// </example>
        [HttpPost]
        [ResponseType(typeof(Design))]
        public IHttpActionResult AddDesign(Design design)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Designs.Add(design);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = design.Design_Id }, design);
        }


        // --------------------------------------------- DELETE DESIGN
        /// <summary>
        /// Deletes a design from database by its ID
        /// </summary>
        /// <param name="id">The primary key of the Design</param>
        /// <returns>
        /// HEADER: 200 (OK)
        /// or
        /// HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <example>
        /// POST: api/DesignData/DeleteDesign/5
        /// FORM DATA: (empty)
        /// </example>
        // DELETE: api/DesignData/DeleteDesign/5
        [HttpPost]
        [ResponseType(typeof(Design))]
        public IHttpActionResult DeleteDesign(int id)
        {
            Design design = db.Designs.Find(id);
            if (design == null)
            {
                return NotFound();
            }

            db.Designs.Remove(design);
            db.SaveChanges();

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DesignExists(int id)
        {
            return db.Designs.Any(e => e.Design_Id == id);
        }
    }
}
