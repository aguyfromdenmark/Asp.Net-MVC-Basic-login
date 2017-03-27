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
using LoginExercise.Context;
using LoginExercise.Models;

namespace LoginExercise.Controllers
{
    public class NoteModelsController : ApiController
    {
        private LoginContext db = new LoginContext();

        // GET: api/NoteModels
        public IQueryable<NoteModel> GetNotes()
        {
            return db.Notes;
        }

        // GET: api/NoteModels/5
        [ResponseType(typeof(NoteModel))]
        public IHttpActionResult GetNoteModel(Guid id)
        {
            NoteModel noteModel = db.Notes.Find(id);
            if (noteModel == null)
            {
                return NotFound();
            }

            return Ok(noteModel);
        }

        // PUT: api/NoteModels/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutNoteModel(Guid id, NoteModel noteModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != noteModel.Id)
            {
                return BadRequest();
            }

            db.Entry(noteModel).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NoteModelExists(id))
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

        // POST: api/NoteModels
        [ResponseType(typeof(NoteModel))]
        public IHttpActionResult PostNoteModel(NoteModel noteModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Notes.Add(noteModel);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (NoteModelExists(noteModel.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = noteModel.Id }, noteModel);
        }

        // DELETE: api/NoteModels/5
        [ResponseType(typeof(NoteModel))]
        public IHttpActionResult DeleteNoteModel(Guid id)
        {
            NoteModel noteModel = db.Notes.Find(id);
            if (noteModel == null)
            {
                return NotFound();
            }

            db.Notes.Remove(noteModel);
            db.SaveChanges();

            return Ok(noteModel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool NoteModelExists(Guid id)
        {
            return db.Notes.Count(e => e.Id == id) > 0;
        }
    }
}