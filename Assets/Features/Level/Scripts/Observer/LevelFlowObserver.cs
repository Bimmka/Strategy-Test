using Features.Level.Scripts.Flow;
using UnityEngine;
using Zenject;

namespace Features.Level.Scripts.Observer
{
  public class LevelFlowObserver : MonoBehaviour
  {
    private LevelFlow levelFlow;

    [Inject]
    public void Construct(LevelFlow levelFlow)
    {
      this.levelFlow = levelFlow;
    }

    private void Start()
    {
      levelFlow.StartGame();
    }
  }
}