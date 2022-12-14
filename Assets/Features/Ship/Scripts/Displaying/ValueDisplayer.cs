using UnityEngine;
using UnityEngine.UI;

namespace Features.Ship.Scripts.Displaying
{
  public class ValueDisplayer : MonoBehaviour
  {
    [SerializeField] private Image valueBar;
    
    private IValueChangeable valueChangeable;

    public void Construct(IValueChangeable valueChangeable)
    {
      this.valueChangeable = valueChangeable;
      valueChangeable.Changed += Display;
      Display(valueChangeable.CurrentValue, valueChangeable.CurrentValue);
    }

    public void Cleanup()
    {
      valueChangeable.Changed -= Display;
    }

    private void Display(float currentValue, float maxValue) => 
      valueBar.fillAmount = currentValue / maxValue;
  }
}