using Features.Ship.Data.InputBindings;
using Features.Ship.Data.Settings;
using Features.Ship.Scripts.Base;
using Features.Ship.Scripts.Factory;
using Features.Ship.Scripts.Modules.Data;
using Features.Ship.Scripts.Weapons.Data;
using UnityEngine;

namespace Features.Ship.Scripts.Spawn
{
  public class ShipSpawner
  {
    private readonly ShipFactory factory;
    private readonly ShipPresenter shipPresenterPrefab;
    private readonly Transform shipSpawnParent;

    public ShipSpawner(ShipFactory factory, ShipPresenter shipPresenterPrefab, Transform shipSpawnParent)
    {
      this.factory = factory;
      this.shipPresenterPrefab = shipPresenterPrefab;
      this.shipSpawnParent = shipSpawnParent;
    }

    public ShipPresenter Create(ShipType shipType, WeaponType[] weapons, ModuleType[] modules, PlayerType playerType) => 
      factory.Create(shipType, weapons, modules, playerType, shipSpawnParent, shipPresenterPrefab);
  }
}