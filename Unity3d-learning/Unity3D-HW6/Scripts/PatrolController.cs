
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolController : MonoBehaviour {

    private bool flag = true;
    public int state = 0;
    public GameObject role;
    private float pos_X, pos_Z;
    public float speed = 0.5f;
    private float distance = 0;
    int n = 0;
    public SceneController sceneController;
    void Start()
    {
        sceneController = (SceneController)SSDirector.getInstance().currentScenceController;
        role = sceneController.player;
        pos_X = this.transform.position.x;
        pos_Z = this.transform.position.z;
    }

    //正方形巡逻
    void patrol()
    {
        if (flag)
        {
            switch (n)
            {
                case 0:
                    pos_X += 1;
                    pos_Z -= 1;
                    break;
                case 1:
                    pos_X += 1;
                    pos_Z += 1;
                    break;
                case 2:
                    pos_X -= 1;
                    pos_Z += 1;
                    break;
                case 3:
                    pos_X -= 1;
                    pos_Z -= 1;
                    break;
            }
            flag = false;
        }
        distance = Vector3.Distance(transform.position, new Vector3(pos_X, 0, pos_Z));
        if (distance > 0.3)
        {
            transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(pos_X, 0, pos_Z), speed * Time.deltaTime);
        }
        else
        {
            n++;
            n %= 4;
            flag = true;
        }
    }
    //追捕活动
    void chase(GameObject role)
    {
        transform.position = Vector3.MoveTowards(this.transform.position, role.transform.position, 0.5f * speed * Time.deltaTime);
    }

    void FixedUpdate()
    {
        if (state == 0)
        {
            patrol();//巡逻  
        }
        else
        {
            chase(role);//追捕  
        }
    }
}


