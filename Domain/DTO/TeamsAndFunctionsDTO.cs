using Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTO
{
    public class TeamsAndFunctionsDTO
    {
        public TeamsAndFunctionsDTO() { }
        public Team Teams { get; set; }
        public Team_Function Functions { get; set; }
    }
}
