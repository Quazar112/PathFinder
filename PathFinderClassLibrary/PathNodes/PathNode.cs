using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace PathFinderClassLibrary
{
    public abstract class PathNode<T> where T : PathNode<T>
    {
        public string Name { get; }
        public abstract ICollection<T> Neighbors { get; }

        public PathNode(string name)
        {
            Name = name;
        }

        public abstract void AddNeighbor(T node);

        public abstract double DistanceTo(T node);

        // add multiple nodes? 
    }
}
