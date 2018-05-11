using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


    public class SceneController : MonoBehaviour, ISceneController, IUserAction
    {
        public GameObject player;
        public Text FinalText;
        public int game = 1;
        void Awake()
        
        {
            SSDirector director = SSDirector.getInstance();
            director.setFPS(60);
            director.currentScenceController = this;
            director.currentScenceController.LoadResources();
        }
        public void LoadResources()  //载入资源    
        {

           
            player = Instantiate(Resources.Load("Prefabs/player")) as GameObject;
            Instantiate(Resources.Load("Prefabs/Maze"));
            Instantiate(Resources.Load("Prefabs/Patrol"), new Vector3(2, 0.4f, 3), Quaternion.identity);
            Instantiate(Resources.Load("Prefabs/Patrol"), new Vector3(2, 0.4f, -3), Quaternion.identity);
            Instantiate(Resources.Load("Prefabs/Patrol"), new Vector3(-4, 0.4f, 3), Quaternion.identity);
            Instantiate(Resources.Load("Prefabs/Patrol"), new Vector3(-4, 0.4f, -3), Quaternion.identity);

        }
        // Use this for initialization  
        void Start()
        {
            PlayerTrigger.gameOver += GameOver;//订阅
        }

        // Update is called once per frame  
        void Update()
        {

        }
        public void ShowDetail()
        {
            GUI.Label(new Rect(220, 50, 500, 500), "Try to escape away from\n patrol. Each success escape for one point.if you are\n caught by Guard, Game Over!");
         }

    //实现IUserAction的接口  
        public void ReStart()
        {
             game = 1;
        }
    void GameOver()
        {
            FinalText.text = "Game Over!!!";
            game = 0;
        }
        //实现IUserAction的接口  
        

    }
