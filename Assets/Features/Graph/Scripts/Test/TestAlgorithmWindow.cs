using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Features.Graph.Scripts.Test
{
  public class TestAlgorithmWindow : MonoBehaviour
  {
    [SerializeField] private Button startAlgorithmButton;
    [SerializeField] private TMP_InputField startVertexInputField;
    [SerializeField] private TMP_InputField endVertexInputField;
    [SerializeField] private TextMeshProUGUI outPutText;
    [SerializeField] private TextMeshProUGUI errorText;
    [SerializeField] private TestAlgorithm testAlgorithm;

    private void Awake()
    {
      startAlgorithmButton.onClick.AddListener(StartAlgorithm);
    }

    private void OnDestroy()
    {
      startAlgorithmButton.onClick.RemoveListener(StartAlgorithm);
    }

    private void StartAlgorithm()
    {
      if (int.TryParse(startVertexInputField.text, out int startVertex) == false)
      {
        errorText.text = "Start Vertex Invalid";
        return;
      }
      
      if (int.TryParse(endVertexInputField.text, out int endVertex) == false)
      {
        errorText.text = "End Vertex Invalid";
        return;
      }

      if (endVertex == startVertex)
      {
        errorText.text = "Vertices Are Equal";
        return;
      }

      (int, int) distances = testAlgorithm.Distance(startVertex, endVertex);
      (int, int) cachedDistances = testAlgorithm.CachedDistance(startVertex, endVertex);

      errorText.text = "";
      outPutText.text = $"Calculated Distances:\nBFS Vertexes {distances.Item1}, Transfers: {distances.Item2}\n" +
                        $"Cached BFS Vertexes {cachedDistances.Item1}, Transfers: {cachedDistances.Item2}";
    }
  }
}