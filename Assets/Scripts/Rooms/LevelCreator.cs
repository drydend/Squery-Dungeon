using System.Collections.Generic;
using UnityEngine;

public class LevelCreator : MonoBehaviour
{
    private const float DistanceBetweenRomms = 50;
    [SerializeField]
    private AnimationCurve _difficultyCurve;

    [SerializeField]
    private List<GameObject> _passages;
    [SerializeField]
    private List<TrialRoomWithEnemies> _trialRoomsWithEnemies;
    [SerializeField]
    private List<Room> _specialRoomPrefabs;
    [SerializeField]
    private StartRoom _startRoomPrefab;
    [SerializeField]
    private BossRoom _bossRoomPrefab;


    [SerializeField]
    private int _startRoomMapPosX = 0;
    [SerializeField]
    private int _startRoomMapPosY = 0;
    [SerializeField]
    private GameObject _gatewayPrefab;
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
    private Room _lastCreatedRoom;

    [SerializeField]
    private EnemySpawner _enemySpawner;
    [SerializeField]
    private BossSpawner _bossSpawner;
    [SerializeField]
    private EnemyWaveCreator _enemyWaveCreator;
    [SerializeField]
    private PowerUPHandler _powerUpHandler;
    [SerializeField]
    private RewardHandlerOfFinalRoom _rewardHandlerOfFinalRoom;

    public StartRoom StartRoom { get; private set; }
    public Room FinaleRoom { get; private set; }

    public Room[,] CreateLevel()
    {
        _roomsMap = new Room[_maxXPos, _maxYPos];
        Stack<Room> roomCreationStack = new Stack<Room>();

        var startRoom = CreateStartRoom();

        roomCreationStack.Push(startRoom);
        _lastCreatedRoom = startRoom;

        int numberOfGeneratedRooms = 0;
       
        while (numberOfGeneratedRooms < _numberOfRooms)
        {
            var adjacentEmptyPoints = new List<Vector2Int>();
           
            if (TryFindAllAdjacentEmptyPoint(_lastCreatedRoom.MapPoistion, adjacentEmptyPoints) 
                && _lastCreatedRoom.CanBeConnected)
            {
                var newRoom = CreateTrialRoomWithEnemies(adjacentEmptyPoints, numberOfGeneratedRooms);

                roomCreationStack.Push(newRoom);
                numberOfGeneratedRooms++;
                _lastCreatedRoom = newRoom;
            }
            else
            {
                _lastCreatedRoom = roomCreationStack.Pop();
            }
        }

        CreateBossRoom();

        return _roomsMap;
    }

    private BossRoom CreateBossRoom()
    {   
        Room furthestRoom = StartRoom;
        float sumOfFurthestRoomCoordinatDifference = 0;
        Vector2Int bossRoomMapPosition = Vector2Int.zero;

        for (int x = 0; x < _roomsMap.GetLength(0); x++)
        {
            for (int y = 0; y < _roomsMap.GetLength(1); y++)
            {
                if(_roomsMap[x,y] == null)
                {
                    continue;
                }

                float sumOfCoordinatDifference = Mathf.Abs(x - StartRoom.MapPoistion.x) + Mathf.Abs(y - StartRoom.MapPoistion.y);
                
                if(sumOfCoordinatDifference > sumOfFurthestRoomCoordinatDifference )
                {   
                    var adjacentEmptyPoints = new List<Vector2Int>();
                    if (TryFindAllAdjacentEmptyPoint(new Vector2Int(x, y), adjacentEmptyPoints))
                    {
                        furthestRoom = _roomsMap[x, y];
                        sumOfFurthestRoomCoordinatDifference = sumOfCoordinatDifference;
                        bossRoomMapPosition = adjacentEmptyPoints.GetRandomValue();
                    }
                }
            }
        }

        var roomWordsPosition = RoomMapToWorldsPosititon(bossRoomMapPosition);

        var bossRoom = Instantiate(_bossRoomPrefab, roomWordsPosition, _bossRoomPrefab.transform.rotation);
        bossRoom.Initialize(bossRoomMapPosition);
        bossRoom.SetBossSpawner(_bossSpawner);
        bossRoom.SetRevardHandler(_rewardHandlerOfFinalRoom);

        _roomsMap[bossRoom.MapPoistion.x, bossRoom.MapPoistion.y] = bossRoom;
        bossRoom.ConnectToRoom(furthestRoom, _passages.GetRandomValue());

        return bossRoom;

    }

    private TrialRoomWithEnemies CreateTrialRoomWithEnemies(List<Vector2Int> adjacentEmptyPoints, int roomNumber)
    {
        int roomDifficulty = (int)_difficultyCurve.Evaluate(roomNumber / _numberOfRooms) * 10;
        var roomMapPos = adjacentEmptyPoints[Random.Range(0, adjacentEmptyPoints.Count)];
        var roomWorldsPos = RoomMapToWorldsPosititon(roomMapPos);
        var roomPrefab = _trialRoomsWithEnemies[Random.Range(0, _trialRoomsWithEnemies.Count)];

        var newRoom = Instantiate(roomPrefab, roomWorldsPos, roomPrefab.transform.rotation);

        newRoom.Initialize(roomMapPos);
        var enemyWaves = _enemyWaveCreator.GenerateEnemyWaves(roomDifficulty, newRoom.MaxEnemiesInWave, newRoom.MinEnemiesInWave);
        newRoom.SetEnemyWaves(enemyWaves);
        newRoom.SetEnemySpawner(_enemySpawner);
        newRoom.SetRevardHandler(_powerUpHandler);

        _roomsMap[roomMapPos.x, roomMapPos.y] = newRoom;
        newRoom.ConnectToRoom(_lastCreatedRoom , _passages.GetRandomValue());

        if (newRoom.CanBeConnected)
        {
            var adjacentRooms = new List<Room>();
            TryFindAllAdjacentRooms(newRoom.MapPoistion, adjacentRooms);
            adjacentRooms.Remove(_lastCreatedRoom);
            ConnectRooms(newRoom, adjacentRooms);
        }

        return newRoom;
    }

    private StartRoom CreateStartRoom()
    {
        var startRoomWorldPos = new Vector2(_startRoomMapPosX * DistanceBetweenRomms, _startRoomMapPosY * DistanceBetweenRomms);

        var startRoom = Instantiate(_startRoomPrefab, startRoomWorldPos, _startRoomPrefab.transform.rotation);
        StartRoom = startRoom;
        startRoom.Initialize(new Vector2Int(_startRoomMapPosX, _startRoomMapPosY));
        _roomsMap[_startRoomMapPosX, _startRoomMapPosY] = startRoom;

        return startRoom;
    }

    private void ConnectRooms(Room originRoom, List<Room> adjacentRooms)
    {
        foreach (var adjacentRoom in adjacentRooms)
        {
            if (RandomUtils.RandomBoolean(_chanceToConnectRooms * 100) && adjacentRoom.CanBeConnected)
            {
                originRoom.ConnectToRoom(adjacentRoom, _passages.GetRandomValue());
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
        return (Vector2)mapPosition * DistanceBetweenRomms;
    }
}
