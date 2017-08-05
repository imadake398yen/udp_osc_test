using UnityEngine;
using System.Net.Sockets;
using System.Text;
using System.Collections;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;

public class UDPClient : SingletonMonoBehaviour<UDPClient>
{
    public string host = "172.20.1.93";
    public int port = 3333;
    private UdpClient client;

    private Osc.Parser osc = new Osc.Parser();
    void OnMessage(Osc.Message msg)
    {
        // ここで適当に処理する
        Debug.LogFormat("{0} => {1}", msg.path, msg.data[0]);
    }

    void Start ()
    {
        client = new UdpClient();
        client.Connect(host, port);
        StartCoroutine(Send());
    }

    IEnumerator Send () {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            byte[] dgram = Encoding.UTF8.GetBytes("hello!");
            client.Send(dgram, dgram.Length);
        }
    }

    public void SendData (string message) {
        byte[] dgram = Encoding.UTF8.GetBytes(message);
        client.Send(dgram, dgram.Length);
    }

    void OnGUI()
    {
        if(GUI.Button (new Rect (10,10,100,40), "Send"))
        {
            byte[] dgram = Encoding.UTF8.GetBytes("hello!");
            client.Send(dgram, dgram.Length);
        }
    }

    void OnApplicationQuit()
    {
        client.Close();
    }
}