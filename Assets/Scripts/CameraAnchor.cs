/***
 * This script will anchor a GameObject to a relative screen position.
 * This script is intended to be used with ViewportHandler.cs by Marcel Căşvan, available here: http://gamedev.stackexchange.com/a/89973/50623
 * It is also copied in this gist below.
 * 
 * Note: For performance reasons it's currently assumed that the game resolution will not change after the game starts.
 * You could not make this assumption by periodically calling UpdateAnchor() in the Update() function or a coroutine, but is left as an exercise to the reader.
 */
/* The MIT License (MIT)

Copyright (c) 2015, Eliot Lash
*/
using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class CameraAnchor : MonoBehaviour {
	public enum AnchorType {
		BottomLeft,
		BottomCenter,
		BottomRight,
		MiddleLeft,
		MiddleCenter,
		MiddleRight,
		TopLeft,
		TopCenter,
		TopRight,
	};
	public AnchorType anchorType;
	public Vector3 anchorOffset;

	IEnumerator updateAnchorRoutine; //Coroutine handle so we don't start it if it's already running

	// Use this for initialization
	void Start () {
		updateAnchorRoutine = UpdateAnchorAsync();
		StartCoroutine(updateAnchorRoutine);
	}

	/// <summary>
	/// Coroutine to update the anchor only once ViewportHandler.Instance is not null.
	/// </summary>
	IEnumerator UpdateAnchorAsync() {
		uint cameraWaitCycles = 0;
		while(ViewportHandler.Instance == null) {
			++cameraWaitCycles;
			yield return new WaitForEndOfFrame();
		}
		if (cameraWaitCycles > 0) {
			print(string.Format("CameraAnchor found ViewportHandler instance after waiting {0} frame(s). You might want to check that ViewportHandler has an earlie execution order.", cameraWaitCycles));
		}
		UpdateAnchor();
		updateAnchorRoutine = null;
	}

	void UpdateAnchor() {
		switch(anchorType) {
		case AnchorType.BottomLeft:
			SetAnchor(ViewportHandler.Instance.BottomLeft);
			break;
		case AnchorType.BottomCenter:
			SetAnchor(ViewportHandler.Instance.BottomCenter);
			break;
		case AnchorType.BottomRight:
			SetAnchor(ViewportHandler.Instance.BottomRight);
			break;
		case AnchorType.MiddleLeft:
			SetAnchor(ViewportHandler.Instance.MiddleLeft);
			break;
		case AnchorType.MiddleCenter:
			SetAnchor(ViewportHandler.Instance.MiddleCenter);
			break;
		case AnchorType.MiddleRight:
			SetAnchor(ViewportHandler.Instance.MiddleRight);
			break;
		case AnchorType.TopLeft:
			SetAnchor(ViewportHandler.Instance.TopLeft);
			break;
		case AnchorType.TopCenter:
			SetAnchor(ViewportHandler.Instance.TopCenter);
			break;
		case AnchorType.TopRight:
			SetAnchor(ViewportHandler.Instance.TopRight);
			break;
		}
	}

	void SetAnchor(Vector3 anchor) {
		Vector3 newPos = anchor + anchorOffset;
		if (!transform.position.Equals(newPos)) {
			transform.position = newPos;
		}
	}

#if UNITY_EDITOR
	// Update is called once per frame
	void Update () {
		if (updateAnchorRoutine == null) {
			updateAnchorRoutine = UpdateAnchorAsync();
			StartCoroutine(updateAnchorRoutine);
		}
	}
#endif
}