using UnityEngine;

namespace Features.Ship.Scripts.Modules.Data
{
  [CreateAssetMenu(fileName = "ShipModuleSettings", menuName = "StaticData/Ship/Module/Create Module Settings", order = 52)]
  public class ModuleSettings : ScriptableObject
  {
    public ModuleType Type;
    public float Value;
    public GameObject View;
  }
}