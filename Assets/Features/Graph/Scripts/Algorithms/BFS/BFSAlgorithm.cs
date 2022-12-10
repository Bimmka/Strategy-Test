using System.Collections.Generic;
using Features.Graph.Data;
using Features.Graph.Scripts.Vertex.Data;

namespace Features.Graph.Scripts.Algorithms.BFS
{
  public class BFSAlgorithm
  {
    public (int,int) CalculateDistances(int startVertex, int endVertex, Dictionary<int, VertexData> allVertices)
    {
      bool[] visitedVertex = new bool[allVertices.Count]; 
      Queue<WaveWay> waysQueue = new Queue<WaveWay>(allVertices.Count);
      VertexData vertex = allVertices[startVertex];
      WaveWay way;
      WaveWay temporalWay;
      visitedVertex[startVertex] = true;
      for (int i = 0; i < vertex.Edges.Length; i++)
      {
        way = new WaveWay(vertex.Edges[i]);
        visitedVertex[vertex.Edges[i].NextVertex] = true;
        if (IsEndVertex(endVertex,vertex.Edges[i].NextVertex))
          return Distances(way);
        waysQueue.Enqueue(way);
      }
      
      while (waysQueue.Count > 0)
      {
        way = waysQueue.Dequeue();
        vertex = allVertices[way.LastEdge.NextVertex];

        for (int i = 0; i < vertex.Edges.Length; i++)
        {
          if (visitedVertex[vertex.Edges[i].NextVertex])
            continue;
          
          temporalWay = NewWay(way, vertex.Edges[i]);
          visitedVertex[vertex.Edges[i].NextVertex] = true;
          
          if (IsEndVertex(endVertex, vertex.Edges[i].NextVertex))
            return Distances(temporalWay);
          waysQueue.Enqueue(temporalWay);
        }
      }

      return (-1, -1);
    }

    private (int, int) Distances(WaveWay way) => 
      (way.EdgeCount, way.TransferCount);

    private bool IsEndVertex(int endVertex, int currentVertex) => 
      endVertex == currentVertex;

    private bool IsTransfer(GraphLineType currentLineTypes, GraphLineType nextLineTypes) => 
      currentLineTypes != nextLineTypes;

    private WaveWay NewWay(WaveWay way, Edge newEdge) =>
      new WaveWay
      (
        way.EdgeCount + 1,
        newEdge,
        IsTransfer(way.LastEdge.LineType, newEdge.LineType) ? way.TransferCount + 1 : way.TransferCount
      );
  }
}