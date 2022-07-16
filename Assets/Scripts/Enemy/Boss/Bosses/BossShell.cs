using System.Collections;
using UnityEngine;
using System;

[RequireComponent(typeof(SpriteRenderer), typeof(PolygonCollider2D))]
public class BossShell : MonoBehaviour
{
    [SerializeField]
    private int _shellOrder;
    [SerializeField]
    private ParticleSystem _destructionParticle;
    [SerializeField]
    private ParticleSystem _hitParticle;
    [SerializeField]
    private float _hitAnimationDuration = 0.1f;
    [SerializeField]
    private float _cameraShakeOnDestuctDuration = 1;

    private SpriteRenderer _spriteRenderer;
    private Color _startColor;

    public PolygonCollider2D Collider2D { get; private set; }
    public int ShellOrder => _shellOrder;
    public Transform Transform => transform;

    private void Awake()
    {
        Collider2D = GetComponent<PolygonCollider2D>();
        Collider2D.enabled = false;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _startColor = _spriteRenderer.color;
    }

    public void DestroyShell()
    {
        CameraShaker.Instance.ShakeCamera(_cameraShakeOnDestuctDuration, 0.2f, false);
        Collider2D.enabled = false;
        Instantiate(_destructionParticle, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    public void OnHit()
    {
        Instantiate(_hitParticle, transform.position, Quaternion.identity);
        StartCoroutine(HitAnimation());
    }

    private IEnumerator HitAnimation()
    {
        _spriteRenderer.color = Color.white;
        yield return new WaitForSeconds(_hitAnimationDuration);
        _spriteRenderer.color = _startColor;
    }

    public void RecieveHit(float damage, GameObject sender)
    {
        throw new System.NotImplementedException();
    }
}
