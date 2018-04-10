using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCActionManager : SSActionManager, ISSActionCallback{
  public FirstController SceneController;
  public GetOnTheBoat i;
  public GetOffTheBoat j;
  public MoveTheBoat k;

  protected void Start(){
    SceneController = (FirstController)SSDirector.GetInstance().currentSceneController;
    SceneController.actionManager = this;
  }

  protected new void Update(){
    if (SceneController.state == FirstController.GameState.WIN || SceneController.state == FirstController.GameState.LOSE) {
      return;
    }
    if (Input.GetMouseButtonDown(0) && SceneController.state == STOP) {
      Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
      RaycastHit hit;
      if (Physics.Raycast(ray, out hit))
      {
        if (hit.transform.tag == "Devil" || hit.transform.tag == "Priest")
        {
            if (hit.collider.gameObject == SceneController.ship[0] || hit.collider.gameObject == SceneController.ship[1]) //下船
            {
                if (hit.collider.gameObject == SceneController.ship[0])
                {
                    j = GetOffTheBoat.GetSSAction(0);
                    this.RunAction(hit.collider.gameObject, j, this);
                }
                else
                {
                    j = GetOffTheBoat.GetSSAction(1);
                    this.RunAction(hit.collider.gameObject, j, this);
                }
            }
            else   //上船
            {
                i = GetOnTheBoat.GetSSAction();
                this.RunAction(hit.collider.gameObject, i, this);
            }
        }
        else if (hit.transform.tag == "Boat"/* && SceneController.Capacity != 2*/)  //移动船
        {
            k = MoveTheBoat.GetSSAction();
            this.RunAction(hit.collider.gameObject, k, this);
        }
    }
}
      base.Update();
    }
    public void SSActionEvent(SSAction source, SSActionEventType events = SSActionEventType.Completed,
      int intParam = 0, string strParam = null, Object objectParam = null)
      {

      }
  }
