using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class PagedResult<T>
    {
    public List<T> Items { get; set; } = new List<T>();
    public int TotalCount { get; set; }
    }
}