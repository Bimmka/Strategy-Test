using System;
using Features.Graph.Data;

namespace Features.Graph.Scripts.Vertex.Data
{
  [Serializable]
  public struct Edge
  {
    public int NextVertex;
    public GraphLineType LineType;

    public Edge(int nextVertex, GraphLineType lineType)
    {
      NextVertex = nextVertex;
      LineType = lineType;
    }
  }
}