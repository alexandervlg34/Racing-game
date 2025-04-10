using UnityEngine;
using UnityEngine.UI;

public class SpeedUI : MonoBehaviour
{
    [SerializeField] private CarMover _mover;
    [SerializeField] private Text carSpeedText;
    
    private void Start()
    {
        if (_mover != null)
        {
            _mover.OnSpeedChanged += UpdateSpeedDisplay;
            UpdateSpeedDisplay(Mathf.Abs(_mover.Speed));
        }
        else if (carSpeedText != null)
        {
            carSpeedText.text = "0";
        }
    }
    
    private void OnDestroy()
    {
        if (_mover != null)
        {
            _mover.OnSpeedChanged -= UpdateSpeedDisplay;
        }
    }

    private void UpdateSpeedDisplay(float speed)
    {
        if (carSpeedText != null)
        {
            carSpeedText.text = Mathf.RoundToInt(speed).ToString();
        }
    }
}