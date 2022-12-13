using Features.UI.Windows.Base;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Features.UI.Windows.GameEnd.Scripts
{
  public class UIGameEndWindow : BaseWindow
  {
    [SerializeField] private Button playButton;

    [Inject]
    public void Construct()
    {

    }

    protected override void Subscribe()
    {
      base.Subscribe();
      playButton.onClick.AddListener(StartLevel);
    }

    protected override void Cleanup()
    {
      base.Cleanup();
      playButton.onClick.RemoveListener(StartLevel);
    }

    private void StartLevel()
    {
      
      Destroy();
    }
  }
}