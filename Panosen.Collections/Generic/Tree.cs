using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Panosen.Collections.Generic
{
    /// <summary>
    /// Tree
    /// </summary>
    public class Tree<TData>
    {
        /// <summary>
        /// 所有节点
        /// </summary>
        public List<TreeItem<TData>> TreeItemList { get; set; }
    }

    /// <summary>
    /// TreeExtension
    /// </summary>
    public static class TreeExtension
    {
        /// <summary>
        /// AddTreeItem
        /// </summary>
        public static Tree<TData> AddTreeItem<TData>(this Tree<TData> tree, TreeItem<TData> treeItem)
        {
            if (tree.TreeItemList == null)
            {
                tree.TreeItemList = new List<TreeItem<TData>>();
            }

            if (tree.TreeItemList.Contains(treeItem))
            {
                return tree;
            }

            tree.TreeItemList.Add(treeItem);

            return tree;
        }

        /// <summary>
        /// AddTreeItem
        /// </summary>
        public static TreeItem<TData> AddTreeItem<TData>(this Tree<TData> tree, TData data)
        {
            if (tree.TreeItemList == null)
            {
                tree.TreeItemList = new List<TreeItem<TData>>();
            }

            var treeItem = new TreeItem<TData>();
            treeItem.Data = data;

            AddTreeItem(tree, treeItem);

            return treeItem;
        }

        /// <summary>
        /// AddTreeItem
        /// </summary>
        public static Tree<TData> AddTreeItemChain<TData>(this Tree<TData> tree, params TreeItem<TData>[] treeItems)
        {
            if (treeItems == null || treeItems.Length == 0)
            {
                return tree;
            }

            if (tree.TreeItemList == null)
            {
                tree.TreeItemList = new List<TreeItem<TData>>();
            }

            for (int i = 0; i < treeItems.Length; i++)
            {
                AddTreeItem(tree, treeItems[i]);

                if (i < treeItems.Length - 1)
                {
                    treeItems[i].AddChild(treeItems[i + 1]);
                }
            }

            return tree;
        }

        /// <summary>
        /// AddTreeItem
        /// </summary>
        public static Tree<TData> AddTreeItemChain<TData>(this Tree<TData> tree, List<TreeItem<TData>> treeItems)
        {
            if (treeItems == null || treeItems.Count == 0)
            {
                return tree;
            }

            if (tree.TreeItemList == null)
            {
                tree.TreeItemList = new List<TreeItem<TData>>();
            }

            for (int i = 0; i < treeItems.Count; i++)
            {
                AddTreeItem(tree, treeItems[i]);

                if (i < treeItems.Count - 1)
                {
                    treeItems[i].AddChild(treeItems[i + 1]);
                }
            }

            return tree;
        }

        /// <summary>
        /// AddTreeItem
        /// </summary>
        public static Tree<TData> AddTreeItemChain<TData>(this Tree<TData> tree, params TData[] datas)
        {
            if (datas == null || datas.Length == 0)
            {
                return tree;
            }

            if (tree.TreeItemList == null)
            {
                tree.TreeItemList = new List<TreeItem<TData>>();
            }

            var treeItems = datas.Select(v => new TreeItem<TData> { Data = v }).ToList();

            return AddTreeItemChain(tree, treeItems);
        }

        /// <summary>
        /// AddTreeItem
        /// </summary>
        public static Tree<TData> AddTreeItemChain<TData>(this Tree<TData> tree, List<TData> datas)
        {
            if (datas == null || datas.Count == 0)
            {
                return tree;
            }

            if (tree.TreeItemList == null)
            {
                tree.TreeItemList = new List<TreeItem<TData>>();
            }

            var treeItems = datas.Select(v => new TreeItem<TData> { Data = v }).ToList();

            return AddTreeItemChain(tree, treeItems);
        }

        /// <summary>
        /// AddTreeItem
        /// </summary>
        public static Tuple<TreeItem<TData>, TreeItem<TData>> AddTreeItem<TData>(this Tree<TData> tree, TData treeItemData, TData parentTreeItemData)
        {
            if (tree.TreeItemList == null)
            {
                tree.TreeItemList = new List<TreeItem<TData>>();
            }

            TreeItem<TData> treeItem = new TreeItem<TData>();
            treeItem.Data = treeItemData;

            TreeItem<TData> parentTreeItem = new TreeItem<TData>();
            parentTreeItem.Data = parentTreeItemData;

            AddTreeItem(tree, treeItem);
            AddTreeItem(tree, parentTreeItem);

            treeItem.Parent = parentTreeItem;
            parentTreeItem.AddChild(treeItem);

            return new Tuple<TreeItem<TData>, TreeItem<TData>>(treeItem, parentTreeItem);
        }

        /// <summary>
        /// AddTreeItem
        /// </summary>
        public static Tree<TData> AddTreeItem<TData>(this Tree<TData> tree, TreeItem<TData> treeItem, TreeItem<TData> parentTreeItem)
        {
            if (tree.TreeItemList == null)
            {
                tree.TreeItemList = new List<TreeItem<TData>>();
            }

            AddTreeItem(tree, treeItem);
            AddTreeItem(tree, parentTreeItem);

            treeItem.Parent = parentTreeItem;
            parentTreeItem.AddChild(treeItem);

            return tree;
        }

        /// <summary>
        /// AddTreeItem
        /// </summary>
        public static Tree<TData> AddTreeItem<TData>(this Tree<TData> tree, params TreeItem<TData>[] treeItems)
        {
            if (treeItems == null || treeItems.Length == 0)
            {
                return tree;
            }

            if (tree.TreeItemList == null)
            {
                tree.TreeItemList = new List<TreeItem<TData>>();
            }

            foreach (var treeItem in treeItems)
            {
                AddTreeItem(tree, treeItem);
            }

            return tree;
        }

        /// <summary>
        /// GetRoots
        /// </summary>
        public static List<TreeItem<TData>> GetRoots<TData>(this Tree<TData> tree)
        {
            if (tree.TreeItemList == null)
            {
                return null;
            }
            return tree.TreeItemList.Where(v => v.Parent == null).ToList();
        }
    }
}
