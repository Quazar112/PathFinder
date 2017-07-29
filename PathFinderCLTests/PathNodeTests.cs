using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PathFinderClassLibrary.PathNodes;

namespace PathFinderCLTests
{
    [TestClass]
    public class PathNodeTests
    {
        [TestMethod]
        public void TestCoordinateNodeDistance()
        {
            CoordinatePathNode cn1 = new CoordinatePathNode("node1", 0, 0);
            CoordinatePathNode cn2 = new CoordinatePathNode("node2", 3, 4);
            CoordinatePathNode cn3 = new CoordinatePathNode("node3", -1.3f, 2.2f);

            Assert.AreEqual(cn1.DistanceTo(cn2), 5);
            Assert.AreEqual(cn2.DistanceTo(cn3), 4.66f, 0.01f);
            Assert.AreEqual(cn3.DistanceTo(cn1), cn1.DistanceTo(cn3));
            Assert.AreEqual(cn1.DistanceTo(cn1), 0);
            Assert.AreEqual(cn3.DistanceTo(cn1), 2.555f, 0.001f);
        }

        [TestMethod]
        public void TestDistanceNodeDistance()
        {
            DistancePathNode dn1 = new DistancePathNode("node1");
            DistancePathNode dn2 = new DistancePathNode("node2");
            DistancePathNode dn3 = new DistancePathNode("node3");

            dn1.AddNeighbor(dn2);
            dn1.AddNeighbor(dn3, 3.5f);
            dn2.AddNeighbor(dn1);
            dn2.AddNeighbor(dn3, 1f);
            dn3.AddNeighbor(dn1, 2f);

            Assert.AreEqual(dn1.DistanceTo(dn2), 1f);
            Assert.AreEqual(dn2.DistanceTo(dn1), 1f);
            Assert.AreEqual(dn1.DistanceTo(dn3), 3.5f);
            Assert.AreEqual(dn3.DistanceTo(dn1), 2f);
            Assert.AreEqual(dn2.DistanceTo(dn3), 1f);

            Assert.AreEqual(dn3.DistanceTo(dn2), -1);
            Assert.ThrowsException<ArgumentException>(() => dn1.AddNeighbor(new DistancePathNode("node1")));
            Assert.ThrowsException<ArgumentException>(() => dn1.AddNeighbor(new DistancePathNode("node2")));
        }

    }
}
