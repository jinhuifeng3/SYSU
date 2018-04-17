using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCFlyAction : SSAction
{

 
    float gravity; // g
    float horizontalSpeed; //水平速度
    float time;
    Vector3 direction; //飞行方向
    Rigidbody rigidbody;
    Disk disk;

    public static CCFlyAction GetCCFlyAction()
    {
        CCFlyAction action = ScriptableObject.CreateInstance<CCFlyAction>();
        return action;
    }

    public override void Start()
    {
        disk = gameobject.GetComponent<Disk>();
        enable = true;
        gravity = 9.8f;
        time = 0;
        horizontalSpeed = disk.speed;
        direction = disk.direction;

        rigidbody = this.gameobject.GetComponent<Rigidbody>();
        if (rigidbody)
        {
            rigidbody.velocity = horizontalSpeed * direction;
        }

    }

    public override void Update()
    {
        if (gameobject.activeSelf)
        {
            time += Time.deltaTime;
            transform.Translate(Vector3.down * gravity * time * Time.deltaTime);
            transform.Translate(direction * horizontalSpeed * Time.deltaTime);
            if (this.transform.position.y < -5)
            {
                this.destroy = true;
                this.enable = false;
                this.callback.SSActionEvent(this);
            }
        }

    }

    public override void FixedUpdate()
    {

        if (gameobject.activeSelf)
        {
            if (this.transform.position.y < -5)
            {
                this.destroy = true;
                this.enable = false;
                this.callback.SSActionEvent(this);
            }
        }
    }


}