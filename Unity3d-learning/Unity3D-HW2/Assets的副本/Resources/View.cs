using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hw2;

public class View : MonoBehaviour
{

    SSDirector Instance;
    UserAction action;

    // Use this for initialization  
    void Start()
    {
        Instance = SSDirector.GetInstance();
        action = SSDirector.GetInstance() as UserAction;
    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(70, 60, 70, 45), "Priest_On"))
        {
            action.priestSOnB();
        }

        if (GUI.Button(new Rect(160, 60, 70, 45), "Devil_On"))
        {
            action.devilSOnB();
        }

        if (GUI.Button(new Rect(490, 60, 70, 45), "Priest_On"))
        {
            action.priestEOnB();
        }

        if (GUI.Button(new Rect(580, 60, 70, 45), "Devil_On"))
        {
            action.devilEOnB();
        }

         if (GUI.Button(new Rect(260, 360, 75, 50), "Left_Off"))
        {
            action.offShipL();
        }

        if (GUI.Button(new Rect(375, 360, 75, 50), "Right_Off"))
        {
            action.offShipR();
        }

        if (GUI.Button(new Rect(310, 60, 100, 50), "Go"))
        {
            action.moveShip();
        }

        if (Instance.state == State.WIN)
        {
            if (GUI.Button(new Rect(285, 120, 150, 50), "Win!\n(click here to reset)"))
            {
                action.reset();
            }
        }

        if (Instance.state == State.LOSE)
        {
            if (GUI.Button(new Rect(285, 120, 150, 50), "Lose!\n(click here to reset)"))
            {
                action.reset();
            }
        }
    }
}