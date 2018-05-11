using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


    public class PlayerTrigger : MonoBehaviour
    {
        public delegate void GameOver();
        public static event GameOver gameOver;
        void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.name == "Patrol")
            {
                if (gameOver != null)
                {
                    gameOver();
                }
            }
        }
    }

