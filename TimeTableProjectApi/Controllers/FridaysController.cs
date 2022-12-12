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
    public class FridaysController : ApiController
    {
        private Entities db = new Entities();

        // GET: api/Fridays
        [ResponseType(typeof(List<FridayModel>))]
        public IHttpActionResult GetFriday()
        {
            return Ok(db.Friday.ToList().ConvertAll(x => new FridayModel(x)));
        }
        [Route("api/Fridays/SortedList")]

        [HttpGet]

        public async Task<IHttpActionResult> SortedList(bool f)
        {

            if (f)
            {
                return Ok(db.Friday.ToList().ConvertAll(x => new FridayModel(x)).OrderBy(x => x.Classroom));
            }
            else
            {
                return Ok(db.Friday.ToList().ConvertAll(x => new FridayModel(x)).OrderBy(x => x.Classroom).Reverse());
            }
        }
        // GET: api/Fridays/5
        [ResponseType(typeof(Friday))]
        public IHttpActionResult GetFriday(int id)
        {
            Friday friday = db.Friday.Find(id);
            if (friday == null)
            {
                return NotFound();
            }

            return Ok(friday);
        }

        // PUT: api/Fridays/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutFriday(int id, Friday friday)
        {
            var dbFriday = db.Friday.FirstOrDefault(x => x.Id_lesson.Equals(id));
            dbFriday.Lesson = friday.Lesson;
            dbFriday.Classroom = friday.Classroom;
            dbFriday.Teacher = friday.Teacher;
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FridayExists(id))
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

        // POST: api/Fridays
        [ResponseType(typeof(Friday))]
        public IHttpActionResult PostFriday(Friday friday)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Friday.Add(friday);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = friday.Id_lesson }, friday);
        }

        // DELETE: api/Fridays/5
        [ResponseType(typeof(Friday))]
        public IHttpActionResult DeleteFriday(int id)
        {
            Friday friday = db.Friday.Find(id);
            if (friday == null)
            {
                return NotFound();
            }

            db.Friday.Remove(friday);
            db.SaveChanges();

            return Ok(friday);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FridayExists(int id)
        {
            return db.Friday.Count(e => e.Id_lesson == id) > 0;
        }
    }
}