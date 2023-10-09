using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class UIMove : MonoBehaviour
{
    private RectTransform rectTransform;
    private Vector3 initialPosition;

    private Button btnGameObject;
    private Vector3 startPosition;

    private Ýþlemler islemlerCs;

    private bool isMove=false;
    private int whichOne=0;

    public Text value;

    public bool isConvert=false; 

    void Start()
    {
        islemlerCs = GameObject.FindGameObjectWithTag("GameController").GetComponent<Ýþlemler>();
        rectTransform = GetComponent<RectTransform>();
        btnGameObject= GetComponent<Button>();
        startPosition=rectTransform.position;
    }

    private void Update()
    {
        if (isConvert)
        { 
            rectTransform.position=startPosition;
            gameObject.SetActive(false);
        }
        else
            gameObject.SetActive(true);
    }

    public void sayiYerlestir()
    {
        if (!isMove)
        {
            if (!islemlerCs.fullUiObject1)
            {
                rectTransform.position = islemlerCs.uiObject1.position;
                islemlerCs.ConvertObject1 = gameObject;
                islemlerCs.ConvertObject1Position = startPosition;
                islemlerCs.ConvertObjectRect1 = rectTransform;
                islemlerCs.fullUiObject1 = true;
                isMove = true;
                whichOne = 1;
                islemlerCs.firstValue = Convert.ToInt32(value.text);
            }
            else if (!islemlerCs.fullUiObject2)
            {
                rectTransform.position = islemlerCs.uiObject2.position;
                islemlerCs.ConvertObject2 = gameObject;
                islemlerCs.ConvertObject2Position = startPosition;
                islemlerCs.ConvertObjectRect2 = rectTransform;
                islemlerCs.fullUiObject2 = true;
                isMove = true;
                whichOne= 2;
                islemlerCs.secondValue = Convert.ToInt32(value.text);
            }
        }
        else 
        {
            rectTransform.position = startPosition;
            isMove= false;
            if (whichOne == 1) 
            {
                islemlerCs.fullUiObject1 = false;
                whichOne = 0;
                islemlerCs.firstValue = 0;
            }
            else if(whichOne==2) 
            {
                islemlerCs.fullUiObject2 = false;
                whichOne = 0;
                islemlerCs.secondValue= 0;
            }
        }

        

        

    }
}
