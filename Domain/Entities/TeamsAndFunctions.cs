using Core.Enums;
using Core.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class TeamsAndFunctions : ITeamsAndFunctions
    {
        public TeamsAndFunctions()
        {
            Teams = new Team();
            Functions = new Team_Function();
        }
        public int Id { get; set; }
        public Team Teams { get; set; }
        public Team_Function Functions { get; set; }
        public ICollection<UserTeam> UserTeams { get; set; }
    } 
}
