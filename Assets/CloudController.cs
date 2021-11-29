using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudController : MonoBehaviour
{

    // 最小サイズ
    private float minimum = 1.0f;
    // 拡大縮小スピード
    private float magSpeed = 10.0f;
    // 拡大率
    private float magnification = 0.07f;

    // Use this for initialization
    void Start ()
    {

    }

    // Update is called once per frame
    void Update ()
    {
        // 雲を拡大縮小
        // 拡大率をthis.transform.localScaleに代入して指定
        // 3DなのでVector3を使って３方向の拡大率を指定
        // 引数１ x軸方向の拡縮：最小サイズ ＋ -1～1を周期的に返すSin（ラジアン：経過時間 × スピードをもとにした値） × 拡大率
        //         つまり、最小サイズ ＋ 拡縮の速さ × 拡縮の大きさ
        // 引数２ y軸：現在のyの拡大率（y軸方向はサイズ変更なし）
        // 引数３ z軸：引数１と同じ
        this.transform.localScale = new Vector3 (this.minimum + Mathf.Sin (Time.time * this.magSpeed) * this.magnification, this.transform.localScale.y, this.minimum + Mathf.Sin (Time.time * this.magSpeed) * this.magnification);

    }
}
