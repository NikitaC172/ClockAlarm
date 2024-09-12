using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class TimeRequester : MonoBehaviour
{
    [SerializeField] private string[] _urlServers;

    private List<Task<DateTime>> _tasks;
    private DateTime _globDateTime;
    private float _intervalRequestSecondsHour = 3600f;
    private float _intervalRequestSeconds = 15f;
    private bool _isActivePeriodicRequest = true;

    public Action<int> CurrentTime;

    private void Start()
    {
        RequestTimePeriodic();
    }

    private async void RequestTimePeriodic()
    {
        while (_isActivePeriodicRequest)
        {
            CreateTaskTimeRequest();
            Task<DateTime> getTime = await Task.WhenAny(_tasks);
            _globDateTime = await getTime;

            if (_globDateTime == DateTime.MinValue)
            {
                await Task.Delay(TimeSpan.FromSeconds(_intervalRequestSeconds));
            }
            else
            {
                SetCurrentTime();
                await Task.Delay(TimeSpan.FromSeconds(_intervalRequestSecondsHour));
            }
        }
    }

    private void SetCurrentTime()
    {
        int timeSeconds = _globDateTime.Hour * 3600 + _globDateTime.Minute * 60 + _globDateTime.Second;
        CurrentTime?.Invoke(timeSeconds);
    }

    private void CreateTaskTimeRequest()
    {
        _tasks = new List<Task<DateTime>>();

        for (int i = 0; i < _urlServers.Length; i++)
        {
            _tasks.Add(CheckGlobalTime(_urlServers[i]));
        }
    }

    private async Task<DateTime> CheckGlobalTime(string URL)
    {
        DateTime globDateTime;
        UnityWebRequest unityWebRequest = new UnityWebRequest(URL);
        unityWebRequest.SendWebRequest();
        string timeStr = null;

        while (!unityWebRequest.isDone && unityWebRequest.error == null)
        {
            await Task.Delay(TimeSpan.FromSeconds(1));
        }

        timeStr = unityWebRequest.GetResponseHeader("Date");

        if (!DateTime.TryParse(timeStr, out globDateTime))
        {
            await Task.Delay(TimeSpan.FromSeconds(15));
            return DateTime.MinValue;
        }

        return globDateTime;
    }
}
