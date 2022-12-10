using System;
using System.Collections.Generic;
using System.Linq;
using Features.Graph.Data;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Features.Graph.Scripts.Vertex.Data
{
  [CreateAssetMenu(fileName = "VertexData", menuName = "StaticData/Graph/Create Vertex", order = 52)]
  public class VertexData : ScriptableObject
  {
    public int ID;
    [ValidateInput("ValidateVertex", "$errorMessage", IncludeChildren = true)]
    public Edge[] Edges;

    private string errorMessage = "Error";

    private bool ValidateVertex(Edge[] edges)
    {
      if (edges == null || edges.Length == 0)
        return true;

      int count;
      for (int i = 0; i < edges.Length; i++)
      {
        edges[i] = new Edge(edges[i].NextVertex, edges[i].LineType);
        if (edges[i].NextVertex == ID)
        {
          errorMessage = $"Contains Circle To This Vertex In Edge {i}";
          return false;
        }
        
        count = edges.Count(x => x.NextVertex == edges[i].NextVertex);
        if (count > 1)
        {
          errorMessage = $"Have Duplicate Edge To ID {edges[i].NextVertex}";
          return false;
        }
      }

      return true;
    }

    private static IEnumerable<GraphLineType> Lines()
    {
      string[] values = Enum.GetNames(typeof(GraphLineType));
      List<GraphLineType> types = new List<GraphLineType>(values.Length);
      for (int i = 0; i < values.Length; i++)
      {
        types.Add((GraphLineType) Enum.Parse(typeof(GraphLineType),values[i]));
      }

      return types;
    }
  }
}