using System;
using TMPro;
using UnityEngine;

public class DigitalInput : MonoBehaviour
{
    [SerializeField] private TMP_InputField _inputFieldHour;
    [SerializeField] private TMP_InputField _inputFieldMinute;
    [SerializeField] private AnalogInput _alarmAnalogInput;

    private int _hour = 0;
    private int _minute = 0;
    private const int LeftPadding = 2;

    public Action<int> AlarmTimeChanged;

    private void OnEnable()
    {
        _inputFieldHour.onEndEdit.AddListener(ValidateHour);
        _inputFieldMinute.onEndEdit.AddListener(ValidateMinute);
        _inputFieldMinute.onDeselect.AddListener(ValidateMinuteDesecelect);
        _alarmAnalogInput.TimeChanged += TimeAlarmOnChange;
        _alarmAnalogInput.TimeInputed += SetTimeOnAnalogClock;
    }

    private void OnDisable()
    {
        _inputFieldHour.onEndEdit.RemoveListener(ValidateHour);
        _inputFieldMinute.onEndEdit.RemoveListener(ValidateMinute);
        _inputFieldMinute.onDeselect.RemoveListener(ValidateMinuteDesecelect);
        _alarmAnalogInput.TimeChanged -= TimeAlarmOnChange;
        _alarmAnalogInput.TimeInputed -= SetTimeOnAnalogClock;
    }

    private void SetTimeOnAnalogClock(float hour, float minute)
    {
        _hour = (int)hour;
        _minute = (int)minute;
        _inputFieldHour.text = _hour.ToString();
        _inputFieldMinute.text = _minute.ToString();
    }

    private void ValidateHour(string number)
    {
        int.TryParse(number, out _hour);
        _hour = Mathf.Clamp(_hour, 0, 23);
        _inputFieldHour.text = _hour.ToString();
        TimeAlarmOnChange();
    }

    private void ValidateMinute(string number)
    {
        int.TryParse(number, out _minute);
        _minute = Mathf.Clamp(_minute, 0, 59);
        _inputFieldMinute.text = _minute.ToString();
        TimeAlarmOnChange();
    }

    private void ValidateMinuteDesecelect(string number)
    {
        if (_minute < 10)
        {
            _inputFieldMinute.text = $"{_minute.ToString().PadLeft(LeftPadding, '0')}";
        }
    }

    private void TimeAlarmOnChange()
    {
        int secondsInHour = 3600;
        int secondsInMinute = 60;
        int timeAlarm = _hour * secondsInHour + _minute * secondsInMinute;
        ValidateMinuteDesecelect(null);
        AlarmTimeChanged?.Invoke(timeAlarm);
    }
}
