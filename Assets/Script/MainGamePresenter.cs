using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UniRx;

public class MainGamePresenter : MonoBehaviour {

	[SerializeField]	
	private MainGameModel _model;

	[SerializeField]	
	private MainGameView _view;

	private AudioSource _typeSound;

	[SerializeField] 
	private GameObject _keyButtonPrefab;

	[SerializeField]
	private GameObject _keyboardController;

	//キーボードのキャッシュ(Keyはキーボードの小文字 KanaKeyPosInfoと紐付ける)
	private Dictionary <string,KeyButton> _keyButtons; 

	//現在選択されているキー
	private KeyButton _selectedKey ;

	//シフトボタンの情報
	[SerializeField] 
	private ShiftButton  _rightShiftButton;

	[SerializeField] 
	private ShiftButton  _leftShiftButton;



	// Use this for initialization
	void Start () {

		//モデル初期化
		_model = new MainGameModel();
	
	 	//AudioSourceコンポーネントを取得し、変数に格納
 		_typeSound = GetComponent<AudioSource>();

		//キーを並べる
		makeKeyPos();

		//キーボードからの入力を取得する
		var keyStream = Observable.EveryUpdate()
		.Select(_ => Input.inputString) 
		.Where(xs => Input.anyKeyDown);

		//同時押しの間隔を200msにする
		keyStream.Buffer(keyStream.Throttle(TimeSpan.FromMilliseconds(200)))
    	.Where(xs => xs.Count >= 1)
    	.Subscribe(x =>
            {
				bool rtn = CheckTargetCharactor(nowTargetChara(),x);
				//Debug.Log(rtn);
				//Debug.Log("入力文字:" +  Util.GetKeybordCharactor(x));
				InputResult(rtn);
				if(rtn == true){
					//正解！
					//タイプ音
					_typeSound.PlayOneShot(_typeSound.clip);					

					//次のターゲット文字列を設定する
					_model.NextTargetChara();
				}

        });

		//カウンター監視
		_model.TargetIndex
        .Subscribe(x=> 
		{
			//現在の入力文字を表示する
			_view.UpdateTargetChara(nowTargetChara());

			//ガイドの設定
			_view.UpdateGuide(guildString(nowTargetChara()));

			ChangeSelectKeyborde(nowTargetChara());

			Debug.Log("targetカウンター更新:" +  _model.TargetIndex.Value);
		});

		//練習対象の文字列変更ボタンが押された
		_model.TrainingHistoryIndex
		.Subscribe(x=>
		{
			//練習対象の文字列の設定
			string data = _model.TrainingHistory[x].caption;
			_view.SelectCaption(data);
			Debug.Log("TrainingHistoryIndex更新:" +  _model.TrainingHistoryIndex.Value);

			//テキスト設定
			_model.ResetTargetCharas(x);
			//現在の入力文字を表示する
			_view.UpdateTargetChara(nowTargetChara());

			//ガイドの設定
			_view.UpdateGuide(guildString(nowTargetChara()));

			ChangeSelectKeyborde(nowTargetChara());
		
			
		});

	 	//現在の入力文字を表示する
		_view.UpdateTargetChara(nowTargetChara());

		//ガイドの設定
		_view.UpdateGuide(guildString(nowTargetChara()));
		//ChangeSelectKeyborde(nowTargetChara());

		//モバイルキーボードを表示する
		//TouchScreenKeyboard.Open("", TouchScreenKeyboardType.ASCIICapable);

		//イベント設定
		SetEvent();

	}

	//イベント設定
	void SetEvent(){
		_view.OnSelectLeftClickedListener = OnSelectLeftClicked;
		_view.OnSelectRightClickedListener = OnSelectRightClicked;
	}

   /// <summary>
    /// 疑似キーボードを作成する
    /// </summary>
	void makeKeyPos(){

		//疑似キーボードの情報を保存しておく
		_keyButtons = new Dictionary<string, KeyButton>();

		float basePosX = -355.0f;
		float basePosY = 82.0f;

		float offsetX = 65.0f;
	

		//一列目作成
		foreach (var item in _model.KanaKeyPosInfoData)
		{
			KanaKeyPosInfo info = item.Value;

			if(info.yPos == 2){
				var obj = GameObject.Instantiate(_keyButtonPrefab);
				obj.gameObject.transform.parent = _keyboardController.gameObject.transform;
				obj.gameObject.transform.localPosition = new Vector3(basePosX + info.xPos *offsetX, basePosY );
				obj.gameObject.name = info.typeKey;
				obj.gameObject.transform.localScale = new Vector3(1, 1, 1);
				KeyButton infodata = obj.GetComponent<KeyButton>();
				//文字を設定する
				infodata.Initialization(item.Value);

				_keyButtons[item.Value.typeKey] = infodata;
				//infodata.SelectKey("る");
			}

		}
		
		//２列目の基本位置
		//basePosX = -420.0f;
		basePosY += -65.0f;

		

		//2列目作成
		foreach (var item in _model.KanaKeyPosInfoData)
		{
			KanaKeyPosInfo info = item.Value;

			if(info.yPos == 3){
				var obj = GameObject.Instantiate(_keyButtonPrefab);
				obj.gameObject.transform.parent = _keyboardController.gameObject.transform;
				obj.gameObject.transform.localPosition = new Vector3(basePosX + info.xPos *offsetX, basePosY );
				obj.gameObject.name = info.typeKey;
				obj.gameObject.transform.localScale = new Vector3(1, 1, 1);
				KeyButton infodata = obj.GetComponent<KeyButton>();
				//文字を設定する
				infodata.Initialization(item.Value);

				_keyButtons[item.Value.typeKey] = infodata;
			}

		}

		//3列目作成
		//２列目の基本位置
		//basePosX = -410.0f;
		basePosY += -65.0f;

		foreach (var item in _model.KanaKeyPosInfoData)
		{
			KanaKeyPosInfo info = item.Value;

			if(info.yPos == 4){
				var obj = GameObject.Instantiate(_keyButtonPrefab);
				obj.gameObject.transform.parent = _keyboardController.gameObject.transform;
				obj.gameObject.transform.localPosition = new Vector3(basePosX + info.xPos *offsetX, basePosY );
				obj.gameObject.name = info.typeKey;
				obj.gameObject.transform.localScale = new Vector3(1, 1, 1);
				KeyButton infodata = obj.GetComponent<KeyButton>();
				//文字を設定する
				infodata.Initialization(item.Value);

				_keyButtons[item.Value.typeKey] = infodata;
			}

		}
	}

   /// <summary>
    /// 入力された文字が、ターゲットどおりになっているか判定する
    /// </summary>
    bool CheckTargetCharactor(string targetChara, IList<string> inputdatas){
        bool rtn = false;
        bool shiftOn = false;

        //targetCharaから判定文字を選択する
        KanaKeyMapInfo info =  _model.KanaKeyMapInfoData[targetChara];
        
        //シフト判定
        if(info.leftShift == 1 || info.rightShift == 1){
            shiftOn = true;
        }

        //親指シフト判定
        Debug.Log("ターゲット文字:" + targetChara);

        //文字判定
        string strInput = Util.GetKeybordCharactor(inputdatas);
        if(shiftOn == true){
            //２文字以上
            if((strInput.Contains(" "+ info.typeKey)) || (strInput.Contains(info.typeKey + " "))){
                rtn = true;
            }
        }else{
            //１文字
            if(info.typeKey == strInput){
                rtn = true;
            }
        }
        return rtn;
    }

   	/// <summary>
    /// 今回入力する文字列を取得する
    /// </summary>	
	private string nowTargetChara(){
		string targetChar =  _model.TargetCharas.Substring(_model.TargetIndex.Value,1);
		return targetChar;
	}

   	/// <summary>
    /// 今回入力する文字列のガイドを出す
    /// </summary>	
	private string guildString(string targetChara){
		string guide = "";
		KanaKeyMapInfo info =  _model.KanaKeyMapInfoData[targetChara];

		guide = info.typeKey;

		//右シフト？左シフト？
		if(info.leftShift == 1){
			//左シフト
			guide += " + 左シフト";
		}
		else if(info.rightShift == 1){
			guide += " + 右シフト";
		}

		return guide;
	}

   	/// <summary>
    /// 今回の入力結果
    /// </summary>	
	void InputResult(bool result){
		string str = "";

		if(result == true){
			str ="OK";
		}
		else{
			str ="NG";
		}

		_view.UpdateInputChara(str);

	}	

   	/// <summary>
    /// キーボードの表示の更新
    /// </summary>	
	void ChangeSelectKeyborde(string kana){
	

		//現在のキーボードの表示を修正する
		if(_selectedKey != null)
		_selectedKey.ResetKey();

		//該当するキー
        KanaKeyMapInfo info =  _model.KanaKeyMapInfoData[kana];
		_keyButtons[info.typeKey].SelectKey(kana);

		//現在のキーボードの参照更新
		_selectedKey = _keyButtons[info.typeKey];

		//シフトの表示
		_rightShiftButton.ResetKey();
		_leftShiftButton.ResetKey();

		if(info.rightShift == 1){
			_rightShiftButton.SelectKey();
		}
		else if(info.leftShift == 1){
			_leftShiftButton.SelectKey();
		}
	}

	//左矢印ボタン押した
	private void OnSelectLeftClicked(){
		//カウンターを下げる
		_model.ChangeTrainingHistory(true);
	}

	//右矢印ボタン押した
	private void OnSelectRightClicked(){
		//カウンターを１つ上げる
		_model.ChangeTrainingHistory(false);
	}

}
