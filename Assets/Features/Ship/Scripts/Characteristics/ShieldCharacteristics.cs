namespace Features.Ship.Scripts.Characteristics
{
  public readonly struct ShieldCharacteristics
  {
    public readonly float Max;
    public readonly float RestoreValue;
    public readonly float RestoreTime;

    public ShieldCharacteristics(float max, float restoreValue, float restoreTime)
    {
      Max = max;
      RestoreValue = restoreValue;
      RestoreTime = restoreTime;
    }
  }
}