using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 【課題用】得点の計算と表示を制御するクラス
public class PointRegulator : MonoBehaviour
{

    // 得点を表示するテキスト
    GameObject pointText;

    // 小さい星の点数
    int smallStarPoint = 100;
    // 大きい星の点数
    int largeStarPoint = 250;
    // 小さい雲の点数
    int smallCloudPoint = 500;
    // 大きい雲の点数
    int largeCloudPoint = 1000;

    void Start ()
    {
        // シーン中のPointTextオブジェクトを取得
        this.pointText = GameObject.Find ("PointText");
    }

    void Update ()
    {

    }

    void OnCollisionEnter (Collision other)
    {
        // 衝突前の得点を取得
        Text beforePointText = this.pointText.GetComponent<Text> ();

        // 計算用に変換
        int point = int.Parse (beforePointText.text);

        // ターゲットの種類に応じて追加する得点を選択
        if (other.transform.tag == "SmallStarTag")
        {
            point += smallStarPoint;
        }
        else if (other.transform.tag == "LargeStarTag")
        {
            point += largeStarPoint;
        }
        else if (other.transform.tag == "SmallCloudTag")
        {
            point += smallCloudPoint;
        }
        else if (other.transform.tag == "LargeCloudTag")
        {
            point += largeCloudPoint;
        }

        // 衝突後の得点を表示
        beforePointText.text = point.ToString ();
    }

}
