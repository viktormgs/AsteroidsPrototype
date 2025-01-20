using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NewRecordScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI recordText;
    private const float fadeSpeed = 2f;

    private void Start()
    {
        GameplayEvents.OnNewRecord += ShowNewRecordText;
        if (recordText.alpha == 1) recordText.alpha = 0;
    }

    private void OnDestroy()
    {
        GameplayEvents.OnNewRecord -= ShowNewRecordText;
    }

    private void ShowNewRecordText()
    {
        StartCoroutine(MakeVisible());
        IEnumerator MakeVisible()
        {
            recordText.alpha = 1;
            yield return new WaitForSeconds(2.5f);

            while (recordText.alpha > 0f)
            {
                recordText.alpha -= fadeSpeed * Time.deltaTime;
            }

            recordText.alpha = 0;
            yield break;
        }
    }



}
