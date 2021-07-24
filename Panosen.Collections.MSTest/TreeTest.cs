using Microsoft.VisualStudio.TestTools.UnitTesting;
using Panosen.Collections.Generic;
using System.Collections.Generic;

namespace Panosen.Collections.MSTest
{
    [TestClass]
    public class TreeTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            /*
             * 1 => 2 => 3
             * 1 => 2 => 4 => 5
             * 1 => 2 => 4 => 7
             * 1 => 6 => 8
             */
            Tree<int> tree = new Tree<int>();

            TreeItem<int> treeItem1 = tree.AddTreeItem(1);
            TreeItem<int> treeItem2 = tree.AddTreeItem(2);
            TreeItem<int> treeItem3 = tree.AddTreeItem(3);
            TreeItem<int> treeItem4 = tree.AddTreeItem(4);
            TreeItem<int> treeItem5 = tree.AddTreeItem(5);
            TreeItem<int> treeItem6 = tree.AddTreeItem(6);
            TreeItem<int> treeItem7 = tree.AddTreeItem(7);
            TreeItem<int> treeItem8 = tree.AddTreeItem(8);

            treeItem2.AddTo(treeItem1);
            treeItem3.AddTo(treeItem2);
            treeItem4.AddTo(treeItem2);
            treeItem5.AddTo(treeItem4);
            treeItem6.AddTo(treeItem1);
            treeItem7.AddTo(treeItem4);
            treeItem8.AddTo(treeItem6);

            Assert(tree, treeItem1, treeItem2, treeItem3, treeItem4, treeItem5, treeItem6, treeItem7, treeItem8);
        }

        [TestMethod]
        public void TestMethod2()
        {
            /*
             * 1 => 2 => 3
             * 1 => 2 => 4 => 5
             * 1 => 2 => 4 => 7
             * 1 => 6 => 8
             */
            Tree<int> tree = new Tree<int>();

            TreeItem<int> treeItem1 = new TreeItem<int> { Data = 1 };
            TreeItem<int> treeItem2 = new TreeItem<int> { Data = 2 };
            TreeItem<int> treeItem3 = new TreeItem<int> { Data = 3 };
            TreeItem<int> treeItem4 = new TreeItem<int> { Data = 4 };
            TreeItem<int> treeItem5 = new TreeItem<int> { Data = 5 };
            TreeItem<int> treeItem6 = new TreeItem<int> { Data = 6 };
            TreeItem<int> treeItem7 = new TreeItem<int> { Data = 7 };
            TreeItem<int> treeItem8 = new TreeItem<int> { Data = 8 };

            tree.AddTreeItemChain(treeItem1, treeItem2, treeItem3);
            tree.AddTreeItemChain(treeItem1, treeItem2, treeItem4, treeItem5);
            tree.AddTreeItemChain(treeItem1, treeItem2, treeItem4, treeItem7);
            tree.AddTreeItemChain(treeItem1, treeItem6, treeItem8);

            Assert(tree, treeItem1, treeItem2, treeItem3, treeItem4, treeItem5, treeItem6, treeItem7, treeItem8);
        }

        private static void Assert(Tree<int> tree, TreeItem<int> treeItem1, TreeItem<int> treeItem2, TreeItem<int> treeItem3, TreeItem<int> treeItem4, TreeItem<int> treeItem5, TreeItem<int> treeItem6, TreeItem<int> treeItem7, TreeItem<int> treeItem8)
        {
            var roots = tree.GetRoots();
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(roots);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(1, roots.Count);

            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNull(treeItem1.Parent);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(treeItem2.Parent);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(treeItem3.Parent);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(treeItem4.Parent);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(treeItem5.Parent);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(treeItem6.Parent);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(treeItem7.Parent);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(treeItem8.Parent);

            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNull(treeItem3.Children);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNull(treeItem5.Children);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNull(treeItem7.Children);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNull(treeItem8.Children);

            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(treeItem1.Children);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(treeItem2.Children);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(treeItem4.Children);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(treeItem6.Children);

            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(2, treeItem1.Children.Count);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(2, treeItem2.Children.Count);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(2, treeItem4.Children.Count);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(1, treeItem6.Children.Count);
        }
    }
}
