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
    public class WednesdaysController : ApiController
    {
        private Entities db = new Entities();

        // GET: api/Wednesdays
        [ResponseType(typeof(List<WednesdayModel>))]
        public IHttpActionResult GetWednesday()
        {
            return Ok(db.Wednesday.ToList().ConvertAll(x => new WednesdayModel(x)));
        }
        [Route("api/Wednesdays/SortedList")]

        [HttpGet]

        public async Task<IHttpActionResult> SortedList(bool f)
        {

            if (f)
            {
                return Ok(db.Wednesday.ToList().ConvertAll(x => new WednesdayModel(x)).OrderBy(x => x.Classroom));
            }
            else
            {
                return Ok(db.Wednesday.ToList().ConvertAll(x => new WednesdayModel(x)).OrderBy(x => x.Classroom).Reverse());
            }
        }
        // GET: api/Wednesdays/5
        [ResponseType(typeof(Wednesday))]
        public IHttpActionResult GetWednesday(int id)
        {
            Wednesday wednesday = db.Wednesday.Find(id);
            if (wednesday == null)
            {
                return NotFound();
            }

            return Ok(wednesday);
        }

        // PUT: api/Wednesdays/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutWednesday(int id, Wednesday wednesday)
        {
            var dbWednesday = db.Wednesday.FirstOrDefault(x => x.Id_lesson.Equals(id));
            dbWednesday.Lesson = wednesday.Lesson;
            dbWednesday.Classroom = wednesday.Classroom;
            dbWednesday.Teacher = wednesday.Teacher;
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WednesdayExists(id))
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

        // POST: api/Wednesdays
        [ResponseType(typeof(Wednesday))]
        public IHttpActionResult PostWednesday(Wednesday wednesday)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Wednesday.Add(wednesday);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = wednesday.Id_lesson }, wednesday);
        }

        // DELETE: api/Wednesdays/5
        [ResponseType(typeof(Wednesday))]
        public IHttpActionResult DeleteWednesday(int id)
        {
            Wednesday wednesday = db.Wednesday.Find(id);
            if (wednesday == null)
            {
                return NotFound();
            }

            db.Wednesday.Remove(wednesday);
            db.SaveChanges();

            return Ok(wednesday);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool WednesdayExists(int id)
        {
            return db.Wednesday.Count(e => e.Id_lesson == id) > 0;
        }
    }
}