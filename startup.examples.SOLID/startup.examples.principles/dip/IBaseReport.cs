using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace startup.examples.principles.dip
{
    public interface IBaseReport
    {
        byte[] GenerarReporte();
    }
}