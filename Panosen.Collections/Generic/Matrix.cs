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
    public class Matrix<TRow, TCol, TValue>
    {
        /// <summary>
        /// 每次有增加或者删除元素，都会触发数字自增
        /// </summary>
        internal int Version { get; set; }

        /// <summary>
        /// 字典
        /// </summary>
        internal readonly Dictionary<TRow, Dictionary<TCol, TValue>> Maps = new Dictionary<TRow, Dictionary<TCol, TValue>>();
    }
}
