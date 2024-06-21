using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public GameObject topDoor;
    public GameObject bottomDoor;
    public GameObject leftDoor;
    public GameObject rightDoor;

    public Vector2Int RoomIndex { get; set; }

    [SerializeField] private GameObject[] _enemies;
    private Vector2 roomSize = new Vector2(16.5f, 7.5f); // –азмеры комнаты (ширина, высота)

    private const int _minSpawn = 1;
    private const int _maxSpawn = 3;

    private List<GameObject> spawnedEnemies = new List<GameObject>();
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        foreach (var enemy in _enemies)
        {
            for (int i = 0; i < Random.Range(_minSpawn, _maxSpawn); i++)
            {
                var spawnedEnemy = Instantiate(enemy, new Vector3(transform.position.x + Random.Range(-3, 3), transform.position.y + Random.Range(-3, 3)), Quaternion.identity);
                spawnedEnemy.transform.parent = transform;
                spawnedEnemies.Add(spawnedEnemy);
            }
        }

        SetDoorsState(true); // Initially doors are open
    }

    private void Update()
    {
        if (IsPlayerInRoom())
        {
            UpdateDoors();
        }
    }

    private bool IsPlayerInRoom()
    {
        if (player == null) return false;

        // »спользуем Physics2D.OverlapBox дл€ определени€ нахождени€ игрока в пр€моугольной области комнаты
        Collider2D hit = Physics2D.OverlapBox(transform.position, roomSize, 0, LayerMask.GetMask("Player"));
        return hit != null && hit.gameObject == player;
    }

    private void UpdateDoors()
    {
        bool hasEnemies = CheckForEnemies();
        SetDoorsState(!hasEnemies);
    }

    private bool CheckForEnemies()
    {
        spawnedEnemies.RemoveAll(enemy => enemy == null); // Remove any destroyed enemies from the list
        return spawnedEnemies.Count > 0;
    }

    private void SetDoorsState(bool open)
    {
        topDoor.SetActive(!open);
        bottomDoor.SetActive(!open);
        leftDoor.SetActive(!open);
        rightDoor.SetActive(!open);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, roomSize);
    }
}