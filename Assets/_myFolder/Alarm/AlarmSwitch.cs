using System;
using UnityEngine;
using UnityEngine.UI;

public class AlarmSwitch : MonoBehaviour
{
    [SerializeField] private Button _buttonSwitch;
    [SerializeField] private Color _colorOn;
    [SerializeField] private Color _colorOff;

    private Image _image;
    private bool _isActive = false;

    public Action<bool> Activated;

    private void Start()
    {
        _image = _buttonSwitch.GetComponent<Image>();
    }

    private void OnEnable()
    {
        _buttonSwitch.onClick.AddListener(SwitchAlarm);
    }

    private void OnDisable()
    {
        _buttonSwitch.onClick.RemoveListener(SwitchAlarm);
    }

    private void SwitchAlarm()
    {
        if (_isActive == false)
        {
            OnAlarm();
        }
        else
        {
            OffAlarm();
        }
    }

    private void OnAlarm()
    {
        _isActive = true;
        Activated?.Invoke(_isActive);
        _image.color = _colorOn;
    }

    private void OffAlarm()
    {
        _isActive = false;
        _image.color = _colorOff;
        Activated?.Invoke(_isActive);
    }
}
