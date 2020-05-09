using System;
using System.Collections.Generic;
using System.Linq;

namespace WindowsFormsApp1
{
    public class SpecialKindOfGraphForCheckStronglyDirection
    {
        internal int VertexCount;
        internal List<int>[] AdjacencyList;

        internal SpecialKindOfGraphForCheckStronglyDirection(int vertexCount)
        {
            VertexCount = vertexCount;

            AdjacencyList = new List<int>[vertexCount];

            for (var i = 0; i < vertexCount; i++)
            {
                AdjacencyList[i] = new List<int>();
            }
        }

        internal void AddEdge(int firstVertex, int secondVertex) =>
            AdjacencyList[firstVertex].Add(secondVertex);

        internal bool IsStronglyConnection()
        {
            var visited = new bool[VertexCount];

            Array.Clear(visited, 0, VertexCount);

            DfsUtil(0, visited);

            if (!HasNotHaveUnvisitedVertex(visited))
            {
                return false;
            }

            var transposeGraph = GetTranspose();

            Array.Clear(visited, 0, VertexCount);

            transposeGraph.DfsUtil(0, visited);

            return HasNotHaveUnvisitedVertex(visited);
        }

        private bool HasNotHaveUnvisitedVertex(IReadOnlyList<bool> visited)
        {
            for (var i = 0; i < VertexCount; i++)
            {
                {
                    if (!visited[i])
                        return false;
                }
            }

            return true;
        }

        private void DfsUtil(int vertexNumber, IList<bool> visited)
        {
            visited[vertexNumber] = true;

            foreach (var n in AdjacencyList[vertexNumber].Where(n => !visited[n]))
            {
                DfsUtil(n, visited);
            }
        }

        private SpecialKindOfGraphForCheckStronglyDirection GetTranspose()
        {
            var graph = new SpecialKindOfGraphForCheckStronglyDirection(VertexCount);

            for (var i = 0; i < VertexCount; i++)
            {
                foreach (var vertex in AdjacencyList[i])
                {
                    graph.AdjacencyList[vertex].Add(i);
                }
            }

            return graph;
        }
    }
}