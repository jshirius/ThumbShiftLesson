using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyButton : MonoBehaviour {

	Color baseCharaColor;//通常の文字色
	Color baseButtonColor;//通常の背景の色

	//タイピング対象文字色(赤)
	readonly Color baseTargetCharaColor = new Color(1.0f, 0.0f, 0.0f, 1.0f);

	//タイピング対象の背景色
	readonly Color baseTargetColor = new Color(21/255f, 42/255f, 1.0f, 1.0f);


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

		//背景色
		baseButtonColor = gameObject.GetComponent<Image>().color;

		//文字の色
		baseCharaColor = kana1.color;


	}
	
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//初期化処理
	public void Initialization(KanaKeyPosInfo keyInfo){

		//英語
		typeKey.text = keyInfo.typeKey.ToUpper();

		//下の段
		kana1.text = keyInfo.kana1;

		//上の段
		kana2.text = keyInfo.kana2;

		//逆シフト
		kana3.text = keyInfo.kana3;
	}

	//打つ対象のボタン色
	public void SelectKey(string kana){
		//ボタンの色を変更する
		gameObject.GetComponent<Image>().color = baseTargetColor;

		//該当かな文字を変更する
		if(kana == kana1.text ){
			kana1.color  = baseTargetCharaColor;
		}
		else if(kana == kana2.text ){
			kana2.color  = baseTargetCharaColor;
		}	
		else if(kana == kana3.text ){
			kana3.color  = baseTargetCharaColor;
		}

	}

	//キーの設定をもとに戻す
	public void ResetKey(){
		//ボタンの色を変更する
		gameObject.GetComponent<Image>().color = baseButtonColor;

		//ボタン文字色
		kana1.color = baseCharaColor;
		kana2.color = baseCharaColor;
		kana3.color = baseCharaColor;
	}
}
