using System.Collections.Generic;
using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Bosses/AttackPatterns/RotatingAndShooting")]
public class RotatingAndShootingAttackPattern : ShootingAttackPattern
{ 
    [SerializeField]
    private float _rotatingSpeed;

    protected override IEnumerator AttackCoroutine()
    {
        var newRotationAngle =_boss.transform.rotation.eulerAngles.z + _rotatingSpeed * Time.deltaTime;
        Timer attackTimer = new Timer(_attackSpeed);
        
        
        attackTimer.OnFinished += () =>
        {
            var attackDirection = Quaternion.Euler(0,0, newRotationAngle) * Vector3.up;
            _weapon.Attack(_boss.transform.position + attackDirection, _projectilePrefab, _boss.Target.transform,
                _numberOfProjectiles, _angleBetweenProjectiles);
            attackTimer.ResetTimer();
        };
        
        var timeElapsed = 0f;

        while(timeElapsed < _duration)
        {
            _boss.transform.rotation = Quaternion.Euler(0, 0, newRotationAngle);
            newRotationAngle = _boss.transform.rotation.eulerAngles.z + _rotatingSpeed * Time.deltaTime;

            attackTimer.UpdateTick(Time.deltaTime);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        OnAttackEnded();
    }
}

