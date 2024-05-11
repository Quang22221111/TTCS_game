using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadDisplayControl : MonoBehaviour
{
    public GameObject enemys;
    public GameObject trans;
    private int num;

    private void Start()
    {
        trans.gameObject.SetActive(false);
    }

    private void Update()
    {
    
        num = enemys.transform.childCount;

        if (num == 0)
        {
            DisplayRoad();
        }
    }

    public void DisplayRoad()
    {
        trans.gameObject.SetActive(true);
    }
}
