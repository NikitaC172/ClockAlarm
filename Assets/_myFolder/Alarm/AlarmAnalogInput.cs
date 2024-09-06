using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

public class AlarmAnalogInput : MonoBehaviour
{
    [SerializeField] private AlarmDigitalInput _digitalInput;
    [SerializeField] private PanelAnalogAlarm _panelClockHour;
    [SerializeField] private PanelAnalogAlarm _panelClockMinute;
    [SerializeField] private HourArrow _hourArrow;
    [SerializeField] private MinuteArrow _minuteArrow;
    [SerializeField] private Camera _camera;

    private float _hour = 0;
    private float _minute = 0;
    private bool _isPressedHour = false;
    private bool _isPressedMinute = false;

    public Action<float, float> TimeInputed;
    public Action TimeChanged;

    private void OnEnable()
    {
        _digitalInput.AlarmTimeChanged += RenderArrow;
        _panelClockHour.Pressed += OnPressedHour;
        _panelClockMinute.Pressed += OnPressedMinute;
    }

    private void OnDisable()
    {
        _panelClockHour.Pressed -= OnPressedHour;
        _panelClockMinute.Pressed -= OnPressedMinute;
        _digitalInput.AlarmTimeChanged -= RenderArrow;
    }

    private async void SetTimeHour()
    {
        while (_isPressedHour == true)
        {
            CalculateAngle(_hourArrow, true);
            await Task.Yield();
        }

        TimeChanged?.Invoke();
    }

    private async void SetTimeMinute()
    {
        while (_isPressedMinute == true)
        {
            CalculateAngle(_minuteArrow, false);
            await Task.Yield();
        }

        TimeChanged?.Invoke();
    }

    private void CalculateAngle(Arrow arrow, bool isHourArrow)
    {
        float relPositionX = Mouse.current.position.x.ReadValue() - RectTransformUtility.WorldToScreenPoint(_camera, arrow.RectTransform.position).x;
        float relPositionY = Mouse.current.position.y.ReadValue() - RectTransformUtility.WorldToScreenPoint(_camera, arrow.RectTransform.position).y;
        float angle = -Mathf.Atan2(relPositionX, relPositionY) * Mathf.Rad2Deg;
        arrow.SetAngle(angle);

        if (isHourArrow == true)
        {
            if (angle <= 0)
            {
                _hour = -angle / 30;
            }
            else
            {
                _hour = (360 - angle) / 30;
            }
        }
        else
        {
            if (angle <= 0)
            {
                _minute = -angle / 6;
            }
            else
            {
                _minute = (360 - angle) / 6;
            }
        }

        TimeInputed?.Invoke(_hour, _minute);
    }

    private void OnPressedHour(bool isPressed)
    {
        _isPressedHour = isPressed;

        if (isPressed == true)
        {
            SetTimeHour();
        }
    }

    private void OnPressedMinute(bool isPressed)
    {
        _isPressedMinute = isPressed;

        if (isPressed == true)
        {
            SetTimeMinute();
        }
    }

    private void RenderArrow(int seconds)
    {
        float hour = seconds / 3600f;
        float minute = seconds % 3600f / 60f;

        _hourArrow.SetAngleByTime(hour);
        _minuteArrow.SetAngleByTime(minute);
    }
}
