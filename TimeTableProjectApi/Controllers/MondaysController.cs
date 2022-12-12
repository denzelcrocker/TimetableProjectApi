using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using TimeTableProjectApi.Models;

namespace TimeTableProjectApi.Controllers
{
    public class MondaysController : ApiController
    {
        private Entities db = new Entities();

        // GET: api/Mondays
        [ResponseType(typeof(List<MondayModel>))]
        public IHttpActionResult GetMonday()
        {
            return Ok(db.Monday.ToList().ConvertAll(x => new MondayModel(x)));
        }
        [Route("api/Mondays/SortedList")]

        [HttpGet]

        public async Task<IHttpActionResult> SortedList(bool f)
        {

            if (f)
            {
                return Ok(db.Monday.ToList().ConvertAll(x => new MondayModel(x)).OrderBy(x => x.Classroom));
            }
            else
            {
                return Ok(db.Monday.ToList().ConvertAll(x => new MondayModel(x)).OrderBy(x => x.Classroom).Reverse());
            }
        }
        // GET: api/Mondays/5
        [ResponseType(typeof(Monday))]
        public IHttpActionResult GetMonday(int id)
        {
            Monday monday = db.Monday.Find(id);
            if (monday == null)
            {
                return NotFound();
            }

            return Ok(monday);
        }

        // PUT: api/Mondays/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMonday(int id, Monday monday)
        {
            var dbMonday = db.Monday.FirstOrDefault(x => x.Id_lesson.Equals(id));
            dbMonday.Lesson = monday.Lesson;
            dbMonday.Classroom = monday.Classroom;
            dbMonday.Teacher = monday.Teacher;
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MondayExists(id))
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

        // POST: api/Mondays
        [ResponseType(typeof(Monday))]
        public IHttpActionResult PostMonday(Monday monday)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Monday.Add(monday);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = monday.Id_lesson }, monday);
        }

        // DELETE: api/Mondays/5
        [ResponseType(typeof(Monday))]
        public IHttpActionResult DeleteMonday(int id)
        {
            Monday monday = db.Monday.Find(id);
            if (monday == null)
            {
                return NotFound();
            }

            db.Monday.Remove(monday);
            db.SaveChanges();

            return Ok(monday);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MondayExists(int id)
        {
            return db.Monday.Count(e => e.Id_lesson == id) > 0;
        }
    }
}