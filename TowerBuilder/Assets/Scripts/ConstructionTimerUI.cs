﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConstructionTimerUI : MonoBehaviour
{
    [SerializeField] private BuildingConstructor buildingConstructor;
    private Image constructionProgressImage;

    private void Awake()
    {
        constructionProgressImage = transform.Find("mask").Find("image").GetComponent<Image>();
    }

    private void Update()
    {
        constructionProgressImage.fillAmount = buildingConstructor.GetConstrucitonTimerNormalized();
    }
}
