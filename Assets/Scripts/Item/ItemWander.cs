using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class ItemWander : MonoBehaviour
{
    public float Speed = 0f;
    public float NoiseScale = 1f;
    public float RotationScale = 1f;
    public float stunDuration = 1f;
    public Bounds bounds;

    private Vector2 Direction;
    private Coroutine stunRoutine;

    private void Awake()
    {
        Direction = Random.insideUnitCircle.normalized;
        var item = GetComponent<Item>();
        item.OnCarry.AddListener(() => { if (stunRoutine != null) StopCoroutine(stunRoutine); enabled = false; });
        item.OnDrop.AddListener(() => stunRoutine = StartCoroutine(EnableAfterDrop()));
    }

    private IEnumerator EnableAfterDrop()
    {
        yield return new WaitForSeconds(stunDuration);
        enabled = true;
    }

    private void FixedUpdate()
    {
        var noise = Mathf.PerlinNoise(transform.position.x, transform.position.y) - 0.5f;
        Direction = Quaternion.Euler(0f, 0f, noise * RotationScale) * Direction;
        var closestPoint = (Vector2)bounds.ClosestPoint((Vector2)transform.position);
        if ((Vector2)transform.position != closestPoint)
        {
            if (transform.position.x < bounds.center.x - bounds.extents.x || transform.position.x > bounds.center.x + bounds.extents.x) Direction.x *= -1;
            if (transform.position.y < bounds.center.y - bounds.extents.y || transform.position.y > bounds.center.y + bounds.extents.y) Direction.y *= -1;
            transform.position = closestPoint;
        }
        transform.position += (Vector3)Direction * Speed;
    }
}
