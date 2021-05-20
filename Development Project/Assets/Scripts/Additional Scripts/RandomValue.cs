using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class RandomValue<T>
{
    [SerializeField]
    protected T min;
    [SerializeField]
    protected T max;
    [HideInInspector]
    public T lastValue;

    public RandomValue(T min, T max)
    {
        this.min = min;
        this.max = max;
    }


    public virtual T pickRandomValue()
    {
        return min;
    }




}

[System.Serializable]
public class RandomInt : RandomValue<int>
{
    public RandomInt(int min, int max) : base(min, max) { }
    public override int pickRandomValue()
    {
        return lastValue = Random.Range(min, max);
    }
}


[System.Serializable]
public class RandomFloat : RandomValue<float>
{
    public RandomFloat(float min, float max) : base(min, max) { }
    public override float pickRandomValue()
    {
        return lastValue = Random.Range(min, max);
    }
}


[System.Serializable]
public class RandomColor : RandomValue<Color>
{
    public RandomColor(Color min, Color max) : base(min, max) { }
    public override Color pickRandomValue()
    {
        return lastValue = new Color(Random.Range(min.r, max.r), Random.Range(min.g, max.g),
                                      Random.Range(min.b, max.b), Random.Range(min.a, max.a));
    }
}


[System.Serializable]
public class RandomSimpleNoise : RandomValue<NoiseSettings.SimpleNoiseSettings>
{
    public RandomSimpleNoise(NoiseSettings.SimpleNoiseSettings min, NoiseSettings.SimpleNoiseSettings max) : base(min, max) { }
    public override NoiseSettings.SimpleNoiseSettings pickRandomValue()
    {
        NoiseSettings.SimpleNoiseSettings settings = new NoiseSettings.SimpleNoiseSettings();
        settings.baseRoughness = Random.Range(min.baseRoughness, max.baseRoughness);
        settings.minValue = Random.Range(min.minValue, max.minValue);
        settings.numLayers = Random.Range(min.numLayers, max.numLayers);
        settings.persistence = Random.Range(min.persistence, max.persistence);
        settings.roughness = Random.Range(min.roughness, max.roughness);
        settings.strength = Random.Range(min.strength, max.strength);
        settings.centre = RandomXT.RandomVector3(min.centre, max.centre);
        return lastValue = settings;
    }
}