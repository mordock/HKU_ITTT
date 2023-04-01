using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class TestConnection : MonoBehaviour
{
    SerialPort stream = new SerialPort("COM3", 9600);

    private float timeValue;
    private float waitTime = 2;

    int value = 0;
    // Start is called before the first frame update
    void Start()
    {
        stream.Open();

    }

    // Update is called once per frame
    void Update()
    {
        timeValue += Time.deltaTime;

        if (timeValue > waitTime) {
            timeValue = 0;
            stream.Write(value.ToString());

            value++;
            Debug.Log("HIT");
        }
    }
}
