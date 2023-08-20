using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yaroo.BackgroundServices.BackgroundAction
{
    public interface IStatusCollector
    {
        IEnumerable<BackgroundActionStatus> CollectStatuses();
    }
}
