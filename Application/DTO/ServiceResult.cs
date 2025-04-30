using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public class ServiceResult
    {
            public bool Success { get; set; }
            public string Message { get; set; }
            public User User { get; set; }
    }
}
