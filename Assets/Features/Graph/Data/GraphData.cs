using System.Collections.Generic;
using System.Linq;
using Features.Graph.Scripts.Vertex.Data;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Features.Graph.Data
{
  [CreateAssetMenu(fileName = "GraphData", menuName = "StaticData/Graph/Create Graph", order = 52)]
  public class GraphData : SerializedScriptableObject
  {
    [ValidateInput("ValidateVertexes", "$errorMessage", IncludeChildren = true)]
    public Dictionary<int,VertexData> Vertexes;

    private string errorMessage = "Error";

    private bool ValidateVertexes(Dictionary<int,VertexData> vertexes)
    {
      if (vertexes == null || vertexes.Count == 0)
        return true;

      int count;
      
      
      foreach (KeyValuePair<int,VertexData> vertex in vertexes)
      {
        if (vertex.Key != vertex.Value.ID)
        {
          errorMessage = $"Dictionary Key Does Not Equal Vertex ID. Key: {vertex.Key}, Vertex ID: {vertex.Value.ID} ";
          return false;
        }
        
        for (int j = 0; j < vertex.Value.Edges.Length; j++)
        {
          count = vertexes.Count(x => x.Key == vertex.Value.Edges[j].NextVertex);
          if (count > 1)
          {
            errorMessage = $"Have Duplicate Vertex With ID {vertex.Value.Edges[j].NextVertex}";
            return false;
          }

          if (count == 0)
          {
            errorMessage =
              $"Dont Have Vertex With ID {vertex.Value.Edges[j].NextVertex} While Vertex With ID {vertex.Value.ID} Has Edge To It";
            return false;
          }

          VertexData nextVertex = vertexes[vertex.Value.Edges[j].NextVertex];
          if (nextVertex.Edges.Contains(new Edge(vertex.Key, vertex.Value.Edges[j].LineType)) == false)
          {
            errorMessage = $"Vertex {nextVertex.ID} Does Not Have Edge Or Color Difference To Vertex {vertex.Key} While Back Edge Exists";
            return false;
          }
        }
      }
      return true;
    }
  }
}