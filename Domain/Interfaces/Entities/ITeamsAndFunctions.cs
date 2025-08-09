using Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Entities
{
    public interface ITeamsAndFunctions
    {
        Team Teams { get; set; }
        Team_Function Functions { get; set; }
    }
}
