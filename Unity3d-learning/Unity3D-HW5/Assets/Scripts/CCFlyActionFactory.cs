using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCFlyActionFactory : MonoBehaviour
{


    /** 
    * used是用来保存正在使用的动作 
    * free是用来保存还未被激活的动作 
    */

    private Dictionary<int, SSAction> used = new Dictionary<int, SSAction>();
    private List<SSAction> free = new List<SSAction>();
    private List<int> wait = new List<int>();
    public CCFlyAction FlyAction;

    // Use this for initialization  
    void Start()
    {
        FlyAction = CCFlyAction.GetCCFlyAction();
    }

    private void Update()
    {
        foreach (var temp in used.Values)
        {
            if (temp.destroy)
            {
                wait.Add(temp.GetInstanceID());
            }
        }

        foreach (int temp in wait)
        {
            FreeSSAction(used[temp]);
        }
        wait.Clear();
    }

    public SSAction GetSSAction()
    {
        SSAction action = null;
        if (free.Count > 0)
        {
            action = free[0];
            free.Remove(free[0]);
        }
        else
        {
            action = ScriptableObject.Instantiate<CCFlyAction>(FlyAction);
        }
        used.Add(action.GetInstanceID(), action);
        return action;
    }

    public void FreeSSAction(SSAction action)
    {
        SSAction temp = null;
        int id = action.GetInstanceID();
        if (used.ContainsKey(id))
        {
            temp = used[id];
        }

        if (temp != null)
        {
            temp.reset();
            free.Add(temp);
            used.Remove(id);
        }
    }

    public void clear()
    {
        foreach (var temp in used.Values)
        {
            temp.enable = false;
            temp.destroy = true;
        }
    }
}