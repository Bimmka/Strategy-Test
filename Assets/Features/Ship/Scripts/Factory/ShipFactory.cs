using System;
using Features.Bullet.Scripts.Spawner;
using Features.CustomCoroutine;
using Features.Services.Assets;
using Features.Services.StaticData;
using Features.Ship.Data.InputBindings;
using Features.Ship.Data.Settings;
using Features.Ship.Scripts.Base;
using Features.Ship.Scripts.Damage;
using Features.Ship.Scripts.Health;
using Features.Ship.Scripts.Input.Scripts;
using Features.Ship.Scripts.Modules.Data;
using Features.Ship.Scripts.Modules.Scripts.Container;
using Features.Ship.Scripts.Modules.Scripts.Element;
using Features.Ship.Scripts.Move.Data;
using Features.Ship.Scripts.Move.Scripts;
using Features.Ship.Scripts.Rotate.Data;
using Features.Ship.Scripts.Rotate.Scripts;
using Features.Ship.Scripts.Shield;
using Features.Ship.Scripts.Weapons.Container;
using Features.Ship.Scripts.Weapons.Data;
using Features.Ship.Scripts.Weapons.Elements;
using Features.Ship.Scripts.Weapons.Marker;
using UnityEngine;

namespace Features.Ship.Scripts.Factory
{
  public class ShipFactory
  {
    private readonly IAssetProvider assetProvider;
    private readonly IStaticDataService staticDataService;
    private readonly BulletSpawner bulletSpawner;

    public ShipFactory(IAssetProvider assetProvider, IStaticDataService staticDataService, BulletSpawner bulletSpawner)
    {
      this.assetProvider = assetProvider;
      this.staticDataService = staticDataService;
      this.bulletSpawner = bulletSpawner;
    }

    public ShipPresenter Create(ShipType shipType, WeaponType[] weaponTypes, ModuleType[] moduleTypes, PlayerType playerType,
      Transform parent, ShipPresenter prefab, Vector3 at)
    {
      ShipSettings shipSettings = staticDataService.ForShip(shipType);
      ShipPresenter spawnedShip = Presenter(prefab, at, parent);
      ShipView view = View(shipSettings.View, spawnedShip.transform);
      ShipInput input = Input(playerType);
      ShipRotate rotate = Rotate(spawnedShip.transform, shipSettings.RotateSettings);
      ShipMove move = Move(spawnedShip.transform, shipSettings.MoveSettings, rotate, spawnedShip.GetComponent<CharacterController>());
      ShipWeapons weapons = Weapons(weaponTypes, playerType, view.FirePointMarkers, bulletSpawner);
      ShipModules modules = Modules(moduleTypes);
      ShipHealth health = ShipHealth(shipSettings.MaxHealth);
      ShipShield shield = ShipShield(shipSettings.MaxShield, 1, shipSettings.ShieldRegenPerSecond);
      ShipDamageReceiver damageReceiver = ShipDamageReceiver(health, shield);
      ShipModel model = ShipModel(health, shield, damageReceiver, input, move, weapons, modules, playerType);
      spawnedShip.Construct(view, model);
      return spawnedShip;
    }

    private ShipPresenter Presenter(ShipPresenter prefab, Vector3 at, Transform parent) => 
      assetProvider.Instantiate(prefab, at, Quaternion.identity, parent);

    private ShipView View(ShipView view, Transform shipPresenter) => 
      assetProvider.Instantiate(view, shipPresenter);

    private ShipInput Input(PlayerType playerType)
    {
      ShipControlBindings bindings = staticDataService.ForInput(playerType);
      return new ShipInput(bindings);
    }

    private ShipRotate Rotate(Transform ship, ShipRotateSettings rotateSettings) => 
      new ShipRotate(ship, rotateSettings);

    private ShipMove Move(Transform ship, ShipMoveSettings moveSettings, ShipRotate rotate, CharacterController shipController) => 
      new ShipMove(ship, moveSettings, rotate, shipController);

    private ShipWeapons Weapons(WeaponType[] weaponTypes, PlayerType playerType, ShipFirePointMarker[] markers, BulletSpawner bulletSpawner)
    {
      Weapon[] weapons = new Weapon[weaponTypes.Length];

      for (int i = 0; i < weaponTypes.Length; i++)
      {
        weapons[i] = Weapon(staticDataService.ForWeapon(weaponTypes[i]), playerType, markers[i].transform, bulletSpawner);
      }
      
      return new ShipWeapons(weapons);
    }

    private Weapon Weapon(WeaponSettings weaponSettings, PlayerType playerType, Transform firePoint, BulletSpawner bulletSpawner)
    {
      switch (weaponSettings.Type)
      {
        case WeaponType.Gun:
          return new Gun(weaponSettings, playerType, firePoint, bulletSpawner);
        case WeaponType.RocketLauncher:
          return new RocketLauncher(weaponSettings, playerType, firePoint, bulletSpawner);
        case WeaponType.LaserLauncher:
          return new LaserLauncher(weaponSettings, playerType, firePoint, bulletSpawner);
        default:
          throw new ArgumentOutOfRangeException();
      }
      
    }

    private ShipModules Modules(ModuleType[] moduleTypes)
    {
      Module[] modules = new Module[moduleTypes.Length];

      for (int i = 0; i < moduleTypes.Length; i++)
      {
        modules[i] = Module(staticDataService.ForModule(moduleTypes[i]));
      }
      
      return new ShipModules(modules);
    }

    private Module Module(ModuleSettings moduleSettings)
    {
      return new Module();
    }

    private ShipHealth ShipHealth(int healthCount) => 
      new ShipHealth(healthCount);
    
    private ShipShield ShipShield(int shieldCount, int reloadTime, int restoreValuePerSecond) => 
      new ShipShield(shieldCount, reloadTime, restoreValuePerSecond);

    private ShipDamageReceiver ShipDamageReceiver(ShipHealth health, ShipShield shield) => 
      new ShipDamageReceiver(health, shield);

    private ShipModel ShipModel(ShipHealth health, ShipShield shield, ShipDamageReceiver damageReceiver, ShipInput input, ShipMove move,
      ShipWeapons weapons, ShipModules modules, PlayerType playerType) => 
      new ShipModel(health, shield, damageReceiver, input, move, weapons, modules, playerType);
  }
}