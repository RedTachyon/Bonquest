using UnityEngine;

public class ColorScheme
{
    public static Color GetColor(int index)
    {
        Color color;
        switch (index)
        {
            case 0:
                color = Color.white;
                break;
            case 1:
                color = Color.red;
                break;
            default:
                color = Color.blue;
                break;
        }

        return color;
    } 
}