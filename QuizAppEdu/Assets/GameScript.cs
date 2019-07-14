//Незаконченная версия скрипта, версия урока 1.1
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameScript : MonoBehaviour
{

    public QuestionList[] questions;
    public Text[] answersText;
    public Text qText;

    List<object> qList;
    int randQ;

    public void OnClickPlay()
    {
        qList = new List<object>(questions);
        questionGenerate();
    }
    void questionGenerate()
    {
        randQ = Random.Range(0, qList.Count);
        QuestionList crntQ = qList[randQ] as QuestionList;
        qText.text = crntQ.question;
        for (int i = 0; i < crntQ.answers.Length; i++) answersText[i].text = crntQ.answers[i];
    }
    public void AnswerBttns()
    {
        qList.RemoveAt(randQ);
        questionGenerate();
    }
}
[System.Serializable]
public class QuestionList
{
    public string question;
    public string[] answers = new string[3];
}