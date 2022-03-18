using System.Collections.Generic;
using UnityEngine;

public class LevelCreator : MonoBehaviour
{
    private const int _minRoomConnections = 2;
    private const int _maxRoomConnections = 4;
    [SerializeField]
    private AnimationCurve _difficultyCurve;
    [SerializeField]
    private List<TrialRoom> _trialRoomsPrefabs;
    [SerializeField]
    private List<Room> _specialRoomPrefabs;
    [SerializeField]
    private StartRoom _startRoomPrefab;
    [SerializeField]
    private int _startRoomMapPosX = 0;
    [SerializeField]
    private int _startRoomMapPosY = 0;
    [SerializeField]
    private Room _finaleRommPrefab;
    [SerializeField]
    private GameObject _gatewayPrefab;
    [SerializeField]
    private float _distanceBetweenRomms;
    [SerializeField]
    private int _numberOfRooms;
    [SerializeField]
    private int _maxXPos;
    [SerializeField]
    private int _maxYPos;
    private Room[,] _roomsMap;
    private float _minRoomConnectionsDelta;
    private float _maxRoomConnectionsDelta;

    public StartRoom StartRoom { get; private set; }
    public Room FinaleRoom { get; private set; }

    private void Awake()
    {
        _roomsMap = new Room[_maxXPos, _maxYPos];
    }

    public void CreateLevel(EnemyWaveCreator enemyWaveCreator, EnemySpawner enemySpawner)
    {
        Stack<Room> roomCreationStack = new Stack<Room>();
        var startRoomWorldPos = new Vector2(_startRoomMapPosX * _distanceBetweenRomms, _startRoomMapPosY * _distanceBetweenRomms);

        var startRoom = Instantiate(_startRoomPrefab, startRoomWorldPos, _startRoomPrefab.transform.rotation);
        StartRoom = startRoom;
        roomCreationStack.Push(startRoom);
        startRoom.Initialize(new Vector2Int(_startRoomMapPosX, _startRoomMapPosY), 1);
        _roomsMap[_startRoomMapPosX, _startRoomMapPosY] = startRoom;

        Room currentRoom = startRoom;

        int numberOfGeneratedRooms = 0;
        while (numberOfGeneratedRooms < _numberOfRooms)
        {
            var adjacentEmptyPoints = new List<Vector2Int>();
            var adjacentRooms = new List<Room>();
            if (currentRoom.CanBeConnected && TryFindAllAdjacentEmptyPoint(currentRoom.MapPoistion, adjacentEmptyPoints))
            {
                int roomDifficulty = (int)_difficultyCurve.Evaluate(numberOfGeneratedRooms / _numberOfRooms) * 10;
                var roomMapPos = adjacentEmptyPoints[Random.Range(0, adjacentEmptyPoints.Count)];
                var roomWorldsPos = RoomMapToWorldsPosititon(roomMapPos);
                var roomPrefab = _trialRoomsPrefabs[Random.Range(0, _trialRoomsPrefabs.Count)];

                var newRoom = Instantiate(roomPrefab, roomWorldsPos, roomPrefab.transform.rotation);

                newRoom.Initialize(roomMapPos, GetConnectionsAmountForRoom(roomMapPos));
                var enemyWaves = enemyWaveCreator.GenerateEnemyWaves(roomDifficulty, newRoom.MaxEnemiesInWave, newRoom.MinEnemiesInWave);
                newRoom.SetEnemyWaves(enemyWaves);
                newRoom.SetEnemySpawner(enemySpawner);

                _roomsMap[roomMapPos.x, roomMapPos.y] = newRoom;
                newRoom.TryConnectToRoom(currentRoom);
                TryFindAllAdjacentRooms(newRoom.MapPoistion, adjacentRooms);
                adjacentRooms.Remove(currentRoom);
                ConnectRooms(newRoom, adjacentRooms);

                if (newRoom.CanBeConnected)
                {
                    roomCreationStack.Push(newRoom);
                }
                numberOfGeneratedRooms++;
                currentRoom = newRoom;
            }
            else
            {
                currentRoom = roomCreationStack.Pop();
            }
        }

    }

    private void ConnectRooms(Room originRoom, List<Room> adjacentRooms)
    {
        foreach (var adjacentRoom in adjacentRooms)
        {
            originRoom.TryConnectToRoom(adjacentRoom);
        }
    }

    private int GetConnectionsAmountForRoom(Vector2Int roomMapPos)
    {
        var currentRoomMaxConnections = (int)Mathf.Round(_maxRoomConnections + _maxRoomConnectionsDelta);
        currentRoomMaxConnections = currentRoomMaxConnections > 4 ? 4 : currentRoomMaxConnections;
        var currentRoomMinConnections = (int)Mathf.Round(_minRoomConnections + _minRoomConnectionsDelta);
        currentRoomMinConnections = currentRoomMinConnections < 2 ? 1 : currentRoomMinConnections;

        if (roomMapPos.x == 0 || roomMapPos.x == _roomsMap.GetLength(0) - 1)
            currentRoomMaxConnections -= 1;
        if (roomMapPos.y == 0 || roomMapPos.y == _roomsMap.GetLength(0) - 1)
            currentRoomMaxConnections -= 1;

        var connectionsAmount = Random.Range(currentRoomMinConnections, currentRoomMaxConnections);

        switch (connectionsAmount)
        {
            case 1:
                _maxRoomConnectionsDelta = 0;
                _minRoomConnectionsDelta = 0;
                break;
            case 2:
                _maxRoomConnectionsDelta += 0.5f;
                _minRoomConnectionsDelta += 0.5f;
                break;
            case 3:
                _maxRoomConnectionsDelta -= 0.4f;
                _minRoomConnectionsDelta -= 0.4f;
                break;
            case 4:
                _maxRoomConnectionsDelta -= 0.4f;
                _minRoomConnectionsDelta -= 0.4f;
                break;
        }

        return connectionsAmount;
    }

    private bool TryFindAllAdjacentRooms(Vector2Int currentRoomMapPos, List<Room> adjacentRooms)
    {
        foreach (var point in _roomsMap.GetIndexesOfAllAdjacentElements(currentRoomMapPos))
        {
            if (_roomsMap[point.x, point.y] != null)
            {
                adjacentRooms.Add(_roomsMap[point.x, point.y]);
            }
        }

        return adjacentRooms.Count > 0;
    }

    private bool TryFindAllAdjacentEmptyPoint(Vector2Int currentRoomMapPos, List<Vector2Int> adjacentEmptyPoints)
    {
        foreach (var point in _roomsMap.GetIndexesOfAllAdjacentElements(currentRoomMapPos))
        {
            if (_roomsMap[point.x, point.y] == null)
            {
                adjacentEmptyPoints.Add(new Vector2Int(point.x, point.y));
            }
        }

        return adjacentEmptyPoints.Count > 0;
    }

    public Vector2 RoomMapToWorldsPosititon(Vector2Int mapPosition)
    {
        return (Vector2)mapPosition * _distanceBetweenRomms;
    }
}
