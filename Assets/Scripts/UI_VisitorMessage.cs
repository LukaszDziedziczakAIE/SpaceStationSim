using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_VisitorMessage : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI messageElement;
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float alphaChangeRate = 1f;

    RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        MovementLogic();
        AlphaLogic();
        RotationLogic();
    }

    private void RotationLogic()
    {
        rectTransform.LookAt(Camera.main.transform);
        rectTransform.eulerAngles = new Vector3(0, rectTransform.eulerAngles.y + 180, 0);
    }

    public void SetMessage(string message)
    {
        messageElement.text = message;
    }

    private void AlphaLogic()
    {
        float alpha = messageElement.alpha;
        alpha -= alphaChangeRate * Time.deltaTime;
        messageElement.alpha = alpha;

        if (messageElement.alpha <= 0) Destroy(gameObject);
    }

    private void MovementLogic()
    {
        Vector3 position = rectTransform.position;

        position.y += moveSpeed * Time.deltaTime;

        rectTransform.position = position;
    }
}
