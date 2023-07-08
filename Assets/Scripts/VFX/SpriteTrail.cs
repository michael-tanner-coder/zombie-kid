using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteTrail : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    
    [SerializeField] private Material _material;
    
    [SerializeField] private float _spawnRate;
    private float _currentSpawnRate;

    [SerializeField] private float _lifeSpan;
    
    [SerializeField] private Color _color;
    
    private List<GameObject> _spawnedTrailObjects = new List<GameObject>();

    void Update()
    {
        _currentSpawnRate -= Time.deltaTime;

        if (_currentSpawnRate <= 0f)
        {
            GameObject trailObject = new GameObject();
            trailObject.transform.position = transform.position;
            trailObject.transform.localRotation = transform.localRotation;
            SpriteRenderer _trailSpriteRenderer = trailObject.AddComponent<SpriteRenderer>();
            trailObject.AddComponent<LifeSpan>().SetLifeSpan(_lifeSpan);
            _trailSpriteRenderer.sprite = _spriteRenderer.sprite;
            _trailSpriteRenderer.material = _material;
            _trailSpriteRenderer.color = _color;
            _trailSpriteRenderer.sortingLayerName = _spriteRenderer.sortingLayerName;
            _trailSpriteRenderer.sortingOrder = _spriteRenderer.sortingOrder - 1;
            _spawnedTrailObjects.Add(trailObject);
            _currentSpawnRate = _spawnRate;
        }

        foreach(GameObject obj in _spawnedTrailObjects)
        {
            SpriteRenderer renderer = obj.GetComponent<SpriteRenderer>();
            Color newColor = new Color(renderer.color.r, renderer.color.g, renderer.color.b, renderer.color.a - Time.deltaTime);
            renderer.color = newColor;
        }
    }
}
