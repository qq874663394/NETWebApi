using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Domain.Model
{
    public class JwtToken
    {
        public Guid? UserId { get; set; }
    }
}
