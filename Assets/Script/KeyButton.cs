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
	private Text typeKey;	//英語小文字

	[SerializeField]	
	private Text kana1,kana2,kana3;	//日本語(シフトなし,上段シフト、逆シフト))

    // クリック時のイベントリスナー
    public System.Action OnClickedListener;
	private KanaKeyPosInfo _keyInfo;

	void Awake()
	{
		//初期化
		kana1.text = "";
		kana2.text = "";
		kana3.text = "";
	}
	
	// Use this for initialization
	void Start () {
		//文字の基本文字
		baseCharaColor = gameObject.GetComponent<Image>().color;

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//初期化処理
	public void Initialization(KanaKeyPosInfo keyInfo){

		//英語
		typeKey.text = keyInfo.typeKey;

		//下の段
		kana1.text = keyInfo.kana1;

		//上の段
		kana2.text = keyInfo.kana2;

		//逆シフト
		kana3.text = keyInfo.kana3;
	}
}
