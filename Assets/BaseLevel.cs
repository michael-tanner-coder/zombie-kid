using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseLevel : MonoBehaviour
{
    [SerializeField] private int _levelIndex;
    [SerializeField] private LevelManagerParams _levelManagerParams;
    
    void Awake()
    {
        // update graphics of all rooms/doors to match the current level
        foreach(Transform child in transform)
        {
            Room room = child.GetComponent<Room>();
            if (room)
            {
                room.Background.sprite = _levelManagerParams.levels[_levelIndex].Graphics["room"];
                foreach(GameObject door in room.Doors)
                {
                    door.GetComponent<RoomWall>().Background.sprite = _levelManagerParams.levels[_levelIndex].Graphics["wall"];
                }
            }
        }
    }
}
