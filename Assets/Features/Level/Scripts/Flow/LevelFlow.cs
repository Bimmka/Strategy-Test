using Features.GameStates;
using Features.GameStates.States;
using Features.Services.Cleanup;
using Features.Ship.Data.InputBindings;
using Features.Ship.Data.Settings;
using Features.Ship.Scripts.Base;
using Features.Ship.Scripts.Modules.Data;
using Features.Ship.Scripts.Spawn;
using Features.Ship.Scripts.Weapons.Data;
using UnityEngine;

namespace Features.Level.Scripts.Flow
{
  public class LevelFlow : ICleanup
  {
    private readonly ShipSpawner shipSpawner;
    private readonly ICleanupService cleanupService;
    private readonly IGameStateMachine gameStateMachine;
    private SpawnedPlayersContainer playersContainer;

    public LevelFlow(ShipSpawner shipSpawner, ICleanupService cleanupService, IGameStateMachine gameStateMachine)
    {
      this.shipSpawner = shipSpawner;
      this.cleanupService = cleanupService;
      this.gameStateMachine = gameStateMachine;
      cleanupService.Register(this);
    }

    public void Cleanup()
    {
      playersContainer.FirstPlayer.Disabled -= EndGame;
      playersContainer.SecondPlayer.Disabled -= EndGame;
    }

    public void StartGame()
    {
      SpawnPlayers();
    }

    public void EndGame()
    {
      DisablePlayers();
      cleanupService.CleanupElements();
      gameStateMachine.Enter<GameEndState>();
    }

    private void SpawnPlayers()
    {
      WeaponType[] weapons = new WeaponType[] {WeaponType.Gun, WeaponType.RocketLauncher};
      ModuleType[] modules = new ModuleType[] {ModuleType.AddHealth, ModuleType.AddShield};
      
      ShipPresenter firstPlayer = shipSpawner.Create(ShipType.Small, weapons, modules, PlayerType.First, Vector3.left * 10);
      ShipPresenter secondPlayer = shipSpawner.Create(ShipType.Big, weapons, modules, PlayerType.Second, Vector3.right * 10);
      firstPlayer.Disabled += EndGame;
      secondPlayer.Disabled += EndGame;
      playersContainer = new SpawnedPlayersContainer(firstPlayer, secondPlayer);
    }

    private void DisablePlayers()
    {
      playersContainer.FirstPlayer.Disable();
      playersContainer.SecondPlayer.Disable();
    }
  }
}