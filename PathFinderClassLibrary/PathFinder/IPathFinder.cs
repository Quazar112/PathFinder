using System.Collections.Generic;
using PathFinderClassLibrary.PathNodes;

namespace PathFinderClassLibrary.PathFinder
{
    public interface IPathFinder<T> where T : PathNode<T>
    {
        bool HasPath { get; }
        bool HasShortestPath { get; }
        bool IsStarted { get; }

        IReadOnlyList<string> ShortestPath { get; }
        float ShortestDistance { get; }
        ICollection<T> Nodes { get; }

        PathFinderStep<T> DoPathFinderStep();

        List<string> FindPath(string startNode, string endNode, out int totalDistance);

        List<string> FindShortestPath(T startNode, T endNode, out int totalDistance);

        //Constructor has nodes as arguments
    }
}