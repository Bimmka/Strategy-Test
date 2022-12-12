using Features.Services.Cleanup;
using Features.Ship.Scripts.Weapons.Data;
using UnityEngine;

namespace Features.Ship.Scripts.Weapons.Elements
{
  public class Weapon
  {
    private readonly WeaponSettings settings;
    private float currentReloadTime;
    public Weapon(WeaponSettings settings)
    {
      this.settings = settings;
      currentReloadTime = 0;
    }

    private void Shoot()
    {
      Debug.Log("Shoot");
    }

    public void Tick(in float deltaTime)
    {
      currentReloadTime += deltaTime;

      if (currentReloadTime >= settings.ShootCooldown)
      {
        currentReloadTime %= settings.ShootCooldown;
        Shoot();
      }
    }
  }
}