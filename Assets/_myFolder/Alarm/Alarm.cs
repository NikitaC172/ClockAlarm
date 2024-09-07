using System;
using UnityEngine;

public class Alarm : MonoBehaviour
{
    [SerializeField] private Clock _clock;
    [SerializeField] private AlarmSetter _alarmSetter;

    private int _timeAlarm;
    private bool _isActive;

    public Action TimeComes;
    public Action<int> AlarmTimeChanged;

    private void OnEnable()
    {
        _clock.timeChangedSeconds += CheckTime;
        _alarmSetter.AlarmTimeSetted += SetTimeAlarm;
        _alarmSetter.AlarmSwitched += SwitchAlarm;
    }

    private void OnDisable()
    {
        _clock.timeChangedSeconds -= CheckTime;
        _alarmSetter.AlarmTimeSetted -= SetTimeAlarm;
        _alarmSetter.AlarmSwitched -= SwitchAlarm;
    }

    public void SwitchAlarm(bool status)
    {
        _isActive = status;
    }

    public void SetTimeAlarm(int timeSeconds)
    {
        _timeAlarm = timeSeconds;
        AlarmTimeChanged?.Invoke(_timeAlarm);
    }

    private void CheckTime(int seconds)
    {
        if(_isActive == true && _timeAlarm == seconds)
        {
            ActivateAlarm();
        }
    }

    private void ActivateAlarm()
    {
        TimeComes?.Invoke();
    }
}
