using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiskFactory : MonoBehaviour
{
    private Dictionary<int, Disk> used = new Dictionary<int, Disk>();
    private List<Disk> free = new List<Disk>();
    private List<int> wait = new List<int>();
    public GameObject disk;

    private void Awake()
    {
        disk = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/disk"), Vector3.zero, Quaternion.identity);
        disk.SetActive(false);
    }

    private void Update()
    {

        foreach (var temp in used.Values)
        {

            if (!temp.gameObject.activeSelf)
            {
                wait.Add(temp.GetInstanceID());
            }
        }

        foreach (int temp in wait)
        {
            FreeDisk(used[temp].gameObject);
        }
        wait.Clear();
    }
    
    public GameObject GetDisk(int round, ActionMode mode)
    {
        GameObject aDisk = null;
        if (free.Count == 0)
        {
            aDisk = GameObject.Instantiate<GameObject>(disk, Vector3.zero, Quaternion.identity);
            aDisk.AddComponent<Disk>();
            
        }
        else
        {
            aDisk = free[0].gameObject;
            free.Remove(free[0]);
        }
        Disk diskdata = aDisk.GetComponent<Disk>();
        switch (round)
        {

            case 0:
                {
                    diskdata.color = Color.red;
                    diskdata.speed = 3.0f;
                    float rx;
                    if (UnityEngine.Random.Range(-1f, 1f) < 0)
                        rx = 1;
                    else
                        rx = -1;
                    diskdata.direction = new Vector3(rx, 1, 0);
                    aDisk.GetComponent<Renderer>().material.color = Color.red;
                    break;
                }
            case 1:
                {
                    diskdata.color = Color.yellow;
                    diskdata.speed = 5.0f;
                    float rx;
                    if (UnityEngine.Random.Range(-1f, 1f) < 0)
                        rx = 1;
                    else
                        rx = -1;
                    diskdata.direction = new Vector3(rx, 1, 0);
                    aDisk.GetComponent<Renderer>().material.color = Color.yellow;
                    break;
                }
            case 2:
                {
                    diskdata.color = Color.blue;
                    diskdata.speed = 7.0f;
                    float rx;
                    if (UnityEngine.Random.Range(-1f, 1f) < 0)
                        rx = 1;
                    else
                        rx = -1;
                    diskdata.direction = new Vector3(rx, 1, 0);
                    aDisk.GetComponent<Renderer>().material.color = Color.blue ;
                    break;
                }
        }
        if (mode == ActionMode.PHYSICAL)
        {
            aDisk.AddComponent<Rigidbody>();
        }
        used.Add(diskdata.GetInstanceID(), diskdata);
        aDisk.name = aDisk.GetInstanceID().ToString();
        
        return aDisk;
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