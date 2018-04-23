using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { START, ROUNDFINISH, RUNNING, PAUSE, NEWROUND }
public enum ActionMode { KINEMATIC, NOTSET, PHYSICAL}


public interface IUserAction
{
    void setGameState(GameState gs);
    void setMode(ActionMode m);
    int getScore();
    ActionMode getMode();
    void hit(Vector3 pos);
    void GameOver();
    GameState getGameState();
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

    private double time = 0;

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

    private void DiskRun()
    {
        if (diskQueue.Count != 0)
        {
            GameObject disk = diskQueue.Dequeue();
            Vector3 position = new Vector3(0, 0, 0);
            float y = UnityEngine.Random.Range(0f, 4f);
            position = new Vector3(-disk.GetComponent<Disk>().direction.x * 7, y, 0);
            disk.transform.position = position;
            disk.SetActive(true);
        }

    }
    private void Update()
    {
        
        if (mode != ActionMode.NOTSET && actionManager != null)
        {
            time += Time.deltaTime;
            if (actionManager.getDiskNumber() == 0 && gameState == GameState.RUNNING)
            {
                gameState = GameState.ROUNDFINISH;

            }

            if (actionManager.getDiskNumber() == 0 && gameState == GameState.START)
            {
                currentRound = (currentRound + 1) % round;
                DiskFactory df = Singleton<DiskFactory>.Instance;
                for (int i = 0; i < diskNumber; i++)
                {
                    diskQueue.Enqueue(df.GetDisk(currentRound, mode));
                }

                actionManager.StartThrow(diskQueue);
                actionManager.setDiskNumber(10);
                gameState = GameState.RUNNING;
            }

            if (time > 1)
            {
                DiskRun();
                time = 0;
            }           
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
                hit.collider.gameObject.transform.position = new Vector3(0, -5, 0);
            }

        }
    }
    public int getScore()
    {
        return scoreRecorder.score;
    }
    public void setGameState(GameState gs)
    {
        gameState = gs;
    }

    public void setMode(ActionMode m)
    {
        if (m == ActionMode.PHYSICAL)
        {
            this.gameObject.AddComponent<PhysicalActionManager>();
        }
        else
        {
            this.gameObject.AddComponent<CCActionManager>();
        }
        mode = m;
    }

    public GameState getGameState()
    {
        return gameState;
    }


    public ActionMode getMode()
    {
        return mode;
    }
}