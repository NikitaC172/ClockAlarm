using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnalogInput : MonoBehaviour
{
    [SerializeField] private DigitalInput _digitalInput;
    [SerializeField] private PanelAnalogAlarm _panelClockHour;
    [SerializeField] private PanelAnalogAlarm _panelClockMinute;
    [SerializeField] private HourArrow _hourArrow;
    [SerializeField] private MinuteArrow _minuteArrow;
    [SerializeField] private Camera _camera;

    private float _hour = 0;
    private float _minute = 0;
    float _relPositionX;
    float _relPositionY;
    private int _hourInOneRound = 12;
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

    private float VectorLength(float x, float y)
    {
        return new Vector2(x, y).magnitude;
    }

    private void CalculateAngle(Arrow arrow, bool isHourArrow)
    {
        _relPositionX = Mouse.current.position.x.ReadValue() - RectTransformUtility.WorldToScreenPoint(_camera, arrow.RectTransform.position).x;
        _relPositionY = Mouse.current.position.y.ReadValue() - RectTransformUtility.WorldToScreenPoint(_camera, arrow.RectTransform.position).y;
        float angle = -Mathf.Atan2(_relPositionX, _relPositionY) * Mathf.Rad2Deg;
        arrow.SetAngle(angle);

        if (isHourArrow == true)
        {
            int currentRound;

            if (VectorLength(_relPositionX, _relPositionY) > 250f)
            {
                currentRound = _hourInOneRound;
            }
            else
            {
                currentRound = 0;
            }

            if (angle <= 0)
            {
                _hour = -angle / 30 + currentRound;
            }
            else
            {
                _hour = (360 - angle) / 30 + currentRound;
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
        _hour = seconds / 3600f;
        _minute = seconds % 3600f / 60f;

        _hourArrow.SetAngleByTime(_hour);
        _minuteArrow.SetAngleByTime(_minute);
    }
}
