using System;
using System.Collections.Generic;
using System.Text;

namespace Do.Domain.AggregatesModel.CaseAggregate
{
    public abstract class Pay
    {
        public abstract string PayName { get; set; }
    }
}
