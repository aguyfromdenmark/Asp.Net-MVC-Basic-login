using LoginExercise.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LoginExercise.Context
{
    public class LoginContext : DbContext
    {
        public DbSet<UserModel> Users { get; set; }
        public DbSet<NoteModel> Notes { get; set; }

        public LoginContext()
            : base("ConnStringDb1") 
        {
        }

        public static LoginContext Create()
        {
            return new LoginContext();
        }
    }
}