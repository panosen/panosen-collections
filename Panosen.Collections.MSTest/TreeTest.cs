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

            var roots = tree.GetRoots();
            Assert.IsNotNull(roots);
            Assert.AreEqual(1, roots.Count);

            Assert.IsNull(treeItem1.Parent);
            Assert.IsNotNull(treeItem2.Parent);
            Assert.IsNotNull(treeItem3.Parent);
            Assert.IsNotNull(treeItem4.Parent);
            Assert.IsNotNull(treeItem5.Parent);
            Assert.IsNotNull(treeItem6.Parent);
            Assert.IsNotNull(treeItem7.Parent);
            Assert.IsNotNull(treeItem8.Parent);

            Assert.IsNull(treeItem3.Childen);
            Assert.IsNull(treeItem5.Childen);
            Assert.IsNull(treeItem7.Childen);
            Assert.IsNull(treeItem8.Childen);

            Assert.IsNotNull(treeItem1.Childen);
            Assert.IsNotNull(treeItem2.Childen);
            Assert.IsNotNull(treeItem4.Childen);
            Assert.IsNotNull(treeItem6.Childen);

            Assert.AreEqual(2, treeItem1.Childen.Count);
            Assert.AreEqual(2, treeItem2.Childen.Count);
            Assert.AreEqual(2, treeItem4.Childen.Count);
            Assert.AreEqual(1, treeItem6.Childen.Count);

        }
    }
}
