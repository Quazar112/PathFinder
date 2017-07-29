using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PathFinderClassLibrary.PathNodes;

namespace PathFinderClassLibrary.PathFinder
{
    public interface IPathFinder<T> where T : PathNode<T>
    {
        bool HasPath { get; }
        bool HasShortestPath { get; }
        bool IsStarted { get; }

        List<string> ShortestPath { get; }
        int ShortestDistance { get; }
        T Nodes { get; }

        PathFinderStep<T> DoPathFinderStep();

        List<string> FindPath(string startNode, string endNode, out int totalDistance);

        List<string> FindShortestPath(T startNode, T endNode, out int totalDistance);

        //Constructor has nodes as arguments
    }
}
