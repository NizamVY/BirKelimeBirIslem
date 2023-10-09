using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class İşlemler : MonoBehaviour
{
    public bool fullUiObject1=false;
    public bool fullUiObject2=false;

    public RectTransform uiObject1;
    public RectTransform uiObject2;

    public int firstValue;
    public int secondValue;

    public GameObject esittir;
    public Dropdown dropDownItem;

    public int CurrentValue;

    public GameObject ConvertObject1;
    public GameObject ConvertObject2;
    public Vector3 ConvertObject1Position;
    public Vector3 ConvertObject2Position;
    public RectTransform ConvertObjectRect1;
    public RectTransform ConvertObjectRect2;

    public GameObject uiPrefab;
    public Transform spawnPoint;
    public Transform parentFolder;

    public bool isMod = true;
    public Text uiText;

    public GameObject numberBox;
    public Text[] numberObjects;
    public GameObject[] numberImageObjects;
    public int MainTarget = 0;

    public GameObject btnNextScene;
    public MainScene mainSceneManager;

    public Image timerImage;
    public Text timerText;

    public bool playerisLose;
    public GameObject resolutionText;
    public Text equationText;


    // Start is called before the first frame update
    void Start()
    {
        mainSceneManager=GameObject.FindGameObjectWithTag("MainSceneTag").GetComponent<MainScene>();
        mainSceneManager.timeRemaining = mainSceneManager.crosswordTime;
        mainSceneManager.timerIsRunning = true;
        resolutionText.SetActive(false);
        equationText.gameObject.SetActive(false);
        playerisLose= false;
        CurrentValue = 0;
        uiText.text = "2";

        int[] anaSayılar = {UnityEngine.Random.Range(5,19)*5, UnityEngine.Random.Range(3,10), UnityEngine.Random.Range(5, 10), UnityEngine.Random.Range(2, 10), UnityEngine.Random.Range(2, 10), UnityEngine.Random.Range(1, 4) };
        MainTarget = anaSayılar[0] * anaSayılar[1] + (anaSayılar[3] * anaSayılar[4] + (anaSayılar[2] - anaSayılar[5]));

        int islem1 = anaSayılar[0] * anaSayılar[1];
        int islem2 = anaSayılar[3] * anaSayılar[4];
        int islem3 = anaSayılar[2] - anaSayılar[5];
        int islem4 = islem1 + islem2;
        int islem5 = islem4 + islem3;

        numberObjects[0].text = anaSayılar[0].ToString();
        numberObjects[1].text = anaSayılar[1].ToString();
        numberObjects[2].text = anaSayılar[2].ToString();
        numberObjects[3].text = anaSayılar[3].ToString();
        numberObjects[4].text = anaSayılar[4].ToString();
        numberObjects[5].text = anaSayılar[5].ToString();
        numberObjects[6].text = MainTarget.ToString();

        btnNextScene.gameObject.SetActive(false);

        equationText.text = "İşlem 1: "+ anaSayılar[0] + " * " + anaSayılar[1]+" = "+islem1+"\n"+
                            "İşlem 2: "+ anaSayılar[3] + " * " + anaSayılar[4] + " = " + islem2+"\n"+
                            "İşlem 3: "+ anaSayılar[2] + " - " + anaSayılar[5] + " = " + islem3+"\n"+
                            "İşlem 4: "+ islem1+" + "+islem2 + " = " + islem4+"\n"+
                            "İşlem 5: "+ islem4+" + "+islem3 + " = " + islem5;


    }

    // Update is called once per frame
    void Update()
    {
        if (fullUiObject1 && fullUiObject2)
        {
            esittir.SetActive(true);
        }
        else 
        {
            esittir.SetActive(false);
        }

        if(Convert.ToInt32(uiText.text)==MainTarget)
        {
            btnNextScene.gameObject.SetActive(true);
        }

        timerImage.fillAmount = mainSceneManager.timeRemaining / mainSceneManager.crosswordTime;
        timerText.text = Convert.ToInt32(mainSceneManager.timeRemaining).ToString();


        if (timerText.text == "0")
        {
            playerisLose = true;
            StartCoroutine(CoroutineWait());
        }
    }



    public void LoadMainScene()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void NextScene()
    {
        SceneManager.LoadScene("GameKelime");
        DontDestroyOnLoad(mainSceneManager.gameObject);
        mainSceneManager.timeRemaining = mainSceneManager.crosswordTime;
    }

    public void Restart()
    {
        foreach (var t in numberImageObjects)
        {
           t.gameObject.SetActive(true);
        }
        GameObject[] objectsToDestroy = GameObject.FindGameObjectsWithTag("Clone");

        foreach (var i in objectsToDestroy)
        { 
            Destroy(i.gameObject);
        }

        fullUiObject1=false;
        fullUiObject2=false;

        if (ConvertObjectRect1 != null)
        {
            ConvertObjectRect1.position = ConvertObject1Position;
        }

        if (ConvertObjectRect2 != null)
        {
            ConvertObjectRect2.position = ConvertObject2Position;
        }

        uiText.text = "0";
    }

    public void SayiTasi()
    {
        
        Debug.Log("Deneme");
    }

    public void buttonEsittir()
    {
        string dropDownValue = dropDownItem.value.ToString();

        switch (dropDownValue)
        {
            case "0":
                CurrentValue = firstValue + secondValue;
                isMod = true;
                fullUiObject1 = false;
                fullUiObject2 = false;
                break;
            case "1":
                if (firstValue >= secondValue)
                {
                    CurrentValue = firstValue - secondValue;
                    isMod = true;
                    fullUiObject1 = false;
                    fullUiObject2 = false;
                }
                else
                {
                    isMod = false;
                }
                break;
            case "3":
                CurrentValue = firstValue * secondValue;
                isMod = true;
                fullUiObject1 = false;
                fullUiObject2 = false;
                break;
            case "2":
                if (firstValue % secondValue == 0)
                {
                    CurrentValue = firstValue / secondValue;
                    isMod = true;
                    fullUiObject1 = false;
                    fullUiObject2 = false;
                }
                else
                {
                    isMod = false;
                }

                break;
        }

        if (isMod)
        {
            ConvertObjectRect1.position = ConvertObject1Position;
            ConvertObjectRect2.position = ConvertObject2Position;
            ConvertObject1.SetActive(false);
            ConvertObject2.SetActive(false);

            fullUiObject1 = false;
            fullUiObject2 = false;


            // UI prefabını spawn etmek için Instantiate kullanın
            GameObject spawnedUI = Instantiate(uiPrefab, parentFolder);
            spawnedUI.transform.position = ConvertObject2Position;


            // Eğer UI nesnesindeki metin veya diğer bileşenlere erişmeniz gerekiyorsa, aşağıdaki gibi yapabilirsiniz:
            // Örnek olarak, bir metin bileşenine erişme
            uiText = spawnedUI.GetComponentInChildren<Text>();
            if (uiText != null)
            {
                uiText.text = CurrentValue.ToString(); // Metni değiştirme
            }

            fullUiObject1 = false;
            fullUiObject2 = false;

            
        }
    }

    IEnumerator CoroutineWait()
    {
        Restart();
        
        foreach (var i in numberImageObjects)
        {
            UIMove iUIMove= i.GetComponent<UIMove>();
            if(iUIMove != null)
            {
                iUIMove.enabled= false;
            }
        }

        foreach (var i in numberObjects)
        {
            Button iBtn = i.GetComponent<Button>();
            if(iBtn!=null)
            {
                iBtn.enabled= false;
            }
        }


        resolutionText.SetActive(true);
        yield return new WaitForSecondsRealtime(1.0f);
        equationText.gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(5.0f);
        btnNextScene.SetActive(true);

    }

    public void GameOver()
    {
        Application.Quit();
    }
}
