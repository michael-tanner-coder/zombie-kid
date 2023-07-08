using UnityEngine;
using UnityEngine.Events;

public class LifeSpan : MonoBehaviour
{
    [SerializeField]
    private float _lifespan;
    public float Lifespan => _lifespan;

    [SerializeField]
    private UnityEvent _onDestroy;

    void Update() 
    {
        _lifespan -= Time.deltaTime;

        if (_lifespan <= 0f)
        {
            Destroy(gameObject);
        }
    }

    void OnDestroy() 
    {
        if (_onDestroy != null)
        {
            _onDestroy?.Invoke();
        }
    }

    public void SetLifeSpan(float lifespan)
    {
        _lifespan = lifespan;
    }
}
