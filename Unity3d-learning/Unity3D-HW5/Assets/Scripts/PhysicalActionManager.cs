using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalActionManager : MonoBehaviour, IActionManager, ISSActionCallback
{
    private Dictionary<int, SSAction> actions = new Dictionary<int, SSAction>();        
    private List<SSAction> waitingAdd = new List<SSAction>();                           
    private List<int> waitingDelete = new List<int>();

    public FirstSceneControl sceneController;
    public int DiskNumber = 0;

    public int getDiskNumber()
    {
        return DiskNumber;
    }

    public void setDiskNumber(int num)
    {
        DiskNumber = num;
    }

    protected void Start()
    {
        sceneController = (FirstSceneControl)Director.getInstance().currentSceneControl;
        sceneController.actionManager = this;

    }

    protected void Update()
    {
        foreach (SSAction ac in waitingAdd) actions[ac.GetInstanceID()] = ac;
        waitingAdd.Clear();

        foreach (KeyValuePair<int, SSAction> kv in actions)
        {
            SSAction ac = kv.Value;
            if (ac.destroy)
            {
                waitingDelete.Add(ac.GetInstanceID());
            }
            else if (ac.enable)
            {
                ac.FixedUpdate();
            }
        }

        foreach (int key in waitingDelete)
        {
            SSAction ac = actions[key];
            actions.Remove(key);
            DestroyObject(ac);
        }
        waitingDelete.Clear();
    }
    public void RunAction(GameObject gameobject, SSAction action, ISSActionCallback manager)
    {
        action.gameobject = gameobject;
        action.transform = gameobject.transform;
        action.callback = manager;
        waitingAdd.Add(action);
        action.Start();
    }


    public void SSActionEvent(SSAction source,
        SSActionEventType events = SSActionEventType.Competeted,
        int intParam = 0,
        string strParam = null,
        UnityEngine.Object objectParam = null)
    {
        if (source is CCFlyAction)
        { 
            source.gameobject.SetActive(false);
            DiskNumber--;
        }
    }

    public void StartThrow(Queue<GameObject> diskQueue)
    {
        CCFlyActionFactory temp
            = Singleton<CCFlyActionFactory>.Instance;
        foreach (GameObject disk in diskQueue)
        {
            RunAction(disk, temp.GetSSAction(), (ISSActionCallback)this);
        }
    }
}