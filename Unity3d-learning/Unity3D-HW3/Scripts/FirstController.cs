using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstController: MonoBehaviour,ISceneController
{
    public SSActionManager actionManager{get; set;}
    public Stack<GameObject> StartBankPriests = new Stack<GameObject>();
    public Stack<GameObject> EndBankPriests = new Stack<GameObject>();
    public Stack<GameObject> StartBankDevils = new Stack<GameObject>();
    public Stack<GameObject> EndBankDevils = new Stack<GameObject>();
    public GameObject[] ship = new GameObject[2];
    public GameObject ship_obj;

    public Vector3 shipStartPos = new Vector3(-3f, -5.5f, 0);
    public Vector3 shipEndPos = new Vector3(7f, -5.5f, 0);
    public Vector3 bankStartPos = new Vector3(-10f, -5f, 0);
    public Vector3 bankEndPos = new Vector3(14f, -5f, 0);
    public Vector3 priestsStartPos = new Vector3(-10.5f, -1f, 0);
    public Vector3 priestsEndPos = new Vector3(13.5f, -1f, 0);
    public Vector3 devilsStartPos = new Vector3(-6f, -1f, 0);
    public Vector3 devilsEndPos = new Vector3(18f, -1f, 0);

    public float speed = 100f;
    float gap = 1.5f;
    public int BoatPosition = 0;
    public int ItemClicked = 0;
    public int Capacity = 2;

    public enum GameState {WIN,LOSE,STOP};
    public GameState state = GameState.STOP;

    SSDirector Instance;

    void Awake()
    {
      Instance = SSDirector.GetInstance();
      Instance.currentScenceController = this;
      Instance.LoadResources();
    }

    void Start()
    {

    }

    public void LoadResources()
    {
        Instantiate(Resources.Load("Prefabs/Bank"), bankStartPos, Quaternion.identity);
        Instantiate(Resources.Load("Prefabs/Bank"), bankEndPos, Quaternion.identity);
        ship_obj = Instantiate(Resources.Load("Prefabs/Ship"), shipStartPos, Quaternion.identity) as GameObject;
        for (int i = 0; i < 3; i++)
        {
            StartBankPriests.Push(Instantiate(Resources.Load("Prefabs/Priest")) as GameObject);
            StartBankDevils.Push(Instantiate(Resources.Load("Prefabs/Devil")) as GameObject);
        }
    }

    public void OnGUI()
    {
        if (state == GameState.WIN) {
          if (GUI.Button(new Rect(285, 120, 150, 50), "Win!\n(click here to reset)"))
            Instance.currentScenceController.Restart();
        }else if (state == GameState.LOSE) {
          if (GUI.Button(new Rect(285, 120, 150, 50), "Lose!\n(click here to reset)"))
            Instance.currentScenceController.Restart();
        }
    }

    public void Restart()//重新开始游戏
    {
        SceneManager.LoadScene("main");
    }


    void setposition(Stack<GameObject> stack, Vector3 pos)
    {
        GameObject[] temp = stack.ToArray();
        for (int i = 0; i < stack.Count; i++)
        {
            temp[i].transform.position = pos + new Vector3(-gap * i, 0, 0);
        }
    }

    void check()
    {
        int sp = 0, sd = 0, ep = 0, ed = 0;

        sp = StartBankPriests.Count;
        sd = StartBankDevils.Count;
        ep = EndBankPriests.Count;
        ed = EndBankDevils.Count;

        if (EndBankDevils.Count == 3 && EndBankPriests.Count == 3)
        {
            state = GameState.WIN;
        }

        if ((sp != 0 && sp < sd) || (ep != 0 && ep < ed))
        {
            state = GameState.LOSE;
        }
    }

    void Update()
    {
        setposition(StartBankPriests, priestsStartPos);
        setposition(EndBankPriests, priestsEndPos);
        setposition(StartBankDevils, devilsStartPos);
        setposition(EndBankDevils, devilsEndPos);
    }
}
