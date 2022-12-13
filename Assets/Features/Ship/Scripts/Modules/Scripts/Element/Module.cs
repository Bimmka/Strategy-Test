using Features.Ship.Scripts.Characteristics;
using Features.Ship.Scripts.Modules.Data;

namespace Features.Ship.Scripts.Modules.Scripts.Element
{
  public abstract class Module
  {
    protected readonly ModuleType type;
    protected readonly float value;

    public Module(ModuleType type, float value)
    {
      this.type = type;
      this.value = value;
    }
    public abstract void Apply(ref ShipCharacteristics characteristics);
  }
}