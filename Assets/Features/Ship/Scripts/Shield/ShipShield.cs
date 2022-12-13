using System;
using Features.Reloader;

namespace Features.Ship.Scripts.Shield
{
  public class ShipShield
  {
    private readonly int restoreValuePerSecond;
    private readonly ItemReloader reloader;
    private readonly int maxValue;
    public int CurrentShield { get; private set; }

    public event Action<int> Changed;

    public ShipShield(int startCount, int reloadTime, int restoreValuePerSecond)
    {
      this.restoreValuePerSecond = restoreValuePerSecond;
      reloader = new ItemReloader(reloadTime);
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

    public void DecreaseShield(int count)
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