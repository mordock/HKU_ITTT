using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class TestConnection : MonoBehaviour
{
    SerialPort stream = new SerialPort("COM3", 9600);

    private float timeValue;
    private float waitTime = 2;

    float value = 0;
    private GameObject player;
    // Start is called before the first frame update
    void Start() {
        stream.Open();
        stream.ReadTimeout = 5;

        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update() {
        try {
            value = float.Parse(stream.ReadLine());
        } catch (System.Exception) {

        }

        if (value > 0) {
            player.GetComponent<Jumping>().Jump(value);
            value = 0;
        }
        //timeValue += Time.deltaTime;

        //if (timeValue > waitTime) {
        //    timeValue = 0;
        //    stream.Write(value.ToString());

        //    value++;
        //    Debug.Log("HIT");
        //}
    }

    public void SendValueToArduino(string valueToSend) {
        stream.Write(valueToSend);
    }

    public void CloseStream() {
        stream.Close();
    }
}
