using UnityEngine;

namespace Features.Ship.Scripts.Modules.Data
{
  [CreateAssetMenu(fileName = "ShipModuleSettings", menuName = "StaticData/Ship/Create Module Settings", order = 52)]
  public class ShipModuleSettings : ScriptableObject
  {
    public ModuleType Type;
    public float Value;
  }
}