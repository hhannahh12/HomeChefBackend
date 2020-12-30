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
        private Guid Id { get; }
        private String Email { get; }
        private String Password { get; }
    }
}
