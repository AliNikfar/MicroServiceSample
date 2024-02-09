using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Eexceptions
{
    public class NotFoundException : ApplicationException
    {
        public NotFoundException(string name , Object key)
            : base($"Entity \"{name}\" and key {key} was not found !")
        {

        }
    }
}
