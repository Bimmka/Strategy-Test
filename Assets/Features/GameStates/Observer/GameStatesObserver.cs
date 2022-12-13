using System;
using UnityEngine;
using Zenject;

namespace Features.GameStates.Observer
{
  public class GameStatesObserver : MonoBehaviour
  {
    private IGameStateMachine gameStateMachine;

    [Inject]
    public void Construct(IGameStateMachine gameStateMachine)
    {
      this.gameStateMachine = gameStateMachine;
    }

    private void Awake()
    {
      DontDestroyOnLoad(this);
    }
  }
}