using Features.Ship.Scripts.Health;
using Features.Ship.Scripts.Shield;
using Features.UI.Windows.Base;
using UnityEngine;

namespace Features.UI.Windows.HUD.Scripts
{
  public class UIHUD : BaseWindow
  {
    [SerializeField] private ShipCharacteristicsDisplayer firstPlayerDisplayer;
    [SerializeField] private ShipCharacteristicsDisplayer secondPlayerDisplayer;

    public void ConstructFirstPlayerDisplayer(ShipShield shield, ShipHealth health) => 
      firstPlayerDisplayer.Construct(shield, health);

    public void ConstructSecondPlayerDisplayer(ShipShield shield, ShipHealth health) => 
      secondPlayerDisplayer.Construct(shield, health);

    protected override void Cleanup()
    {
      base.Cleanup();
      firstPlayerDisplayer.Cleanup();
      secondPlayerDisplayer.Cleanup();
    }
  }
}