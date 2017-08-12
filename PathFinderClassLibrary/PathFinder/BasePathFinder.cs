using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PathFinderClassLibrary.PathNodes;
using PriorityQueueClassLibrary;

namespace PathFinderClassLibrary.PathFinder
{
    public class BasePathFinder<T> : IPathFinder<T> where T : PathNode<T>
    {
        public bool HasPath { get; private set; } = false;
        public bool HasShortestPath { get; private set; } = false;
        public bool IsStarted { get; private set; } = false;
        public bool IsEnded { get; private set; } = false;
        public IReadOnlyList<string> ShortestPath {
            get {
                if (HasShortestPath) return _shortest_step.Path;
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
        //public ICollection<T> Nodes => _nodes.AsReadOnly();

        private Dictionary<string, float> _shortest_sofar;
        private Func<PathFinderStep<T>, T, float> _heuristic;
        private T _goal;
        //private List<T> _nodes;
        private float _shortest_distance;
        //private PathFinderStep<T> _step;
        private PathFinderStep<T> _shortest_step;
        private PriorityQueue<PathFinderStep<T>> _queue;

        public BasePathFinder(/*List<T> nodes, */Func<PathFinderStep<T>, T, float> heuristicFunction)
        {
           // _nodes = nodes;
            _queue = new PriorityQueue<PathFinderStep<T>>();
            _shortest_sofar = new Dictionary<string, float>();
            _heuristic = heuristicFunction;
        }

        // dictionary of shortest distance to each node, drop steps with distance to current node >= minimum so far
        // to find shortest: once find first path, can stop once total distances are higher

        public PathFinderStep<T> DoPathFinderStep()
        {
            if (_queue.IsEmpty) {
                if (!IsStarted) {
                    throw new InvalidOperationException("Path finding must be started before stepping through.");
                } else if(IsEnded) {
                    throw new IndexOutOfRangeException("Cannot step through after all paths have been examined.");
                } else {
                    IsEnded = true;
                    if (HasPath) {
                        HasShortestPath = true;
                        _shortest_distance = _shortest_step.TotalDistance;
                    }
                    return null; 
                }
            }

            PathFinderStep<T> step = _queue.Dequeue(); 

            if(HasPath && step.TotalDistance > _shortest_distance) { //know have shortest path
                _queue.Clear();
                HasShortestPath = true;
                IsEnded = true;
                return null;
            }
            if(step.CurentNode.Name == _goal.Name) {
                if (!HasPath || step.TotalDistance < _shortest_distance) {
                    _shortest_distance = step.TotalDistance;
                    _shortest_step = step;
                }
                HasPath = true;
            }

            List<PathFinderStep<T>> nextSteps = step.GetNextSteps();

            foreach(PathFinderStep<T> s in nextSteps) {
                if (_shortest_sofar.ContainsKey(s.CurentNode.Name) && s.TotalDistance >= _shortest_sofar[s.CurentNode.Name]) {
                    continue; //if returned step goes to a node that already has been visited with same or shorter distance, ignore it
                } else {
                    _shortest_sofar[s.CurentNode.Name] = s.TotalDistance;
                    _queue.Enqueue(s, _heuristic(s, _goal));
                }
            }

            return step;
        }

        public void StartPathFinding(T startNode, T endNode)
        {
            HasPath = HasShortestPath = IsEnded = false;
            IsStarted = true;
            _goal = endNode;
            PathFinderStep<T> step = new PathFinderStep<T>(startNode);
            _queue.Enqueue(step, _heuristic(step, _goal));
        }
        public IReadOnlyList<string> FindPath(T startNode, T endNode, out float totalDistance)
        {
            StartPathFinding(startNode, endNode);
            totalDistance = -1;
            while(!HasPath || !IsEnded) {
                DoPathFinderStep();
            }
            if (!HasPath) return null;
            else {
                totalDistance = _shortest_step.TotalDistance;
                return _shortest_step.Path;
            }
        }
        public IReadOnlyList<string> FindShortestPath(T startNode, T endNode, out float totalDistance)
        {
            StartPathFinding(startNode, endNode);
            totalDistance = -1;
            while (!HasShortestPath || !IsEnded) {
                DoPathFinderStep();
            }
            if (!HasPath) return null;
            else {
                totalDistance = _shortest_step.TotalDistance;
                return _shortest_step.Path;
            }
        }
    }
}


//TODO:
//- may not need all the different state variables, some may be the same
//- don't need nodes assuming they are connected to their neighbors
//- copy certain functions to IPathFinder