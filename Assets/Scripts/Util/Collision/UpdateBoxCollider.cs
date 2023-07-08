using UnityEngine;
using ScriptableObjectArchitecture;

public class UpdateBoxCollider : MonoBehaviour
{
    [SerializeField] private BoxCollider2D _boxCollider;
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private FloatVariable _width;

    void Update()
    {
        _rectTransform.sizeDelta = new Vector2(_width.Value, _rectTransform.sizeDelta.y);
        _boxCollider.size = new Vector2(_width.Value, _boxCollider.size.y);
    }
}
