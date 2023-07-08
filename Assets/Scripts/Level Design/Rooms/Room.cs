using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _background;
    public SpriteRenderer Background => _background;
    public List<GameObject> Doors = new List<GameObject>();

    public void Awake()
    {
        _background = GetComponent<SpriteRenderer>();
    }
}
