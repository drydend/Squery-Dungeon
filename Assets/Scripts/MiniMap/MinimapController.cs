using System;
using UnityEngine;

public class MinimapController : MonoBehaviour
{
    [SerializeField]
    private MinimapCreator _minimapCreator;
    private Room[,] _roomMap;
    private MinimapRoomIcon[,] _minimap;

    private MinimapRoomIcon _selectedRoom;

    public void Initialize(Room[,] roomMap)
    {
        _roomMap = roomMap;

        _minimap = _minimapCreator.GenerateMinimap(_roomMap);

        for (int x = 0; x < _minimap.GetLength(0); x++)
        {
            var x1 = x;
            for (int y = 0; y < _minimap.GetLength(1); y++)
            {
                var y1 = y;

                if (_roomMap[x, y] == null)
                {
                    continue;
                }
                else if (_minimap[x, y] == null)
                {
                    throw new Exception("Minimap generated incorrectly");
                }

                _minimap[x, y].BecameInvisible();

                _roomMap[x, y].OnEntered += () =>
                {   
                    if(_selectedRoom != null)
                    {
                        _selectedRoom.Unselect();
                    }

                    _minimap[x1, y1].Select();
                    _selectedRoom = _minimap[x1, y1];
                };

                _roomMap[x, y].OnCompleated += () =>
                {
                    _minimap[x1, y1].BecameVisible();
                    _minimap[x1, y1].CompleateAllPassages();

                    foreach (var roomIcon in _minimap[x1, y1].ConnectedRoomIcons)
                    {
                        roomIcon.BecameVisible();
                    }
                };
            }
        }
    }
}

