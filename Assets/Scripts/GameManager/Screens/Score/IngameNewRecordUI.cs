using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IngameNewRecordUI : MonoBehaviour
{
    TextMeshProUGUI textRecord;
    const float speed = 2f;

    void Start()
    {
        ScoreManager.instance.OnIngameNewRecord += ShowNewRecordText;

        textRecord = GetComponent<TextMeshProUGUI>();

        if (textRecord.alpha == 1) textRecord.alpha = 0;
    }

    void ShowNewRecordText() {
        StartCoroutine(MakeVisible());
        IEnumerator MakeVisible()
        {
            textRecord.alpha = 1;
            yield return new WaitForSeconds(2.5f);

            while (textRecord.alpha > 0f) textRecord.alpha -= speed * Time.deltaTime;

            textRecord.alpha = 0;
            yield break;
        }
    }

    

}
