using UnityEngine;

public class GameplayMediator
{
    [SerializeField] private DefeatPanel _defeatPanel;
    private Level _level;

    public void Initialize(Level level)
    {
        _level = level;
        _level.Defeat += OnDefeat;
    }

    private void OnDestroy()
    {
        _level.Defeat -= OnDefeat;
    }

    private void OnDefeat() => _defeatPanel.Show();
    
    public void RestartLevel()
    {
        _defeatPanel.Hide();
        _level.Restart();
    }
}