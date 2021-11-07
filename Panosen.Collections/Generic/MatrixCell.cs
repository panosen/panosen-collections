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
    public sealed class MatrixCell<TRow, TCol, TValue>
    {
        /// <summary>
        /// Key
        /// </summary>
        public TRow Row { get; private set; }

        /// <summary>
        /// SubKey
        /// </summary>
        public TCol Col { get; private set; }

        /// <summary>
        /// Value
        /// </summary>
        public TValue Value { get; private set; }

        /// <summary>
        /// KeysValuePair
        /// </summary>
        public MatrixCell(TRow row, TCol col, TValue value)
        {
            this.Row = row;
            this.Col = col;
            this.Value = value;
        }
    }
}
