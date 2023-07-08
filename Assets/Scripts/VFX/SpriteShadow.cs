using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteShadow : MonoBehaviour
{
    public Vector2 offset  = new Vector2(-3,-3);
    private SpriteRenderer sprRndCaster;
    private SpriteRenderer sprRndShadow;

    private Transform transCaster;
    public Transform transShadow;

    public Material shadowMaterial;
    public Color shadowColor;

    void Start()
    {
        transCaster = transform;

        if (transShadow == null)
        {
            transShadow = new GameObject().transform;
        }

        transShadow.parent = transCaster;
        transShadow.gameObject.name = "shadow";
        transShadow.localRotation = Quaternion.identity;
        transShadow.localScale = transCaster.localScale;

        sprRndCaster = GetComponent<SpriteRenderer>();

        sprRndShadow = transShadow.gameObject.GetComponent<SpriteRenderer>();
        if (sprRndShadow == null)
        {
            sprRndShadow = transShadow.gameObject.AddComponent<SpriteRenderer>();
        }

        sprRndShadow.material = shadowMaterial;
        sprRndShadow.color = shadowColor;
        sprRndShadow.sortingLayerName = sprRndCaster.sortingLayerName;
        sprRndShadow.sortingOrder = sprRndShadow.sortingOrder - 1;
    }

    void LateUpdate()
    {
        transShadow.position = new Vector2(transCaster.position.x + offset.x, transCaster.position.y + offset.y);

        sprRndShadow.sprite = sprRndCaster.sprite;
    }
}
