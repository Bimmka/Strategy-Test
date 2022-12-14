using TMPro;
using UnityEngine;

namespace Features.UI.Windows.MainMenu.Scripts
{
  public class ShipPartView : MonoBehaviour
  {
    [SerializeField] private TextMeshProUGUI[] moduleTips;
    [SerializeField] private TMP_Dropdown[] modulesDropdown;
    public void UpdateVisibleModulesCount(int modulesCount)
    {
      for (int i = 0; i < modulesDropdown.Length; i++)
      {
        if (i + 1 > modulesCount)
        {
          ChangeEnableState(moduleTips[i].gameObject, false);
          ChangeEnableState(modulesDropdown[i].gameObject,false);
        }
        else
        {
          ChangeEnableState(moduleTips[i].gameObject, true);
          ChangeEnableState(modulesDropdown[i].gameObject,true);
        }
      }
    }

    private void ChangeEnableState(GameObject changeableObject, bool isEnable)
    {
      if (changeableObject.activeSelf == isEnable)
        return;
      changeableObject.SetActive(isEnable);
    }
  }
}