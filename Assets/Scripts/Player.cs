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
        var clickedNode = TouchManager.Instance.GetClickedNode(clickLocation);
        
        // Debug.Log("Update");

        if (clickLocation is { })
        {
            
            if (clickedNode != null && _focus is null)
            {
                // You clicked something, nothing is in focus
                // Clicked object enters focus
                _focus = clickedNode;
                _focus.Click();
            } 
            else if (clickedNode == null && _focus != null)
            {
                // You clicked empty space, something is in focus
                // Remove focus
                _focus.Unclick();
                _focus = null;
            } 
            else if (clickedNode != null && _focus != null)
            {
                // You clicked something, something is in focus
                // Fun begins
                ProcessAttack(_focus, clickedNode);
                _focus = null;
            }
            else if (clickedNode == null && _focus is null)
            {
                // Chill, nothing to do
            }
            
        }

        
        // else if (clickLocation is { } && _focus is { })
        // {
        //     // Debug.Log("Unclicking");
        //     _focus.Unclick();
        //     _focus = null;
        // }
    }

    private void ProcessAttack(Node source, Node target)
    {
        if (source == target)
        {
            Debug.Log("Clicking the same guy");
            source.Unclick();
            source.Click();
            return;
        }
        
        // Start an army animation?

        Debug.Log("Clicking someone else");
        source.Unclick();
        
        source.RemoveArmy(5);
        target.RemoveArmy(10);
    }
}
