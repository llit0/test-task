using System.Collections;
using Scellecs.Morpeh;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
    [SerializeField] private UnityEvent eventOnClick;

    private void Awake()
    { 
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        eventOnClick?.Invoke();
    }
    
    
}
