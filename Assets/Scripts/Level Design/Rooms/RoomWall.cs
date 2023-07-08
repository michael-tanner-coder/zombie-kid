using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomWall : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _background;
    public SpriteRenderer Background => _background;

    public void Awake()
    {
        _background = GetComponent<SpriteRenderer>();
    }
}
