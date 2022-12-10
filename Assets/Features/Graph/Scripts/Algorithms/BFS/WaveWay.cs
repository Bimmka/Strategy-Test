using Features.Graph.Scripts.Vertex.Data;

namespace Features.Graph.Scripts.Algorithms.BFS
{
  public readonly struct WaveWay
  {
    public readonly int EdgeCount;
    public readonly int TransferCount;
    public readonly Edge LastEdge;

    public WaveWay(Edge lastEdge)
    {
      EdgeCount = 1;
      TransferCount = 0;
      LastEdge = lastEdge;
    }

    public WaveWay(int edgeCount, Edge lastEdge, int transferCount)
    {
      EdgeCount = edgeCount;
      TransferCount = transferCount;
      LastEdge = lastEdge;
    }
  }
}