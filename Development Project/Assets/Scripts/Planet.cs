

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{

    [Range(2, 256)]
    public int resolution = 10;
    public bool autoUpdate = true;

    public enum FaceRenderMask { All, Top, Bottom, Left, Right, Front, Back}
    public FaceRenderMask faceRenderMask;
   
    public ShapeSettings shapeSettings;
    public ColourSettings colourSettings;

    [HideInInspector]
    public bool shapeSettingsFoldout;
    [HideInInspector]
    public bool colourSettingsFoldout;

    ShapeGenerator shapeGenerator = new ShapeGenerator();
    ColourGenerator colourGenerator = new ColourGenerator();

    [SerializeField, HideInInspector]
    MeshFilter[] meshFilters;
    Terrain[] terrainFaces;

    private bool procedurallyGenerated = false;

    

    public float mass;
    public float radius;
   // public Vector3 initialVelocity;
   // Vector3 currentVelocity;
    Rigidbody rb;
    // public float surfaceGravity;
    public float gravity = 9.81f;
    //public Transform gravityTarget;

    Planet[] allBodies;
    //   //Gravity




    private void Awake()
    {
        
        rb = GetComponent<Rigidbody>();
     //   currentVelocity = initialVelocity;
    }






    /*
        public void UpdateVelocity(Planet[] allBodies, float timeStep)
        {
            foreach (var otherBody in allBodies)
            {
                if(otherBody != this)
                {
                    float squareDistance = (otherBody.rb.position - rb.position).sqrMagnitude;
                    Vector3 forceDir = (otherBody.rb.position - rb.position).normalized;

                    Vector3 acceleration = forceDir * Universe.gravitationalConstant * otherBody.mass / squareDistance;
                    currentVelocity += acceleration * timeStep;

                }
            }
        }*/

    /*
        public void UpdateVelocity( float timeStep)
        {
            rb.position += currentVelocity * timeStep;

        }

        public void UpdatePosition(float timeStep)
        {
            rb.MovePosition(rb.position + currentVelocity * timeStep);

        }*/


    void Start()
    {
        if (!procedurallyGenerated)
        {
            GeneratePlanet();
        }
    }

    public void ConstructRandomPlanet(int res, ShapeSettings ssettings, ColourSettings csettings)
    {
        this.resolution = res;
        this.shapeSettings = ssettings;
        this.colourSettings = csettings;
        procedurallyGenerated = true;

        GeneratePlanet();
    }


    private void OnValidate()
    {

       

      //  GeneratePlanet();
    }

  /*  public void RecalculateMass()
    {
        mass = surfaceGravity * radius * radius / Universe.gravitationalConstant;
        Rigidbody.mass = mass;
    }*/

  

    void Initialize()
    {
        shapeGenerator.UpdateSettings(shapeSettings);
        colourGenerator.UpdateSettings(colourSettings);
        if (meshFilters == null || meshFilters.Length == 0)
        {
            meshFilters = new MeshFilter[6];
        }
        terrainFaces = new Terrain[6];

        Vector3[] directions = { Vector3.up, Vector3.down, Vector3.left, Vector3.right, Vector3.forward, Vector3.back };

        for (int i = 0; i < 6; i++)
        {
            if (meshFilters[i] == null)
            {
                GameObject meshObj = new GameObject("mesh");
                meshObj.transform.parent = transform;

                meshObj.AddComponent<MeshRenderer>();
                meshFilters[i] = meshObj.AddComponent<MeshFilter>();
                meshFilters[i].mesh = new Mesh();
            }

           meshFilters[i].GetComponent<MeshRenderer>().material = colourSettings.planetMaterial;


            terrainFaces[i] = new Terrain(shapeGenerator, meshFilters[i].sharedMesh, resolution, directions[i]);
            bool renderFace = faceRenderMask == FaceRenderMask.All || (int)faceRenderMask - 1 == i;
            meshFilters[i].gameObject.SetActive(renderFace);
        }
    }
    

    public void GeneratePlanet()
    {
        Initialize();
        GenerateMesh();
        GenerateColours();
    }

    void GenerateMesh()
    {
        for (int i = 0; i < 6; i++)
        {
            if (meshFilters[i].gameObject.activeSelf)
            {
                terrainFaces[i].ConstructMesh(procedurallyGenerated);
            }
        }

        colourGenerator.UpdateElevation(shapeGenerator.elevationMinMax);

    }

    public void onShapeSettingsUpdate()
    {
        if (!autoUpdate)
            return;

        Initialize();
        GenerateMesh();
    }

    public void onColourSettingsUpdate()
    {
        if (!autoUpdate)
            return;

        Initialize();
        GenerateColours();
    }





 


   

    void GenerateColours()
    {
        colourGenerator.UpdateColour();
        for (int i = 0; i < 6; i++)
        {
            if (meshFilters[i].gameObject.activeSelf)
            {
                terrainFaces[i].UpdateUVs(colourGenerator);
            }
        }




    }



    //
    /*
    public Rigidbody Rigidbody
    {
        get
        {
            if (!rb)
            {
                rb = GetComponent<Rigidbody>();
            }
            return rb;
        }
    }

    public Vector3 Position
    {
        get
        {
            return rb.position;
        }
    }*/

}
