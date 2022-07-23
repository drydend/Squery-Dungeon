using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TrialRoom : Room
{
    [SerializeField]
    protected Transform _centreOfRoom;
    [SerializeField]
    protected float _roomRadius;
    [SerializeField]
    protected AudioClip _compleationRoom;
    protected AudioSource _audioSource;

    protected IRewardHandler _rewardHandler;
    protected RoomState _roomState = RoomState.NotFinished;

    public override void Initialize(Vector2Int mapPosition)
    {
        base.Initialize(mapPosition);
        _audioSource = AudioSourceProvider.Instance.GetSoundsSource();
    }

    public void SetRevardHandler(IRewardHandler rewardHandler)
    {
        _rewardHandler = rewardHandler;
    }

    public Vector3 GetRandomPositionInRoom()
    {
        var randomPoint = _centreOfRoom.position + (Vector3)UnityEngine.Random.insideUnitCircle * _roomRadius;
        NavMeshHit navMeshHit;
        if (NavMesh.SamplePosition(randomPoint, out navMeshHit, 5f, NavMesh.AllAreas))
        {
            var enemyPosition = navMeshHit.position;
            return enemyPosition;
        }
        else if(NavMesh.SamplePosition(_centreOfRoom.position, out navMeshHit, _roomRadius, NavMesh.AllAreas))
        {
            var enemyPosition = navMeshHit.position;
            return enemyPosition;
        }
        Debug.LogException(new Exception("Can`t find random place on navMesh"));
        return Vector3.zero;
    }

    protected virtual void StartRoomTrial()
    {
        ActivateTraps();
        _upperEntrance.Close();
        _lowerEntrance.Close();
        _rightEntrance.Close();
        _leftEntrance.Close();
    }

    protected virtual void EndRoomTrial()
    {
        DeactivateTraps();
        _upperEntrance.Open();
        _lowerEntrance.Open();
        _rightEntrance.Open();
        _leftEntrance.Open();
        OnRoomCompleated();
        _audioSource.PlayOneShot(_compleationRoom);
        _rewardHandler.GiveReward();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Character character))
        {
            OnRoomEntered();
            if (_roomState == RoomState.NotFinished)
            {
               _roomState = RoomState.InProcess;
                StartRoomTrial();
            }
        }
    }
}
