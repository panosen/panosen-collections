using System;
using System.Collections.Generic;

namespace Panosen.Collections.Generic
{
    /// <summary>
    /// 单个节点
    /// </summary>
    public class TreeItem<TData>
    {
        /// <summary>
        /// 节点id
        /// </summary>
        public TData Data { get; set; }

        /// <summary>
        /// 父级id
        /// </summary>
        public TreeItem<TData> Parent { get; set; }

        /// <summary>
        /// 子级
        /// </summary>
        public List<TreeItem<TData>> Childen { get; set; }
    }

    /// <summary>
    /// TreeItem 扩展
    /// </summary>
    public static class TreeItemExtension
    {
        /// <summary>
        /// 添加到父级
        /// </summary>
        public static void AddTo<TData>(this TreeItem<TData> treeItem, TreeItem<TData> treeItemParent)
        {
            if (treeItemParent.Childen == null)
            {
                treeItemParent.Childen = new List<TreeItem<TData>>();
            }

            treeItem.Parent = treeItemParent;
            treeItemParent.Childen.Add(treeItem);
        }

        /// <summary>
        /// 增加子级
        /// </summary>
        public static void AddChild<TData>(this TreeItem<TData> treeItem, TreeItem<TData> child)
        {
            if (treeItem.Childen == null)
            {
                treeItem.Childen = new List<TreeItem<TData>>();
            }

            child.Parent = treeItem;
            treeItem.Childen.Add(child);
        }
    }
}
