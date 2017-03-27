using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoginExercise.Models
{
    public class UserModel
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int LoginTries { get; set; }
        public DateTime LoginTimeout { get; set; }

        public UserModel()
        {
            LoginTries = 0;
            LoginTimeout = DateTime.Now;
        }

    }
}