using System;
using System.Collections.Generic;

namespace PathFinderClassLibrary.PathNodes
{
    public sealed class DistancePathNode : PathNode<DistancePathNode>
    {
        private Dictionary<String, float> _distance_to;
        private List<DistancePathNode> _neighbors;
        public override IEnumerable<DistancePathNode> Neighbors => _neighbors;

        public DistancePathNode(string name) : base(name)
        {
            _neighbors = new List<DistancePathNode>();
            _distance_to = new Dictionary<string, float>();
        }

        /// <summary>
        /// Adds a neighbor with a distance of 1. 
        /// </summary>
        public override void AddNeighbor(DistancePathNode node)
        {
            _neighbors.Add(node);
            _distance_to.Add(node.Name, 1f);
        }

        public void AddNeighbor(DistancePathNode node, float distance_to)
        {
            _neighbors.Add(node);
            _distance_to.Add(node.Name, distance_to);
        }

        public override float DistanceTo(DistancePathNode node)
        {
            if (!_distance_to.ContainsKey(node.Name)) return -1;
            else return _distance_to[node.Name];
        }
    }
}
