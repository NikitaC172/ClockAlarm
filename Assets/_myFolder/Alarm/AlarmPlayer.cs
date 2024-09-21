using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using UnityEngine.UI;

public class AlarmPlayer : MonoBehaviour
{
    [SerializeField] private Alarm _alarm;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _audioClip;
    [SerializeField] private Button _button;
    [SerializeField] private AlarmPanel _alarmPanel;

    private void OnEnable()
    {
        _alarm.TimeComes += Activate;
        _button.onClick.AddListener(Deactivate);
    }

    private void OnDisable()
    {
        _alarm.TimeComes -= Activate;
        _button.onClick.RemoveListener(Deactivate);
    }

    public void Deactivate()
    {
        _audioSource.Stop();
        _alarmPanel.gameObject.SetActive(false);
    }

    private void Activate()
    {
        _alarmPanel.gameObject.SetActive(true);
        _audioSource.clip = _audioClip;
        _audioSource.Play();
        _ = DelayAfterOff();

    }

    private async UniTask DelayAfterOff()
    {
        await UniTask.Delay(TimeSpan.FromMinutes(1f));
        Deactivate();
    }
}
