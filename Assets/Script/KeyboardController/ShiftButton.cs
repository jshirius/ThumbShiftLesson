using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ShiftButton : MonoBehaviour {


	Color baseCharaColor;//通常の文字色
	Color baseButtonColor;//通常の背景の色

	//タイピング対象文字色(赤)
	readonly Color baseTargetCharaColor = new Color(1.0f, 0.0f, 0.0f, 1.0f);

	//タイピング対象の背景色
	readonly Color baseTargetColor = new Color(21/255f, 42/255f, 1.0f, 1.0f);

	[SerializeField]
	private Text textCap;	//テキスト

	void Awake()
	{

		//背景色
		baseButtonColor = gameObject.GetComponent<Image>().color;
		baseCharaColor = textCap.color;
	}

	//シフト押すときのボタン
	public void SelectKey(){
		//ボタンの色を変更する
		gameObject.GetComponent<Image>().color = baseTargetColor;
		textCap.color  = baseTargetCharaColor;
		

	}

	//キーの設定をもとに戻す
	public void ResetKey(){
		//ボタンの色を変更する
		gameObject.GetComponent<Image>().color = baseButtonColor;

		//ボタン文字色
		textCap.color = baseCharaColor;

	}	

}
