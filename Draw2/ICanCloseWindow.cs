using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Draw2
{
    internal interface ICanCloseWindow
    {
        event EventHandler RequestClose;
    }
}
