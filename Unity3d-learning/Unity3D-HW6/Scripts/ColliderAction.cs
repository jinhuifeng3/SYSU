using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


    public class ColliderAction : MonoBehaviour
    {
        public delegate void AddScore();
        public static event AddScore addScore;
 

        void OnTriggerEnter(Collider collider)
        {
            if (collider.gameObject.name == "player")
            {
                //设置追捕和巡逻的状态
                if (this.transform.parent.GetComponent<GetPatrol>().patrol.GetComponent<PatrolController>().state == 0)
                    this.transform.parent.GetComponent<GetPatrol>().patrol.GetComponent<PatrolController>().state = 1;
                else
                {
                    this.transform.parent.GetComponent<GetPatrol>().patrol.GetComponent<PatrolController>().state = 0;
                    escapeSuccessfully();
                }
            }
            if (collider.gameObject.name == "Patrol")
            {
                this.transform.parent.GetComponent<GetPatrol>().patrol.GetComponent<PatrolController>().state = 0;
            }
        }
        //成功逃离计分
        void escapeSuccessfully()
        {
            if (addScore != null)
            {
                addScore();
            }
        }
    }
