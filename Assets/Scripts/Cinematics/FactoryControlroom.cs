using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FactoryControlroom : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI[] tmp;
    public bool active = true;
    public int n = 0;
    float t = 0;
    private void Start()
    {
        
    }
    private void Update()
    {

        t += Time.deltaTime;
        for (int i = n+1; i < tmp.Length+1; i++)
        {
            if (t > Random.Range(0.01f, 0.05f))
            {
                tmp[i-1].text = Random.Range(0, 9) + "";
                t = 0;
            }
        }
        if (active)
        {
            if (n == 0 || n == 2)
            {
                tmp[n].text = "0";
            }
            if (n == 1)
            {
                tmp[n].text = "4";
            }
            if (n == 3)
            {
                tmp[n].text = "7";
            }
        }
    }
}
