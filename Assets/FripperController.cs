using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FripperController : MonoBehaviour
{

    // HingeJointコンポーネントを入れる
    private HingeJoint myHingeJoint;

    // 初期の傾き
    private float defaultAngle = 20;
    // 弾いた時の傾き
    private float flickAngle = -20;

    // 前回のタッチ数の履歴
    private int leftPreCnt;
    private int rightPreCnt;

    // Use this for initialization
    void Start ()
    {
        // HingeJointコンポーネント取得
        this.myHingeJoint = GetComponent<HingeJoint> ();

        // フリッパーの傾きを設定
        SetAngle (this.defaultAngle);
    }

    // Update is called once per frame
    void Update ()
    {
        // 【発展課題】タッチ操作に対応するための処理を追記


        // １、タッチ状態によって、フリッパーを動かすかどうか判定

        // タッチ情報を取得
        Touch[] touches = Input.touches;

        // 左右のEndedの数
        int leftEndedCnt = 0;
        int rightEndedCnt = 0;

        // 左右にタッチがあるかどうか
        bool[] isLeftTouches = new bool[touches.Length];
        bool[] isRightTouches = new bool[touches.Length];

        // すべての指のタッチ状態をチェック
        for (int i = 0; i < touches.Length; i++)
        {
            // 左右のタッチの有無を取得
            isLeftTouches[i] = touches[i].position.x < 1080 / 2;
            isRightTouches[i] = touches[i].position.x >= 1080 / 2;

            // 左右のタッチがEndedだったらそれぞれ加算
            leftEndedCnt += isLeftTouches[i] && touches[i].phase == TouchPhase.Ended ? 1 : 0;
            rightEndedCnt += isRightTouches[i] && touches[i].phase == TouchPhase.Ended ? 1 : 0;
        }


        // ２、複数の指でタッチされた場合の対応

        // 今回、最後の指のタッチか離された時かどうかを左右それぞれチェック
        bool isLeftEnded = leftEndedCnt == 1;
        bool isRightEnded = rightEndedCnt == 1;

        // 今回のタッチ数をカウントして代入
        int leftCnt = 0;
        int rightCnt = 0;

        for (int i = 0; i < isLeftTouches.Length; i++)
        {
            leftCnt += isLeftTouches[i] ? 1 : 0;
        }

        for (int i = 0; i < isRightTouches.Length; i++)
        {
            rightCnt += isRightTouches[i] ? 1 : 0;
        }


        // ３、タッチしたまま反対側に移動されるとEndedが検出できない問題への対応

        // 前回と今回のタッチ数を比較
        // タッチが左右の反対側へすべて移動した場合、Endedのフラグをtrueにする
        if (this.leftPreCnt != leftCnt && leftCnt == 0)
        {
            isLeftEnded = true;
        }
        if (this.rightPreCnt != rightCnt && rightCnt == 0)
        {
            isRightEnded = true;
        }

        // 次回の判定用に、今回の左右のタッチ数を記録
        this.leftPreCnt = leftCnt;
        this.rightPreCnt = rightCnt;


        // ４、フリッパーの操作

        // 左矢印キーを押した時、または左側のタッチがある時、左フリッパーを動かす
        if (tag == "LeftFripperTag" && (Input.GetKeyDown (KeyCode.LeftArrow) || leftCnt > 0))
        {
            SetAngle (this.flickAngle);
        }

        // 右矢印キーを押した時、または右側のタッチがある時、右フリッパーを動かす
        if (tag == "RightFripperTag" && (Input.GetKeyDown (KeyCode.RightArrow) || rightCnt > 0))
        {
            SetAngle (this.flickAngle);
        }

        // 矢印キーが離された時、または左右のタッチが離された時、それぞれフリッパーを元に戻す
        if (tag == "LeftFripperTag" && (Input.GetKeyUp (KeyCode.LeftArrow) || isLeftEnded))
        {
            SetAngle (this.defaultAngle);
        }
        if (tag == "RightFripperTag" && (Input.GetKeyUp (KeyCode.RightArrow) || isRightEnded))
        {
            SetAngle (this.defaultAngle);
        }

    }

    // フリッパーの傾きを設定
    public void SetAngle (float angle)
    {
        JointSpring jointSpr = this.myHingeJoint.spring;
        jointSpr.targetPosition = angle;
        this.myHingeJoint.spring = jointSpr;
    }

}
