using Features.Services.Assets;
using Features.Services.StaticData;
using Features.Ship.Data.InputBindings;
using Features.Ship.Data.Settings;
using Features.Ship.Scripts.Base;
using Features.Ship.Scripts.Input.Scripts;
using Features.Ship.Scripts.Modules.Data;
using Features.Ship.Scripts.Modules.Scripts.Container;
using Features.Ship.Scripts.Modules.Scripts.Element;
using Features.Ship.Scripts.Move.Data;
using Features.Ship.Scripts.Move.Scripts;
using Features.Ship.Scripts.Rotate.Data;
using Features.Ship.Scripts.Rotate.Scripts;
using Features.Ship.Scripts.Weapons.Container;
using Features.Ship.Scripts.Weapons.Data;
using Features.Ship.Scripts.Weapons.Elements;
using UnityEngine;

namespace Features.Ship.Scripts.Factory
{
  public class ShipFactory
  {
    private readonly IAssetProvider assetProvider;
    private readonly IStaticDataService staticDataService;

    public ShipFactory(IAssetProvider assetProvider, IStaticDataService staticDataService)
    {
      this.assetProvider = assetProvider;
      this.staticDataService = staticDataService;
    }

    public ShipPresenter Create(ShipType shipType, WeaponType[] weaponTypes, ModuleType[] moduleTypes,
      PlayerType playerType, Transform parent, ShipPresenter prefab)
    {
      ShipSettings shipSettings = staticDataService.ForShip(shipType);
      ShipPresenter spawnedShip = Presenter(prefab, parent);
      ShipView view = View(shipSettings.View, spawnedShip.transform);
      ShipInput input = Input(playerType);
      ShipRotate rotate = Rotate(spawnedShip.transform, shipSettings.RotateSettings);
      ShipMove move = Move(spawnedShip.transform, shipSettings.MoveSettings, rotate, spawnedShip.GetComponent<CharacterController>());
      ShipWeapons weapons = Weapons(weaponTypes);
      ShipModules modules = Modules(moduleTypes);
      ShipModel model = ShipModel(input, move, weapons, modules);
      spawnedShip.Construct(view, model);
      return spawnedShip;
    }

    private ShipPresenter Presenter(ShipPresenter prefab, Transform parent) => 
      assetProvider.Instantiate(prefab, parent);

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

    private ShipWeapons Weapons(WeaponType[] weaponTypes)
    {
      Weapon[] weapons = new Weapon[weaponTypes.Length];

      for (int i = 0; i < weaponTypes.Length; i++)
      {
        weapons[i] = Weapon(staticDataService.ForWeapon(weaponTypes[i]));
      }
      
      return new ShipWeapons(weapons);
    }

    private Weapon Weapon(WeaponSettings weaponSettings)
    {
      return new Weapon(weaponSettings);
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

    private ShipModel ShipModel(ShipInput input, ShipMove move, ShipWeapons weapons, ShipModules modules) => 
      new ShipModel(input, move, weapons, modules);
  }
}