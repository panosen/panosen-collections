using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panosen.Collections.Generic
{
    /// <summary>
    /// MatrixExtension
    /// </summary>
    public static class MatrixExtension
    {
        /// <summary>
        /// ContainsKey
        /// </summary>
        public static bool ContainsKey<TRow, TCol, TValue>(this Matrix<TRow, TCol, TValue> matrix, TRow key, TCol subKey)
        {
            if (matrix == null)
            {
                throw new ArgumentNullException(nameof(matrix));
            }
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }
            if (subKey == null)
            {
                throw new ArgumentNullException(nameof(subKey));
            }

            if (!matrix.Maps.ContainsKey(key))
            {
                return false;
            }

            return matrix.Maps[key].ContainsKey(subKey);
        }

        /// <summary>
        /// 添加键值对
        /// </summary>
        public static void Add<TRow, TCol, TValue>(this Matrix<TRow, TCol, TValue> matrix, TRow key, TCol subKey, TValue value)
        {
            if (matrix == null)
            {
                throw new ArgumentNullException(nameof(matrix));
            }
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }
            if (subKey == null)
            {
                throw new ArgumentNullException(nameof(subKey));
            }

            if (matrix.Maps.ContainsKey(key) && matrix.Maps[key].ContainsKey(subKey))
            {
                throw new ArgumentException($"已经存在 {nameof(key)}.{nameof(subKey)} 相同的元素");
            }

            if (!matrix.Maps.ContainsKey(key))
            {
                matrix.Maps.Add(key, new Dictionary<TCol, TValue>());
            }

            matrix.Maps[key].Add(subKey, value);
            matrix.Version++;
        }

        /// <summary>
        /// 将所有键和值从 Map&lt;TKey, TSubKey, TValue&gt; 中移除。
        /// </summary>
        public static void Clear<TRow, TCol, TValue>(this Matrix<TRow, TCol, TValue> matrix)
        {
            matrix.Maps.Clear();
            matrix.Version++;
        }

        /// <summary>
        /// Remove
        /// </summary>
        public static bool Remove<TRow, TCol, TValue>(this Matrix<TRow, TCol, TValue> matrix, TRow row, TCol col)
        {
            if (row == null)
            {
                throw new ArgumentNullException(nameof(row));
            }
            if (col == null)
            {
                throw new ArgumentNullException(nameof(col));
            }

            if (!matrix.Maps.ContainsKey(row) || !matrix.Maps[row].ContainsKey(col))
            {
                return false;
            }

            var removed = matrix.Maps[row].Remove(col);
            if (removed)
            {
                matrix.Version++;
            }

            return removed;
        }

        /// <summary>
        /// 尝试获取一个值
        /// </summary>
        public static bool TryGetValue<TRow, TCol, TValue>(this Matrix<TRow, TCol, TValue> matrix, TRow key, TCol subKey, out TValue value)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }
            if (subKey == null)
            {
                throw new ArgumentNullException(nameof(subKey));
            }

            if (!matrix.Maps.ContainsKey(key) || !matrix.Maps[key].ContainsKey(subKey))
            {
                value = default;
                return false;
            }

            value = matrix.Maps[key][subKey];
            return true;
        }

        /// <summary>
        /// 获取键值对的个数
        /// </summary>
        public static int Count<TRow, TCol, TValue>(this Matrix<TRow, TCol, TValue> matrix)
        {
            return matrix.Maps.Values.Sum(v => v.Count);
        }

        /// <summary>
        /// GetEnumerator
        /// </summary>
        public static MatrixEnumerator<TRow, TCol, TValue> GetEnumerator<TRow, TCol, TValue>(this Matrix<TRow, TCol, TValue> matrix)
        {
            return new MatrixEnumerator<TRow, TCol, TValue>(matrix);
        }

        /// <summary>
        /// GetValue
        /// </summary>
        public static List<TValue> GetValues<TRow, TCol, TValue>(this Matrix<TRow, TCol, TValue> matrix, TRow row)
        {
            if (!matrix.Maps.ContainsKey(row))
            {
                return default;
            }

            return matrix.Maps[row].Values.ToList();
        }

        /// <summary>
        /// GetValue
        /// </summary>
        public static TValue GetValue<TRow, TCol, TValue>(this Matrix<TRow, TCol, TValue> matrix, TRow row, TCol col)
        {
            if (!matrix.Maps.ContainsKey(row))
            {
                return default;
            }

            if (!matrix.Maps[row].ContainsKey(col))
            {
                return default;
            }

            return matrix.Maps[row][col];
        }
    }
}