using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.ARCore;
using Unity.Collections;

public class FacePoseTracker : MonoBehaviour
{
    ARFaceManager faceManager;
    ARSessionOrigin sessionOrigin;

    ARCoreFaceSubsystem subsystem;

    public GameObject nose;
    public GameObject left;
    public GameObject right;

    void Start()
    {
        faceManager = GetComponent<ARFaceManager>();
        sessionOrigin = GetComponent<ARSessionOrigin>();
        subsystem = faceManager.subsystem as ARCoreFaceSubsystem;
    }

    
    void Update()
    {
        foreach(var trackable in faceManager.trackables)
        {

            NativeArray<ARCoreFaceRegionData> regions = new NativeArray<ARCoreFaceRegionData>();
            subsystem.GetRegionPoses(trackable.trackableId, Allocator.Persistent, ref regions);

            foreach(var faceRegion in regions)
            {
                switch(faceRegion.region)
                {
                    case ARCoreFaceRegion.NoseTip:
                        nose.transform.SetPositionAndRotation(faceRegion.pose.position, faceRegion.pose.rotation);
                        break;
                    case ARCoreFaceRegion.ForeheadLeft:
                        left.transform.SetPositionAndRotation(faceRegion.pose.position, faceRegion.pose.rotation);
                        break;
                    case ARCoreFaceRegion.ForeheadRight:
                        right.transform.SetPositionAndRotation(faceRegion.pose.position, faceRegion.pose.rotation);
                        break;
                }
            }
        }
    }
}
