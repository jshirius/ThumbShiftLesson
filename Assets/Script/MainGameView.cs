using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainGameView : MonoBehaviour {

	[SerializeField]
	private Text targetChara;

	[SerializeField]
	private Text guide;

	//[SerializeField]
	//private Text inputChara;

	// Use this for initialization
	public void UpdateTargetChara (string str) {
		targetChara.text = str;
	}
	
	// Use this for initialization
	public void UpdateGuide (string str) {
		guide.text = str;
	}
}
