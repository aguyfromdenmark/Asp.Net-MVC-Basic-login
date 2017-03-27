using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoginExercise.Models
{
    public class NoteModel
    {
        public Guid Id { get; set; }
        public string Content { get; set; }

        public NoteModel()
        {
            Id = Guid.NewGuid();
        }
    }
}