using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance;

    [SerializeField] private float transitionSpeed = 5f;

    private Transform _cameraTransform;
    private Vector3 _targetPosition;
    private bool _isMoving;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        _cameraTransform = Camera.main.transform;
        _targetPosition = _cameraTransform.position;
    }

    private void Update()
    {
        if (_isMoving)
        {
            _cameraTransform.position = Vector3.Lerp(_cameraTransform.position, _targetPosition, transitionSpeed * Time.deltaTime);
            if (Vector3.Distance(_cameraTransform.position, _targetPosition) < 0.01f)
            {
                _cameraTransform.position = _targetPosition;
                _isMoving = false;
            }
        }
    }

    public void UpdateCameraPosition(Room room)
    {
        if (room != null)
        {
            _targetPosition = new Vector3(room.transform.position.x, room.transform.position.y, _cameraTransform.position.z);
            _isMoving = true;
        }
    }
}