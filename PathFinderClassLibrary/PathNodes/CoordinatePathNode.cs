using System;
using System.Collections.Generic;
using System.Text;

namespace PathFinderClassLibrary.PathNodes
{
    public sealed class CoordinatePathNode : PathNode<CoordinatePathNode>
    {
        private double x, y;
        private List<CoordinatePathNode> _neighbors = new List<CoordinatePathNode>();
        public override ICollection<CoordinatePathNode> Neighbors => _neighbors;

        public CoordinatePathNode(string name, int x, int y) : base(name)
        {
            this.x = x;
            this.y = y;
        }

        public override double DistanceTo(CoordinatePathNode node)
        {
            return Math.Sqrt(((this.x - node.x) * (this.x - node.x)) + ((this.y - node.y) * (this.y - node.y)));
        }

        public override void AddNeighbor(CoordinatePathNode node)
        {
            _neighbors.Add(node);
        }
    }
}
