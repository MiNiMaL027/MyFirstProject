using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace List_Service.Interfaces
{
    internal interface IToDoListService<T> : IDefaultService<T> where T : class
    {
    }
}
