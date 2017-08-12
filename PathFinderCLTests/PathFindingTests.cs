using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PathFinderClassLibrary.PathFinder;
using PathFinderClassLibrary.PathNodes;
using PathFinderClassLibrary;
using System.Collections.Generic;

namespace PathFinderCLTests
{
    [TestClass]
    public class PathFindingTests
    {
        public static Func<PathFinderStep<CoordinatePathNode>, CoordinatePathNode, float> Heuristic = (n1, n2) => n1.TotalDistance + n1.CurentNode.DistanceTo(n2);
        internal class TestPathFinder : BasePathFinder<CoordinatePathNode>
        {
            internal TestPathFinder() : base(Heuristic) { }
        }

        [TestMethod]
        public void EmptyPath()
        {
            TestPathFinder pf = new TestPathFinder();
            CoordinatePathNode n = new CoordinatePathNode("n", 0, 0);

            Assert.IsFalse(pf.HasPath && pf.HasShortestPath && pf.IsEnded && pf.IsStarted);
            Assert.ThrowsException<InvalidOperationException>(() => pf.ShortestPath);
            Assert.ThrowsException<InvalidOperationException>(() => pf.ShortestDistance);

            float d;
            IReadOnlyList<string> p = pf.FindShortestPath(n, n, out d);

            Assert.AreEqual(0, d);
            Assert.AreEqual(1, p.Count);
            Assert.AreEqual("n", p[0]);
        }
    }
}
