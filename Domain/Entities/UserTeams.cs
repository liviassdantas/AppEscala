using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class UserTeam
    {
        public string UserId { get; set; }
        public User User { get; set; }

        public int TeamId { get; set; }
        public TeamsAndFunctions Teams { get; set; }
    }

}
