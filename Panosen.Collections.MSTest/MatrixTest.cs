using Microsoft.VisualStudio.TestTools.UnitTesting;
using Panosen.Collections.Generic;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Panosen.Collections.MSTest
{
    [TestClass]
    public class MatrixTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            Matrix<int, int, string> matrix = new Matrix<int, int, string>();
            matrix.Add(1, 1, "a");
            matrix.Add(1, 2, "b");
            matrix.Add(2, 1, "c");
            matrix.Add(2, 2, "d");

            Assert.AreEqual(4, matrix.Count());

            StringBuilder builder = new StringBuilder();
            var enumerator = matrix.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var current = enumerator.Current;
                builder.Append($"{current.Row}-{current.Col}-{current.Value};");
            }
            Assert.AreEqual("1-1-a;1-2-b;2-1-c;2-2-d;", builder.ToString());

            Assert.AreEqual("a", matrix.GetValue(1, 1));
            Assert.AreEqual("b", matrix.GetValue(1, 2));
            Assert.AreEqual("c", matrix.GetValue(2, 1));
            Assert.AreEqual("d", matrix.GetValue(2, 2));

            var cell = matrix.FirstOrDefault(v => v.Row == 1 && v.Col == 2);
            Assert.AreEqual("b", cell.Value);

            var empty = matrix.FirstOrDefault(v => v.Row == 2 && v.Col == 3);
            Assert.IsNull(empty);
        }
    }
}
