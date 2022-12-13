using Features.Ship.Scripts.Characteristics;
using Features.Ship.Scripts.Modules.Data;

namespace Features.Ship.Scripts.Modules.Scripts.Element
{
  public class ReduceWeaponReloadModule : Module
  {
    public ReduceWeaponReloadModule(ModuleType type, float value) : base(type, value)
    {
    }

    public override void Apply(ref ShipCharacteristics characteristics) => 
      characteristics.ReduceWeaponsReloadTime(value);
  }
}