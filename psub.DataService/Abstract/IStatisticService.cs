using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Psub.DataService.Abstract
{
    public interface IStatisticService
    {
        int Save(string url, string urlReferrer);
    }
}
