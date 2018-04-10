using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetOnTheBoat : SSAction {

    public FirstController SceneController;
    public static GetOnTheBoat GetSSAction()
    {
        GetOnTheBoat action = ScriptableObject.CreateInstance<GetOnTheBoat>();
        return action;
    }

	  public override void Start () {
        SceneController = (FirstController)SSDirector.GetInstance().currentSceneController;
	  }

    public override void Update () {
        if (SceneController.Capacity > 0) {
          if (SceneController.BoatPosition == 0) {//船在左岸上船事件
            for (int i = 0; i < 3; i++) {
              if (SceneController.StartBankPriests[i] == GameObject) {
                SceneController.ItemClicked = 1;
                SceneController.StartBankPriests[i] = null;
              }
              if (SceneController.StartBankDevils[i] == GameObject) {
                SceneController.ItemClicked = 1;
                SceneController.StartBankDevils[i] = null;
              }
            }
          }
          if (SceneController.BoatPosition == 1) {//船在右岸上船事件
            for (int i = 0; i < 3; i++) {
              if (SceneController.EndBankPriests[i] == GameObject) {
                SceneController.ItemClicked = 1;
                SceneController.EndBankPriests[i] = null;
              }
              if (SceneController.EndBankDevils[i] == GameObject) {
                SceneController.ItemClicked = 1;
                SceneController.EndBankDevils[i] = null;
              }
            }
          }
          if (ItemClicked == 1) {
            GameObject.transform.parent = SceneController.ship_obj.transform;
            if (SceneController.ship[0] == null) {
              SceneController.ship[0] = GameObject;
              this.transform.localPosition = new Vector3(-0.4f, 1, 0);
              SceneController.ship[0].transform.tag = GameObject.transform.tag;
              SceneController.Capacity--;
            }else if (SceneController.ship[1] == null) {
              SceneController.ship[1] = GameObject;
              this.transform.localPosition = new Vector3(0.4f, 1, 0);
              SceneController.ship[1].transform.tag = GameObject.transform.tag;
              SceneController.Capacity--;
            }
          }
        }
        SceneController.ItemClicked = 0;
        this.destroy = true;
        this.callback.SSActionEvent(this);
    }
}
