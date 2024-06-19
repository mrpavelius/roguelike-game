using UnityEngine;

public class PlayerRoomTracker : MonoBehaviour
{
    private Room _currentRoom;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Room room = collision.GetComponent<Room>();
        if (room != null)
        {
            _currentRoom = room;
            Debug.Log(_currentRoom.transform.position.x);
            CameraController.Instance.UpdateCameraPosition(_currentRoom);
        }
    }

    public Room GetCurrentRoom()
    {
        return _currentRoom;
    }
}