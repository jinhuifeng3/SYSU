using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserGUI : MonoBehaviour
{
    private IUserAction action;
    bool isFirst = true;

    void Start()
    {
        action = Director.getInstance().currentSceneControl as IUserAction;

    }

    private void OnGUI()
    {
            if(action.getMode() == ActionMode.NOTSET)
            action.setMode(ActionMode.KINEMATIC);
            if (Input.GetButtonDown("Fire1"))
            {

                Vector3 pos = Input.mousePosition;
                action.hit(pos);

            }

            GUI.Label(new Rect(300, 0, 400, 400), action.GetScore().ToString());

            if (isFirst && GUI.Button(new Rect(100, 100, 60, 60), "Start"))
            {
                isFirst = false;
                action.setGameState(GameState.ROUND_START);
            }

            if (!isFirst && action.getGameState() == GameState.ROUND_FINISH && GUI.Button(new Rect(700, 100, 90, 90), "Next Round"))
            {
                action.setGameState(GameState.ROUND_START);
            }

    }


}