using System;
using System.Collections;
using Features.CustomCoroutine;
using UnityEngine;

namespace Features.Ship.Scripts.Weapons.Elements
{
  public class WeaponReloader
  {
    private readonly ICoroutineRunner coroutineRunner;
    private readonly float reloadTime;

    private bool isActive;
    
    public event Action Reloaded;

    public WeaponReloader(ICoroutineRunner coroutineRunner, float reloadTime)
    {
      this.coroutineRunner = coroutineRunner;
      this.reloadTime = reloadTime;
    }

    public void Start()
    {
      isActive = true;
      coroutineRunner.StartCoroutine(Reloading());
    }

    public void Stop()
    {
      isActive = false;
    }

    private IEnumerator Reloading()
    {
      float currentReloadTime = 0;
      while (isActive)
      {
        yield return null;
        currentReloadTime += Time.deltaTime;

        if (IsReloaded(currentReloadTime, reloadTime))
        {
          currentReloadTime %= reloadTime;
          NotifyAboutReload();
        }
      }
    }

    private bool IsReloaded(float currentReloadTime, float maxTime) => 
      currentReloadTime >= maxTime;

    private void NotifyAboutReload() => 
      Reloaded?.Invoke();
  }
}