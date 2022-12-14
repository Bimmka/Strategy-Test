using System;

namespace Features.Ship.Scripts.Displaying
{
  public interface IValueChangeable
  {
    float CurrentValue { get; }
    event Action<float, float> Changed;
  }
}