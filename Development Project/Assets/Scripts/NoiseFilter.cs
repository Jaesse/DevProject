using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleNoiseFilter : iNoiseFilter
{
    NoiseSettings.SimpleNoiseSettings settings;
    Noise noise = new Noise();


    public SimpleNoiseFilter(NoiseSettings.SimpleNoiseSettings settings)
    {
        this.settings = settings;

    }


    public float Evaluate(Vector3 point)
    {
        float noiseValue = 0;
        float frequency = settings.baseRoughness;
        float amplitute = 1;

        for (int i =1; i < settings.numLayers; i++)
        {
            float v = noise.Evaluate(point * frequency + settings.centre);
            noiseValue += (v + 1) * 0.5f * amplitute;
            frequency *= settings.roughness;
            amplitute *= settings.persistence;

        }

        noiseValue = noiseValue - settings.minValue;
        return noiseValue * settings.strength;
    }


}
