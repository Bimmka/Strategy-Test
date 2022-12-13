using System;

namespace Features.Ship.Scripts.Health
{
  public class ShipHealth
  {
    public int CurrentHealth { get; private set; }

    public event Action<int> Changed; 
    public event Action Dead; 

    public ShipHealth(int startCount)
    {
      CurrentHealth = startCount;
    }

    public void DecreaseHealth(int count)
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