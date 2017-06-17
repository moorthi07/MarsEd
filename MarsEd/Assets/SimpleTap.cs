using HoloToolkit.Unity;
using HoloToolkit.Unity.SpatialMapping;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.VR.WSA.Input;
using UnityEngine.VR.WSA.WebCam;


//This is to show how to do a simplest tap
//Some of these examples may not compile, they are guides for your code.

public class SimpleTap : MonoBehaviour {

    GestureRecognizer recognizer;
    public float speed;

    void Start () {
        recognizer = new GestureRecognizer();
        recognizer.SetRecognizableGestures(GestureSettings.Tap | GestureSettings.Hold);
        recognizer.TappedEvent += Recognizer_TappedEvent; ;
        recognizer.HoldStartedEvent += Recognizer_HoldStartedEvent;
        recognizer.HoldCompletedEvent += Recognizer_HoldCompletedEvent;
        recognizer.StartCapturingGestures();
    }

    private void Recognizer_HoldCompletedEvent(InteractionSourceKind source, Ray headRay)
    {
       
    }

    private void Recognizer_TappedEvent(InteractionSourceKind source, int tapCount, Ray headRay)
    {
        Debug.Log("Recognizer_TappedEvent");

        //DropCubeInFrontOfCamera();

       // DropCubeAtRaycastPosition();

        transform.Rotate(Vector3.up * Time.deltaTime * speed);

    }

    private void DropCubeInFrontOfCamera()
    {
        // Create a cube
        var cube = GameObject.CreatePrimitive(PrimitiveType.Cube); 
         // Make the cube smaller
        cube.transform.localScale = Vector3.one * 0.3f;
         // Start to drop it in front of the camera
        cube.transform.position = Camera.main.transform.position + Camera.main.transform.forward;
         // Apply physics
        cube.AddComponent<Rigidbody>();
    }

    private void DropCubeAtRaycastPosition()
    {
        var headPosition = Camera.main.transform.position;
        var gazeDirection = Camera.main.transform.forward;

        //Example - when user taps raycast out and see if they've hit a collider.
        //If you want to exclude the spatial mapping mesh use XXX instead of YYYY
        RaycastHit hitInfo;
        

        //Use only the spatial mapping mesh. Ex. If you are placing on a tap
        if (Physics.Raycast(headPosition, gazeDirection, out hitInfo,
            30.0f, SpatialMappingManager.Instance.LayerMask))
        {
            //We've hit a spatial mesh. Drop a cube at the contact point
            GameObject cube = GameObject.CreatePrimitive( PrimitiveType.Cube );
            //To prevent any kind of physics popping, remove collider.
            //We could move the cube up or out a bit and let physics kick in
            //but that assumes we're figuring direction of normal on surface before
            //smoothing it and we'd have to check wall hits nearby as well. 
            //Easiest to just show it here :)
            DestroyImmediate(cube.GetComponent<BoxCollider>());
            cube.transform.position = hitInfo.point;
        }        
    }

    private void Recognizer_HoldStartedEvent(InteractionSourceKind source, Ray headRay)
    {
        Debug.Log("Recognizer_HoldStartedEvent");
    }

    void OnDestroy()
    {
        //recognizer.TappedEvent -= Recognizer_TappedEvent;
        recognizer.HoldStartedEvent -= Recognizer_HoldStartedEvent;
    }

}


