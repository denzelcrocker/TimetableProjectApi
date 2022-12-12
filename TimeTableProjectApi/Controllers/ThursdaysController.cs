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
    public class ThursdaysController : ApiController
    {
        private Entities db = new Entities();

        // GET: api/Thursdays
        [ResponseType(typeof(List<ThursdayModel>))]
        public IHttpActionResult GetThursday()
        {
            return Ok(db.Thursday.ToList().ConvertAll(x => new ThursdayModel(x)));
        }
        [Route("api/Thursdays/SortedList")]

        [HttpGet]

        public async Task<IHttpActionResult> SortedList(bool f)
        {

            if (f)
            {
                return Ok(db.Thursday.ToList().ConvertAll(x => new ThursdayModel(x)).OrderBy(x => x.Classroom));
            }
            else
            {
                return Ok(db.Thursday.ToList().ConvertAll(x => new ThursdayModel(x)).OrderBy(x => x.Classroom).Reverse());
            }
        }
        // GET: api/Thursdays/5
        [ResponseType(typeof(Thursday))]
        public IHttpActionResult GetThursday(int id)
        {
            Thursday thursday = db.Thursday.Find(id);
            if (thursday == null)
            {
                return NotFound();
            }

            return Ok(thursday);
        }

        // PUT: api/Thursdays/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutThursday(int id, Thursday thursday)
        {
            var dbThuesday = db.Thursday.FirstOrDefault(x => x.Id_lesson.Equals(id));
            dbThuesday.Lesson = thursday.Lesson;
            dbThuesday.Classroom = thursday.Classroom;
            dbThuesday.Teacher = thursday.Teacher;
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ThursdayExists(id))
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

        // POST: api/Thursdays
        [ResponseType(typeof(Thursday))]
        public IHttpActionResult PostThursday(Thursday thursday)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Thursday.Add(thursday);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = thursday.Id_lesson }, thursday);
        }

        // DELETE: api/Thursdays/5
        [ResponseType(typeof(Thursday))]
        public IHttpActionResult DeleteThursday(int id)
        {
            Thursday thursday = db.Thursday.Find(id);
            if (thursday == null)
            {
                return NotFound();
            }

            db.Thursday.Remove(thursday);
            db.SaveChanges();

            return Ok(thursday);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ThursdayExists(int id)
        {
            return db.Thursday.Count(e => e.Id_lesson == id) > 0;
        }
    }
}