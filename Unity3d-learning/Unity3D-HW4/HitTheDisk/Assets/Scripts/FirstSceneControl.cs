using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { ROUND_START, ROUND_FINISH, RUNNING, PAUSE, START }
public enum ActionMode { KINEMATIC, NOTSET }


public interface IUserAction
{
    void GameOver();
    GameState getGameState();
    void setGameState(GameState gs);
    int GetScore();
    void hit(Vector3 pos);
    ActionMode getMode();
    void setMode(ActionMode m);
}

public interface ISceneControl
{
    void LoadResources();
}

public class FirstSceneControl : MonoBehaviour, ISceneControl, IUserAction
{

    public ActionMode mode { get; set; }

    public IActionManager actionManager { get; set; }

    public ScoreRecorder scoreRecorder { get; set; }


    public Queue<GameObject> diskQueue = new Queue<GameObject>();

    private int diskNumber;

    private int currentRound = -1;

    public int round = 3;

    private float time = 0;

    private GameState gameState = GameState.START;

    void Awake()
    {
        Director director = Director.getInstance();
        director.setFPS(60);
        director.currentSceneControl = this;
        diskNumber = 10;
        this.gameObject.AddComponent<ScoreRecorder>();
        this.gameObject.AddComponent<DiskFactory>();
        this.gameObject.AddComponent<CCFlyActionFactory>();
        mode = ActionMode.NOTSET;
        scoreRecorder = Singleton<ScoreRecorder>.Instance;
        director.currentSceneControl.LoadResources();
    }

    private void Update()
    {
        /** 
         * 以下代码用来管理游戏的状态 
         */
        if (mode != ActionMode.NOTSET && actionManager != null)
        {
            if (actionManager.getDiskNumber() == 0 && gameState == GameState.RUNNING)
            {
                gameState = GameState.ROUND_FINISH;

            }

            if (actionManager.getDiskNumber() == 0 && gameState == GameState.ROUND_START)
            {
                currentRound = (currentRound + 1) % round;
                Next();
                actionManager.setDiskNumber(10);
                gameState = GameState.RUNNING;
            }

            if (time > 1)
            {
                DiskRun();
                time = 0;
            }
            else
            {
                time += Time.deltaTime;
            }
        }



    }

    private void Next()
    {
        DiskFactory df = Singleton<DiskFactory>.Instance;
        for (int i = 0; i < diskNumber; i++)
        {
            diskQueue.Enqueue(df.GetDisk(currentRound, mode));
        }

        actionManager.StartThrow(diskQueue);

    }

    void DiskRun()
    {
        if (diskQueue.Count != 0)
        {
            GameObject disk = diskQueue.Dequeue();

            /** 
             * 以下几句代码是随机确定飞碟出现的位置 
             */

            Vector3 position = new Vector3(0, 0, 0);
            float y = UnityEngine.Random.Range(0f, 4f);
            position = new Vector3(-disk.GetComponent<Disk>().direction.x * 7, y, 0);
            disk.transform.position = position;

            disk.SetActive(true);
        }

    }

    public void LoadResources()
    {
 
    }


    public void GameOver()
    {

    }

    public void hit(Vector3 pos)
    {
        Ray ray = Camera.main.ScreenPointToRay(pos);

        RaycastHit[] hits;
        hits = Physics.RaycastAll(ray);
        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit hit = hits[i];

            if (hit.collider.gameObject.GetComponent<Disk>() != null)
            {
                scoreRecorder.Record(hit.collider.gameObject);

                /** 
                 * 如果飞碟被击中，那么就移到地面之下，由工厂负责回收 
                 */

                hit.collider.gameObject.transform.position = new Vector3(0, -5, 0);
            }

        }
    }
    public int GetScore()
    {
        return scoreRecorder.score;
    }
    public void setGameState(GameState gs)
    {
        gameState = gs;
    }

    public GameState getGameState()
    {
        return gameState;
    }


    public ActionMode getMode()
    {
        return mode;
    }

    public void setMode(ActionMode m)
    {
        this.gameObject.AddComponent<CCActionManager>();
        mode = m;
    }
}