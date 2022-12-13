using System;
using Features.Reloader;

namespace Features.Ship.Scripts.Shield
{
  public class ShipShield
  {
    private readonly float restoreValuePerSecond;
    private readonly ItemReloader reloader;
    private readonly float maxValue;
    public float CurrentShield { get; private set; }

    public event Action<float> Changed;

    public ShipShield(float startCount, float restoreValuePerSecond, float restoreTime)
    {
      this.restoreValuePerSecond = restoreValuePerSecond;
      reloader = new ItemReloader(restoreTime);
      reloader.TimeOut += OnReloadTimeOut;
      maxValue = startCount;
      CurrentShield = startCount;
    }

    public void Cleanup() => 
      reloader.TimeOut -= OnReloadTimeOut;

    public void Tick(float deltaTime)
    {
      if (CurrentShield != maxValue)
        reloader.Tick(deltaTime);
    }

    public void DecreaseShield(float count)
    {
      CurrentShield -= count;
      if (CurrentShield <= 0) 
        CurrentShield = 0;

      NotifyAboutChange();
    }

    private void OnReloadTimeOut()
    {
      CurrentShield += restoreValuePerSecond;

      if (CurrentShield >= maxValue)
      {
        CurrentShield = maxValue;
        reloader.ResetTime();
      }
      
      NotifyAboutChange();
    }

    private void NotifyAboutChange() => 
      Changed?.Invoke(CurrentShield);
  }
}