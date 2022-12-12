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
    public class Actual_timetableController : ApiController
    {
        private Entities db = new Entities();

        // GET: api/Actual_timetable
        [ResponseType(typeof(List<ActualModel>))]
        public IHttpActionResult GetActual_timetable()
        {
            return Ok(db.Actual_timetable.ToList().ConvertAll(x => new ActualModel(x)));
        }
        [Route("api/Actual_timetable/SortedList")]

        [HttpGet]

        public async Task<IHttpActionResult> SortedList(bool f)
        {

            if (f)
            {
                return Ok(db.Actual_timetable.ToList().ConvertAll(x => new ActualModel(x)).OrderBy(x => x.Classroom));
            }
            else
            {
                return Ok(db.Actual_timetable.ToList().ConvertAll(x => new ActualModel(x)).OrderBy(x => x.Classroom).Reverse());
            }
        }
        // GET: api/Actual_timetable/5
        [ResponseType(typeof(Actual_timetable))]
        public IHttpActionResult GetActual_timetable(int id)
        {
            Actual_timetable actual_timetable = db.Actual_timetable.Find(id);
            if (actual_timetable == null)
            {
                return NotFound();
            }

            return Ok(actual_timetable);
        }

        // PUT: api/Actual_timetable/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutActual_timetable(int id, Actual_timetable actual_timetable)
        {
            var dbActual = db.Actual_timetable.FirstOrDefault(x => x.Id_lesson.Equals(id));
            dbActual.Lesson = actual_timetable.Lesson;
            dbActual.Classroom = actual_timetable.Classroom;
            dbActual.Subgroup = actual_timetable.Subgroup;
            dbActual.Count = actual_timetable.Count;
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Actual_timetableExists(id))
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

        // POST: api/Actual_timetable
        [ResponseType(typeof(Actual_timetable))]
        public IHttpActionResult PostActual_timetable(Actual_timetable actual_timetable)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Actual_timetable.Add(actual_timetable);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = actual_timetable.Id_lesson }, actual_timetable);
        }

        // DELETE: api/Actual_timetable/5
        [ResponseType(typeof(Actual_timetable))]
        public IHttpActionResult DeleteActual_timetable(int id)
        {
            Actual_timetable actual_timetable = db.Actual_timetable.Find(id);
            if (actual_timetable == null)
            {
                return NotFound();
            }

            db.Actual_timetable.Remove(actual_timetable);
            db.SaveChanges();

            return Ok(actual_timetable);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Actual_timetableExists(int id)
        {
            return db.Actual_timetable.Count(e => e.Id_lesson == id) > 0;
        }
    }
}