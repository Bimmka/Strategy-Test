using UnityEngine;

namespace Features.Ship.Scripts.Weapons.Data
{
  [CreateAssetMenu(fileName = "WeaponSettings", menuName = "StaticData/Ship/Weapon/Create Weapon Settings", order = 52)]
  public class WeaponSettings : ScriptableObject
  {
    public WeaponType Type;
    public int Damage;
    public float ShootCooldown;
  }
}