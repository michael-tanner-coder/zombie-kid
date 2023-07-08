using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] private string _startingSong;

    void Start()
    {
        ServiceLocator.Instance.Get<MusicManager>().StartSong(_startingSong, 0, false);
    }
}
