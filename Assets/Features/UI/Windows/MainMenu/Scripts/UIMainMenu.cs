using Features.GameStates;
using Features.GameStates.States;
using Features.Services.ShipParts;
using Features.UI.Windows.Base;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Features.UI.Windows.MainMenu.Scripts
{
  public class UIMainMenu : BaseWindow
  {
    [SerializeField] private ShipPartChooseArea firstPlayerArea;
    [SerializeField] private ShipPartChooseArea secondPlayerArea;
    [SerializeField] private Button startGameButton;

    private IGameStateMachine gameStateMachine;
    private IShipChosenPartsService shipChosenPartsService;

    [Inject]
    public void Construct(IGameStateMachine gameStateMachine, IShipChosenPartsService shipChosenPartsService)
    {
      this.shipChosenPartsService = shipChosenPartsService;
      this.gameStateMachine = gameStateMachine;
    }

    protected override void Initialize()
    {
      base.Initialize();
      firstPlayerArea.Initialize();
      secondPlayerArea.Initialize();
    }

    protected override void Subscribe()
    {
      base.Subscribe();
      startGameButton.onClick.AddListener(StartGame);
      firstPlayerArea.Subscribe();
      secondPlayerArea.Subscribe();
    }

    protected override void Cleanup()
    {
      base.Cleanup();
      startGameButton.onClick.RemoveListener(StartGame);
      firstPlayerArea.Cleanup();
      secondPlayerArea.Cleanup();
    }

    private void StartGame()
    {
      shipChosenPartsService.SetFirstPlayerParts(firstPlayerArea.ChosenParts());
      shipChosenPartsService.SetSecondPlayerParts(secondPlayerArea.ChosenParts());
      gameStateMachine.Enter<GameLoadState>();
    }
  }
}