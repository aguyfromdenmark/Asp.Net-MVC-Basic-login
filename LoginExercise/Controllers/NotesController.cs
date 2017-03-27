using LoginExercise.Context;
using LoginExercise.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace LoginExercise.Controllers
{
    public class NotesController : ApiController
    {
        LoginContext db = new LoginContext();

        [HttpGet]
        public IQueryable<NoteModel> GetAllNotes()
        {
            return db.Notes;
        }

        [ResponseType(typeof(NoteModel))]
        public IHttpActionResult CreateNote(string noteContent)
        {
            var note = new NoteModel();
            note.Content = noteContent;
            db.Notes.Add(note);
            db.SaveChanges();
            return CreatedAtRoute("DefaultApi", new { id = note.Id }, note);
        }
    }
}
