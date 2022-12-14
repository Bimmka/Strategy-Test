using Features.Services.ShipParts;
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

    public ShipPresenter Create(ShipChosenParts chosenParts, PlayerType playerType, Vector3 at, Quaternion rotation) => 
      factory.Create(chosenParts.ShipType, chosenParts.WeaponTypes, chosenParts.ModuleTypes, playerType, 
        shipSpawnParent, shipPresenterPrefab, at, rotation);
  }
}