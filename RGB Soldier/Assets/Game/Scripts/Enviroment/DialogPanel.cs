﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class DialogPanel : MonoBehaviour
{

    public Text displayText;
    public GameObject dialogPanelObject;
    public float letterPause = 0.2f;

    private bool keyPressed;
    private static DialogPanel dialogPanel;

    public static DialogPanel Instance()
    {
        if (!dialogPanel)
        {
            dialogPanel = FindObjectOfType(typeof(DialogPanel)) as DialogPanel;
            if (!dialogPanel)
                Debug.LogError("There needs to be one active ModalPanel script on a GameObject in your scene.");
        }

        return dialogPanel;
    }

    void Update()
    {
        if (Input.anyKeyDown || Input.touchCount > 0)
        {
            keyPressed = true;
        }
    }

    // Insert Dialog
    public IEnumerator StartDialog(List<string> allText)
    {
        dialogPanelObject.SetActive(true);
        foreach (var text in allText)
        {
            yield return StartCoroutine(TypeText(text));
            yield return StartCoroutine(WaitForKeyPress());
        }
    }

    IEnumerator WaitForKeyPress()
    {
        while (!keyPressed)
        {
            yield return null;
        }
        keyPressed = false;
    }

    IEnumerator TypeText(string text)
    {
        displayText.text = "";
        keyPressed = false;
        foreach (char letter in text.ToCharArray())
        {
            bool printing = true;
            if (keyPressed && printing)
            {
                displayText.text = text;
                keyPressed = false;
                break;
            }
            else
            {
                displayText.text += letter;
                yield return new WaitForSeconds(letterPause);
            }

        }
    }

    void ClosePanel()
    {
        dialogPanelObject.SetActive(false);
    }
}