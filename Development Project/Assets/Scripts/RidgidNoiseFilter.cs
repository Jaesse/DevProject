using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RidgidNoiseFilter : iNoiseFilter 
{
    NoiseSettings.RidgidNoiseSettings settings;
    Noise noise = new Noise();


    public RidgidNoiseFilter(NoiseSettings.RidgidNoiseSettings settings)
    {
        this.settings = settings;

    }


    public float Evaluate(Vector3 point)
    {
        float noiseValue = 0;
        float frequency = settings.baseRoughness;
        float amplitute = 1;
        float weight = 1;

        for (int i = 1; i < settings.numLayers; ++i)
        {
            float v = 1-Mathf.Abs(noise.Evaluate(point * frequency + settings.centre));
            v *= v;
            v *= weight;
            weight = Mathf.Clamp01(v * settings.weightMultiplier);
            noiseValue += v  * amplitute;
            frequency *= settings.roughness;
            amplitute *= settings.persistence;

        }

        noiseValue =  noiseValue - settings.minValue;
        return noiseValue * settings.strength;
    }

}
