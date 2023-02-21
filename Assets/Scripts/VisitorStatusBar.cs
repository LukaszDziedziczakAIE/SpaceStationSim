using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VisitorStatusBar : MonoBehaviour
{
    [SerializeField] Image statusBar;
    
    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void UpdateBar(float amount)
    {
        statusBar.fillAmount = amount;
    }
}
