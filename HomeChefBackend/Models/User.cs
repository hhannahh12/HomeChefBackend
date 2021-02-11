using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeChefBackend.Models
{
    public class User
    {
        //Password should never be expose in the system or passed between front and backend after being validated
        //That is why password is not part of this object.
        public User(string id, string email)
        {
            Id = Guid.Parse(id);
            Email = email;
        }
        private Guid Id { get; }
        private String Email { get;  }
    }
}
