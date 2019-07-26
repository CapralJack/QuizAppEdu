using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameScript : MonoBehaviour
{

	public QuestionList[] questions;
	public Text[] answersText;
	public Text qText;
	public GameObject headPanel;
	public Button[] answerBttns = new Button[3];

	List<object> qList;
	QuestionList crntQ;
	int randQ;

	public void OnClickPlay()
	{
		qList = new List<object>(questions);
		questionGenerate();
		if (!headPanel.GetComponent<Animator>().enabled) headPanel.GetComponent<Animator>().enabled = true;
		else headPanel.GetComponent<Animator>().SetTrigger("In");
	}
	void questionGenerate()
	{
		if (qList.Count > 0)
		{
			randQ = Random.Range(0, qList.Count);
			crntQ = qList[randQ] as QuestionList;
			qText.text = crntQ.question;
			List<string> answers = new List<string>(crntQ.answers);
			for (int i = 0; i < crntQ.answers.Length; i++)
			{
				int rand = Random.Range(0, answers.Count);
				answersText[i].text = answers[rand];
				answers.RemoveAt(rand);
			}
			//StartCoroutine(animBttns());
		}
		else
		{
			print("Вы прошли игру");
		}
	}
	public void AnswerBttns(int index)
	{
		if (answersText[index].text.ToString() == crntQ.answers[0]) print("Правильный ответ");
		else print("Неправильный ответ");
	}
}
[System.Serializable]
public class QuestionList
{
	public string question;
	public string[] answers = new string[3];
}