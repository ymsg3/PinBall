using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallController : MonoBehaviour
{

    // ボールが見える可能性のあるz軸の最大値
    private float visiblePosZ = -6.5f;

    // ゲームオーバーを表示するテキスト
    private GameObject gameoverText;

    // Use this for initialization
    void Start()
    {
        // シーン中のGameOverTextオブジェクトを取得
        this.gameoverText = GameObject.Find ("GameOverText");
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.position.z < this.visiblePosZ)
        {
            // GameoverTextにゲームオーバーを表示
            this.gameoverText.GetComponent<Text> ().text = "Game ★ Over";
        }
    }
}
