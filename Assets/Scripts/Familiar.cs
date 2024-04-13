//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class Familiar : MonoBehaviour
{
    public Transform carryTransform;
    public Transform dropTransform;

    [SerializeField] float lerpSpeed = 0.01f;
    [SerializeField] Vector2 lerpOffset = default;

    private Camera mainCamera;

    public static Familiar Instance => GameObject.FindGameObjectWithTag("Player").GetComponent<Familiar>();

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        Vector2 mouseWorld = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        transform.position = Vector2.Lerp(transform.position, mouseWorld + lerpOffset, lerpSpeed);
    }

}
