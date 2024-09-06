using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] protected int _angleDelta;

    public RectTransform RectTransform => _rectTransform;

    public void SetAngleByTime(float time)
    {
        _rectTransform.localEulerAngles = new Vector3(0, 0, time * _angleDelta);
    }

    public void SetAngle(float angle)
    {
        _rectTransform.localEulerAngles = new Vector3(0, 0, angle);
    }
}
