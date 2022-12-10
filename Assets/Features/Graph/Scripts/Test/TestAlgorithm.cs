using System;
using Features.Graph.Data;
using Features.Graph.Scripts.Algorithms.BFS;
using UnityEngine;

namespace Features.Graph.Scripts.Test
{
  public class TestAlgorithm : MonoBehaviour
  {
    [SerializeField] private GraphData graphData;

    private readonly CachedBFSAlgorithm cachedAlgorithm = new CachedBFSAlgorithm();
    private readonly BFSAlgorithm algorithm = new BFSAlgorithm();

    private void Awake()
    {
      cachedAlgorithm.Initialize(graphData.Vertexes);
    }

    public (int, int) CachedDistance(int startVertex, int endVertex)
    {
      if (IsValidVertex(startVertex) && (IsValidVertex(endVertex)))
        return cachedAlgorithm.Distances(startVertex, endVertex);
      return (-1, -1);
    }

    public (int, int) Distance(int startVertex, int endVertex)
    {
      if (IsValidVertex(startVertex) && (IsValidVertex(endVertex)))
        return algorithm.CalculateDistances(startVertex, endVertex, graphData.Vertexes);
      return (-1, -1);
    }

    private bool IsValidVertex(int vertex) => 
      graphData.Vertexes.ContainsKey(vertex);
  }
}