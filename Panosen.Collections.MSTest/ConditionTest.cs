using Microsoft.VisualStudio.TestTools.UnitTesting;
using Panosen.Collections.Generic;
using System.Linq;

namespace Panosen.Collections.MSTest
{
    [TestClass]
    public class ConditionTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            var must = new Must<string>();
            must.AddSingle("x = 1");
            must.AddSingle("y = 2");

            var should = must.AddShould();
            should.AddSingle("m = 3");
            should.AddSingle("n = 4");

            var actual = Parse(must);

            var expected = "(x = 1 && y = 2 && (m = 3 || n = 4))";

            Assert.AreEqual(expected, actual);
        }

        private string Parse(Condition<string> condition)
        {
            var single = condition as Single<string>;
            if (single != null)
            {
                return single.Value;
            }

            var must = condition as Must<string>;
            if (must != null)
            {
                if (must.Items == null || must.Items.Count == 0)
                {
                    return string.Empty;
                }
                var text = string.Join(" && ", must.Items.Select(v => Parse(v)).Where(v => !string.IsNullOrEmpty(v)));
                if (must.Items.Count == 1)
                {
                    return text;
                }
                return $"({text})";
            }

            var should = condition as Should<string>;
            if (should != null)
            {
                if (should.Items == null || should.Items.Count == 0)
                {
                    return string.Empty;
                }
                var text = string.Join(" || ", should.Items.Select(v => Parse(v)).Where(v => !string.IsNullOrEmpty(v)));
                if (should.Items.Count == 1)
                {
                    return text;
                }
                return $"({text})";
            }

            return string.Empty;
        }
    }
}
