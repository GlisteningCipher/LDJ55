//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class Familiar : MonoBehaviour
{
    public Transform carryTransform;
    public Transform dropTransform;

    [SerializeField] float lerpSpeed = 0.01f;
    [SerializeField] Vector2 lerpOffset = default;
    [SerializeField] Bounds playerBounds;

    private Camera mainCamera;

    public static Familiar Instance => GameObject.FindGameObjectWithTag("Player").GetComponent<Familiar>();

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(playerBounds.center, playerBounds.extents * 2f);
    }

    void Update()
    {
        Vector2 mouseWorld = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 destination = playerBounds.ClosestPoint(mouseWorld);
        transform.position = Vector2.Lerp(transform.position, destination + lerpOffset, lerpSpeed);
    }

}
