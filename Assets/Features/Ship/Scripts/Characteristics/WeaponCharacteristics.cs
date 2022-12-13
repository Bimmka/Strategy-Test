using Features.Ship.Scripts.Weapons.Data;

namespace Features.Ship.Scripts.Characteristics
{
  public readonly struct WeaponCharacteristics
  {
    public readonly WeaponType Type;
    public readonly int Damage;
    public readonly float ReloadTime;

    public WeaponCharacteristics(WeaponType type, int damage, float reloadTime)
    {
      Type = type;
      Damage = damage;
      ReloadTime = reloadTime;
    }
  }
}