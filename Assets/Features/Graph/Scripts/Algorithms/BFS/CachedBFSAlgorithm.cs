using System.Collections.Generic;
using Features.Graph.Data;
using Features.Graph.Scripts.Vertex.Data;

namespace Features.Graph.Scripts.Algorithms.BFS
{
  public class CachedBFSAlgorithm
  {
    private readonly Dictionary<PairVertex, WaveWay> verticesTable;
    
    public CachedBFSAlgorithm()
    {
      verticesTable = new Dictionary<PairVertex, WaveWay>(90);
    }

    public (int, int) Distances(int startVertex, int endVertex)
    {
      WaveWay way = Way(startVertex, endVertex);
      return (way.EdgeCount, way.TransferCount);
    }

    public void Initialize(Dictionary<int, VertexData> allVertices)
    {
      foreach (KeyValuePair<int,VertexData> vertex in allVertices)
      {
        CalculateDistances(vertex.Key, allVertices);
      }
    }

    private void CalculateDistances(int startVertex, Dictionary<int, VertexData> allVertices)
    {
      Queue<WaveWay> waysQueue = new Queue<WaveWay>(allVertices.Count);
      WaveWay way;
      WaveWay temporalWay;
      VertexData vertex = allVertices[startVertex];
      for (int i = 0; i < vertex.Edges.Length; i++)
      {
        if (IsVisited(vertex.ID, vertex.Edges[i].NextVertex))
          continue;
        
        way = new WaveWay(vertex.Edges[i]);
        if (startVertex != vertex.Edges[i].NextVertex)
          verticesTable.Add(new PairVertex(startVertex, vertex.Edges[i].NextVertex),way);
        waysQueue.Enqueue(way);
      }
      
      while (waysQueue.Count > 0)
      {
        way = waysQueue.Dequeue();
        vertex = allVertices[way.LastEdge.NextVertex];

        for (int i = 0; i < vertex.Edges.Length; i++)
        {
          if (IsVisited(startVertex,vertex.Edges[i].NextVertex))
            continue;
          
          temporalWay = NewWay(way, vertex.Edges[i]);
          if (startVertex != vertex.Edges[i].NextVertex)
            verticesTable.Add(new PairVertex(startVertex, vertex.Edges[i].NextVertex),temporalWay );
          
          waysQueue.Enqueue(temporalWay);
        }
      }
    }

    private bool IsTransfer(GraphLineType currentLineTypes, GraphLineType nextLineTypes) => 
      currentLineTypes != nextLineTypes;

    private bool IsVisited(int startVertex, int endVertex)
    {
      PairVertex pairVertex = new PairVertex(startVertex, endVertex);

      if (verticesTable.ContainsKey(pairVertex))
        return true;
      
      pairVertex = new PairVertex(endVertex, startVertex);

      return verticesTable.ContainsKey(pairVertex);
    }

    private WaveWay Way(int startVertex, int endVertex)
    {
      PairVertex pairVertex = new PairVertex(startVertex, endVertex);

      if (verticesTable.ContainsKey(pairVertex))
        return verticesTable[pairVertex];
      
      pairVertex = new PairVertex(endVertex, startVertex);
      if (verticesTable.ContainsKey(pairVertex))
        return verticesTable[pairVertex];
      
      return new WaveWay();
    }

    private WaveWay NewWay(WaveWay way, Edge newEdge) =>
      new WaveWay
      (
        way.EdgeCount + 1,
        newEdge,
        IsTransfer(way.LastEdge.LineType, newEdge.LineType) ? way.TransferCount + 1 : way.TransferCount
      );
    
    private readonly struct PairVertex
    {
      public readonly int First;
      public readonly int Second;

      public PairVertex(int first, int second)
      {
        First = first;
        Second = second;
      }
    }
  }
}