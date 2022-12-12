using Features.Bullet.Data;
using Features.Bullet.Scripts.Spawner;
using Features.Ship.Data.InputBindings;
using Features.Ship.Scripts.Weapons.Data;
using UnityEngine;

namespace Features.Ship.Scripts.Weapons.Elements
{
  public class LaserLauncher : Weapon
  {
    public LaserLauncher(WeaponSettings settings, PlayerType playerType, Transform firePoint, BulletSpawner bulletSpawner) : base(settings, playerType, firePoint, bulletSpawner)
    {
    }

    protected override BulletType Type() => 
      BulletType.Laser;
  }
}