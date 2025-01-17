using UnityEngine;
using System.Collections;
using System;
using TMPro;

public class SimpleGestureListener : MonoBehaviour, KinectGestures.GestureListenerInterface
{
	// GUI Text to display the gesture messages.
	public TextMeshProUGUI GestureInfo;
    public TextMeshProUGUI MouseInfo;
    
    // private bool to track if progress message has been displayed
    private bool progressDisplayed;
	
	
	public void UserDetected(uint userId, int userIndex)
	{
		// as an example - detect these user specific gestures
		KinectManager manager = KinectManager.Instance;

		manager.DetectGesture(userId, KinectGestures.Gestures.Jump);
		manager.DetectGesture(userId, KinectGestures.Gestures.Squat);

//		manager.DetectGesture(userId, KinectGestures.Gestures.Push);
//		manager.DetectGesture(userId, KinectGestures.Gestures.Pull);
		
//		manager.DetectGesture(userId, KinectWrapper.Gestures.SwipeUp);
//		manager.DetectGesture(userId, KinectWrapper.Gestures.SwipeDown);
		
		if(GestureInfo != null)
		{
			GestureInfo.GetComponent<TextMeshProUGUI>().text = "SwipeLeft, SwipeRight, Jump or Squat.";
		}
	}
	
	public void UserLost(uint userId, int userIndex)
	{
		if(GestureInfo != null)
		{
            GestureInfo.GetComponent<TextMeshProUGUI>().text = string.Empty;
		}
	}

	public void GestureInProgress(uint userId, int userIndex, KinectGestures.Gestures gesture, 
	                              float progress, KinectWrapper.NuiSkeletonPositionIndex joint, Vector3 screenPos)
	{
		//GestureInfo.guiText.text = string.Format("{0} Progress: {1:F1}%", gesture, (progress * 100));
		if(gesture == KinectGestures.Gestures.Click && progress > 0.3f)
		{
            string sGestureText = "";// string.Format ("{0} {1:F1}% complete", gesture, progress * 100);
            if (MouseInfo != null)
            {
                if (gesture == KinectGestures.Gestures.Click)
                {
                    if (progress < 0.2)
                        sGestureText =" 5";
                    else if (progress < 0.4)
                        sGestureText = "4";
                    else if (progress < 0.6)
                        sGestureText = "3";
                    else if (progress < 0.8)
                        sGestureText = "2";
                    else if (progress < 0.85)
                        sGestureText = "1";
                    else if (progress < 0.99)
                        sGestureText = "0";
                    else sGestureText = "";

                    MouseInfo.GetComponent<TextMeshProUGUI>().text = sGestureText;

                }
                
            }
			progressDisplayed = true;
		}		
		else if((gesture == KinectGestures.Gestures.ZoomOut || gesture == KinectGestures.Gestures.ZoomIn) && progress > 0.5f)
		{
			string sGestureText = string.Format ("{0} detected, zoom={1:F1}%", gesture, screenPos.z * 100);
			if(GestureInfo != null)
				GestureInfo.GetComponent<TextMeshProUGUI>().text = sGestureText;
			
			progressDisplayed = true;
		}
		else if(gesture == KinectGestures.Gestures.Wheel && progress > 0.5f)
		{
			string sGestureText = string.Format ("{0} detected, angle={1:F1} deg", gesture, screenPos.z);
			if(GestureInfo != null)
				GestureInfo.GetComponent<TextMeshProUGUI>().text = sGestureText;
			
			progressDisplayed = true;
		}
	}

	public bool GestureCompleted (uint userId, int userIndex, KinectGestures.Gestures gesture, 
	                              KinectWrapper.NuiSkeletonPositionIndex joint, Vector3 screenPos)
	{
        /*string sGestureText = gesture + " detected";*/
		//if(gesture == KinectGestures.Gestures.Click)        
           //sGestureText += string.Format(" at ({0:F1}, {1:F1})", screenPos.x, screenPos.y);*/
        
		
		progressDisplayed = false;
		
		return true;
	}

	public bool GestureCancelled (uint userId, int userIndex, KinectGestures.Gestures gesture, 
	                              KinectWrapper.NuiSkeletonPositionIndex joint)
	{
		if(progressDisplayed)
		{
			// clear the progress info
			if(GestureInfo != null)
				GestureInfo.GetComponent<TextMeshProUGUI>().text = String.Empty;
            if (MouseInfo != null)
                MouseInfo.GetComponent<TextMeshProUGUI>().text = String.Empty;

            progressDisplayed = false;
		}
		
		return true;
	}
	
}
