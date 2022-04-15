using UnityEngine;

public class Room : MonoBehaviour
{
    protected Vector2Int _mapPosition;
    protected int _maxConnections;
    protected int _connectionsAmount;
    [SerializeField]
    protected GameObject _gatewayPrefab;
    [SerializeField]
    protected RoomEntrance _upperEntrance;
    [SerializeField]
    protected RoomEntrance _lowerEntrance;
    [SerializeField]
    protected RoomEntrance _rightEntrance;
    [SerializeField]
    protected RoomEntrance _leftEntrance;

    public int MaxConnections => _maxConnections;
    public bool CanBeConnected => _maxConnections > _connectionsAmount;
    public Vector2Int MapPoistion => _mapPosition;

    public void Initialize(Vector2Int mapPosition, int connectionsAmount)
    {
        _mapPosition = mapPosition;
        _maxConnections = connectionsAmount;
        _upperEntrance.Block();
        _rightEntrance.Block();
        _leftEntrance.Block();
        _lowerEntrance.Block();
    }

    public bool TryConnectToRoom(Room room)
    {
        if (CanBeConnected && room.CanBeConnected)
        {
            room._connectionsAmount++;
            _connectionsAmount++;
            var directionToRoom =  room.MapPoistion - _mapPosition;

            var gatewayPosition = transform.position + (room.transform.position - transform.position) / 2;
            var gatewayRotation = (room.MapPoistion - MapPoistion).x == 0 ? 90 : 0;

            Instantiate(_gatewayPrefab, gatewayPosition, Quaternion.Euler(0, 0, gatewayRotation));

            if (directionToRoom == Vector2Int.up)
            {
                _upperEntrance.Unblock();
                room._lowerEntrance.Unblock();
            }
            else if (directionToRoom == Vector2Int.down)
            {
                _lowerEntrance.Unblock();
                room._upperEntrance.Unblock();
            }
            else if(directionToRoom == Vector2Int.right)
            {
                _rightEntrance.Unblock();
                room._leftEntrance.Unblock();
            }
            else if (directionToRoom == Vector2Int.left)
            {
                _leftEntrance.Unblock();
                room._rightEntrance.Unblock();
            }

            return true;
        }
        else
        {
            return false;
        }
    }
}
