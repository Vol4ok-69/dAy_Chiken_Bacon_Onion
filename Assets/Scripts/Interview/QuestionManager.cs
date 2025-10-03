using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class QuestionManager : MonoBehaviour
{
    [System.Serializable]
    public class QuestionData
    {
        public string question;
        public string[] answers;
        public int correctAnswerIndex;
    }

    [Header("UI Elements")]
    [SerializeField] private TMP_Text questionText;
    [SerializeField] private Button[] answerButtons;
    [SerializeField] private TMP_Text[] answerTexts;

    [Header("End Game Panel")]
    [SerializeField] private GameObject endPanel;
    [SerializeField] private Image resultImage;
    [SerializeField] private Sprite successSprite;
    [SerializeField] private Sprite failSprite;

    [Header("Questions & Answers")]
    [SerializeField] private QuestionData[] questions;
    
    [Header("Colors")]
    [SerializeField] private Color normalColor = Color.white;
    [SerializeField] private Color correctColor = Color.green;
    [SerializeField] private Color wrongColor = Color.red;
    [SerializeField] private float colorDuration = 1.5f;

    private int currentQuestionIndex = 0;
    private int wrongAnswers = 0;

    void Start()
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            int index = i;
            answerButtons[i].onClick.AddListener(() => OnAnswerSelected(index));
        }

        endPanel.SetActive(false);
        LoadQuestion(0);
    }

    void LoadQuestion(int questionIndex)
    {
        if (questionIndex >= questions.Length) return;

        currentQuestionIndex = questionIndex;
        QuestionData currentQuestion = questions[questionIndex];

        questionText.text = currentQuestion.question;

        for (int i = 0; i < answerTexts.Length; i++)
        {
            if (i < currentQuestion.answers.Length)
            {
                answerTexts[i].text = currentQuestion.answers[i];
            }
        }
    }

    void OnAnswerSelected(int selectedAnswerIndex)
    {
        StartCoroutine(ProcessAnswer(selectedAnswerIndex));
    }

    IEnumerator ProcessAnswer(int selectedAnswerIndex)
    {
        SetButtonsInteractable(false);

        QuestionData currentQuestion = questions[currentQuestionIndex];
        bool isCorrect = (selectedAnswerIndex == currentQuestion.correctAnswerIndex);

        Image selectedButtonImage = answerButtons[selectedAnswerIndex].GetComponent<Image>();
        Color originalColor = selectedButtonImage.color;
        selectedButtonImage.color = isCorrect ? correctColor : wrongColor;

        if (!isCorrect)
        {
            Image correctButtonImage = answerButtons[currentQuestion.correctAnswerIndex].GetComponent<Image>();
            correctButtonImage.color = correctColor;
            wrongAnswers++;
            
            if (wrongAnswers >= 3)
            {
                yield return new WaitForSeconds(colorDuration);
                ShowEndPanel(false);
                yield break;
            }
        }

        yield return new WaitForSeconds(colorDuration);

        foreach (Button button in answerButtons)
        {
            button.GetComponent<Image>().color = normalColor;
        }

        SetButtonsInteractable(true);
        LoadNextQuestion();
    }

    void LoadNextQuestion()
    {
        currentQuestionIndex++;
        if (currentQuestionIndex < questions.Length)
        {
            LoadQuestion(currentQuestionIndex);
        }
        else
        {
            ShowEndPanel(true);
        }
    }

    void ShowEndPanel(bool success)
    {
        endPanel.SetActive(true);
        resultImage.sprite = success ? successSprite : failSprite;
        
        // Отключаем основные элементы викторины
        questionText.text = "";
        foreach (TMP_Text answerText in answerTexts)
        {
            answerText.text = "";
        }
        foreach (Button button in answerButtons)
        {
            button.GetComponent<Image>().color = normalColor;
        }
        SetButtonsInteractable(false);
    }

    void SetButtonsInteractable(bool interactable)
    {
        foreach (Button button in answerButtons)
        {
            button.interactable = interactable;
        }
    }

    public void RestartQuiz()
    {
        currentQuestionIndex = 0;
        wrongAnswers = 0;
        endPanel.SetActive(false);
        
        // Восстанавливаем цвета кнопок
        foreach (Button button in answerButtons)
        {
            button.GetComponent<Image>().color = normalColor;
        }
        
        SetButtonsInteractable(true);
        LoadQuestion(0);
    }
}