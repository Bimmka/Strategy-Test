using Features.Services.UI.Factory;
using Features.Ship.Data.InputBindings;
using Features.Ship.Data.Settings;
using Features.Ship.Scripts.Modules.Data;
using Features.Ship.Scripts.Weapons.Data;
using Features.StaticData.Windows;

namespace Features.Services.StaticData
{
  public interface IStaticDataService 
  {
    ShipSettings ForShip(ShipType shipType);
    WeaponSettings ForWeapon(WeaponType weaponType);
    ModuleSettings ForModule(ModuleType moduleType);
    ShipControlBindings ForInput(PlayerType playerType);
    WindowInstantiateData ForWindow(WindowId id);
  }
}