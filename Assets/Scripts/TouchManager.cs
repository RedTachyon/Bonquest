using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using Random = UnityEngine.Random;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

public class TouchManager : MonoBehaviour
{
    private Mouse _mouse;
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
        _mouse = Mouse.current;
        _camera = Camera.main;
        
        // EnhancedTouchSupport.Enable();

        if (_mouse != null)
        {
            InputSystem.EnableDevice(_mouse);
        }

    }

    public Vector3? GetClickLocation()
    {
        Vector3? pos = null;

        if (Touch.activeTouches.Count > 0 && Touch.activeTouches[0].phase == TouchPhase.Began)
        {
            pos = Touch.activeTouches[0].screenPosition;
            // pos = Camera.main.ScreenToWorldPoint(touchPosition);
            // Debug.Log($"Touched at {pos}");
        }
        else if (_mouse != null && _mouse.leftButton.wasPressedThisFrame)
        {
            pos = _mouse.position.ReadValue();
            // pos = Camera.main.ScreenToWorldPoint(clickPosition);
            // Debug.Log($"Clicked at {pos}");
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
            // if (hit.collider != null)
            // {
            //     target = hit.transform;
            // }
            
        }

        return target;
    }
}
