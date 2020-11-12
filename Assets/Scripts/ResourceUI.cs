using System;
using UnityEngine;
using UnityEngine.UI;

public class ResourceUI : MonoBehaviour
{
    [SerializeField] private Text resourceText;
    [SerializeField] private Resource resource;

    private void Update()
    {
        resourceText.text = resource.CurrentAmount.ToString($"{resource.name}: 0");
    }
}
