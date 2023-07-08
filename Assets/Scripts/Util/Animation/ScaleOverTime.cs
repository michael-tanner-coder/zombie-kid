using UnityEngine;
using ScriptableObjectArchitecture;

public class ScaleOverTime : MonoBehaviour
{
    [SerializeField] private AnimationCurveVariable _scaleCurve;
    [SerializeField]private float _scaleRate;
    [SerializeField] private bool _startScaling = false;
    [SerializeField] private bool _useMaxScale = false;
    [SerializeField] private bool _loop = false;
    private float _maxScale = 1f;
    private float _scaleTime = 0f;
    private Vector3 _currentScale;

    void Awake()
    {
        _currentScale = transform.localScale;
    }
    void Update() 
    {
        // don't scale if we have this bool turned off
        if (!_startScaling) return;

        // Evaluate animation curve
        _scaleTime += Time.deltaTime * _scaleRate;
        float scaleCurve = _scaleCurve.Value.Evaluate(_scaleTime);

        // get numbers for next scale update
        float x =  _currentScale.x + scaleCurve;
        float y =  _currentScale.y + scaleCurve;
        float z =  _currentScale.z + scaleCurve;

        // limit to max scale value
        if (_useMaxScale)
        {
            x = Mathf.Clamp(x, 0f, _maxScale);
            y = Mathf.Clamp(y, 0f, _maxScale);
            z = Mathf.Clamp(z, 0f, _maxScale);
        }

        // perform scaling 
        _currentScale = new Vector3(x,y,z);
        transform.localScale = _currentScale;

        // looping 
        if (_loop && _scaleTime >= 1f)
        {
            _scaleTime = 0;
        }
    }
}
