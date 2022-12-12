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
    public class SaturdaysController : ApiController
    {
        private Entities db = new Entities();

        // GET: api/Saturdays
        [ResponseType(typeof(List<SaturdayModel>))]
        public IHttpActionResult GetSaturday()
        {
            return Ok(db.Saturday.ToList().ConvertAll(x => new SaturdayModel(x)));
        }
        [Route("api/Saturdays/SortedList")]

        [HttpGet]

        public async Task<IHttpActionResult> SortedList(bool f)
        {

            if (f)
            {
                return Ok(db.Saturday.ToList().ConvertAll(x => new SaturdayModel(x)).OrderBy(x => x.Classroom));
            }
            else
            {
                return Ok(db.Saturday.ToList().ConvertAll(x => new SaturdayModel(x)).OrderBy(x => x.Classroom).Reverse());
            }
        }
        // GET: api/Saturdays/5
        [ResponseType(typeof(Saturday))]
        public IHttpActionResult GetSaturday(int id)
        {
            Saturday saturday = db.Saturday.Find(id);
            if (saturday == null)
            {
                return NotFound();
            }

            return Ok(saturday);
        }

        // PUT: api/Saturdays/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSaturday(int id, Saturday saturday)
        {
            var dbSaturday= db.Saturday.FirstOrDefault(x => x.Id_lesson.Equals(id));
            dbSaturday.Lesson = saturday.Lesson;
            dbSaturday.Classroom = saturday.Classroom;
            dbSaturday.Teacher = saturday.Teacher;
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SaturdayExists(id))
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

        // POST: api/Saturdays
        [ResponseType(typeof(Saturday))]
        public IHttpActionResult PostSaturday(Saturday saturday)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Saturday.Add(saturday);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = saturday.Id_lesson }, saturday);
        }

        // DELETE: api/Saturdays/5
        [ResponseType(typeof(Saturday))]
        public IHttpActionResult DeleteSaturday(int id)
        {
            Saturday saturday = db.Saturday.Find(id);
            if (saturday == null)
            {
                return NotFound();
            }

            db.Saturday.Remove(saturday);
            db.SaveChanges();

            return Ok(saturday);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SaturdayExists(int id)
        {
            return db.Saturday.Count(e => e.Id_lesson == id) > 0;
        }
    }
}