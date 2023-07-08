using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjectArchitecture;

public class AmmoCounter : MonoBehaviour
{
    [Tooltip("Variable representing how much ammo is currently left")]
    [SerializeField] private IntVariable ammoCount;

    [Tooltip("Collection of animation frames corresponding to an ammo amount")]
    [SerializeField] Sprite[] sprites;
    
    [Tooltip("Sprite renderer to display the ammo counter frames")]
    [SerializeField] SpriteRenderer _spriteRenderer;

    void Update()
    {
        if (sprites[ammoCount.Value] != null)
        {
            _spriteRenderer.sprite = sprites[ammoCount.Value];
        }
    }
}
