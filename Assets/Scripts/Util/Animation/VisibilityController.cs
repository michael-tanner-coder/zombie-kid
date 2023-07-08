using UnityEngine;

public class VisibilityController : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer _spriteRenderer;

    public void ToggleVisibility()
    {
        if (_spriteRenderer != null)
        {
            _spriteRenderer.enabled = _spriteRenderer.enabled ? false : true;
        }
    }
    public void SetVisibility(bool isVisible)
    {
        if (_spriteRenderer != null)
        {
            _spriteRenderer.enabled = isVisible;
        }
    }
}
