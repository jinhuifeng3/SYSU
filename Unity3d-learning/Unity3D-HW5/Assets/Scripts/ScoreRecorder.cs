using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreRecorder : MonoBehaviour
{

    public int score = 0;

    private Dictionary<Color, int> rules = new Dictionary<Color, int>();

    void Start()
    {
        rules.Add(Color.red, 1); //得分规则
        rules.Add(Color.yellow, 3);
        rules.Add(Color.blue, 5);
    }

    public void Reset()
    {
        score = 0;
    }

    public void Record(GameObject disk)
    {
        score += rules[disk.GetComponent<Disk>().color];
    }
}