﻿using UnityEngine;

public class CameraScript : MonoBehaviour
{

	private float h;
    private float v;
	/// <summary>
	/// The main camera rotates relative to this container.
	/// </summary>
	private GameObject cameraContainer;
	/// <summary>
	/// A rotation used to calibrate the phone's camera to the game's camera.
	/// </summary>
	private Quaternion calibrationRotation;

	private void Awake()
    {
        cameraContainer = new GameObject("Camera Container");
    }

    private void Start()
    {
        cameraContainer.transform.SetPositionAndRotation(transform.position, transform.rotation);
        transform.SetParent(cameraContainer.transform);
        cameraContainer.transform.Rotate(90f, -90f, 0f);
#if UNITY_ANDROID
		calibrationRotation = new Quaternion(0, 0, 1, 0);
#endif
	}

	private void Update()
    {
#if UNITY_ANDROID //Camera Control on Android

        // Rotates the camera using feedback from the gyroscope. It flips the
        // input because the Gyroscope is right-handed and Unity is left-handed.
        transform.localRotation = Input.gyro.attitude * calibrationRotation;
#endif

#if UNITY_EDITOR_WIN //Camera Control on PC
        if (Input.GetButton("Fire2"))
        {
            h += 5 * Input.GetAxis("Mouse Y");
            v += 5 * Input.GetAxis("Mouse X");
            Camera.main.transform.eulerAngles = new Vector3(-h, v, 0);
        }
#endif
    }

//Debug: Camera rotation
//    private void OnGUI()
//    {
//#if DEBUG
//        GUILayout.Label(string.Format("Gyro attitude: {0}\nCamera attitude: {1}\nCamera local attitude: {2}", Input.gyro.attitude, transform.rotation, transform.localRotation));
//#endif
//    }
}