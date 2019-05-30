﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hand_left : MonoBehaviour
{
	[SerializeField]
	private ParticleSystem Particula;
    public KinectWrapper.NuiSkeletonPositionIndex joint = KinectWrapper.NuiSkeletonPositionIndex.HandLeft;
	

    // joint position at the moment, in Kinect coordinates
	public Vector3 outputPosition;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // get the joint position
		KinectManager manager = KinectManager.Instance;

		if(manager && manager.IsInitialized())
		{

			if(manager.IsUserDetected())
			{

				uint userId = manager.GetPlayer1ID();

				if(manager.IsJointTracked(userId, (int)joint))
				{

					// output the joint position for easy tracking
					Vector3 jointPos = manager.GetJointPosition(userId, (int)joint);
					jointPos.Set(jointPos.x * 2, jointPos.y * 2, jointPos.z);
					transform.position = jointPos;

				}
			}
		}
    }
	void OnCollisionEnter (Collision col)
    {
		ParticleSystem obj;
		obj = (ParticleSystem) Instantiate(Particula, this.transform.position, Quaternion.identity);
		
		obj.Play();
		Destroy(col.gameObject,1f);
		Destroy(obj, 1f); 
	
    }
}