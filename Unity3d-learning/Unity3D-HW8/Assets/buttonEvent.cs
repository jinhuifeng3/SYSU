using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buttonEvent : MonoBehaviour
{

    private Button myButton;
    public Text text;
    private int frame = 20;

    // Use this for initialization  
    void Start()
    {
        Button btn = this.gameObject.GetComponent<Button>();
        btn.onClick.AddListener(OnClick);
    }
    //text 折叠
    IEnumerator rotateIn()
    {
        float rx = 0;
        float ry = 50; //文本高度
        for (int i = 0; i < frame; i++)
        {
            rx -= 90f / frame;
            ry -= 50f / frame;
            //渐变
            text.transform.rotation = Quaternion.Euler(rx, 0, 0);
            text.rectTransform.sizeDelta = new Vector2(text.rectTransform.sizeDelta.x, ry);
            if (i == frame - 1)
            {
                text.gameObject.SetActive(false);
            }
            yield return null;
        }
    }
    //text显示出来
    IEnumerator rotateOut()
    {
        float rx = -90;
        float xy = 0;
        for (int i = 0; i < frame; i++)
        {
            rx += 90f / frame;
            xy += 50f / frame;
            text.transform.rotation = Quaternion.Euler(rx, 0, 0);
            text.rectTransform.sizeDelta = new Vector2(text.rectTransform.sizeDelta.x, xy);
            if (i == 0)
            {
                text.gameObject.SetActive(true);
            }
            yield return null;
        }
    }

    //点击事件
    void OnClick()
    {
        if (text.gameObject.activeSelf)
        {
            StartCoroutine(rotateIn());
        }
        else
        {
            StartCoroutine(rotateOut());
        }

    }
}
