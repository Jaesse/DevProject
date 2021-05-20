using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlanetGenerator : MonoBehaviour
{

    [Header("Random Planet Attributes")]
    public RandomPlanetSettings settings;

    [HideInInspector]
    public bool settingsFoldout = false;

    [Header("Other attributes")]
    public Material copyMaterial;


    private GameObject planetgo;
    private Planet planet;

    private void Awake()
    {
        planetgo = new GameObject("Generated Planet");
        planet = planetgo.AddComponent<Planet>();

        // terrain settings randomisation
        ShapeSettings shapeSettings = new ShapeSettings();
        shapeSettings.planetRadius = settings.planetRadius.pickRandomValue();
        shapeSettings.noiseLayers = new ShapeSettings.NoiseLayer[settings.noiseLayers.pickRandomValue()];
        for (int i = 0; i < shapeSettings.noiseLayers.Length; ++i)
        {
            ShapeSettings.NoiseLayer randLayer = new ShapeSettings.NoiseLayer();
            randLayer.enabled = true;
            randLayer.useFirstLayerAsMask = i == 0 ? false : RandomXT.RandomBool();

            NoiseSettings randNoiseSettings = new NoiseSettings();
            randNoiseSettings.filterType = NoiseSettings.FilterType.Simple;
            randNoiseSettings.simpleNoiseSettings = settings.terrainNoiseSettings.pickRandomValue();


            randLayer.noiseSettings = randNoiseSettings;
            shapeSettings.noiseLayers[i] = randLayer;
        }

        // colour settings randomisation
        ColourSettings colourSettings = new ColourSettings();
        colourSettings.planetMaterial = new Material(copyMaterial);
        colourSettings.planetMaterial.SetFloat("_smoothness", settings.smoothness.pickRandomValue());
        colourSettings.biomeColourSettings = new ColourSettings.BiomeColourSettings();
        colourSettings.biomeColourSettings.blendAmount = settings.biomeBlendAmount.pickRandomValue();
        colourSettings.biomeColourSettings.noiseOffset = settings.biomeNoiseoffset.pickRandomValue();
        colourSettings.biomeColourSettings.noiseStrength = settings.biomeNoiseStrength.pickRandomValue();


        NoiseSettings biomenoisesettings = new NoiseSettings();
        biomenoisesettings.filterType = NoiseSettings.FilterType.Simple;
        
        biomenoisesettings.simpleNoiseSettings = settings.biomeNoiseSettings.pickRandomValue();
        colourSettings.biomeColourSettings.noise = biomenoisesettings;
        colourSettings.biomeColourSettings.oceanColour = RandomXT.RandomGradient(new Color[] { settings.oceanDepth.pickRandomValue(),
                                                                                                          settings.oceanSurface.pickRandomValue() });

        colourSettings.biomeColourSettings.biomes = new ColourSettings.BiomeColourSettings.Biome[settings.biomeCount.pickRandomValue()];

        float startHeight = 0f;
        float increment = 1f / (float)settings.biomeCount.lastValue;

        
        for(int i = 0; i < settings.biomeCount.lastValue; ++i)
        {
            colourSettings.biomeColourSettings.biomes[i] = new ColourSettings.BiomeColourSettings.Biome();
            colourSettings.biomeColourSettings.biomes[i].tintPerecent = settings.biomeTintPercent.pickRandomValue();
            colourSettings.biomeColourSettings.biomes[i].startHeight = startHeight;
            colourSettings.biomeColourSettings.biomes[i].gradient = RandomXT.RandomGradient(new Color[] { settings.ground.pickRandomValue(),
                                                                                                          settings.cliff.pickRandomValue(),
                                                                                                          settings.cliffTop.pickRandomValue() });

            colourSettings.biomeColourSettings.biomes[i].tint = colourSettings.biomeColourSettings.biomes[i].gradient.Evaluate(Random.Range(0.2f, 0.8f));


            startHeight += increment;
        }
        //Rotate rotateComponent = planetgo.AddComponent<Rotate>();
      //  rotateComponent.axis = RandomXT.RandomUnitrVector3();
      //  rotateComponent.speed = Random.Range(15f, 25f);

        planet.ConstructRandomPlanet(settings.resolution.pickRandomValue(), shapeSettings, colourSettings);

    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }


}
