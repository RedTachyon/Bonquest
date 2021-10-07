using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Node : MonoBehaviour
{
    public int army = 0;
    public int maxArmy = 100;

    public TextMeshPro counter;

    private Animation _clickAnimation;
    private void Start()
    {
        InvokeRepeating(nameof(Increment), 1, 1);

        _clickAnimation = GetComponent<Animation>();
    }
    
    private void Update()
    {
        counter.text = army.ToString();
        // int currentValue = Int32.Parse(text.text);
    }

    private void Increment()
    {
        if (army < maxArmy)
        {
            army++;
        }
    }

    public void Click()
    {
        // Debug.Log("Launching animation");
        _clickAnimation.Play("Selected");
    }

    public void Unclick()
    {
        Debug.Log("Stopping animation");
        _clickAnimation.Rewind();
        _clickAnimation.Sample();
        _clickAnimation.Stop();

    }
}