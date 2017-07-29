using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PathFinderClassLibrary.PathNodes;
using PriorityQueueClassLibrary;

namespace PathFinderClassLibrary.PathFinder
{
    abstract class BasePathFinder<T> : IPathFinder<T> where T : PathNode<T>
    {
        public bool HasPath { get; private set; } = false;
        public bool HasShortestPath { get; private set; } = false;
        public bool IsStarted { get; private set; } = false;
        public IReadOnlyList<string> ShortestPath {
            get {
                if (HasShortestPath) return _step.Path;
                else throw new InvalidOperationException("Shortest path has not been calculated.");
            }
        }
        public float ShortestDistance {
            get {
                if (HasShortestPath) return _shortest_distance;
                else throw new InvalidOperationException("Shortest path has not been calculated.");
            }
            private set {
                    _shortest_distance = value;
            }
        }
        public ICollection<T> Nodes => _nodes.AsReadOnly();

        private List<T> _nodes;
        private float _shortest_distance;
        private PathFinderStep<T> _step;
        private PriorityQueue<T> _queue;

        public BasePathFinder(List<T> nodes)
        {
            _nodes = nodes;
            _queue = new PriorityQueue<T>();


        }

        // dictionary of shortest distance to each node, drop steps with distance to current node >= minimum so far
        // to find shortest: once find first path, can stop once total distances are higher

        public abstract PathFinderStep<T> DoPathFinderStep();
        public abstract List<string> FindPath(string startNode, string endNode, out int totalDistance);
        public abstract List<string> FindShortestPath(T startNode, T endNode, out int totalDistance);
    }
}
