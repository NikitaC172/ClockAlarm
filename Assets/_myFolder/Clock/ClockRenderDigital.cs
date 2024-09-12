using TMPro;
using UnityEngine;

public class ClockRenderDigital : MonoBehaviour
{
    [SerializeField] private Clock _clock;
    [SerializeField] private TMP_Text _textTime;
    private const int LeftPadding = 2;

    private void OnEnable()
    {
        _clock.timeChanged += RenderClock;
    }

    private void OnDisable()
    {
        _clock.timeChanged -= RenderClock;
    }

    public void RenderClock(TimeStruct time)
    {
        int hourInt = (int)time.hour;
        int minuteInt = (int)time.minute;
        int secondInt = (int)time.second;

        _textTime.text =
                        $"{hourInt}" +
                        $":{minuteInt.ToString().PadLeft(LeftPadding, '0')}" +
                        $":{secondInt.ToString().PadLeft(LeftPadding, '0')}";
    }
}
