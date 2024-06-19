using UnityEngine;

public class Room : MonoBehaviour
{
    public GameObject topDoor;
    public GameObject bottomDoor;
    public GameObject leftDoor;
    public GameObject rightDoor;

    public Vector2Int RoomIndex { get; set; }
}