using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTheBoat : SSAction
{
  public FirstController SceneController;

  public static MoveTheBoat GetSSAction()
  {
    MoveTheBoat action = ScriptableObject.CreateInstance<MoveTheBoat>();
    return action;
  }

  public override void Start()
  {
    SceneController = (FirstController)SSDirector.GetInstance().currentSceneController;
  }

  public override void Update()
  {
    if (SceneController.BoatPosition == 0) {
      SceneController.BoatPosition = 1;
      while (this.transform.position != SceneController.shipEndPos)
        this.transform.position = Vector3.MoveTowards(this.transform.position,SceneController.shipEndPos,5);

    }else{
      SceneController.BoatPosition = 0;
      while(this.transform.position != SceneController.shipStartPos)
        this.transform.position = Vector3.MoveTowards(this.transform.position,SceneController.shipStartPos,5);
    }
    SceneController.check();
    this.destroy = true;
    this.callback.SSActionEvent(this);
  }
}
