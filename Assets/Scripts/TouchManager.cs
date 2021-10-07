using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using Random = UnityEngine.Random;

public class TouchManager : MonoBehaviour
{
    private Camera _camera;

    public static TouchManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        } else {
            Instance = this;
        }
    }
    private void Start()
    {
        _camera = Camera.main;

    }

    public Vector3? GetClickLocation()
    {
        // Return the screen position of a click
        Vector3? pos = null;

        if (Input.GetMouseButtonDown(0))
        {
            pos = Input.mousePosition;
        }

        return pos;
    }

    [CanBeNull]
    public Transform GetClickedObject(Vector3? clickLocation)
    {
        Transform target = null;
        if (clickLocation.HasValue)
        {

            var ray = _camera.ScreenPointToRay(clickLocation.Value);
            
            var hit = Physics2D.Raycast(ray.origin, ray.direction);
            Debug.DrawRay(ray.origin, ray.direction*15f, Color.red, 1);

            target = hit.transform;
        }

        return target;
    }

    [CanBeNull]
    public Node GetClickedNode(Vector3? clickLocation)
    {
        Transform target = GetClickedObject(clickLocation);
        return target != null ? target.GetComponent<Node>() : null;
    }
}
