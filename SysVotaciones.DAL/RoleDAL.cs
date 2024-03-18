using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SysVotaciones.EN;

namespace SysVotaciones.DAL
{
    public class RoleDAL
    {

        public static async Task<Role> GetRole(int id)
        {
            Role? role = new();
            var dbContext = new ComunDB();
            role = await dbContext.Role.FirstOrDefaultAsync(r =>  r.Id == id);
            return role;
        }
    }
}
