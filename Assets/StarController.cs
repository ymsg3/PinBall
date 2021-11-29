using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarController : MonoBehaviour
{

    // ‰ñ“]‘¬“x
    private float rotSpeed = 0.5f;

    // Use this for initialization
    void Start ()
    {
        // ‰ñ“]‚ğŠJn‚·‚éŠp“x‚ğİ’è
        this.transform.Rotate (0, Random.Range (0, 360), 0);
    }

    // Update is caled once par frame
    void Update ()
    {
        // ‰ñ“]
        this.transform.Rotate (0, this.rotSpeed, 0);
    }


}
