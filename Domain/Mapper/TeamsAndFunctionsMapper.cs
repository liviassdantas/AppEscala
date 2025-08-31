using Core.DTO;
using Core.Entities;
using Core.Enums;
using Core.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Mapper
{
    public static class TeamsAndFunctionsMapper
    {
        public static ITeamsAndFunctions ToDomain(TeamsAndFunctionsDTO dto)
        {
            return new TeamsAndFunctions
            {
                Teams = dto.Teams,
                Functions = dto.Functions
            };
        }
        public static IList<ITeamsAndFunctions> ToDomainList(IList<TeamsAndFunctionsDTO> dtoList)
        {
            if (dtoList == null || dtoList.Count == 0)
                return [];

            return [.. dtoList.Select(dto => ToDomain(dto))];
        }
    }

}
