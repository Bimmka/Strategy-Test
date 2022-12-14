using Features.Ship.Scripts.Health;
using Features.Ship.Scripts.Shield;

namespace Features.Ship.Scripts.Damage
{
  public class ShipDamageReceiver
  {
    private readonly ShipHealth health;
    private readonly ShipShield shield;

    public ShipDamageReceiver(ShipHealth health, ShipShield shield)
    {
      this.health = health;
      this.shield = shield;
    }

    public void TakeDamage(float count)
    {
      if (shield.CurrentValue > 0)
      {
        float shieldCount = shield.CurrentValue;
        shield.DecreaseShield(count);
        count -= shieldCount;
      }
      
      if (count > 0)
        health.DecreaseHealth(count);
    }
  }
}