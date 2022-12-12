﻿using UnityEngine;

namespace Features.Bullet.Scripts.Element
{
  public class BulletView : MonoBehaviour
  {
    public void Show() => 
      gameObject.SetActive(true);

    public void Hide() => 
      gameObject.SetActive(false);
  }
}