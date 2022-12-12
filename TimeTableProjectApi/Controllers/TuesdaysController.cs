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
    public class TuesdaysController : ApiController
    {
        private Entities db = new Entities();

        // GET: api/Tuesdays
        [ResponseType(typeof(List<TuesdayModel>))]
        public IHttpActionResult GetTuesday()
        {
            return Ok(db.Tuesday.ToList().ConvertAll(x => new TuesdayModel(x)));
        }
        [Route("api/Tuesdays/SortedList")]

        [HttpGet]

        public async Task<IHttpActionResult> SortedList(bool f)
        {

            if (f)
            {
                return Ok(db.Tuesday.ToList().ConvertAll(x => new TuesdayModel(x)).OrderBy(x => x.Classroom));
            }
            else
            {
                return Ok(db.Tuesday.ToList().ConvertAll(x => new TuesdayModel(x)).OrderBy(x => x.Classroom).Reverse());
            }
        }
        // GET: api/Tuesdays/5
        [ResponseType(typeof(Tuesday))]
        public IHttpActionResult GetTuesday(int id)
        {
            Tuesday tuesday = db.Tuesday.Find(id);
            if (tuesday == null)
            {
                return NotFound();
            }

            return Ok(tuesday);
        }

        // PUT: api/Tuesdays/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTuesday(int id, Tuesday tuesday)
        {
            var dbTuesday = db.Tuesday.FirstOrDefault(x => x.Id_lesson.Equals(id));
            dbTuesday.Lesson = tuesday.Lesson;
            dbTuesday.Classroom = tuesday.Classroom;
            dbTuesday.Teacher = tuesday.Teacher;
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TuesdayExists(id))
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

        // POST: api/Tuesdays
        [ResponseType(typeof(Tuesday))]
        public IHttpActionResult PostTuesday(Tuesday tuesday)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Tuesday.Add(tuesday);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tuesday.Id_lesson }, tuesday);
        }

        // DELETE: api/Tuesdays/5
        [ResponseType(typeof(Tuesday))]
        public IHttpActionResult DeleteTuesday(int id)
        {
            Tuesday tuesday = db.Tuesday.Find(id);
            if (tuesday == null)
            {
                return NotFound();
            }

            db.Tuesday.Remove(tuesday);
            db.SaveChanges();

            return Ok(tuesday);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TuesdayExists(int id)
        {
            return db.Tuesday.Count(e => e.Id_lesson == id) > 0;
        }
    }
}