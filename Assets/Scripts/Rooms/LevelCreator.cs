using System.Collections.Generic;
using UnityEngine;

public class LevelCreator : MonoBehaviour
{
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
    private Room _finaleRoomPrefab;
    [SerializeField]
    private GameObject _gatewayPrefab;
    [SerializeField]
    private float _distanceBetweenRomms;
    [SerializeField]
    private int _numberOfRooms;
    [SerializeField]
    [Range(0, 1)]
    private float _chanceToConnectRooms;
    [SerializeField]
    private int _maxXPos;
    [SerializeField]
    private int _maxYPos;
    private Room[,] _roomsMap;

    private EnemySpawner _enemySpawner;
    private EnemyWaveCreator _enemyWaveCreator;
    private PowerUPHandler _powerUpHandler;

    public StartRoom StartRoom { get; private set; }
    public Room FinaleRoom { get; private set; }

    public void Initialize(EnemyWaveCreator enemyWaveCreator, EnemySpawner enemySpawner, PowerUPHandler powerUpHandler)
    {
        _enemySpawner = enemySpawner;
        _enemyWaveCreator = enemyWaveCreator;
        _powerUpHandler = powerUpHandler;
    }

    public Room[,] CreateLevel()
    {
        _roomsMap = new Room[_maxXPos, _maxYPos];
        Stack<Room> roomCreationStack = new Stack<Room>();
        var startRoomWorldPos = new Vector2(_startRoomMapPosX * _distanceBetweenRomms, _startRoomMapPosY * _distanceBetweenRomms);

        var startRoom = Instantiate(_startRoomPrefab, startRoomWorldPos, _startRoomPrefab.transform.rotation);
        StartRoom = startRoom;
        roomCreationStack.Push(startRoom);
        startRoom.Initialize(new Vector2Int(_startRoomMapPosX, _startRoomMapPosY));
        _roomsMap[_startRoomMapPosX, _startRoomMapPosY] = startRoom;

        Room currentRoom = startRoom;

        int numberOfGeneratedRooms = 0;
        while (numberOfGeneratedRooms < _numberOfRooms)
        {
            var adjacentEmptyPoints = new List<Vector2Int>();
            var adjacentRooms = new List<Room>();
            if (TryFindAllAdjacentEmptyPoint(currentRoom.MapPoistion, adjacentEmptyPoints))
            {
                int roomDifficulty = (int)_difficultyCurve.Evaluate(numberOfGeneratedRooms / _numberOfRooms) * 10;
                var roomMapPos = adjacentEmptyPoints[Random.Range(0, adjacentEmptyPoints.Count)];
                var roomWorldsPos = RoomMapToWorldsPosititon(roomMapPos);
                var roomPrefab = _trialRoomsPrefabs[Random.Range(0, _trialRoomsPrefabs.Count)];

                var newRoom = Instantiate(roomPrefab, roomWorldsPos, roomPrefab.transform.rotation);

                newRoom.Initialize(roomMapPos);
                var enemyWaves = _enemyWaveCreator.GenerateEnemyWaves(roomDifficulty, newRoom.MaxEnemiesInWave, newRoom.MinEnemiesInWave);
                newRoom.SetEnemyWaves(enemyWaves);
                newRoom.SetEnemySpawner(_enemySpawner);
                newRoom.SetRevardHandler(_powerUpHandler);

                _roomsMap[roomMapPos.x, roomMapPos.y] = newRoom;
                newRoom.ConnectToRoom(currentRoom);
                
                TryFindAllAdjacentRooms(newRoom.MapPoistion, adjacentRooms);
                adjacentRooms.Remove(currentRoom);
                ConnectRooms(newRoom, adjacentRooms);

                roomCreationStack.Push(newRoom);
                numberOfGeneratedRooms++;
                currentRoom = newRoom;
            }
            else
            {
                currentRoom = roomCreationStack.Pop();
            }
        }

        return _roomsMap;
    }

    private void ConnectRooms(Room originRoom, List<Room> adjacentRooms)
    {
        foreach (var adjacentRoom in adjacentRooms)
        {   
            if(RandomUtils.RandomBoolean(_chanceToConnectRooms * 100))
            {
                originRoom.ConnectToRoom(adjacentRoom);
            }
        }
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
