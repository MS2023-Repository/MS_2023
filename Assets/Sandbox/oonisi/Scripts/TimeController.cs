//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//using UnityEngine.SceneManagement;

//namespace InGame.TimeUI
//{
//    public class TimeCounter : MonoBehaviour
//    {
//        //�J�E���g�_�E��
//        GameObject Text;
//        float time = 30.0f;
//        private void Start()
//        {
//            this.Text = GameObject.Find("Text");
//        }

//        // Update is called once per frame
//        void Update()
//        {
//            this.time -= Time.deltaTime;
//            if (this.time < 0)
//            //���Ԃ�\������
//            {
//                this.Text.GetComponent<Text>().text = "�I��";
//                SceneManager.LoadScene("GameOver");
//            }
//            else
//            {
//                this.Text.GetComponent<Text>().text = this.time.ToString("F1");
//            }
//        }
//    }
//}
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//using TMPro;

//public class Timecontroller : MonoBehaviour
//{
//    [SerializeField] float playTime1;
//    float playTime2;
//    [SerializeField] Text timeCdText;
//    [SerializeField] Text timeCuText;
//    [SerializeField] TextMeshProUGUI timeCdTMP;
//    [SerializeField] TextMeshProUGUI timeCuTMP;

//    [SerializeField] Text callText; // �ǋL
//    bool ready = true; // �ǋL

//    // Start is called before the first frame update
//    void Start()
//    {
//        StartCoroutine(StartCall()); // �ǋL
//    }

//    IEnumerator StartCall() // �R���[�`���̒ǋL
//    {
//        callText.text = "��[��";
//        yield return new WaitForSeconds(2f);
//        callText.text = "�X�^�[�g�I";
//        ready = false;
//        yield return new WaitForSeconds(1f);
//        callText.text = "";
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        if (ready) // �ǋL
//        {
//            return;
//        }

//        if (playTime1 <= 0) // ���Ԑ؂ꏈ���̒ǋL
//        {
//            ready = true;
//            callText.text = "���Ԑ؂�`�I";
//        }

//        playTime1 -= Time.deltaTime;
//        timeCdText.text = playTime1.ToString("F1");
//        timeCdTMP.text = playTime1.ToString("F1");

//        playTime2 += Time.deltaTime;
//        timeCuText.text = playTime2.ToString("F1");
//        timeCuTMP.text = playTime2.ToString("F1");
//    }

//    public void OnClickClearBtn() // �{�^���ŌĂԊ֐��̒ǋL
//    {
//        ready = true;
//        callText.text = "�N���A�I";
//    }
//}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeController : MonoBehaviour
{
    public int countdownMinutes = 3;
    private float countdownSeconds;
    private Text timeText;

    private void Start()
    {
        timeText = GetComponent<Text>();
        countdownSeconds = countdownMinutes * 60;
    }

    void Update()
    {
        countdownSeconds -= Time.deltaTime;
        var span = new TimeSpan(0, 0, (int)countdownSeconds);
        timeText.text = span.ToString(@"mm\:ss");

        if (countdownSeconds <= 0)
        {
            // 0�b�ɂȂ����Ƃ��̏���
        }
    }
}