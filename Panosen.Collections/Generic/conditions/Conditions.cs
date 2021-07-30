using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panosen.Collections.Generic
{
    /// <summary>
    /// 多个判断条件
    /// </summary>
    public abstract class Conditions<T> : Condition<T>
    {
        /// <summary>
        /// 判断条件集合
        /// </summary>
        public List<Condition<T>> Items { get; set; }
    }

    /// <summary>
    /// Conditions扩展
    /// </summary>
    public static class BaseConditionsExtension
    {
        /// <summary>
        /// AddCondition
        /// </summary>
        public static Conditions<TValue> AddCondition<TValue>(this Conditions<TValue> conditions, Condition<TValue> condition)
        {
            if (conditions.Items == null)
            {
                conditions.Items = new List<Condition<TValue>>();
            }

            conditions.Items.Add(condition);

            return conditions;
        }

        /// <summary>
        /// AddCondition
        /// </summary>
        public static Conditions<TValue> AddCondition<TValue>(this Conditions<TValue> conditions, Conditions<TValue> condition)
        {
            if (conditions.Items == null)
            {
                conditions.Items = new List<Condition<TValue>>();
            }

            conditions.Items.Add(condition);

            return conditions;
        }

        /// <summary>
        /// AddSimple
        /// </summary>
        public static Single<TValue> AddSingle<TValue>(this Conditions<TValue> conditions, TValue value = default)
        {
            Single<TValue> single = new Single<TValue>
            {
                Value = value
            };

            AddCondition(conditions, single);

            return single;
        }

        /// <summary>
        /// 添加一个Must判断
        /// </summary>
        public static Must<TValue> AddMust<TValue>(this Conditions<TValue> conditions)
        {
            Must<TValue> must = new Must<TValue>();

            AddCondition(conditions, must);

            return must;
        }

        /// <summary>
        /// 添加一个Should判断
        /// </summary>
        public static Should<TValue> AddShould<TValue>(this Conditions<TValue> conditions)
        {
            Should<TValue> should = new Should<TValue>();

            AddCondition(conditions, should);

            return should;
        }
    }
}
