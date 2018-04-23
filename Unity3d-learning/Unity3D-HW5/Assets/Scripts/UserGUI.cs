using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserGUI : MonoBehaviour
{
    private IUserAction action;
    private bool start;

    void Start()
    {
        action = Director.getInstance().currentSceneControl as IUserAction;
        start = true;
    }

    private void OnGUI()
    {
        if (action.getMode() == ActionMode.NOTSET)
        {
            if (GUI.Button(new Rect(200, 100, 60, 60), "KINEMATIC"))
            {
                action.setMode(ActionMode.KINEMATIC);
            }
            if (GUI.Button(new Rect(400, 100, 60, 60), "PHYSICAL"))
            {
                action.setMode(ActionMode.PHYSICAL);
            }
        }
        if (Input.GetButtonDown("Fire1"))
        {
            Vector3 pos = Input.mousePosition;
            action.hit(pos);

        }
        else
        {
            GUI.Label(new Rect(300, 0, 400, 400), action.getScore().ToString());
            if (start)
            {
                if (GUI.Button(new Rect(100, 100, 60, 60), "Start"))
                {
                    start = false;
                    action.setGameState(GameState.NEWROUND);
                }
            }

            if (!start && action.getGameState() == GameState.ROUNDFINISH)
            {
                if (GUI.Button(new Rect(700, 100, 90, 90), "Next Round"))
                {
                    action.setGameState(GameState.NEWROUND);
                }
            }
        }
        
    }
}