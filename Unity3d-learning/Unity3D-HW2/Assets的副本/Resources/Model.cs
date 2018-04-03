using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hw2;

public class Model : MonoBehaviour
{

    Stack<GameObject> StartBankPriests = new Stack<GameObject>();
    Stack<GameObject> EndBankPriests = new Stack<GameObject>();
    Stack<GameObject> StartBankDevils = new Stack<GameObject>();
    Stack<GameObject> EndBankDevils = new Stack<GameObject>();

    GameObject[] ship = new GameObject[2];
    GameObject ship_obj;
    public float speed = 100f;

    SSDirector Instance;

    Vector3 shipStartPos = new Vector3(-3f, -5.5f, 0);
    Vector3 shipEndPos = new Vector3(7f, -5.5f, 0);
    Vector3 bankStartPos = new Vector3(-10f, -5f, 0);
    Vector3 bankEndPos = new Vector3(14f, -5f, 0);

    float gap = 1.5f;
    Vector3 priestsStartPos = new Vector3(-10.5f, -1f, 0);
    Vector3 priestsEndPos = new Vector3(13.5f, -1f, 0);
    Vector3 devilsStartPos = new Vector3(-6f, -1f, 0);
    Vector3 devilsEndPos = new Vector3(18f, -1f, 0);


    // Use this for initialization  
    void Start()
    {
        Instance = SSDirector.GetInstance();
        Instance.setModel(this);
        loadSrc();
    }

    //加载游戏对象  
    void loadSrc()
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
    // Update is called once per frame  

    int Capacity()
    {
        int num = 0;
        for (int i = 0; i < 2; i++)
        {
            if (ship[i] == null)
            {
                num++;
            }
        }
        return num;
    }

    //上船的操作  
    void getOnTheShip(GameObject obj)
    {
        obj.transform.parent = ship_obj.transform;
        if (Capacity() != 0)
        {
            if (ship[0] == null)
            {
                ship[0] = obj;
                obj.transform.localPosition = new Vector3(-0.4f, 1, 0);
            }
            else
            {
                ship[1] = obj;
                obj.transform.localPosition = new Vector3(0.4f, 1, 0);
            }
        }
    }

    //船移动时的操作  
    public void moveShip()
    {
        if (Capacity() != 2)
        {
            if (Instance.state == State.START)
            {
                Instance.state = State.SEMOVING;
            }
            else if (Instance.state == State.END)
            {
                Instance.state = State.ESMOVING;
            }
        }
    }

    //下船的操作  
    public void getOffTheShip(int side)
    {
        if (ship[side] != null)
        {
            ship[side].transform.parent = null;
            if (Instance.state == State.START)
            {
                if (ship[side].tag == "Priest")
                {
                    StartBankPriests.Push(ship[side]);
                }
                else
                {
                    StartBankDevils.Push(ship[side]);
                }
            }
            else if (Instance.state == State.END)
            {
                if (ship[side].tag == "Priest")
                {
                    EndBankPriests.Push(ship[side]);
                }
                else
                {
                    EndBankDevils.Push(ship[side]);
                }
            }
            ship[side] = null;
        }
    }

    public void priS()
    {
        if (StartBankPriests.Count != 0 && Capacity() != 0 && Instance.state == State.START)
        {
            getOnTheShip(StartBankPriests.Pop());
        }
    }
    public void priE()
    {
        if (EndBankPriests.Count != 0 && Capacity() != 0 && Instance.state == State.END)
        {
            getOnTheShip(EndBankPriests.Pop());
        }
    }
    public void delS()
    {
        if (StartBankDevils.Count != 0 && Capacity() != 0 && Instance.state == State.START)
        {
            getOnTheShip(StartBankDevils.Pop());
        }
    }
    public void delE()
    {
        if (EndBankDevils.Count != 0 && Capacity() != 0 && Instance.state == State.END)
        {
            getOnTheShip(EndBankDevils.Pop());
        }
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
        int bp = 0, bd = 0;
        int sp = 0, sd = 0, ep = 0, ed = 0;

        if (EndBankDevils.Count == 3 && EndBankPriests.Count == 3)
        {
            Instance.state = State.WIN;
            return;
        }

        
        for (int i = 0; i < 2; i++)
        {
            if (ship[i] != null && ship[i].tag == "Priest")
            {
                bp++;
            }
            else if (ship[i] != null && ship[i].tag == "Devil")
            {
                bd++;
            }
        }

        if (Instance.state == State.START)
        {
            sp = StartBankPriests.Count + bp;
            ep = EndBankPriests.Count;
            sd = StartBankDevils.Count + bd;
            ed = EndBankDevils.Count;
        }
        else if (Instance.state == State.END)
        {
            sp = StartBankPriests.Count;
            ep = EndBankPriests.Count + bp;
            sd = StartBankDevils.Count;
            ed = EndBankDevils.Count + bd;
        }

        if ((sp != 0 && sp < sd) || (ep != 0 && ep < ed))
        {
            Instance.state = State.LOSE;
        }
    }

    void Update()
    {
        setposition(StartBankPriests, priestsStartPos);
        setposition(EndBankPriests, priestsEndPos);
        setposition(StartBankDevils, devilsStartPos);
        setposition(EndBankDevils, devilsEndPos);

        if (Instance.state == State.SEMOVING)
        {
            ship_obj.transform.position = Vector3.MoveTowards(ship_obj.transform.position, shipEndPos, Time.deltaTime * speed);
            if (ship_obj.transform.position == shipEndPos)
            {
                Instance.state = State.END;
            }
        }
        else if (Instance.state == State.ESMOVING)
        {
            ship_obj.transform.position = Vector3.MoveTowards(ship_obj.transform.position, shipStartPos, Time.deltaTime * speed);
            if (ship_obj.transform.position == shipStartPos)
            {
                Instance.state = State.START;
            }
        }
        else
        {
            check();
        }
    }
}