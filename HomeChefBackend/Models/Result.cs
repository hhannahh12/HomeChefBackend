using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace HomeChefBackend.Models
{
    public class Result
    {
        public enum LoginResult
        {
            success, failure, noaccount
        }
        public LoginResult result { get; set; }

        public User? user { get; set; }
    }
}
