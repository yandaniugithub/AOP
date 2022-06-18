using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yak.AOP.Model;

namespace Yak.AOP.Service
{
    public interface IUserService
    {
        bool AddUser(User user);
    }
}
