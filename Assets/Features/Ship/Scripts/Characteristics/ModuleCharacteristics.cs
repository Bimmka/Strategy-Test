using Features.Ship.Scripts.Modules.Data;

namespace Features.Ship.Scripts.Characteristics
{
  public readonly struct ModuleCharacteristics
  {
    public readonly ModuleType Type;
    public readonly float Value;

    public ModuleCharacteristics(ModuleType type, float value)
    {
      Type = type;
      Value = value;
    }
  }
}