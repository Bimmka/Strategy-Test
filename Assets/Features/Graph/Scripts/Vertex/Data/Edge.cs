using System;
using Sirenix.OdinInspector;

namespace Features.Graph.Scripts.Vertex.Data
{
  [Serializable]
  public struct Edge
  {
    public string NextVertex;
    [ReadOnly]
    public int Weight;

    public Edge(string nextVertex, int weight)
    {
      NextVertex = nextVertex;
      Weight = weight;
    }
  }
}