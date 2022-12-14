using System;
using Features.UI.Windows.HUD.Scripts;

namespace Features.Ship.Scripts.Health
{
  public class ShipHealth : IValueChangeable
  {
    private readonly float maxValue; 
    public float CurrentValue { get; private set; }

    public event Action<float, float> Changed; 
    public event Action Dead; 

    public ShipHealth(float startCount)
    {
      maxValue = startCount;
      CurrentValue = startCount;
    }

    public void DecreaseHealth(float count)
    {
      CurrentValue -= count;
      if (CurrentValue <= 0)
      {
        CurrentValue = 0;
        NotifyAboutDead();
      }
      
      NotifyAboutChange();
    }

    private void NotifyAboutChange()
    {
      Changed?.Invoke(CurrentValue, maxValue);
    }

    private void NotifyAboutDead() => 
      Dead?.Invoke();
  }
}