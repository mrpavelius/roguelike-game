using UnityEngine;

public class Room : MonoBehaviour
{
    public GameObject topDoor;
    public GameObject bottomDoor;
    public GameObject leftDoor;
    public GameObject rightDoor;

    public Vector2Int RoomIndex { get; set; }

    [SerializeField] private GameObject[] _enemies;

    private const int _minSpawn = 1;
    private const int _maxSpawn = 3;

    private void Start()
    {
        foreach (var enemy in _enemies)
        {
            for (int i = 0; i < Random.Range(_minSpawn, _maxSpawn); i++)
            {
                Instantiate(enemy, new Vector3(gameObject.transform.position.x + Random.Range(0, 4), gameObject.transform.position.y + Random.Range(0, 4)), Quaternion.identity);
            }
        }
    }
}