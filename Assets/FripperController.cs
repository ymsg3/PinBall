using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FripperController : MonoBehaviour
{

    // HingeJoint�R���|�[�l���g������
    private HingeJoint myHingeJoint;

    // �����̌X��
    private float defaultAngle = 20;
    // �e�������̌X��
    private float flickAngle = -20;

    // �O��̃^�b�`�̗���
    private int leftPreCnt;
    private int rightPreCnt;

    // Use this for initialization
    void Start ()
    {
        // HingeJoint�R���|�[�l���g�擾
        this.myHingeJoint = GetComponent<HingeJoint> ();

        // �t���b�p�[�̌X����ݒ�
        SetAngle (this.defaultAngle);

        // �^�b�`������0�ɏ�����
        leftPreCnt = 0;
        rightPreCnt = 0;

    }

    // Update is called once per frame
    void Update ()
    {
        // �y���W�ۑ�z�^�b�`����ɑΉ����邽�߂̏�����ǋL

        // �^�b�`�����擾
        Touch[] touches = Input.touches;

        // ������Ended�̐�
        int leftEndedCnt = 0;
        // �E����Ended�̐�
        int rightEndedCnt = 0;

        // �����Ƀ^�b�`�����邩�ǂ���
        bool[] isLeftTouches = new bool[touches.Length];
        // �E���Ƀ^�b�`�����邩�ǂ���
        bool[] isRightTouches = new bool[touches.Length];

        // ���ׂĂ̎w�̃^�b�`��Ԃ��`�F�b�N
        for (int i = 0; i < touches.Length; i++)
        {
            // ���E�̃^�b�`�̗L�����擾
            isLeftTouches[i] = touches[i].position.x < 1080 / 2;
            isRightTouches[i] = touches[i].position.x >= 1080 / 2;

            // ���E�̃^�b�`��Ended�������炻�ꂼ����Z
            leftEndedCnt += isLeftTouches[i] && touches[i].phase == TouchPhase.Ended ? 1 : 0;
            rightEndedCnt += isRightTouches[i] && touches[i].phase == TouchPhase.Ended ? 1 : 0;
        }

        // �����̎w�Ń^�b�`���ꂽ�ꍇ�̑Ή�

        // Ended�̐��𒲂ׁA���E�̗����ꂽ�^�b�`���Ō�̂P�{���ǂ������`�F�b�N
        bool isLeftEnded = leftEndedCnt == 1;
        bool isRightEnded = rightEndedCnt == 1;

        // ���E�̍���̃^�b�`��
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

        // Moved�Ŕ��Α��Ɉړ�������Ended�����o�ł��Ȃ��ꍇ�̑Ή�

        // �O��ƍ���̃^�b�`�����r
        // �^�b�`�����E���Α��ւ��ׂĈړ������ꍇ�AEnded�̃t���O��true�ɂ���
        if (leftPreCnt != leftCnt && leftCnt == 0)
        {
            isLeftEnded = true;
        }
        if (rightPreCnt != rightCnt && rightCnt == 0)
        {
            isRightEnded = true;
        }

        // ����̍��E�̃^�b�`�����L�^
        leftPreCnt = leftCnt;
        rightPreCnt = rightCnt;

        // �����L�[�����������A�܂��͍����̃^�b�`�����鎞�A���t���b�p�[�𓮂���
        if (tag == "LeftFripperTag" && (Input.GetKeyDown (KeyCode.LeftArrow) || leftCnt > 0))
        {
            SetAngle (this.flickAngle);
        }

        // �E���L�[�����������A�܂��͉E���̃^�b�`�����鎞�A�E�t���b�p�[�𓮂���
        if (tag == "RightFripperTag" && (Input.GetKeyDown (KeyCode.RightArrow) || rightCnt > 0))
        {
            SetAngle (this.flickAngle);
        }

        // ���L�[�������ꂽ���A�܂��͍��E�̃^�b�`�������ꂽ���A���ꂼ��t���b�p�[�����ɖ߂�
        if (tag == "LeftFripperTag" && (Input.GetKeyUp (KeyCode.LeftArrow) || isLeftEnded))
        {
            SetAngle (this.defaultAngle);
        }
        if (tag == "RightFripperTag" && (Input.GetKeyUp (KeyCode.RightArrow) || isRightEnded))
        {
            SetAngle (this.defaultAngle);
        }

    }

    // �t���b�p�[�̌X����ݒ�
    public void SetAngle (float angle)
    {
        JointSpring jointSpr = this.myHingeJoint.spring;
        jointSpr.targetPosition = angle;
        this.myHingeJoint.spring = jointSpr;
    }

}
