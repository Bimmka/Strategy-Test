using System;

namespace Features.Ship.Scripts.Health
{
  public class ShipHealth
  {
    public float CurrentHealth { get; private set; }

    public event Action<float> Changed; 
    public event Action Dead; 

    public ShipHealth(float startCount)
    {
      CurrentHealth = startCount;
    }

    public void DecreaseHealth(float count)
    {
      CurrentHealth -= count;
      if (CurrentHealth <= 0)
      {
        CurrentHealth = 0;
        NotifyAboutDead();
      }
      
      NotifyAboutChange();
    }

    private void NotifyAboutChange()
    {
      Changed?.Invoke(CurrentHealth);
    }

    private void NotifyAboutDead() => 
      Dead?.Invoke();
  }
}