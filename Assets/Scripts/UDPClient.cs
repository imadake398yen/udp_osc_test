using UnityEngine;
using System.Net.Sockets;
using System.Text;
using System.Collections;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;

public class UDPClient : MonoBehaviour
{
    // broadcast address
    // public string host = "192.168.151.255";
    private string host = "192.168.144.79";
    // private string host = "127.0.0.1";
    public int port = 3333;
    private UdpClient client;

    void Start ()
    {
        client = new UdpClient();
        client.Connect(host, port);
        // StartCoroutine(Send());
    }

    void Update ()
    {
    }

    IEnumerator Send () {
        while (true)
        {
            yield return new WaitForSeconds(1.0f);
            byte[] dgram = Encoding.UTF8.GetBytes("hello!");
            client.Send(dgram, dgram.Length);
        }
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