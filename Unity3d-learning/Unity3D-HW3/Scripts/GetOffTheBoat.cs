using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetOffTheBoat : SSAction{
  public FirstController SceneController;
  public int seat; //记录船上的物体位置
  public static GetOffTheBoat GetSSAction(int seat)
  {
    GetOffTheBoat action = ScriptableObject.CreateInstance<GetOffTheBoat>();
    action.seat = seat;
    return action;
  }

  public override void Start()
  {
    SceneController = (FirstController)SSDirector.GetInstance().currentSceneController;
  }

  public override void Update()
  {
    if (SceneController.ship[seat] != null) {
      if (SceneController.BoatPosition == 0) {

        if (SceneController.ship[seat].transform.tag == "Priest") {
          for (int i; i < 3; i++) {
            if (StartBankPriests[i] == null) {
              StartBankPriests[i] = SceneController.ship[seat];
              SceneController.Capacity++;
              break;
            }
          }
        }else{
          for (int i; i < 3; i++) {
            if (StartBankDevils[i] == null) {
              StartBankDevils[i] = SceneController.ship[seat];
              SceneController.Capacity++;
              break;
            }
          }
        }

      }else{
        if (SceneController.ship[seat].transform.tag == "Priest") {
          for (int i; i < 3; i++) {
            if (EndBankPriests[i] == null) {
              EndBankPriests[i] = SceneController.ship[seat];
              SceneController.Capacity++;
              break;
            }
          }
        }else{
          for (int i; i < 3; i++) {
            if (EndBankDevils[i] == null) {
              EndBankDevils[i] = SceneController.ship[seat];
              SceneController.Capacity++;
              break;
            }
          }
        }
      }
      SceneController.ship[seat] = null;
    }
    SceneController.check();
    this.destroy = true;
    this.callback.SSActionEvent(this);
  }
}
