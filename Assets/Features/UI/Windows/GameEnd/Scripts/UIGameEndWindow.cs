using Features.GameStates;
using Features.GameStates.States;
using Features.Services.Cleanup;
using Features.UI.Windows.Base;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Features.UI.Windows.GameEnd.Scripts
{
  public class UIGameEndWindow : BaseWindow
  {
    [SerializeField] private Button finishGameButton;
    
    private IGameStateMachine gameStateMachine;
    private ICleanupService cleanupService;

    [Inject]
    public void Construct(IGameStateMachine gameStateMachine, ICleanupService cleanupService)
    {
      this.cleanupService = cleanupService;
      this.gameStateMachine = gameStateMachine;
    }

    protected override void Subscribe()
    {
      base.Subscribe();
      finishGameButton.onClick.AddListener(LoadMainMenu);
    }

    protected override void Cleanup()
    {
      base.Cleanup();
      finishGameButton.onClick.RemoveListener(LoadMainMenu);
    }

    private void LoadMainMenu()
    {
      cleanupService.CleanupElements();
      cleanupService.RemoveElements();
      gameStateMachine.Enter<MainMenuState>();
    }
  }
}