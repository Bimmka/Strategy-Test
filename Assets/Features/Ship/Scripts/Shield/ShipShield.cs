using System;
using Features.Reloader;
using Features.Ship.Scripts.Displaying;

namespace Features.Ship.Scripts.Shield
{
  public class ShipShield : IValueChangeable
  {
    private readonly float restoreValuePerSecond;
    private readonly ItemReloader reloader;
    private readonly float maxValue;
    public float CurrentValue { get; private set; }

    public event Action<float, float> Changed;

    public ShipShield(float startCount, float restoreValuePerSecond, float restoreTime)
    {
      this.restoreValuePerSecond = restoreValuePerSecond;
      reloader = new ItemReloader(restoreTime);
      reloader.TimeOut += OnReloadTimeOut;
      maxValue = startCount;
      CurrentValue = startCount;
    }

    public void Cleanup() => 
      reloader.TimeOut -= OnReloadTimeOut;

    public void Tick(float deltaTime)
    {
      if (CurrentValue != maxValue)
        reloader.Tick(deltaTime);
    }

    public void DecreaseShield(float count)
    {
      CurrentValue -= count;
      if (CurrentValue <= 0) 
        CurrentValue = 0;

      NotifyAboutChange();
    }

    private void OnReloadTimeOut()
    {
      CurrentValue += restoreValuePerSecond;

      if (CurrentValue >= maxValue)
      {
        CurrentValue = maxValue;
        reloader.ResetTime();
      }
      
      NotifyAboutChange();
    }

    private void NotifyAboutChange() => 
      Changed?.Invoke(CurrentValue, maxValue);
  }
}