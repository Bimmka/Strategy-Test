using Features.Services.Cleanup;
using Features.Ship.Scripts.Health;
using Features.Ship.Scripts.Shield;
using UnityEngine;

namespace Features.UI.Windows.HUD.Scripts
{
  public class ShipCharacteristicsDisplayer : MonoBehaviour, ICleanup
  {
    [SerializeField] private ValueDisplayer shieldDisplayer;
    [SerializeField] private ValueDisplayer healthDisplayer;

    public void Construct(ShipShield shield, ShipHealth health)
    {
      shieldDisplayer.Construct(shield);
      healthDisplayer.Construct(health);
    }

    public void Cleanup()
    {
      healthDisplayer.Cleanup();
      shieldDisplayer.Cleanup();
    }
  }
}