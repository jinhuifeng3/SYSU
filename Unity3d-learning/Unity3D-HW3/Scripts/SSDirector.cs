using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class SSDirector : System.Object
    {
        private static SSDirector _instance;
        public ISceneController currentScenceController{get; set;};
        public bool run{get; set;}

        public static SSDirector GetInstance()
        {
            if (_instance == null)
            {
                _instance = new SSDirector();
            }
            return _instance;
        }
    }
