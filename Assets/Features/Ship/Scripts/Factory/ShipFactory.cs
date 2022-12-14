using System;
using System.Collections.Generic;
using Features.Bullet.Scripts.Spawner;
using Features.Services.Assets;
using Features.Services.Cleanup;
using Features.Services.StaticData;
using Features.Services.UI.Factory;
using Features.Services.UI.Windows;
using Features.Ship.Data.InputBindings;
using Features.Ship.Data.Settings;
using Features.Ship.Scripts.Base;
using Features.Ship.Scripts.Characteristics;
using Features.Ship.Scripts.Damage;
using Features.Ship.Scripts.Disable;
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
using Features.UI.Windows.HUD.Scripts;
using UnityEngine;

namespace Features.Ship.Scripts.Factory
{
  public class ShipFactory
  {
    private readonly IAssetProvider assetProvider;
    private readonly IStaticDataService staticDataService;
    private readonly BulletSpawner bulletSpawner;
    private readonly ICleanupService cleanupService;
    private readonly IWindowsService windowsService;

    public ShipFactory(IAssetProvider assetProvider, IStaticDataService staticDataService, BulletSpawner bulletSpawner, ICleanupService cleanupService, 
      IWindowsService windowsService)
    {
      this.assetProvider = assetProvider;
      this.staticDataService = staticDataService;
      this.bulletSpawner = bulletSpawner;
      this.cleanupService = cleanupService;
      this.windowsService = windowsService;
    }

    public ShipPresenter Create(ShipType shipType, WeaponType[] weaponTypes, ModuleType[] moduleTypes, PlayerType playerType,
      Transform parent, ShipPresenter prefab, Vector3 at)
    {
      ShipSettings shipSettings = staticDataService.ForShip(shipType);
      ShipPresenter spawnedShip = Presenter(prefab, at, parent);
      ShipCharacteristics characteristics = ShipCharacteristics(shipSettings, weaponTypes, moduleTypes);
      ShipView view = View(shipSettings.View, spawnedShip.transform);
      ShipInput input = Input(playerType);
      ShipRotate rotate = Rotate(spawnedShip.transform, shipSettings.RotateSettings);
      ShipMove move = Move(spawnedShip.transform, shipSettings.MoveSettings, rotate, spawnedShip.GetComponent<Rigidbody>());
      ShipModules modules = Modules(characteristics.Modules);
      characteristics = UpdateCharacteristicsByModules(characteristics, modules);
      ShipWeapons weapons = Weapons(characteristics.Weapons, playerType, view.FirePointMarkers, bulletSpawner);
      ShipHealth health = ShipHealth(characteristics.Health);
      ShipShield shield = ShipShield(characteristics.Shield);
      ConstructShipCharacteristicsDisplayer((UIHUD) windowsService.Window(WindowId.HUD), health, shield, playerType);
      ShipDamageReceiver damageReceiver = ShipDamageReceiver(health, shield);
      ShipDestroyer destroyer = ShipDestroyer(spawnedShip);
      ShipModel model = ShipModel(health, shield, damageReceiver, input, move, weapons, modules, characteristics, destroyer, playerType,
        cleanupService);
      spawnedShip.Construct(view, model);
      return spawnedShip;
    }

    private ShipPresenter Presenter(ShipPresenter prefab, Vector3 at, Transform parent) => 
      assetProvider.Instantiate(prefab, at, Quaternion.identity, parent);

    private ShipCharacteristics ShipCharacteristics(ShipSettings settings, WeaponType[] weaponTypes, ModuleType[] moduleTypes)
    {
      ShipCharacteristics characteristics = new ShipCharacteristics();
      characteristics.InitializeHealth(settings.MaxHealth);
      characteristics.InitializeShield(settings.MaxShield, settings.ShieldRegenPerSecond, settings.ShieldRegenTime);
      WeaponSettings weaponSettings;
      for (int i = 0; i < weaponTypes.Length; i++)
      {
        weaponSettings = staticDataService.ForWeapon(weaponTypes[i]);
        characteristics.InitializeWeapon(weaponTypes[i], weaponSettings.Damage, weaponSettings.ShootCooldown);
      }
      
      ModuleSettings moduleSettings;
      for (int i = 0; i < moduleTypes.Length; i++)
      {
        moduleSettings = staticDataService.ForModule(moduleTypes[i]);
        characteristics.InitializeModule(moduleTypes[i], moduleSettings.Value);
      }

      return characteristics;
    }

    private ShipView View(ShipView view, Transform shipPresenter) => 
      assetProvider.Instantiate(view, shipPresenter);

    private ShipInput Input(PlayerType playerType)
    {
      ShipControlBindings bindings = staticDataService.ForInput(playerType);
      return new ShipInput(bindings);
    }

    private ShipRotate Rotate(Transform ship, ShipRotateSettings rotateSettings) => 
      new ShipRotate(ship, rotateSettings);

    private ShipMove Move(Transform ship, ShipMoveSettings moveSettings, ShipRotate rotate, Rigidbody shipBody) => 
      new ShipMove(ship, moveSettings, rotate, shipBody);

    private ShipModules Modules(List<ModuleCharacteristics> moduleTypes)
    {
      Module[] modules = new Module[moduleTypes.Count];

      for (int i = 0; i < moduleTypes.Count; i++)
      {
        modules[i] = Module(moduleTypes[i]);
      }
      
      return new ShipModules(modules);
    }

    private Module Module(ModuleCharacteristics characteristic)
    {
      switch (characteristic.Type)
      {
        case ModuleType.AddHealth:
          return new AdditionalHealthModule(characteristic.Type, characteristic.Value);
        case ModuleType.AddShield:
          return new AdditionalShieldModule(characteristic.Type, characteristic.Value);
        case ModuleType.WeaponReloadCooldown:
          return new ReduceWeaponReloadModule(characteristic.Type, characteristic.Value);
        case ModuleType.ShieldRestoreValue:
          return new IncreaseShieldRestoreValueModule(characteristic.Type, characteristic.Value);
        default:
          throw new ArgumentOutOfRangeException();
      }
    }

    private ShipCharacteristics UpdateCharacteristicsByModules(ShipCharacteristics characteristics, ShipModules modules) => 
      modules.ApplyModules(characteristics);

    private ShipWeapons Weapons(List<WeaponCharacteristics> weaponTypes, PlayerType playerType, ShipFirePointMarker[] markers, BulletSpawner bulletSpawner)
    {
      Weapon[] weapons = new Weapon[weaponTypes.Count];

      for (int i = 0; i < weaponTypes.Count; i++)
      {
        weapons[i] = Weapon(staticDataService.ForWeapon(weaponTypes[i].Type), playerType, markers[i].transform, bulletSpawner);
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

    private ShipHealth ShipHealth(HealthCharacteristics healthCharacteristics) => 
      new ShipHealth(healthCharacteristics.Max);
    private ShipShield ShipShield(ShieldCharacteristics characteristics) => 
      new ShipShield(characteristics.Max, characteristics.RestoreValue, characteristics.RestoreTime);

    private ShipDestroyer ShipDestroyer(ShipPresenter shipPresenter) => 
      new ShipDestroyer(shipPresenter);
    
    private void ConstructShipCharacteristicsDisplayer(UIHUD displayer, ShipHealth health, ShipShield shield, PlayerType playerType)
    {
      if (playerType == PlayerType.First)
        displayer.ConstructFirstPlayerDisplayer(shield, health);
      else
        displayer.ConstructSecondPlayerDisplayer(shield, health);
    }

    private ShipDamageReceiver ShipDamageReceiver(ShipHealth health, ShipShield shield) => 
      new ShipDamageReceiver(health, shield);

    private ShipModel ShipModel(ShipHealth health, ShipShield shield, ShipDamageReceiver damageReceiver,
      ShipInput input, ShipMove move, ShipWeapons weapons, ShipModules modules, ShipCharacteristics characteristics, ShipDestroyer destroyer,
      PlayerType playerType, ICleanupService cleanupService) => 
      new ShipModel(health, shield, damageReceiver, input, move, weapons, modules, characteristics, destroyer, playerType, cleanupService);
  }
}