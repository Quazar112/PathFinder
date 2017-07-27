using System;
using System.Collections.Generic;

namespace PathFinderClassLibrary.PathNodes
{
    public sealed class CoordinatePathNode : PathNode<CoordinatePathNode>
    {
        private float x, y;
        private List<CoordinatePathNode> _neighbors;
        public override IEnumerable<CoordinatePathNode> Neighbors => _neighbors;

        public CoordinatePathNode(string name, int x, int y) : base(name)
        {
            _neighbors = new List<CoordinatePathNode>();
            this.x = x;
            this.y = y;
        }

        public override float DistanceTo(CoordinatePathNode node)
        {
            return (float)Math.Sqrt(((this.x - node.x) * (this.x - node.x)) + ((this.y - node.y) * (this.y - node.y)));
        }

        public override void AddNeighbor(CoordinatePathNode node)
        {
            _neighbors.Add(node);
        }
    }
}
