using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Common
{
    internal class Helpers
    {
        internal static (List<T> removed, List<T> added) UpdatePreRequisite<T>(List<T> existing, List<T> updated) where T : notnull
        {
            var removed = existing
                .Where(e => !updated.Contains(e))
                .ToList();
            var added = updated
                .Where(u => !existing.Contains(u))
                .ToList();
            return (removed, added);
        }
    }
}
