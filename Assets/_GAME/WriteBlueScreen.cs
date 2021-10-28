using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WriteBlueScreen : MonoBehaviour
{
    private Text text;
    private QuestionUiController questionUI;
    private QuestionAsset questionAsset;

    private string textFull = "A problem has been detected and windows has been shut down to prevent damage to your computer.\n\nUNMOUNTABLE_BOOT_VOLUME\n\nIf this is the first time you've seen this error screen, \nrestart your computer. If this screen appear again, follow\nthese steps:\n\nCheck to make sure any new hardware or software is properly installed.\nIf this is a new installation, ask your hardware or software manufacturer\nfor any windows updates you might need.\n\nIf problems continue, disable or remove any newly installed hardware\n or software. Disable BIOS memory options such as caching or shadowing.\nIf you need to use Safe Mode to remove or disable components, restart\nyour computer, press F8 to select Advanced Startup Options, and then\nSelect Safe Mode.\n\n Technical Information:\n\n*** STOP: 0x000000ED (0x80F128D0, 0xc000009c, 0x00000000)";
    private string currentText = "";
    private bool isAnswer = false;
    private bool isLevelScene = false;

    [SerializeField, Tooltip("time to write one letter")]
    private float letterTimeToWrite = 0.05f;

    private int i = 0;
    private float writeTime = 0;
    private void Awake()
    {
        questionUI = FindObjectOfType<QuestionUiController>();
        text = GetComponent<Text>();
    }

    private void Update()
    {
        if (i < textFull.Length)
        {
            if(writeTime >= letterTimeToWrite)
            {
                currentText += textFull[i];

                if(isLevelScene)
                    questionUI.TextZone.text = currentText;
                else
                    text.text = currentText;
                

                writeTime = 0;
                i++;
            }
        }

        if(i == 1)
        {
            if(isAnswer)
            {
                questionUI.TextZone.transform.localPosition = new Vector3(3.909f, 197, 0);
                isAnswer = false;
            }
        }

        writeTime += Time.deltaTime;
    }

    public void SetText(QuestionAsset questionAsset)
    {
        isLevelScene = true;
        questionUI.TextZone.transform.localPosition = new Vector3(3.909f, -212.809f, 0);
        currentText = "";
        this.questionAsset = questionAsset;
        textFull = $"{questionAsset.Question}";
        i = 0;
    }

    public void SetAnswer(QuestionAsset questionAsset)
    {
        currentText = "";
        this.questionAsset = questionAsset;
        textFull = $"{questionAsset.Answer}";
        isAnswer = true;
       
        i = 0;
    }
}
