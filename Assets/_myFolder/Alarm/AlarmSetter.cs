using System;
using UnityEngine;

public class AlarmSetter : MonoBehaviour
{
    [SerializeField] private AlarmDigitalInput _digitalInput;
    [SerializeField] private AlarmSwitch _alarmSwitch;

    public Action<int> AlarmTimeSetted;
    public Action<bool> AlarmSwitched;

    private void OnEnable()
    {
        _digitalInput.AlarmTimeChanged += OnSetTimeAlarm;
        _alarmSwitch.Activated += OnActivate;
    }

    private void OnDisable()
    {
        _digitalInput.AlarmTimeChanged -= OnSetTimeAlarm;
        _alarmSwitch.Activated -= OnActivate;
    }

    private void OnActivate(bool isActive)
    {
        AlarmSwitched?.Invoke(isActive);
    }

    private void OnSetTimeAlarm(int timeSeconds)
    {
        AlarmTimeSetted?.Invoke(timeSeconds);
    }
}
