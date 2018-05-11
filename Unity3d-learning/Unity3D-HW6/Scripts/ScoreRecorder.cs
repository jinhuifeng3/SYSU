using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;


    public class ScoreRecorder : MonoBehaviour
    {
        int Score = 0;
        public Text Sc;
        
        void GetScore()
        {
            Score++;
        }
        // Use this for initialization  
        void Start()
        {
            ColliderAction.addScore += GetScore;//订阅 
        }

        // Update is called once per frame  
        void Update()
        {
            Sc.text = "Score:" + Score.ToString(); 
        }
    }

