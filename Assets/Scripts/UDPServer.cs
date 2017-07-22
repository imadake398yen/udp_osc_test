using UnityEngine;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

public class UDPServer : MonoBehaviour
{
    int port = 3333;
    static UdpClient udp;
    Thread thread;

    IPEndPoint remoteIP;
    byte[] data;
    Socket s;

    void Start ()
    {
        // var hostName = Dns.GetHostName();
        // print(hostName);
        // var addresses = Dns.GetHostAddresses("192.168.144.79");
        // print(addresses);
        // foreach (var a in addresses) {
        //     print(a);
        // }

        // var local = new IPEndPoint(IPAddress.Any, port);
        // udp = new UdpClient(local);
        // udp.Client.ReceiveTimeout = 1000;
        // thread = new Thread(new ThreadStart(ThreadMethod));
        // thread.Start(); 

        remoteIP = new IPEndPoint(IPAddress.Parse("192.168.144.79"), port);
        data = Encoding.UTF8.GetBytes("hey!");
        s = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        s.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.IpTimeToLive, 255);
    }

    void OnGUI(){
        if(GUI.Button (new Rect (200,10,100,100), "Send Packet")){
            s.SendTo(data, 0, data.Length, SocketFlags.None, remoteIP);
        }
    }

    void OnApplicationQuit()
    {
        thread.Abort();
    }

    private static void ThreadMethod()
    {
        while(true)
        {
            IPEndPoint remoteEP = null;
            byte[] data = udp.Receive(ref remoteEP);
            string text = Encoding.ASCII.GetString(data);
            Debug.Log(text);
        }
    } 
}