using System;
using UnityEngine;
using UnityEngine.UI;

public class ManualSetterTime : MonoBehaviour
{
    [SerializeField] private DigitalInput _digitalInput;
    [SerializeField] private Button _buttonAccept;

    private int _manualTime;
    public Action<int> ManualTimeSetted;

    private void OnEnable()
    {
        _digitalInput.AlarmTimeChanged += SetTime;
        _buttonAccept.onClick.AddListener(AcceptTime);
    }

    private void OnDisable()
    {
        _digitalInput.AlarmTimeChanged -= SetTime;
        _buttonAccept.onClick.RemoveListener(AcceptTime);
    }

    private void SetTime(int timeSeconds)
    {
        _manualTime = timeSeconds;
    }

    private void AcceptTime()
    {
        ManualTimeSetted?.Invoke(_manualTime);
    }
}
