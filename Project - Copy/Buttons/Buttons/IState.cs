using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Buttons
{
    interface IState
    {
        void Update();
        void Draw();
    }
}
