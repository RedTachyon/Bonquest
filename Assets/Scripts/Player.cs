using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public Node _focus = null;

    private static Player _instance;
    public static Player Instance => _instance;
    
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        } else {
            _instance = this;
        }
    }



    private void Update()
    {
        var clickLocation = TouchManager.Instance.GetClickLocation();
        var clicked = TouchManager.Instance.GetClickedObject(clickLocation);
        
        // Debug.Log("Update");
        if (clicked is { })
        {
            // Debug.Log("clicking");
            _focus?.Unclick();
            _focus = clicked.GetComponent<Node>();
            _focus.Click();
        }
        else if (clickLocation is { } && _focus is { })
        {
            // Debug.Log("Unclicking");
            _focus.Unclick();
            _focus = null;
        }
    }
    
}
