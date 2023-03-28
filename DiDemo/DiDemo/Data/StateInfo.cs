using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiDemo.Data;

public class StateInfo
{
    public int Id { get; init; }
    public string Name { get; init; }
    public string Description { get; set; }

    public StateInfo(int id, string name)
    {
        Id = id;
        Name = name;
        Description = name;
    }
}
