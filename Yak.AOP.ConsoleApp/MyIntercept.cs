using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yak.AOP.ConsoleApp
{
    public class MyIntercept : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            //执行原有方法之前
            Console.WriteLine("增加用户前执行业务");

            //执行原有方法
            invocation.Proceed();

            //执行原有方法之后
            Console.WriteLine("增加用户后执行业务");
        }
    }
}
