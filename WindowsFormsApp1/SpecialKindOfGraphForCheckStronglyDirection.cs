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

        /// <summary>
        /// Вычисляет число компонент.
        /// </summary>
        /// <returns> Количество компонент </returns>
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

        /// <summary>
        /// Добавляем ребро в граф.
        /// </summary>
        /// <param name="firstVertex"> Номер первой вершины </param>
        /// <param name="secondVertex"> Номер второй вершины </param>
        internal void AddEdge(int firstVertex, int secondVertex) =>
            AdjacencyList[firstVertex].Add(secondVertex);

        /// <summary>
        /// Проверка графа на сильную связность.
        /// </summary>
        /// <returns> True - если граф сильно свзяный, false - иначе </returns>
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

        /// <summary>
        /// Дфа для проверки графа на сильную связность.
        /// </summary>
        /// <param name="vertexNumber"> Номер вершины </param>
        /// <param name="visited"> Список посёщнных вершин </param>
        private void SimpleDfs(int vertexNumber, IList<bool> visited)
        {
            visited[vertexNumber] = true;

            foreach (var n in AdjacencyList[vertexNumber].Where(n => !visited[n]))
            {
                SimpleDfs(n, visited);
            }
        }

        /// <summary>
        /// Проверяет, есть ли непосещённые вершины.
        /// </summary>
        /// <param name="visited"> список посещённых вершин</param>
        /// <returns> True - если нет непосещённых вершин,
        /// false - иначе </returns>
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

        /// <summary>
        /// Транспонирует граф.
        /// </summary>
        /// <returns> Транспонированный граф </returns>
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

        /// <summary>
        /// Dfs для поиска компонентов связности.
        /// </summary>
        /// <param name="cur"> номер вершины </param>
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