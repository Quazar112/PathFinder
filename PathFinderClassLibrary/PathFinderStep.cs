using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using PathFinderClassLibrary.PathNodes;

namespace PathFinderClassLibrary
{
    public class PathFinderStep<T> where T : PathNode<T>
    {
        private List<string> _path;
        private T _cur_node;
        private float _total_distance;

        public ReadOnlyCollection<string> Path => _path.AsReadOnly();
        public T CurentNode => _cur_node;
        public float TotalDistance => _total_distance;

        public PathFinderStep(T curNode, float distanceSoFar = 0f, List<string> path = null)
        {
            _cur_node = curNode;
            _total_distance = distanceSoFar;
            if(path == null) {
                _path = new List<string>();
            } else {
                _path = path;
            }
        }

        public List<PathFinderStep<T>> GetNextSteps()
        {
            string[] curPath = _path.ToArray();
            List<string> newPath = new List<string>(curPath);
            newPath.Add(_cur_node.Name);

            List<PathFinderStep<T>> nextSteps = new List<PathFinderStep<T>>();

            foreach(T node in _cur_node.Neighbors) {
                if (!_path.Contains(node.Name)) {
                    nextSteps.Add(new PathFinderStep<T>(node, _cur_node.DistanceTo(node) + _total_distance, newPath));
                }
            }

            return nextSteps;
        }

    }
}
