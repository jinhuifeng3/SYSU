﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCActionManager : MonoBehaviour, IActionManager, ISSActionCallback
{

   
    private Dictionary<int, SSAction> actions = new Dictionary<int, SSAction>();        //保存所以已经注册的动作  
    private List<SSAction> waitingAdd = new List<SSAction>();                           //动作的等待队列，在这个对象保存的动作会稍后注册到动作管理器里  
    private List<int> waitingDelete = new List<int>();  

    public FirstSceneControl sceneController;
    public int DiskNumber = 0;

    public void setDiskNumber(int num)
    {
        DiskNumber = num;
    }
    public int getDiskNumber()
    {
        return DiskNumber;
    }

    protected void Start()
    {
        sceneController = (FirstSceneControl)Director.getInstance().currentSceneControl;
        sceneController.actionManager = this;
    }

    // Update is called once per frame  
    protected void Update()
    {
        //把等待队列里所有的动作注册到动作管理器里  
        foreach (SSAction ac in waitingAdd) actions[ac.GetInstanceID()] = ac;
        waitingAdd.Clear();

        //管理所有的动作，如果动作被标志为删除，则把它加入删除队列，被标志为激活，则调用其对应的Update函数  
        foreach (KeyValuePair<int, SSAction> kv in actions)
        {
            SSAction ac = kv.Value;
            if (ac.destroy)
            {
                waitingDelete.Add(ac.GetInstanceID());
            }
            else if (ac.enable)
            {
                ac.Update();
            }
        }

        //把删除队列里所有的动作删除  
        foreach (int key in waitingDelete)
        {
            SSAction ac = actions[key];
            actions.Remove(key);
            DestroyObject(ac);
        }
        waitingDelete.Clear();
    }

    //初始化一个动作  
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
        CCFlyActionFactory temp = Singleton<CCFlyActionFactory>.Instance;
        foreach (GameObject disk in diskQueue)
        {
            RunAction(disk, temp.GetSSAction(), (ISSActionCallback)this);
        }
    }

}