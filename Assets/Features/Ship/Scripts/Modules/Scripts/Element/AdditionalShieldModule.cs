using Features.Ship.Scripts.Characteristics;
using Features.Ship.Scripts.Modules.Data;

namespace Features.Ship.Scripts.Modules.Scripts.Element
{
  public class AdditionalShieldModule : Module
  {
    public AdditionalShieldModule(ModuleType type, float value) : base(type, value)
    {
    }

    public override void Apply(ref ShipCharacteristics characteristics) => 
      characteristics.AddShield(value);
  }
}