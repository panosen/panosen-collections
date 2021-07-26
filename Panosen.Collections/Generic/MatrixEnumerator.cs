using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panosen.Collections.Generic
{
    /// <summary>
    /// MatrixEnumerator
    /// </summary>
    public class MatrixEnumerator<TRow, TCol, TValue> : IEnumerator<MatrixCell<TRow, TCol, TValue>>
    {
        private readonly Matrix<TRow, TCol, TValue> map;
        private readonly int version;

        /// <summary>
        /// 遍历外层字典
        /// </summary>
        IEnumerator<KeyValuePair<TRow, Dictionary<TCol, TValue>>> rowEnumerator;

        /// <summary>
        /// 遍历内层字典
        /// </summary>
        IEnumerator<KeyValuePair<TCol, TValue>> colEnumerator;

        /// <summary>
        /// 当前项
        /// </summary>
        private MatrixCell<TRow, TCol, TValue> current;

        /// <summary>
        /// MatrixEnumerator
        /// </summary>
        public MatrixEnumerator(Matrix<TRow, TCol, TValue> matrix)
        {
            this.map = matrix;
            this.version = matrix.Version;

            rowEnumerator = matrix.Maps.GetEnumerator();

            current = new MatrixCell<TRow, TCol, TValue>();
        }

        #region IEnumerator<MatrixCell<TRow, TCol, TValue>> Members

        /// <summary>
        /// Crrent
        /// </summary>
        public MatrixCell<TRow, TCol, TValue> Current => current;

        object IEnumerator.Current => current;

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
        }

        /// <summary>
        /// MoveNext
        /// </summary>
        /// <returns></returns>
        public bool MoveNext()
        {
            if (version != map.Version)
            {
                throw new InvalidOperationException("map changed.");
            }

            while (GetSubEnumerator() != null)
            {
                var subMoveNext = this.colEnumerator.MoveNext();
                if (subMoveNext)
                {
                    current = new MatrixCell<TRow, TCol, TValue>(rowEnumerator.Current.Key, this.colEnumerator.Current.Key, this.colEnumerator.Current.Value);
                    return true;
                }

                this.colEnumerator = null;
            }

            return false;
        }

        /// <summary>
        /// Reset
        /// </summary>
        public void Reset()
        {
            this.rowEnumerator.Reset();
            this.colEnumerator = null;
        }

        #endregion

        private IEnumerator<KeyValuePair<TCol, TValue>> GetSubEnumerator()
        {
            if (this.colEnumerator != null)
            {
                return this.colEnumerator;
            }

            if (rowEnumerator.MoveNext())
            {
                this.colEnumerator = rowEnumerator.Current.Value.GetEnumerator();
                return this.colEnumerator;
            }

            return null;
        }
    }
}
