using Features.Ship.Data.Settings;
using Features.Ship.Scripts.Modules.Data;
using Features.Ship.Scripts.Weapons.Data;

namespace Features.Services.ShipParts
{
  public readonly struct ShipChosenParts
  {
    public readonly ShipType ShipType;
    public readonly WeaponType[] WeaponTypes;
    public readonly ModuleType[] ModuleTypes;

    public ShipChosenParts(ShipType shipType, WeaponType[] weaponTypes, ModuleType[] moduleTypes)
    {
      ShipType = shipType;
      WeaponTypes = weaponTypes;
      ModuleTypes = moduleTypes;
    }
  }
}