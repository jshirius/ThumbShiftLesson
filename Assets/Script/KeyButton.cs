using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyButton : MonoBehaviour {

	Color baseCharaColor;//通常の文字色

	//タイピング対象文字色
	readonly Color baseTargetCharaColor = new Color(1.0f, 0.0f, 0.0f, 1.0f);

	//通常時の文字色
	//タイピング対象の文字色

	[SerializeField]
	private Text TypeKey;	//英語小文字

	[SerializeField]	
	private Text kana1,kana2,kana3;	//日本語(シフトなし,上段シフト、逆シフト))

    // クリック時のイベントリスナー
    public System.Action OnClickedListener;

	
	// Use this for initialization
	void Start () {
		//文字の基本文字
		baseCharaColor = gameObject.GetComponent<Image>().color;

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
