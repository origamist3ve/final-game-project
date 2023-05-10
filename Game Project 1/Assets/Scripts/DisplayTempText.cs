using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayTempText : MonoBehaviour
{
    public float displayTime = 5f;
    public Text displayText;

    private Coroutine displayCoroutine;

    public void ShowMessage(string message)
    {
        if (displayCoroutine != null)
        {
            StopCoroutine(displayCoroutine);
        }

        displayText.text = message;
        displayCoroutine = StartCoroutine(DisplayMessageForTime(displayTime));
    }

    private IEnumerator DisplayMessageForTime(float time)
    {
        displayText.enabled = true;
        yield return new WaitForSeconds(time);
        displayText.enabled = false;
    }
}
