using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuartzNetDemo1.Model
{
    public interface IMigrationRepository
    {
        bool Migration();
    }
}
