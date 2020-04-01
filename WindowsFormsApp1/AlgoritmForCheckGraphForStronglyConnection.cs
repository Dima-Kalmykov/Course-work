using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class GraphForAlg
    {
        internal int vertexCount;
        internal List<int>[] listAdj;

        internal GraphForAlg(int vertexCount)
        {
            this.vertexCount = vertexCount;

            listAdj = new List<int>[vertexCount];

            for (int i = 0; i < vertexCount; i++)
                listAdj[i] = new List<int>();
        }

        internal void AddEdge(int v, int w) =>
            listAdj[v].Add(w);

        private void DFSUtil(int vertexNumber, bool[] visited)
        {
            visited[vertexNumber] = true;

            int n;

            foreach (var el in listAdj[vertexNumber])
            {
                n = el;
                if (!visited[n])
                    DFSUtil(n, visited);
            }
        }

        private GraphForAlg getTranspose()
        {
            GraphForAlg g = new GraphForAlg(vertexCount);

            for (int v = 0; v < vertexCount; v++)
                foreach (var el in listAdj[v])
                    g.listAdj[el].Add(v);

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

            GraphForAlg gr = getTranspose();

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