using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiskFactory : MonoBehaviour
{

 
    public GameObject disk;

    private Dictionary<int, Disk> used = new Dictionary<int, Disk>();
    private List<Disk> free = new List<Disk>();
    private List<int> wait = new List<int>();

    private void Awake()
    {
        disk = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/disk"), Vector3.zero, Quaternion.identity);
        disk.SetActive(false);
    }

    private void Update()
    {

        foreach (var tmp in used.Values)
        {

            if (!tmp.gameObject.activeSelf)
            {
                wait.Add(tmp.GetInstanceID());
            }
        }

        foreach (int tmp in wait)
        {
            FreeDisk(used[tmp].gameObject);
        }
        wait.Clear();
    }

    /** 
     * GetDisk这个函数是用来飞碟的， 
     * 每次首次判断free那里还有没有未使用的飞碟， 
     * 有就从free那里获取，没有就生成一个飞碟 
     */

    public GameObject GetDisk(int round, ActionMode mode)
    {
        GameObject newDisk = null;
        if (free.Count > 0)
        {
            newDisk = free[0].gameObject;
            free.Remove(free[0]);
        }
        else
        {
            newDisk = GameObject.Instantiate<GameObject>(disk, Vector3.zero, Quaternion.identity);
            newDisk.AddComponent<Disk>();
        }

        int start = 0;
        if (round == 1) start = 100;
        if (round == 2) start = 250;
        int selectedColor = Random.Range(start, round * 499);

        if (selectedColor > 500)
        {
            round = 2;
        }
        else if (selectedColor > 300)
        {
            round = 1;
        }
        else
        {
            round = 0;
        }

        //回合数判断
        Disk diskdata = newDisk.GetComponent<Disk>();
        switch (round)
        {

            case 0:
                {
                    diskdata.color = Color.yellow;
                    diskdata.speed = 3.0f;
                    float rx;
                    if (UnityEngine.Random.Range(-1f, 1f) < 0)
                        rx = 1;
                    else
                        rx = -1;
                    diskdata.direction = new Vector3(rx, 1, 0);
                    newDisk.GetComponent<Renderer>().material.color = Color.yellow;
                    break;
                }
            case 1:
                {
                    diskdata.color = Color.red;
                    diskdata.speed = 5.0f;
                    float rx;
                    if (UnityEngine.Random.Range(-1f, 1f) < 0)
                        rx = 1;
                    else
                        rx = -1;
                    diskdata.direction = new Vector3(rx, 1, 0);
                    newDisk.GetComponent<Renderer>().material.color = Color.red;
                    break;
                }
            case 2:
                {
                    diskdata.color = Color.black;
                    diskdata.speed = 7.0f;
                    float rx;
                    if (UnityEngine.Random.Range(-1f, 1f) < 0)
                        rx = 1;
                    else
                        rx = -1;
                    diskdata.direction = new Vector3(rx, 1, 0);
                    newDisk.GetComponent<Renderer>().material.color = Color.black;
                    break;
                }
        }

        used.Add(diskdata.GetInstanceID(), diskdata);
        newDisk.name = newDisk.GetInstanceID().ToString();
        return newDisk;
    }

    public void FreeDisk(GameObject disk)
    {
        Disk temp = null;
        foreach (Disk i in used.Values)
        {
            if (disk.GetInstanceID() == i.gameObject.GetInstanceID())
            {
                temp = i;
            }
        }
        if (temp != null)
        {
            temp.gameObject.SetActive(false);
            free.Add(temp);
            used.Remove(temp.GetInstanceID());
        }
    }
}