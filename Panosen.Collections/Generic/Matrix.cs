using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Panosen.Collections.Generic
{
    /// <summary>
    /// Dictionary&lt;TKey1, Dictionary&lt;TKey2, TValue&gt;&gt;
    /// 参考 https://referencesource.microsoft.com/#mscorlib/system/collections/generic/dictionary.cs,cc27fcdd81291584
    /// </summary>
    public class Matrix<TRow, TCol, TValue> : IEnumerable<MatrixCell<TRow, TCol, TValue>>
    {
        private int version;

        private readonly Dictionary<TRow, Dictionary<TCol, TValue>> maps = new Dictionary<TRow, Dictionary<TCol, TValue>>();

        /// <summary>
        /// 添加键值对
        /// </summary>
        /// <param name="key"></param>
        /// <param name="subKey"></param>
        /// <param name="value"></param>
        /// <exception cref="ArgumentNullException">throws if key or subKey is null.</exception>
        /// <exception cref="ArgumentException">Savory.Collections.Generic.Map`3 中已存在具有相同键的元素</exception>
        public void Add(TRow key, TCol subKey, TValue value)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }
            if (subKey == null)
            {
                throw new ArgumentNullException(nameof(subKey));
            }

            if (maps.ContainsKey(key) && maps[key].ContainsKey(subKey))
            {
                throw new ArgumentException($"已经存在 {nameof(key)}.{nameof(subKey)} 相同的元素");
            }

            if (!maps.ContainsKey(key))
            {
                maps.Add(key, new Dictionary<TCol, TValue>());
            }

            maps[key].Add(subKey, value);
            version++;
        }

        /// <summary>
        /// 将所有键和值从 Map&lt;TKey, TSubKey, TValue&gt; 中移除。
        /// </summary>
        public void Clear()
        {
            maps.Clear();
            version++;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="subKey"></param>
        /// <returns></returns>
        public bool ContainsKey(TRow key, TCol subKey)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }
            if (subKey == null)
            {
                throw new ArgumentNullException(nameof(subKey));
            }

            if (!maps.ContainsKey(key))
            {
                return false;
            }

            return maps[key].ContainsKey(subKey);
        }

        /// <summary>
        /// Remove
        /// </summary>
        public bool Remove(TRow row, TCol col)
        {
            if (row == null)
            {
                throw new ArgumentNullException(nameof(row));
            }
            if (col == null)
            {
                throw new ArgumentNullException(nameof(col));
            }

            if (!maps.ContainsKey(row) || !maps[row].ContainsKey(col))
            {
                return false;
            }

            var removed = maps[row].Remove(col);
            if (removed)
            {
                version++;
            }

            return removed;
        }

        /// <summary>
        /// 尝试获取一个值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="subKey"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool TryGetValue(TRow key, TCol subKey, out TValue value)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }
            if (subKey == null)
            {
                throw new ArgumentNullException(nameof(subKey));
            }

            if (!maps.ContainsKey(key) || !maps[key].ContainsKey(subKey))
            {
                value = default;
                return false;
            }

            value = maps[key][subKey];
            return true;
        }

        /// <summary>
        /// 获取指定的值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="subKey"></param>
        /// <returns></returns>
        public TValue this[TRow key, TCol subKey]
        {
            get
            {
                if (key == null)
                {
                    throw new ArgumentNullException(nameof(key));
                }
                if (subKey == null)
                {
                    throw new ArgumentNullException(nameof(subKey));
                }

                if (!maps.ContainsKey(key) || !maps[key].ContainsKey(subKey))
                {
                    throw new KeyNotFoundException();
                }

                return maps[key][subKey];
            }
            set
            {
                Add(key, subKey, value);
            }
        }

        /// <summary>
        /// 获取键值对的个数
        /// </summary>
        public int Count
        {
            get
            {
                return maps.Values.Sum(v => v.Count);
            }
        }

        #region IEnumerable<KeysValuePair<TKey, TSubKey, TValue>> Members

        /// <summary>
        /// GetEnumerator
        /// </summary>
        public IEnumerator<MatrixCell<TRow, TCol, TValue>> GetEnumerator()
        {
            return new Enumerator(this);
        }

        /// <summary>
        /// GetEnumerator
        /// </summary>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return new Enumerator(this);
        }

        #endregion

        /// <summary>
        /// Enumerator
        /// </summary>
        public class Enumerator : IEnumerator<MatrixCell<TRow, TCol, TValue>>
        {
            private Matrix<TRow, TCol, TValue> map;
            private readonly int version;

            /// <summary>
            /// 遍历外层字典
            /// </summary>
            IEnumerator<KeyValuePair<TRow, Dictionary<TCol, TValue>>> mainEnumerator;

            /// <summary>
            /// 遍历内层字典
            /// </summary>
            IEnumerator<KeyValuePair<TCol, TValue>> subEnumerator;

            /// <summary>
            /// 当前项
            /// </summary>
            private MatrixCell<TRow, TCol, TValue> current;

            /// <summary>
            /// Enumerator
            /// </summary>
            /// <param name="map"></param>
            public Enumerator(Matrix<TRow, TCol, TValue> map)
            {
                this.map = map;
                this.version = map.version;

                mainEnumerator = map.maps.GetEnumerator();

                current = new MatrixCell<TRow, TCol, TValue>();
            }

            #region IEnumerator<KeysValuePair<TKey, TSubKey, TValue>> Members

            /// <summary>
            /// Current
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
                if (version != map.version)
                {
                    throw new InvalidOperationException("map changed.");
                }

                while (GetSubEnumerator() != null)
                {
                    var subMoveNext = this.subEnumerator.MoveNext();
                    if (subMoveNext)
                    {
                        current = new MatrixCell<TRow, TCol, TValue>(mainEnumerator.Current.Key, this.subEnumerator.Current.Key, this.subEnumerator.Current.Value);
                        return true;
                    }

                    this.subEnumerator = null;
                }

                return false;
            }

            private IEnumerator<KeyValuePair<TCol, TValue>> GetSubEnumerator()
            {
                if (this.subEnumerator != null)
                {
                    return this.subEnumerator;
                }

                if (mainEnumerator.MoveNext())
                {
                    this.subEnumerator = mainEnumerator.Current.Value.GetEnumerator();
                    return this.subEnumerator;
                }

                return null;
            }

            /// <summary>
            /// Reset
            /// </summary>
            public void Reset()
            {
                this.mainEnumerator.Reset();
                this.subEnumerator = null;
            }

            #endregion
        }
    }
}
