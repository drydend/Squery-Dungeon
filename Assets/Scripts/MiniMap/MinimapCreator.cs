using UnityEngine;
using UnityEngine.UI;

public class MinimapCreator : MonoBehaviour
{
    [SerializeField]
    private float _roomIconWidth;
    [SerializeField]
    private float _gatewayIconWidth;

    [SerializeField]
    private RectTransform _parent;

    [SerializeField]
    private MinimapPassageIcon _passageIconPrefab;
    [SerializeField]
    private MinimapRoomIcon _roomIconsPrefab;

    public MinimapRoomIcon[,] GenerateMinimap(Room[,] roomMap)
    {
        var mapWidth = roomMap.GetLength(0);
        var mapHeight = roomMap.GetLength(1);

        var _roomsIcons = new MinimapRoomIcon[mapWidth, mapHeight];
        
        var newSizeDelta = new Vector2(mapWidth * _roomIconWidth + (mapWidth + 1) * _gatewayIconWidth,
            mapHeight * _roomIconWidth + (mapHeight + 1) * _gatewayIconWidth);
        var positionDelta = _parent.anchoredPosition + (_parent.sizeDelta - newSizeDelta) / 2;
        _parent.sizeDelta = newSizeDelta;
        _parent.anchoredPosition = positionDelta;


        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                var currentRoom = roomMap[x, y];

                if (currentRoom != null)
                {
                    var roomIcon = Instantiate(_roomIconsPrefab, _parent);
                    var roomIconPosition = new Vector2(
                        (_roomIconWidth + _gatewayIconWidth) * (x - (float)mapWidth / 2) + (_roomIconWidth + _gatewayIconWidth) / 2,
                        (_roomIconWidth + _gatewayIconWidth) * (y - (float)mapHeight / 2) + (_roomIconWidth + _gatewayIconWidth) / 2); 
                    var roomIconSize = new Vector2(_roomIconWidth, _roomIconWidth);

                    roomIcon.Initialize(currentRoom.MinimapIcon, roomIconSize, roomIconPosition);

                    _roomsIcons[x, y] = roomIcon;

                    foreach (var connectedRoom in currentRoom.ConnectedRooms)
                    {
                        if(_roomsIcons[connectedRoom.MapPoistion.x, connectedRoom.MapPoistion.y] != null)
                        {
                            var directionToRoom = currentRoom.GetDirectionToRoom(connectedRoom);

                            var passageIcon = Instantiate(_passageIconPrefab, _parent);
                            var passageIconPosition = roomIconPosition + new Vector2(_roomIconWidth / 2 + _gatewayIconWidth / 2,
                                _roomIconWidth / 2 + _gatewayIconWidth / 2) * directionToRoom;
                            var passageIconSize = new Vector2(_gatewayIconWidth, _gatewayIconWidth / 2);
                            var passageIconRotation  = new Vector3();
                            passageIconRotation.z = directionToRoom.x == 0 ? 90 : 0;

                            passageIcon.Initialize(passageIconSize, passageIconPosition, passageIconRotation,
                                _roomsIcons[connectedRoom.MapPoistion.x, connectedRoom.MapPoistion.y], roomIcon);
                            roomIcon.ConnectIcons(_roomsIcons[connectedRoom.MapPoistion.x, connectedRoom.MapPoistion.y], passageIcon);
                        }
                    }
                }
            }
        }

        return _roomsIcons;
    }

}

