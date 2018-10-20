using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainGameView : MonoBehaviour {

	[SerializeField]
	private Text targetChara;

	[SerializeField]
	private Text guide;

	[SerializeField]
	private Text inputChara;

	[SerializeField]
	private Text selectCaption;	

	//リスナー設定
	public System.Action OnSelectLeftClickedListener;
	public System.Action OnSelectRightClickedListener;

	// Use this for initialization
	public void UpdateTargetChara (string str) {
		targetChara.text = str;
	}
	
	// Use this for initialization
	public void UpdateGuide (string str) {
		guide.text = str;
	}

	// Use this for initialization
	public void UpdateInputChara (string str) {
		inputChara.text = str;
	}

	public void SelectCaption (string str) {
		selectCaption.text = str;
	}

	

	public void OnSelectLeftClicked(){
		if(OnSelectLeftClickedListener != null){
			OnSelectLeftClickedListener();
		}
	}

	public void OnSelectRightClicked(){
		if(OnSelectRightClickedListener != null){
			OnSelectRightClickedListener();
		}
	}


}
