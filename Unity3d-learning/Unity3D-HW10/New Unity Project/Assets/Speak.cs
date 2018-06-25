
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
public class Speak : NetworkBehaviour
{
    public GameObject item;
    private Transform content;
    private InputField input;
    private Button send;

    [SyncVar]
    private int onlineNum = 0;
    void Start()
    {
        //添加事件监听器
        content = GameObject.Find("Canvas/Scroll View/Viewport/Content").transform;
        input = GameObject.Find("Canvas/InputField").GetComponent<InputField>();
        send = GameObject.Find("Canvas/send").GetComponent<Button>();
        send.onClick.AddListener(sendCallback);
    }
    private void OnGUI()
    {
        //显示在线人数
        if (!isLocalPlayer)
            return;
        GUI.Label(new Rect(new Vector2(10, 10), new Vector2(150, 50)),
            string.Format("在线人数:{0}", onlineNum));
    }
    private void Update()
    {
      //服务端更新
        if (isServer)
            onlineNum = NetworkManager.singleton.numPlayers;
    }
    void sendCallback()
    {
      //发送
        if (!isLocalPlayer)
            return;
        if (input.text.Length > 0)
        {
            string str = string.Format("{0}:{1}{2}", Network.player.ipAddress, System.Environment.NewLine, input.text);
            CmdSend(str);
            input.text = string.Empty;
        }
    }
    [Command]
    void CmdSend(string str)
    {
        RpcShowMessage(str);
    }
    [ClientRpc]
    void RpcShowMessage(string str)
    {
        GameObject item = Instantiate(item, content);
        item.GetComponentInChildren<Text>().text = str;
    }
}
