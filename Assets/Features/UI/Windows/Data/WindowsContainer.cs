using System.Collections.Generic;
using Features.Services.UI.Factory;
using Features.StaticData.Windows;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Features.UI.Windows.Data
{
  [CreateAssetMenu(fileName = "WindowsContainer", menuName = "StaticData/UI/Create Windows Instantiate Data", order = 52)]
  public class WindowsContainer : SerializedScriptableObject
  {
    [ValidateInput("ValidateDictionaryKeys", "Dictionary Keys Does Not Equal Instantiate Data Key", IncludeChildren = true)]
    public Dictionary<WindowId, WindowInstantiateData> InstantiateData;
    
    private bool ValidateDictionaryKeys(Dictionary<WindowId, WindowInstantiateData> instantiateDatas)
    {
      if (instantiateDatas == null || instantiateDatas.Count == 0)
        return true;

      foreach (KeyValuePair<WindowId,WindowInstantiateData> data in instantiateDatas)
      {
        if (data.Key != data.Value.ID)
          return false;
      }

      return true;
    }
  }
}