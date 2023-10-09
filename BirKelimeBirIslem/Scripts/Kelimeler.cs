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

        string[] kelimeHavuzu = { "ABAK�S", "ABARTI", "ACENTE", "AKBABA" , "BADANA", "BA�CIK" , "BAL�NA" , "BASTON" , "CAMBAZ" , "CIMBIZ" , "C�ZDAN" , "CAYMAK" , "�A�LAR"
        ,"�EMBER" ,"�ENT�K" , "�ILGIN" ,"DA�LIK" ,"DALGI�","DALMAK", "DARICA", "EBAB�L", "ECZANE","E��T�M","ESK�MO", "FAKT�R", "FARAZ�","FAYANS","F�ZYON", "GALER�"
        ,"GARSON","GEZG�N","GRAF�T","HARDAL","HANGAR","HAVALE","HAYVAN","ILIMAN","IZGARA","I�ILTI","ILIMLI","�K��ER","��G�D�","�LG�L�","�NAT�I","JAGUAR","JAMBON","JEOLOG"
        ,"J�B�LE","KAB�NE","KANGAL","KAPALI","KARATE","LAHANA","LAVABO","LEJYON","L�TYUM","MA�AZA","MAHLEP","MAKYAJ","MECAZ�","NAKAVT","NEKTAR","NESNEL","NOKSAN"
        ,"OLUMLU","ORK�DE","ORANTI","OKLAVA","�DENEK","�ZVER�","�R�NT�","�ZENT�","PANCAR","PARSEL","P�YANO","P�R�N�","REKLAM","REPL�K","R�ZGAR","REZERV","SANAY�","SE�K�N"
        ,"SERVET","SIKKIN","�AHBAZ","�EFKAT","��M�NE","�ABLON","TABELA","TAR�IN","TEMS�L","TUR�ST","U�URLU","UZANTI","U�ULTU","UZLA�I","�RET�M","�RK�N�","�TOPYA","���MEK"
        ,"VES�LE","VADEL�","V�TR�N","VASITA","YAL�IN","YENGE�","YILMAZ","YO�URT","ZAHMET","ZEYT�N","Z�RAAT","ZORLUK"};

        int kelimeSayi = UnityEngine.Random.Range(0, kelimeHavuzu.Length);
        anaKelime = kelimeHavuzu[kelimeSayi];

        for(int i=0;i<anaKelime.Length;i++)
        {
            anaKelimeChars[i] = anaKelime[i];
        }

        System.Random rastgele = new System.Random();

        randomQuestionMarkPosition = UnityEngine.Random.Range(1, 6);

        anaKelimeChars[randomQuestionMarkPosition] = '?';

        // Diziyi kar��t�rmak i�in Fisher-Yates algoritmas�n� kullan�n
        for (int i = anaKelimeChars.Length - 1; i > 0; i--)
        {
            int r = rastgele.Next(i + 1); // Rastgele bir indis se�in

            // Dizinin i. eleman� ile rastgele se�ilen eleman� yer de�i�tirin
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

