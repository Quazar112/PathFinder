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

        PathFinderStep<T> DoPathFinderStep();

        IReadOnlyList<string> FindShortestPath(T startNode, T endNode, out float totalDistance);

    }
}