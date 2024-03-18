using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SysVotaciones.DAL;
using SysVotaciones.EN;

namespace SysVotaciones.BLL
{
    public class RoleBLL
    {
        public static async Task<Role> GetRole(int RoleId) => await RoleDAL.GetRole(RoleId);
    }
}
