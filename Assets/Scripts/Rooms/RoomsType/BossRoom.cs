using UnityEngine;

public class BossRoom : TrialRoom
{
    [SerializeField]
    private Boss _bossPrefab;
    [SerializeField]
    private Transform _spawnPosition;
    private BossSpawner _bossSpawner;

    public Transform BossSpawnPosition => _spawnPosition;
    public Boss BossOfRoom => _bossPrefab;

    public void SetBossSpawner(BossSpawner bossSpawner)
    {
        _bossSpawner = bossSpawner;
    }

    protected override void StartRoomTrial()
    {
        base.StartRoomTrial();
        var spawnedBoss = _bossSpawner.SpawnBoss(this);
        spawnedBoss.OnDefeated += EndRoomTrial;
    }

    private void OnCollisionEnter2D(Collision2D collision)
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
