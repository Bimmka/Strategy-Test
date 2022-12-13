using Features.Ship.Scripts.Characteristics;
using Features.Ship.Scripts.Modules.Scripts.Element;

namespace Features.Ship.Scripts.Modules.Scripts.Container
{
  public class ShipModules 
  {
    private readonly Module[] modules;

    public ShipModules(Module[] modules)
    {
      this.modules = modules;
    }

    public ShipCharacteristics ApplyModules(ShipCharacteristics characteristics)
    {
      for (int i = 0; i < modules.Length; i++)
      {
        modules[i].Apply(ref characteristics);
      }

      return characteristics;
    }
  }
}
