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
            List<string> p = pf.FindShortestPath(n, n, out d);

            Assert.AreEqual(0, d);
            Assert.AreEqual(1, p.Count);
            Assert.AreEqual("n", p[0]);

            Assert.AreEqual(0, pf.ShortestDistance);
            Assert.AreEqual(1, pf.ShortestPath.Count);
            Assert.AreEqual("n", pf.ShortestPath[0]);

            Assert.IsTrue(pf.HasPath && pf.HasShortestPath && pf.IsEnded && pf.IsStarted);
        }

        [TestMethod]
        public void OneStepPath()
        {
            TestPathFinder pf = new TestPathFinder();
            CoordinatePathNode n1 = new CoordinatePathNode("a", 0, 0);
            CoordinatePathNode n2 = new CoordinatePathNode("b", 3, 4);
            n1.AddNeighbor(n2);

            float d;
            List<string> p = pf.FindPath(n1, n2, out d);

            Assert.AreEqual(5, d);
            Assert.AreEqual(2, p.Count);
            Assert.AreEqual("a", p[0]);
            Assert.AreEqual("b", p[1]);

            Assert.AreEqual(5, pf.ShortestDistance);
            Assert.AreEqual(2, pf.ShortestPath.Count);
            Assert.AreEqual("a", pf.ShortestPath[0]);
            Assert.AreEqual("b", pf.ShortestPath[1]);

            Assert.IsTrue(pf.HasPath && pf.IsEnded && pf.IsStarted);
        }

        [TestMethod]
        public void TwoStepPath()
        {
            TestPathFinder pf = new TestPathFinder();
            CoordinatePathNode n1 = new CoordinatePathNode("a", 0, 0);
            CoordinatePathNode n2 = new CoordinatePathNode("b", 1, 0);
            CoordinatePathNode n3 = new CoordinatePathNode("c", 3, 0);
            n1.AddNeighbor(n2);
            n2.AddNeighbor(n3);

            pf.StartPathFinding(n1, n3);
            Assert.IsTrue(pf.IsStarted && !pf.IsEnded && !pf.HasShortestPath && !pf.HasPath);

            Assert.ThrowsException<InvalidOperationException>(() => pf.ShortestDistance);
            Assert.ThrowsException<InvalidOperationException>(() => pf.ShortestPath);

            PathFinderStep<CoordinatePathNode> p = pf.DoPathFinderStep();

            Assert.AreEqual(0, p.TotalDistance);
            Assert.AreEqual(1, p.Path.Count);
            Assert.AreEqual(n1, p.CurentNode);

            do {
                p = pf.DoPathFinderStep();
            } while (!pf.HasPath && !pf.IsEnded);

            Assert.AreEqual(3, pf.ShortestDistance);
            Assert.AreEqual(3, pf.ShortestPath.Count);
            Assert.AreEqual(3, p.Path.Count);
        }
    }
}
