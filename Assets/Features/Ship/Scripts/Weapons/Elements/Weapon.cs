using Features.Services.Cleanup;
using UnityEngine;

namespace Features.Ship.Scripts.Weapons.Elements
{
  public class Weapon : ICleanup
  {
    private WeaponReloader reloader;
    public Weapon(ICleanupService cleanupService)
    {
      cleanupService.Register(this);
      reloader.Reloaded += Shoot;
    }

    public void Cleanup()
    {
      reloader.Stop();
      reloader.Reloaded -= Shoot;
    }

    private void Shoot()
    {
      Debug.Log("Shoot");
    }
  }
}