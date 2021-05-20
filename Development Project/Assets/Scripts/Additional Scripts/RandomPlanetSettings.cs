using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu()]
public class RandomPlanetSettings : ScriptableObject
{


    public RandomInt resolution;
    public RandomInt planetRadius;
    public RandomInt noiseLayers;

    public RandomSimpleNoise terrainNoiseSettings;
    public RandomColor oceanDepth;
    public RandomColor oceanSurface;
    public RandomColor ground;
    public RandomColor cliff;
    public RandomColor cliffTop;

    public RandomInt biomeCount;
    public RandomSimpleNoise biomeNoiseSettings;
    public RandomFloat biomeTintPercent;
    public RandomFloat biomeNoiseStrength;
    public RandomFloat biomeNoiseoffset;
    public RandomFloat biomeBlendAmount;
    public RandomFloat smoothness;

}
