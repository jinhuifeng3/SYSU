using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hw2;

namespace Hw2
{
    public enum State { START, SEMOVING, ESMOVING, END, WIN, LOSE };

    public interface UserAction
    {
        void priestSOnB();
        void priestEOnB();
        void devilSOnB();
        void devilEOnB();
        void moveShip();
        void offShipL();
        void offShipR();
        void reset();
    }

    public class SSDirector : System.Object, UserAction
    {
        private static SSDirector _instance;
        public Controller currentScenceController;
        public State state = State.START;
        private Model game_obj;

        public static SSDirector GetInstance()
        {
            if (_instance == null)
            {
                _instance = new SSDirector();
            }
            return _instance;
        }

        public Controller getController()
        {
            return currentScenceController;
        }

        internal void setController(Controller t)
        {
            if (currentScenceController == null)
            {
                currentScenceController = t;
            }
        }

        internal void setModel(Model someone)
        {
            if (game_obj == null)
            {
                game_obj = someone;
            }
        }

        public Model getModel()
        {
            return game_obj;
        }


        public void priestSOnB()
        {
            game_obj.priS();
        }
        public void priestEOnB()
        {
            game_obj.priE();
        }
        public void devilSOnB()
        {
            game_obj.delS();
        }
        public void devilEOnB()
        {
            game_obj.delE();
        }
        public void moveShip()
        {
            game_obj.moveShip();
        }
        public void offShipL()
        {
            game_obj.getOffTheShip(0);
        }
        public void offShipR()
        {
            game_obj.getOffTheShip(1);
        }
        public void reset()
        {
            Application.LoadLevel(Application.loadedLevelName);
            state = State.START;
        }
    }


}

public class Controller : MonoBehaviour
{
    void Start()
    {
        SSDirector instance = SSDirector.GetInstance();
        instance.setController(this);
    }
}