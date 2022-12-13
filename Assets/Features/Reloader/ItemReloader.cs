using System;

namespace Features.Reloader
{
  public class ItemReloader
  {
    private readonly float maxWaitTime;
    private float currentWaitTime;
    public event Action TimeOut;

    public ItemReloader(float maxWaitTime)
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