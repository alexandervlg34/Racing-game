using System;

public class Level
{
    public event Action Defeat;
    public void Start()
    {
        
    }

    public void Restart()
    {
        Start();
    }

    public void OnDefeat()
    {
        Defeat?.Invoke();
    }
}