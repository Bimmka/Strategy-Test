using System;

namespace Features.UI.Windows.HUD.Scripts
{
  public interface IValueChangeable
  {
    float CurrentValue { get; }
    event Action<float, float> Changed;
  }
}