using UnityEngine;

public class ClockRenderAnalog : MonoBehaviour
{
    [SerializeField] private Clock _clock;
    [SerializeField] private HourArrow _hoursArrow;
    [SerializeField] private MinuteArrow _minuteArrow;
    [SerializeField] private SecondArrow _secondArrow;

    private void OnEnable()
    {
        _clock.timeChanged += RenderClock;
    }

    private void OnDisable()
    {
        _clock.timeChanged -= RenderClock;
    }

    private void RenderClock(TimeStruct time)
    {
        _hoursArrow.SetAngleByTime(time.hour);
        _minuteArrow.SetAngleByTime(time.minute);
        _secondArrow.SetAngleByTime(time.second);
    }
}
