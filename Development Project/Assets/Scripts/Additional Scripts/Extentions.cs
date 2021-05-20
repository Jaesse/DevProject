using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RandomXT { 

    public static Vector3 RandomVector3(Vector3 min, Vector3 max)
    {
        return new Vector3(Random.Range(min.x, max.x), Random.Range(min.y, max.y), Random.Range(min.z, max.z));
    }

    public static Vector3 RandomUnitrVector3()
    {
        return new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }

    public static bool RandomBool()
    {
        return Random.Range(0f, 1f) > 0.5f;
    }

    public static Gradient RandomGradient(Color[] colours)
    {
        GradientAlphaKey[] alphaKeys = new GradientAlphaKey[colours.Length];
        GradientColorKey[] colourKeys = new GradientColorKey[colours.Length];


        float time = 0;
        float increment = 1f / (float)colours.Length;
        for(int i = 0; i < colours.Length; ++i)
        {
            alphaKeys[i] = new GradientAlphaKey(colours[i].a, time);
            colourKeys[i] = new GradientColorKey(colours[i], time);
            time += increment;

        }
        Gradient gradient = new Gradient();
        gradient.alphaKeys = alphaKeys;
        gradient.colorKeys = colourKeys;
        return gradient;
    }

}
