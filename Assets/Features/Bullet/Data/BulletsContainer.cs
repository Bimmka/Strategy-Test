using System.Collections.Generic;
using Features.Bullet.Scripts.Element;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Features.Bullet.Data
{
  [CreateAssetMenu(fileName = "BulletsContainer", menuName = "StaticData/Bullet/Create Bullets Container", order = 52)]
  public class BulletsContainer : SerializedScriptableObject
  {
    [ValidateInput("ValidateDictionaryKeys", "Dictionary Keys Does Not Equal Ship Key", IncludeChildren = true)]
    public Dictionary<BulletType, BulletSettings> Settings;

    public BulletPresenter Prefab;

    private bool ValidateDictionaryKeys(Dictionary<BulletType, BulletSettings> settings)
    {
      if (settings == null || settings.Count == 0)
        return true;

      foreach (KeyValuePair<BulletType,BulletSettings> ship in settings)
      {
        if (ship.Key != ship.Value.Type)
          return false;
      }

      return true;
    }
  }
}