using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiDemo.Data;

public static class StateHelper
{
    public static IEnumerable<StateInfo> GetStates()
    {
        for(int i = 0; i < 10; i++)
        {
            yield return new StateInfo(i + 100, $"State-{i}");
        }
    }
}
