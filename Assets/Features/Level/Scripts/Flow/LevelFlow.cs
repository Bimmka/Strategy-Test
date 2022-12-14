using Features.GameStates;
using Features.GameStates.States;
using Features.Level.Data;
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
    private readonly LevelSettings levelSettings;
    private SpawnedPlayersContainer playersContainer;

    public LevelFlow(ShipSpawner shipSpawner, ICleanupService cleanupService, IGameStateMachine gameStateMachine, IWindowsService windowsService, 
      IShipChosenPartsService shipParts, LevelSettings levelSettings)
    {
      this.shipSpawner = shipSpawner;
      this.gameStateMachine = gameStateMachine;
      this.windowsService = windowsService;
      this.shipParts = shipParts;
      this.levelSettings = levelSettings;
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

    private void EndGame()
    {
      StopPlayers();
      gameStateMachine.Enter<GameEndState>();
    }

    private void CreateHUD() => 
      windowsService.Open(WindowId.HUD);

    private void SpawnPlayers()
    {
      ShipPresenter firstPlayer = shipSpawner.Create(shipParts.FirstPlayerParts, PlayerType.First, 
        levelSettings.FirstPlayerSpawnPosition, Quaternion.Euler(levelSettings.FirstPlayerSpawnRotation));
      ShipPresenter secondPlayer = shipSpawner.Create(shipParts.SecondPlayerParts, PlayerType.Second, 
        levelSettings.SecondPlayerSpawnPosition, Quaternion.Euler(levelSettings.SecondPlayerSpawnRotation));
      firstPlayer.Disabled += EndGame;
      secondPlayer.Disabled += EndGame;
      playersContainer = new SpawnedPlayersContainer(firstPlayer, secondPlayer);
    }

    private void StopPlayers()
    {
      playersContainer.FirstPlayer.Stop();
      playersContainer.SecondPlayer.Stop();
    }
  }
}