using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

public class Clock : MonoBehaviour
{
    [SerializeField] private TimeRequester _requester;
    [SerializeField] private ManualSetterTime _manualSetterTime;
    private int _currentTime = 0;
    private int errorTime = 3;
    private bool _isClocking = true;

    public Action<int> timeChangedSeconds;
    public Action<TimeStruct> timeChanged;

    private const int MaxSeconds = 24 * 3600;

    private void Start()
    {
        _ = CountTime();
    }

    private void OnEnable()
    {
        _requester.CurrentTime += CheckTime;

        if (_manualSetterTime != null)
        {
            _manualSetterTime.ManualTimeSetted += CheckTime;
        }
    }

    private void OnDisable()
    {
        _requester.CurrentTime -= CheckTime;

        if (_manualSetterTime != null)
        {
            _manualSetterTime.ManualTimeSetted -= CheckTime;
        }
    }

    private void CheckTime(int seconds)
    {
        int deltaTime = Mathf.Abs(_currentTime - seconds);

        if (deltaTime >= errorTime)
        {
            _currentTime = seconds;
        }
    }

    private async UniTask CountTime()
    {
        while (_isClocking)
        {
            _currentTime++;

            if (_currentTime >= MaxSeconds)
            {
                _currentTime = 0;
            }

            OnChangedTime();
            await UniTask.Delay(TimeSpan.FromSeconds(1));
        }
    }

    private void OnChangedTime()
    {
        float hour = _currentTime / 3600f;
        float minute = _currentTime % 3600f / 60f;
        float seconds = _currentTime % 60f;
        TimeStruct time = new TimeStruct(hour, minute, seconds);
        timeChangedSeconds?.Invoke(_currentTime);
        timeChanged?.Invoke(time);
    }
}
