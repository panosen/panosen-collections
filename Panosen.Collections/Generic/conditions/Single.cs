using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panosen.Collections.Generic
{
    /// <summary>
    /// SimpleCondition
    /// </summary>
    public class Single<TValue> : Condition<TValue>
    {
        /// <summary>
        /// Value
        /// </summary>
        public TValue Value { get; set; }
    }
}
