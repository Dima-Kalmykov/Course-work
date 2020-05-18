using System;
using System.Collections.Generic;
using System.Linq;

namespace WindowsFormsApp1
{
    public class SpecialKindOfGraphForCheckStronglyDirection
    {
        public int VertexCount;
        public List<int>[] AdjacencyList;

        private static bool[] used;

        internal SpecialKindOfGraphForCheckStronglyDirection(int vertexCount)
        {
            VertexCount = vertexCount;

            AdjacencyList = new List<int>[vertexCount];

            for (var i = 0; i < vertexCount; i++)
            {
                AdjacencyList[i] = new List<int>();
            }

            used = new bool[vertexCount];
        }

        internal int GetConnectedComponentsAmount()
        {
            var count = 0;

            for (var i = 0; i < VertexCount; i++)
            {
                used[i] = false;
            }

            for (var i = 0; i < VertexCount; i++)
            {
                if (used[i])
                {
                    continue;
                }
                Dfs(i);
                count++;
            }

            return count;
        }

        internal void AddEdge(int firstVertex, int secondVertex) =>
            AdjacencyList[firstVertex].Add(secondVertex);

        internal bool IsStronglyConnection()
        {
            var visited = new bool[VertexCount];

            Array.Clear(visited, 0, VertexCount);

            SimpleDfs(0, visited);

            if (!HasNotHaveUnvisitedVertex(visited))
            {
                return false;
            }

            var transposeGraph = TransposeCurrentGraph();

            Array.Clear(visited, 0, VertexCount);

            transposeGraph.SimpleDfs(0, visited);

            return HasNotHaveUnvisitedVertex(visited);
        }

        private void SimpleDfs(int vertexNumber, IList<bool> visited)
        {
            visited[vertexNumber] = true;

            foreach (var n in AdjacencyList[vertexNumber].Where(n => !visited[n]))
            {
                SimpleDfs(n, visited);
            }
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

        private SpecialKindOfGraphForCheckStronglyDirection TransposeCurrentGraph()
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

        private void Dfs(int cur)
        {
            used[cur] = true;
            for (var i = 0; i < AdjacencyList[cur].Count; i++)
            {
                var next = AdjacencyList[cur][i];
                if (!used[next])
                {
                    Dfs(next);
                }
            }
        }
    }
}