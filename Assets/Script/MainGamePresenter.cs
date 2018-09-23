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

	// Use this for initialization
	void Start () {

		//モデル初期化
		_model = new MainGameModel();
	

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
				Debug.Log(rtn);

				if(rtn == true){
					//正解！
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

			Debug.Log("targetカウンター更新:" +  _model.TargetIndex.Value);
		});

	 	//現在の入力文字を表示する
		_view.UpdateTargetChara(nowTargetChara());

		//ガイドの設定
		_view.UpdateGuide(guildString(nowTargetChara()));
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

}
