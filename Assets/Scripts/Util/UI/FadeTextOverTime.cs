using UnityEngine;
using ScriptableObjectArchitecture;
using TMPro;

public class FadeTextOverTime : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private AnimationCurveVariable _fadeCurve;
    [SerializeField] private float _fadeRate;
    [SerializeField] private bool _startFading = false;
    [SerializeField] private bool _useMaxfade = false;
    private float _maxfade = 1f;
    private float _fadeTime = 0f;
    private Color _currentfade;


    void Awake()
    {
        _currentfade = _text.color;
    }

    void Update() 
    {
        // don't fade if we have this bool turned off
        if (!_startFading) return;

        // evaluate animation curve
        _fadeTime += Time.deltaTime * _fadeRate;
        float fadeCurveValue = _fadeCurve.Value.Evaluate(_fadeTime);

        // get numbers for next fade update
        float alpha = _currentfade.a - fadeCurveValue;

        // limit to max fade value
        if (_useMaxfade)
        {
            alpha = Mathf.Clamp(alpha, 0f, _maxfade);
        }

        // perform fading
        _currentfade = new Color(_text.color.r, _text.color.b, _text.color.g, alpha);
        _text.color = _currentfade;
    }
}
