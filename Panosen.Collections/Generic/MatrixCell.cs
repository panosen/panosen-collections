using System;
using System.Collections.Generic;
using System.Text;

namespace Panosen.Collections.Generic
{
    /// <summary>
    /// A MatrixCell holds a key and a value from a dictionary.
    /// It is used by the IEnumerable&lt;T&gt; implementation for both IMap&lt;TKey, TSubKey, TValue&gt;
    /// and IReadOnlyMap&lt;TKey, TSubKey, TValue&gt;.
    /// </summary>
    public readonly struct MatrixCell<TKey, TSubKey, TValue>
    {
        /// <summary>
        /// Key
        /// </summary>
        public TKey Key { get; }

        /// <summary>
        /// SubKey
        /// </summary>
        public TSubKey SubKey { get; }

        /// <summary>
        /// Value
        /// </summary>
        public TValue Value { get; }

        /// <summary>
        /// KeysValuePair
        /// </summary>
        public MatrixCell(TKey key, TSubKey subKey, TValue value)
        {
            this.Key = key;
            this.SubKey = subKey;
            this.Value = value;
        }
    }
}
