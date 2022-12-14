using Features.GameStates;
using Features.GameStates.States;
using Features.Services.Cleanup;
using Features.Services.ShipParts;
using Features.Services.UI.Factory;
using Features.Services.UI.Windows;
using Features.Ship.Data.InputBindings;
using Features.Ship.Scripts.Base;
using Features.Ship.Scripts.Spawn;
using UnityEngine;

namespace Features.Level.Scripts.Flow
{
  public class LevelFlow : ICleanup
  {
    private readonly ShipSpawner shipSpawner;
    private readonly IGameStateMachine gameStateMachine;
    private readonly IWindowsService windowsService;
    private readonly IShipChosenPartsService shipParts;
    private SpawnedPlayersContainer playersContainer;

    public LevelFlow(ShipSpawner shipSpawner, ICleanupService cleanupService, IGameStateMachine gameStateMachine, IWindowsService windowsService, 
      IShipChosenPartsService shipParts)
    {
      this.shipSpawner = shipSpawner;
      this.gameStateMachine = gameStateMachine;
      this.windowsService = windowsService;
      this.shipParts = shipParts;
      cleanupService.Register(this);
    }

    public void Cleanup()
    {
      playersContainer.FirstPlayer.Disabled -= EndGame;
      playersContainer.SecondPlayer.Disabled -= EndGame;
    }

    public void StartGame()
    {
      CreateHUD();
      SpawnPlayers();
    }

    public void EndGame()
    {
      gameStateMachine.Enter<GameEndState>();
    }

    private void CreateHUD() => 
      windowsService.Open(WindowId.HUD);

    private void SpawnPlayers()
    {
      ShipPresenter firstPlayer = shipSpawner.Create(shipParts.FirstPlayerParts, PlayerType.First, Vector3.left * 10);
      ShipPresenter secondPlayer = shipSpawner.Create(shipParts.SecondPlayerParts, PlayerType.Second, Vector3.right * 10);
      firstPlayer.Disabled += EndGame;
      secondPlayer.Disabled += EndGame;
      playersContainer = new SpawnedPlayersContainer(firstPlayer, secondPlayer);
    }
  }
}