using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeControl : MonoBehaviour
{

    public float turnDuration = 1f;
    public float fastForwardMultiplier = 5f;
    public bool paused;
    public bool fastForward;
    public bool rewind;

    public delegate void OnTimeAdvanceHandler();
    public static event OnTimeAdvanceHandler OnTimeAdvance;

    float advanceTimer;

    // Start is called before the first frame update
    void Start()
    {
        advanceTimer = turnDuration;
    }

    // Update is called once per frame
    void Update()
    {
        if (!paused)
        {
            advanceTimer -= Time.deltaTime * (fastForward ? fastForwardMultiplier : 1f);
            if (advanceTimer <= 0)
            {
                advanceTimer += turnDuration;
                OnTimeAdvance?.Invoke();
            }
        }

        if(!paused && rewind)
        {
            //rewind
        }

    }
 
    public void Pause()
    {
        paused = true;
        fastForward = false;

    }

    public void Play()
    {
        paused = false;
        fastForward = false;


    }

    public void FastForward()
    {
        paused = false;
        fastForward = true;
    }

    public void Rewind()
    {
        paused = false;
        fastForward = false;
        rewind = true;
    }
}
