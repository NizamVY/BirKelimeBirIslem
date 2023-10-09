using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Kelimeler : MonoBehaviour
{
    public string anaKelime;
    public char[] anaKelimeChars;
    public int randomQuestionMarkPosition;

    public Text[] textObjects;
    public InputField kelimeInput;
    public GameObject newSceneButton;

    public MainScene mainSceneManager;
    public Image timerImage;
    public Text timerText;

    public GameObject resolutionText;
    public bool playerisLose;

    // Start is called before the first frame update
    void Start()
    {
        mainSceneManager = GameObject.FindGameObjectWithTag("MainSceneTag").GetComponent<MainScene>();
        mainSceneManager.timeRemaining = mainSceneManager.crosswordTime;
        mainSceneManager.timerIsRunning = true;
        playerisLose = false;
        resolutionText.SetActive(false);
        newSceneButton.SetActive(false);
        anaKelimeChars = new char[6];

        string[] kelimeHavuzu = { "ABAKÜS", "ABARTI", "ACENTE", "AKBABA" , "BADANA", "BAÐCIK" , "BALÝNA" , "BASTON" , "CAMBAZ" , "CIMBIZ" , "CÜZDAN" , "CAYMAK" , "ÇAÐLAR"
        ,"ÇEMBER" ,"ÇENTÝK" , "ÇILGIN" ,"DAÐLIK" ,"DALGIÇ","DALMAK", "DARICA", "EBABÝL", "ECZANE","EÐÝTÝM","ESKÝMO", "FAKTÖR", "FARAZÝ","FAYANS","FÜZYON", "GALERÝ"
        ,"GARSON","GEZGÝN","GRAFÝT","HARDAL","HANGAR","HAVALE","HAYVAN","ILIMAN","IZGARA","IÞILTI","ILIMLI","ÝKÝÞER","ÝÇGÜDÜ","ÝLGÝLÝ","ÝNATÇI","JAGUAR","JAMBON","JEOLOG"
        ,"JÜBÝLE","KABÝNE","KANGAL","KAPALI","KARATE","LAHANA","LAVABO","LEJYON","LÝTYUM","MAÐAZA","MAHLEP","MAKYAJ","MECAZÝ","NAKAVT","NEKTAR","NESNEL","NOKSAN"
        ,"OLUMLU","ORKÝDE","ORANTI","OKLAVA","ÖDENEK","ÖZVERÝ","ÖRÜNTÜ","ÖZENTÝ","PANCAR","PARSEL","PÝYANO","PÝRÝNÇ","REKLAM","REPLÝK","RÜZGAR","REZERV","SANAYÝ","SEÇKÝN"
        ,"SERVET","SIKKIN","ÞAHBAZ","ÞEFKAT","ÞÖMÝNE","ÞABLON","TABELA","TARÇIN","TEMSÝL","TURÝST","UÐURLU","UZANTI","UÐULTU","UZLAÞI","ÜRETÝM","ÜRKÜNÇ","ÜTOPYA","ÜÞÜMEK"
        ,"VESÝLE","VADELÝ","VÝTRÝN","VASITA","YALÇIN","YENGEÇ","YILMAZ","YOÐURT","ZAHMET","ZEYTÝN","ZÝRAAT","ZORLUK"};

        int kelimeSayi = UnityEngine.Random.Range(0, kelimeHavuzu.Length);
        anaKelime = kelimeHavuzu[kelimeSayi];

        for(int i=0;i<anaKelime.Length;i++)
        {
            anaKelimeChars[i] = anaKelime[i];
        }

        System.Random rastgele = new System.Random();

        randomQuestionMarkPosition = UnityEngine.Random.Range(1, 6);

        anaKelimeChars[randomQuestionMarkPosition] = '?';

        // Diziyi karýþtýrmak için Fisher-Yates algoritmasýný kullanýn
        for (int i = anaKelimeChars.Length - 1; i > 0; i--)
        {
            int r = rastgele.Next(i + 1); // Rastgele bir indis seçin

            // Dizinin i. elemaný ile rastgele seçilen elemaný yer deðiþtirin
            char temp = anaKelimeChars[i];
            anaKelimeChars[i] = anaKelimeChars[r];
            anaKelimeChars[r] = temp;
        }

        textObjects[0].text = anaKelimeChars[0].ToString();
        textObjects[1].text = anaKelimeChars[1].ToString();
        textObjects[2].text = anaKelimeChars[2].ToString();
        textObjects[3].text = anaKelimeChars[3].ToString();
        textObjects[4].text = anaKelimeChars[4].ToString();
        textObjects[5].text = anaKelimeChars[5].ToString();

        
    }


    // Update is called once per frame
    void Update()
    {
        kelimeInput.text=kelimeInput.text.ToUpper();
        if (anaKelime == kelimeInput.text && !playerisLose)
        {
            newSceneButton.SetActive(true);
        }

        timerImage.fillAmount = mainSceneManager.timeRemaining / mainSceneManager.crosswordTime;
        timerText.text=Convert.ToInt32(mainSceneManager.timeRemaining).ToString();


        if (timerText.text == "0")
        {
            playerisLose = true;
            StartCoroutine(CoroutineWait());
        }
    }

    public void LoadNewScene()
    {
        SceneManager.LoadScene("GameIslem");
        DontDestroyOnLoad(mainSceneManager.gameObject);
    }

    public void LoadBackScene()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void GameOver()
    {
        Application.Quit();
    }

    IEnumerator CoroutineWait()
    {
        resolutionText.SetActive(true);
        yield return new WaitForSecondsRealtime(1.0f);
        kelimeInput.text = anaKelime.ToString();
        yield return new WaitForSecondsRealtime(5.0f);
        newSceneButton.SetActive(true);
        
    }
}

