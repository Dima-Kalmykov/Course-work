using System.Collections.Generic;

namespace WindowsFormsApp1
{
    public class GraphForAlg
    {
        internal int vertexCount;
        internal List<int>[] listAdjacency;

        internal GraphForAlg(int vertexCount)
        {
            this.vertexCount = vertexCount;

            listAdjacency = new List<int>[vertexCount];

            for (var i = 0; i < vertexCount; i++)
            {
                listAdjacency[i] = new List<int>();
            }
        }

        internal void AddEdge(int v, int w) =>
            listAdjacency[v].Add(w);

        private void DFSUtil(int vertexNumber, bool[] visited)
        {
            visited[vertexNumber] = true;

            int n;

            foreach (var el in listAdjacency[vertexNumber])
            {
                n = el;
                if (!visited[n])
                    DFSUtil(n, visited);
            }
        }

        private GraphForAlg GetTranspose()
        {
            GraphForAlg g = new GraphForAlg(vertexCount);

            for (int v = 0; v < vertexCount; v++)
                foreach (var el in listAdjacency[v])
                    g.listAdjacency[el].Add(v);

            return g;
        }

        internal bool IsStronglyConnection()
        {
            bool[] visited = new bool[vertexCount];

            for (int i = 0; i < vertexCount; i++)
                visited[i] = false;

            DFSUtil(0, visited);

            for (int i = 0; i < vertexCount; i++)
                if (!visited[i])
                    return false;

            GraphForAlg gr = GetTranspose();

            for (int i = 0; i < vertexCount; i++)
                visited[i] = false;

            gr.DFSUtil(0, visited);

            for (int i = 0; i < vertexCount; i++)
                if (!visited[i])
                    return false;

            return true;
        }
    }
}