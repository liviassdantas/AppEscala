using Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ValueObjects
{
    public class TeamsAndFunctions
    {
        public TeamsAndFunctions()
        {
            Teams = new Team();
            Functions = new Team_Function();
        }
        public Team Teams { get; set; }
        public Team_Function Functions { get; set; }
    } 
}
