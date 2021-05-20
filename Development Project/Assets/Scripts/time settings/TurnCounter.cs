using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnCounter : MonoBehaviour
{

    public Text turnDisplay;

    int turn = 0;


    void OnEnable()
    {
        TimeControl.OnTimeAdvance += Advance;
    }


    void OnDisable()
    {
        TimeControl.OnTimeAdvance -= Advance;
    }




    public void Advance()
    {
        turn++;
        turnDisplay.text = string.Format("Turn: {0}", turn);
    }
}
