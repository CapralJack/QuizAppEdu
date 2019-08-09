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
	public Sprite[] TFIcons = new Sprite[2];
	public Image TFIcon;
	public Text TFText;

	List<object> qList;
	QuestionList crntQ;
	int randQ;
	bool defaultColor = false, trueColor = false, falseColor = false;

	//void Update()
	//{
	//	if (defaultColor) headPanel.GetComponent<Image>().color = Color.Lerp(headPanel.GetComponent<Image>().color, new Color(231 / 255.0F, 78 / 255.0F, 62 / 255.0F), 8 * Time.deltaTime);
	//	if (trueColor) headPanel.GetComponent<Image>().color = Color.Lerp(headPanel.GetComponent<Image>().color, new Color(104 / 255.0F, 184 / 255.0F, 89 / 255.0F), 8 * Time.deltaTime);
	//	if (falseColor) headPanel.GetComponent<Image>().color = Color.Lerp(headPanel.GetComponent<Image>().color, new Color(192 / 255.0F, 57 / 255.0F, 43 / 255.0F), 8 * Time.deltaTime);
	//}

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
			//qText.gameObject.GetComponent<Animator>().SetTrigger("In");
			List<string> answers = new List<string>(crntQ.answers);
			for (int i = 0; i < crntQ.answers.Length; i++)
			{
				int rand = Random.Range(0, answers.Count);
				answersText[i].text = answers[rand];
				answers.RemoveAt(rand);
			}
			StartCoroutine(animBttns());
		}
		else
		{
			print("Вы прошли игру");
		}
	}
	IEnumerator animBttns()
	{
		yield return new WaitForSeconds(1);
		for (int i = 0; i < answerBttns.Length; i++)
		{
			answerBttns[i].interactable = false;
		}
		int a = 0;
		while (a < answerBttns.Length)
		{
			if (!answerBttns[a].gameObject.activeSelf) answerBttns[a].gameObject.SetActive(true);
		//	else answerBttns[a].gameObject.GetComponent<Animator>().SetTrigger("In");
			a++;
			yield return new WaitForSeconds(1);
		}
		for (int i = 0; i < answerBttns.Length; i++) answerBttns[i].interactable = true;
		yield break;
	}
	IEnumerator trueOrFalse(bool check)
	{
		defaultColor = false;
		for (int i = 0; i < answerBttns.Length; i++) answerBttns[i].interactable = false;
		yield return new WaitForSeconds(1);
		for (int i = 0; i < answerBttns.Length; i++)
		{
			// Turn Off
			answerBttns[i].enabled = false;
			//answerBttns[i].gameObject.GetComponent<Animator>().SetTrigger("Out");
		}

		qText.gameObject.GetComponent<Animator>().SetTrigger("Out");
		yield return new WaitForSeconds(0.5f);
		if (!TFIcon.gameObject.activeSelf) TFIcon.gameObject.SetActive(true);
		//else TFIcon.gameObject.GetComponent<Animator>().SetTrigger("In");
		if (check)
		{
			trueColor = true;
			TFIcon.sprite = TFIcons[0];
			TFText.text = "Правильный ответ";
			yield return new WaitForSeconds(1);
			//TFIcon.gameObject.GetComponent<Animator>().SetTrigger("Out");
			qList.RemoveAt(randQ);
			questionGenerate();
			trueColor = false;
			defaultColor = true;
			yield break;
		}
		else
		{
			falseColor = true;
			TFIcon.sprite = TFIcons[1];
			TFText.text = "Неправильный ответ";
			yield return new WaitForSeconds(1);
			TFIcon.gameObject.GetComponent<Animator>().SetTrigger("Out");
			headPanel.GetComponent<Animator>().SetTrigger("Out");
			falseColor = false;
			defaultColor = true;
			yield break;
		}
	}
	public void AnswerBttns(int index)
	{
		if (answersText[index].text.ToString() == crntQ.answers[0]) StartCoroutine(trueOrFalse(true));
		else StartCoroutine(trueOrFalse(false));
	}
}
[System.Serializable]
public class QuestionList
{
	public string question;
	public string[] answers = new string[3];
}