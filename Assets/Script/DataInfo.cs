using System.Collections;
using System.Collections.Generic;


/// <summary>
/// キー情報の管理クラス
/// </summary>
public class KanaKeyMapInfo{
    public string kana; //平仮名
    public string typeKey;//キーボードのキー
    public int leftShift;//左シフトかどうかのフラグ
    public int rightShift;//右シフトかどうかのフラグ
}

/// <summary>
/// キー位置の管理クラス
/// </summary>
public class KanaKeyPosInfo{
    public string typeKey; //キーボードのキー
    public string kana1;//シフトなしのカナ
    public string kana2;//上段シフトのカナ
    public string kana3;//逆シフトのカナ
	public int yPos;	//y位置
	public int xPos;	//x位置
}