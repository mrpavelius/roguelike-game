using UnityEngine;

public class PlayerRoomTracker : MonoBehaviour
{
    private Room _currentRoom;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Room room = collision.GetComponentInParent<Room>();
        if (room != null)
        {
            _currentRoom = room;
            CameraController.Instance.UpdateCameraPosition(_currentRoom);
        }
    }
}