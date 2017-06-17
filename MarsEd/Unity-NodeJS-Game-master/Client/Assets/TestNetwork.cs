using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Quobject.SocketIoClientDotNet.Client;

public class TestNetwork : MonoBehaviour {
    public string ServerUrl;

	// Use this for initialization
	void Start () {


        Debug.Log("Connecting to " + ServerUrl);
        var socket = IO.Socket(ServerUrl);
        socket.On(Socket.EVENT_CONNECT, () =>
        {
            Debug.Log("Emitting hi");
            socket.Emit("hi");
            Debug.Log("Emitted hi");
        });

        socket.On("hi", (data) =>
        {
            Debug.Log("Received");
            Debug.Log(data);
            socket.Disconnect();
        });
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
