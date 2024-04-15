using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class ItemTaxis : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] bool targetPlayer = false;
    [SerializeField] float lerpSpeed = 0.1f;
    [SerializeField] float maxSpeed = 0f;
    [SerializeField] float detectionRadius = 5f;
    [SerializeField] bool avoidTarget = false;
    public float stunDuration = 1f;
    [SerializeField] Bounds bounds;

    private Coroutine stunRoutine;

    private void Awake()
    {
        var item = GetComponent<Item>();
        item.OnCarry.AddListener(() => { StopCoroutine(stunRoutine); enabled = false; });
        item.OnDrop.AddListener(() => stunRoutine = StartCoroutine(EnableAfterDrop()));
    }

    private IEnumerator EnableAfterDrop()
    {
        yield return new WaitForSeconds(stunDuration);
        enabled = true;
    }

    private void Start()
    {
        if(targetPlayer) target = Familiar.Instance.transform;
    }

    public void FixedUpdate()
    {
        if (!target) return;

        var difference = target.position - transform.position;
        if (difference.magnitude > detectionRadius) return;

        if (!avoidTarget)
        {
            var displacement = Vector3.Lerp(Vector3.zero, difference, lerpSpeed);
            if (displacement.magnitude > maxSpeed) displacement = displacement.normalized * maxSpeed;
            transform.position += new Vector3(displacement.x, displacement.y, 0);
        }
        else
        {
            difference *= -1;
            var distance = difference.magnitude;
            var maxMovement = difference.normalized * maxSpeed;
            var displacement = Vector3.Lerp(Vector3.zero, maxMovement, 1-distance/detectionRadius);
            transform.position += new Vector3(displacement.x, displacement.y, 0);
        }

        var closestPoint = (Vector2)bounds.ClosestPoint((Vector2)transform.position);
        if ((Vector2)transform.position != closestPoint)
        {
            transform.position = closestPoint;
        }
    }
}
