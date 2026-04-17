using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject endGamePanel;
    [SerializeField] private AudioClip themeSongClip;
    private TextMeshProUGUI scoreText;
    private TextMeshProUGUI timeText;
    private TextMeshProUGUI endGameTitleText;
    private AudioSource themeAudioSource;
    private float elapsedTime;
    private bool finalTimeCaptured;

    void Start()
    {
        StartThemeSong();
        CreateHudTexts();
        CacheEndGameTitleText();
    }

    void Update()
    {
        if (!GameController.gameOver)
        {
            elapsedTime += Time.deltaTime;
        }

        UpdateHudTexts();

        if (GameController.gameOver)
        {
            if (!finalTimeCaptured)
            {
                finalTimeCaptured = true;
                UpdateEndGameText();
            }

            endGamePanel.SetActive(true);
        }
    }

    private void CreateHudTexts()
    {
        scoreText = CreateTextObject("CandiesText", new Vector2(20f, -20f));
        timeText = CreateTextObject("TimeText", new Vector2(20f, -60f));
    }

    private TextMeshProUGUI CreateTextObject(string objectName, Vector2 anchoredPosition)
    {
        GameObject textObject = new GameObject(objectName, typeof(RectTransform), typeof(TextMeshProUGUI));
        textObject.transform.SetParent(transform, false);

        RectTransform rectTransform = textObject.GetComponent<RectTransform>();
        rectTransform.anchorMin = new Vector2(0f, 1f);
        rectTransform.anchorMax = new Vector2(0f, 1f);
        rectTransform.pivot = new Vector2(0f, 1f);
        rectTransform.anchoredPosition = anchoredPosition;
        rectTransform.sizeDelta = new Vector2(300f, 40f);

        TextMeshProUGUI text = textObject.GetComponent<TextMeshProUGUI>();
        text.font = TMP_Settings.defaultFontAsset;
        text.fontSize = 28;
        text.color = Color.white;
        text.alignment = TextAlignmentOptions.Left;
        text.raycastTarget = false;

        return text;
    }

    private void CacheEndGameTitleText()
    {
        if (endGamePanel == null)
        {
            return;
        }

        Transform titleTransform = endGamePanel.transform.Find("Text (TMP)");
        if (titleTransform != null)
        {
            endGameTitleText = titleTransform.GetComponent<TextMeshProUGUI>();
        }
    }

    private void UpdateHudTexts()
    {
        if (scoreText != null)
        {
            scoreText.text = $"Candies: {GameController.Score}";
        }

        if (timeText != null)
        {
            timeText.text = $"Time: {elapsedTime:0.0}s";
        }
    }

    private void UpdateEndGameText()
    {
        if (endGameTitleText != null)
        {
            if (GameController.HasFailed)
            {
                endGameTitleText.text = $"Poison Candy!\nTime: {elapsedTime:0.0}s";
            }
            else
            {
                endGameTitleText.text = $"Candy Clear!\nTime: {elapsedTime:0.0}s";
            }
        }
    }

    private void StartThemeSong()
    {
        if (themeSongClip == null)
        {
            return;
        }

        themeAudioSource = gameObject.GetComponent<AudioSource>();
        if (themeAudioSource == null)
        {
            themeAudioSource = gameObject.AddComponent<AudioSource>();
        }

        themeAudioSource.playOnAwake = false;
        themeAudioSource.loop = true;
        themeAudioSource.clip = themeSongClip;
        themeAudioSource.Play();
    }

}
