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

    // 前回のタッチの履歴
    private int leftPreCnt;
    private int rightPreCnt;

    // Use this for initialization
    void Start ()
    {
        // HingeJointコンポーネント取得
        this.myHingeJoint = GetComponent<HingeJoint> ();

        // フリッパーの傾きを設定
        SetAngle (this.defaultAngle);

        // タッチ履歴を0に初期化
        leftPreCnt = 0;
        rightPreCnt = 0;

    }

    // Update is called once per frame
    void Update ()
    {
        // 【発展課題】タッチ操作に対応するための処理を追記

        // タッチ情報を取得
        Touch[] touches = Input.touches;

        // 左側のEndedの数
        int leftEndedCnt = 0;
        // 右側のEndedの数
        int rightEndedCnt = 0;

        // 左側にタッチがあるかどうか
        bool[] isLeftTouches = new bool[touches.Length];
        // 右側にタッチがあるかどうか
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

        // 複数の指でタッチされた場合の対応

        // Endedの数を調べ、左右の離されたタッチが最後の１本かどうかをチェック
        bool isLeftEnded = leftEndedCnt == 1;
        bool isRightEnded = rightEndedCnt == 1;

        // 左右の今回のタッチ数
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

        // Movedで反対側に移動されるとEndedが検出できない場合の対応

        // 前回と今回のタッチ数を比較
        // タッチが左右反対側へすべて移動した場合、Endedのフラグをtrueにする
        if (leftPreCnt != leftCnt && leftCnt == 0)
        {
            isLeftEnded = true;
        }
        if (rightPreCnt != rightCnt && rightCnt == 0)
        {
            isRightEnded = true;
        }

        // 今回の左右のタッチ数を記録
        leftPreCnt = leftCnt;
        rightPreCnt = rightCnt;

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
