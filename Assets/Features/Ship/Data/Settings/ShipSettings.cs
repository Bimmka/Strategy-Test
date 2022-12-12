using Features.Ship.Scripts.Base;
using Features.Ship.Scripts.Move.Data;
using Features.Ship.Scripts.Rotate.Data;
using UnityEngine;

namespace Features.Ship.Data.Settings
{
  [CreateAssetMenu(fileName = "ShipSettings", menuName = "StaticData/Ship/Settings/Create Ship Settings", order = 52)]
  public class ShipSettings : ScriptableObject
  {
    public ShipType Type;
    public int MaxHealth;
    public int MaxShield;
    public int ShieldRegenPerSecond;
    public int WeaponsCount;
    public int ModulesCount;
    public ShipMoveSettings MoveSettings;
    public ShipRotateSettings RotateSettings;
    public ShipView View;
  }

  public enum ShipType
  {
    Big,
    Small
  }
}