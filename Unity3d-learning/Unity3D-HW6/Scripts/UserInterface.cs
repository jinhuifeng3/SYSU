using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUserAction
{
    void ShowDetail();//显示规则  
    void ReStart();//重新开始  
}

public class UserInterface : MonoBehaviour
{
    private IUserAction action;
    public GameObject role;
    public float speed = 1;
    public SceneController sceneController;
    // Use this for initialization  
    void Start()
    {
        action = SSDirector.getInstance().currentScenceController as IUserAction;
        sceneController = (SceneController)SSDirector.getInstance().currentScenceController;
        role = sceneController.player;
    }
    void OnGUI()
    {

    }
    // Update is called once per frame  
    void Update()
    {
        if (sceneController.game != 0)
        {
            float translationX = Input.GetAxis("Horizontal") * speed;
            float translationZ = Input.GetAxis("Vertical") * speed;
            translationX *= Time.deltaTime;
            translationZ *= Time.deltaTime;
            role.transform.Translate(translationX, 0, 0);
            role.transform.Translate(0, 0, translationZ);
        }
    }
}