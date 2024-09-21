using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class PanelRenderer : MonoBehaviour
{
    [SerializeField] private float _timeRender;
    [SerializeField] private CanvasGroup _canvasGroup;

    public void ShowPanel()
    {
        _canvasGroup.alpha = 0;
        gameObject.SetActive(true);
        _canvasGroup.DOFade(1, _timeRender);
    }

    public void HidePanel()
    {
        _ = HidePanelTask();
    }

    private async UniTask HidePanelTask()
    {
        _canvasGroup.alpha = 1;
        _canvasGroup.DOFade(0, _timeRender);
        await UniTask.Delay(TimeSpan.FromSeconds(_timeRender));
        gameObject.SetActive(false);
    }
}
