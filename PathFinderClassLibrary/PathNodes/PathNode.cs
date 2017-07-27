using System.Collections.Generic;

namespace PathFinderClassLibrary.PathNodes
{
    public abstract class PathNode<T> where T : PathNode<T>
    {
        public string Name { get; }
        public abstract IEnumerable<T> Neighbors { get; }

        public PathNode(string name)
        {
            Name = name;
        }

        public abstract void AddNeighbor(T node);

        public abstract float DistanceTo(T node);

        // add multiple nodes? 
    }
}
