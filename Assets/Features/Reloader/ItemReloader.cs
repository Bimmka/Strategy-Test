using System;

namespace Features.Reloader
{
  public class ItemReloader
  {
    private readonly int maxWaitTime;
    private float currentWaitTime;
    public event Action TimeOut;

    public ItemReloader(int maxWaitTime)
    {
      this.maxWaitTime = maxWaitTime;
    }

    public void Tick(float deltaTime)
    {
      currentWaitTime += deltaTime;
      if (currentWaitTime >= maxWaitTime)
      {
        currentWaitTime %= maxWaitTime;
        NotifyAboutTimeOut();
      }
    }

    public void ResetTime() => 
      currentWaitTime = 0;

    private void NotifyAboutTimeOut() => 
      TimeOut?.Invoke();
  }
}