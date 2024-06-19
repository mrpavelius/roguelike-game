using System.Collections.Generic;
using UnityEngine;

public class RoomsPlacer : MonoBehaviour
{
    [Header("Комнаты для генерации")]
    [SerializeField] private Room _startRoom;

    [SerializeField] private Room _finalRoom;
    [SerializeField] private Room[] _roomPrefabs;

    [Header("Настройки генерации")]
    [SerializeField] private int _maxRooms = 10;

    [SerializeField] private int _minRooms = 3;

    [Header("Размеры комнат")]
    [SerializeField] private float roomWidth = 18;

    [SerializeField] private float roomHeight = 10;

    [Header("Размеры сетки")]
    [SerializeField] private int gridSizeX = 10;

    [SerializeField] private int gridSizeY = 10;

    private List<Room> _roomObjects = new List<Room>();
    private Queue<Vector2Int> _roomQueue = new Queue<Vector2Int>();
    private int[,] _roomGrid;
    private int _roomCount;
    private int _targetRoomCount;
    private bool _generationComplete = false;

    private void Start()
    {
        _roomGrid = new int[gridSizeX, gridSizeY];
        _roomQueue = new Queue<Vector2Int>();

        _targetRoomCount = Random.Range(_minRooms, _maxRooms + 1);

        Vector2Int initialRoomIndex = new Vector2Int(gridSizeX / 2, gridSizeY / 2);
        StartRoomGenerationFromRoom(initialRoomIndex);

        while (_roomCount < _minRooms)
        {
            Vector2Int roomIndex = new Vector2Int(Random.Range(0, gridSizeX), Random.Range(0, gridSizeY));
            TryGenerateRoom(roomIndex);
        }
    }

    private void Update()
    {
        if (!_generationComplete)
        {
            if (_roomQueue.Count > 0 && _roomCount < _targetRoomCount)
            {
                Vector2Int roomIndex = _roomQueue.Dequeue();
                GenerateAdjacentRooms(roomIndex);
            }
            else if (_roomCount >= _minRooms)
            {
                GenerateBossRoom();
                _generationComplete = true;
                Debug.Log($"Генерация завершена, {_roomCount} комнат создано");
            }
        }
    }

    private void GenerateAdjacentRooms(Vector2Int roomIndex)
    {
        int gridX = roomIndex.x;
        int gridY = roomIndex.y;

        TryGenerateRoom(new Vector2Int(gridX - 1, gridY));
        TryGenerateRoom(new Vector2Int(gridX + 1, gridY));
        TryGenerateRoom(new Vector2Int(gridX, gridY - 1));
        TryGenerateRoom(new Vector2Int(gridX, gridY + 1));
    }

    private void GenerateBossRoom()
    {
        Vector2Int roomIndex;
        do
        {
            roomIndex = new Vector2Int(Random.Range(0, gridSizeX), Random.Range(0, gridSizeY));
        } while (!TryGenerateRoom(roomIndex, true));
    }

    private bool TryGenerateRoom(Vector2Int roomIndex, bool isBossRoom = false)
    {
        int x = roomIndex.x;
        int y = roomIndex.y;

        if (x < 0 || x >= gridSizeX || y < 0 || y >= gridSizeY || _roomGrid[x, y] != 0)
            return false;

        if (!HasAdjacentRoom(roomIndex) && _roomCount != 0)
            return false;

        int adjacentRooms = CountAdjacentRooms(roomIndex);

        if (adjacentRooms > 1)
            return false;

        if (_roomCount >= _targetRoomCount && !isBossRoom)
            return false;

        _roomQueue.Enqueue(roomIndex);
        _roomGrid[x, y] = 1;
        _roomCount++;

        Room newRoomPrefab = isBossRoom ? _finalRoom : _roomPrefabs[Random.Range(0, _roomPrefabs.Length)];
        var newRoom = Instantiate(newRoomPrefab, GetPositionFromGridIndex(roomIndex), Quaternion.identity);
        newRoom.GetComponent<Room>().RoomIndex = roomIndex;
        newRoom.name = isBossRoom ? "BossRoom" : $"Room-{_roomCount}";
        _roomObjects.Add(newRoom);

        return true;
    }

    private bool HasAdjacentRoom(Vector2Int roomIndex)
    {
        int x = roomIndex.x;
        int y = roomIndex.y;

        bool left = x > 0 && _roomGrid[x - 1, y] != 0;
        bool right = x < gridSizeX - 1 && _roomGrid[x + 1, y] != 0;
        bool up = y < gridSizeY - 1 && _roomGrid[x, y + 1] != 0;
        bool down = y > 0 && _roomGrid[x, y - 1] != 0;

        return left || right || up || down;
    }

    private int CountAdjacentRooms(Vector2Int roomIndex)
    {
        int x = roomIndex.x;
        int y = roomIndex.y;
        int count = 0;

        if (x > 0 && _roomGrid[x - 1, y] != 0) count++;
        if (x < gridSizeX - 1 && _roomGrid[x + 1, y] != 0) count++;
        if (y > 0 && _roomGrid[x, y - 1] != 0) count++;
        if (y < gridSizeY - 1 && _roomGrid[x, y + 1] != 0) count++;

        return count;
    }

    private void StartRoomGenerationFromRoom(Vector2Int roomIndex)
    {
        _roomQueue.Enqueue(roomIndex);
        int x = roomIndex.x;
        int y = roomIndex.y;
        _roomGrid[x, y] = 1;
        _roomCount++;
        var initialRoom = Instantiate(_startRoom, GetPositionFromGridIndex(roomIndex), Quaternion.identity);
        initialRoom.name = $"Room-{_roomCount}";
        initialRoom.GetComponent<Room>().RoomIndex = roomIndex;
        _roomObjects.Add(initialRoom);
    }

    private Vector3 GetPositionFromGridIndex(Vector2Int gridIndex)
    {
        int gridX = gridIndex.x;
        int gridY = gridIndex.y;
        return new Vector2(
            roomWidth * (gridX - gridSizeX / 2),
            roomHeight * (gridY - gridSizeY / 2));
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 1, 1, 0.05f);

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 position = GetPositionFromGridIndex(new Vector2Int(x, y));
                Gizmos.DrawWireCube(position, new Vector3(roomWidth, roomHeight, 1));
            }
        }
    }
}