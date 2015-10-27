using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Psub.DataService.Abstract
{
    public interface IDrawingService
    {
        string GetFile(int id, string name);
    }
}
