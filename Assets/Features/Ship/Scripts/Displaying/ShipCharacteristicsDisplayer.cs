using Features.Services.Cleanup;
using Features.Ship.Scripts.Health;
using Features.Ship.Scripts.Shield;
using UnityEngine;

namespace Features.Ship.Scripts.Displaying
{
  public class ShipCharacteristicsDisplayer : MonoBehaviour, ICleanup
  {
    [SerializeField] private ValueDisplayer shieldDisplayer;
    [SerializeField] private ValueDisplayer healthDisplayer;

    public void Construct(ShipShield shield, ShipHealth health, ICleanupService cleanupService)
    {
      shieldDisplayer.Construct(shield);
      healthDisplayer.Construct(health);
      cleanupService.Register(this);
    }

    public void Cleanup()
    {
      healthDisplayer.Cleanup();
      shieldDisplayer.Cleanup();
    }
  }
}