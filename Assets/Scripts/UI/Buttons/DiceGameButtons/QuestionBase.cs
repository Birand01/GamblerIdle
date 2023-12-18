using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public abstract class QuestionBase : MonoBehaviour
{
   
    protected Button button; 
    public static event Action<int> OnChangeQuestion;
    [SerializeField] protected int questionIndex;

    protected virtual void Awake()
    {
        button = GetComponent<Button>();
    }

    protected virtual void Start()
    {
        button.onClick.AddListener(OnButtonClickEvent);
    }
    protected virtual void OnButtonClickEvent()
    {
        OnChangeQuestion?.Invoke(questionIndex);
    }

}
