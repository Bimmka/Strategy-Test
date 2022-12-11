using UnityEngine;

namespace Features.Ship.Data.Settings
{
  [CreateAssetMenu(fileName = "ShipSettings", menuName = "StaticData/Ship/Create Ship Settings", order = 52)]
  public class ShipSettings : ScriptableObject
  {
    public string ShipID;
    public int MaxHealth;
    public int MaxShield;
    public int ShieldRegenPerSecond;
    public int WeaponsCount;
    public int ModulesCount;
    public GameObject View;
  }
}