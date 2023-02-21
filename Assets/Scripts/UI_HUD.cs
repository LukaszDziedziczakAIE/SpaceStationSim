using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_HUD : MonoBehaviour
{
    public static UI_HUD Instance;
    [field: SerializeField] public UI_Bottom BottomUI { get; private set; }
    [field: SerializeField] public UI_BuildMenu BuildMenuUI { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
}
