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

        // タッチ情報を取得
        Touch[] touches = Input.touches;

        // タッチが左側にあるかどうか
        bool isLeftTouch = false;
        // タッチが右側にあるかどうか
        bool isRightTouch = false;

        // タッチが指１本以上ある場合、すべての指のタッチ状態をチェック
        if (touches.Length > 0)
        {

            for (int i = 0; i < touches.Length; i++)
            {
                // 左側のタッチが未検出の場合、左側にタッチがあるかをチェック
                if (!isLeftTouch)
                {
                    // 左側に指があるかどうかを取得
                    isLeftTouch = touches[i].position.x < 1080 / 2;
                }

                // 右側のタッチが未検出の場合、右側にタッチがあるかをチェック
                if (!isRightTouch)
                {
                    // 右側に指があるかどうかを取得
                    isRightTouch = touches[i].position.x >= 1080 / 2;
                }
            }


        }

        // 左矢印キーを押した時、または左側のタッチ開始時、左フリッパーを動かす
        if (tag == "LeftFripperTag" && (Input.GetKeyDown (KeyCode.LeftArrow) || isLeftTouch))
        {
            SetAngle (this.flickAngle);
        }

        // 右矢印キーを押した時、または右側のタッチ開始時、右フリッパーを動かす
        if (tag == "RightFripperTag" && (Input.GetKeyDown (KeyCode.RightArrow) || isRightTouch))
        {
            SetAngle (this.flickAngle);
        }

        // 矢印キーが離された時、またはタッチ終了時、フリッパーを元に戻す
        if (tag == "LeftFripperTag" && (Input.GetKeyUp (KeyCode.LeftArrow) || !isLeftTouch))
        {
            SetAngle (this.defaultAngle);
        }

        if (tag == "RightFripperTag" && (Input.GetKeyUp (KeyCode.RightArrow) || !isRightTouch))
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
