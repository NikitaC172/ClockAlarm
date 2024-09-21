using DG.Tweening;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] protected int _angleDelta;
    [SerializeField] protected bool _isSecondsArrow = false;

    public RectTransform RectTransform => _rectTransform;

    public void SetAngleByTime(float time)
    {
        if (_isSecondsArrow == false)
        {
            _rectTransform.DOLocalRotate(new Vector3(0, 0, time * _angleDelta), 1f);
        }
        else
        {
            _rectTransform.DOLocalRotate(new Vector3(0, 0, (time + 1) * _angleDelta), 1f);
        }
    }

    public void SetAngle(float angle)
    {
        _rectTransform.DOLocalRotate(new Vector3(0, 0, angle), 1f);
    }
}
