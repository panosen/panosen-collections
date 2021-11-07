using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panosen.Collections.Generic
{
    partial class Matrix<TRow, TCol, TValue> : IEnumerable<MatrixCell<TRow, TCol, TValue>>
    {
        /// <summary>
        /// GetEnumerator
        /// </summary>
        public IEnumerator<MatrixCell<TRow, TCol, TValue>> GetEnumerator()
        {
            return new MatrixEnumerator<TRow, TCol, TValue>(this);
        }

        /// <summary>
        /// GetEnumerator
        /// </summary>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return new MatrixEnumerator<TRow, TCol, TValue>(this);
        }
    }
}
