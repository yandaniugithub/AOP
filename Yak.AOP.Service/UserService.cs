using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yak.AOP.Model;

namespace Yak.AOP.Service
{
    public class UserService : IUserService
    {
        public bool AddUser(User user)
        {
            Console.WriteLine("用户添加成功");
            return true;
        }
    }
}
