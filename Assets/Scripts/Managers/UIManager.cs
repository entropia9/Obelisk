using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;

public class UIManager : MonoBehaviour
{
    const string ANIM_INITIAL = "obelisk_initial_sequence";
    const string ANIM_WRITINGS_APPEAR = "obelisk_writings_appear";
    const string ANIM_WRITINGS_GLOW = "obelisk_writings_glow";
    const int MAX_VISIBLE_WORDS = 200;

    [SerializeField]
    GameObject storyTextBox;

    [SerializeField]
    RectTransform storyTextBar;

    TextMeshProUGUI storyText;
    bool isTyping;

    [SerializeField]
    float delay;

    [SerializeField]
    float timeToMove;

    [SerializeField]
    float timeToScale;

    public Vector3 textBarTargetPosition=new();


    [SerializeField]
    Animator backgroundAnimator;

    [SerializeField]
    Animator obeliskAnimator;

    Coroutine lastTypeTextCoroutine;


    void Start()
    {
        storyText = storyTextBox.GetComponent<TextMeshProUGUI>();
        storyText.text = "";
        isTyping = false;
        EventManager.StoryAdvance += TypeText;
        EventManager.AnimationEnd += ManageAnimations;

    }
    void ManageAnimations(string animationName)
    {
        Debug.Log($"{animationName} is triggered");
        switch (animationName)
        {
            case ANIM_INITIAL:
                obeliskAnimator.SetTrigger(ANIM_WRITINGS_APPEAR);
                break;
            case ANIM_WRITINGS_APPEAR:
                obeliskAnimator.SetTrigger(ANIM_WRITINGS_GLOW);
                TextBarAppear();
                EventManager.AnimationEnd -= ManageAnimations;
                break;
            default:
                break;
        }
    }
    public void TextBarAppear()
    {
        MoveTextBar();
        ScaleTextBar();
    }

    void MoveTextBar()
    {
        StartCoroutine(MoveTextBarCoroutine(storyTextBar, textBarTargetPosition, timeToMove));
    }

    void ScaleTextBar()
    {
        StartCoroutine(ScaleTextBarCoroutine(storyTextBar, Vector3.one, timeToScale));
    }

    IEnumerator MoveTextBarCoroutine(RectTransform textBarTransform, Vector3 targetPosition, float timeToMove)
    { Vector3 initialPosition;
        initialPosition = textBarTransform.anchoredPosition;
        bool reachedDestination = false;
        float elapsedTime = 0f;


        while (!reachedDestination)
        {
            if (Vector3.Distance(textBarTransform.anchoredPosition, targetPosition) < 0.01f)
            {
                textBarTransform.anchoredPosition = targetPosition;
                reachedDestination = true;
                break;
            }
            elapsedTime += Time.unscaledDeltaTime;
            float t = Mathf.Clamp(elapsedTime / timeToMove, 0f, 1f);
            t = t * t * t * (t * (t * 6 - 15) + 10);

               textBarTransform.anchoredPosition = Vector3.Lerp(initialPosition, targetPosition, t);

            yield return null;
        }

    }

    IEnumerator ScaleTextBarCoroutine(RectTransform textBarTransform, Vector3 targetScale, float timeToScale)
    {
        Vector3 initialScale;
        initialScale = textBarTransform.localScale;
        bool reachedScale = false;

        float elapsedTime = 0f;


        while (!reachedScale)
        {
            if (Vector3.Distance(textBarTransform.localScale, targetScale) < 0.01f)
            {
                textBarTransform.localScale = targetScale;
                reachedScale = true;
                break;
            }
            elapsedTime += Time.unscaledDeltaTime;
            float t = Mathf.Clamp(elapsedTime / timeToScale, 0f, 1f);
            t = t * t * t * (t * (t * 6 - 15) + 10);

            textBarTransform.localScale = Vector3.Lerp(initialScale, targetScale, t);

            yield return null;
        }


        yield return new WaitForSeconds(delay);

        if(StoryManager.Instance.currentParagraph!="")
        {
            TypeText(StoryManager.Instance.currentParagraph);
        }
        else
        {
            NextButtonClicked();
        }
        
    }
    void TypeText(string text) {
        lastTypeTextCoroutine=StartCoroutine(TypeTextCoroutine(text, storyText));
    }

    IEnumerator TypeTextCoroutine(string text, TextMeshProUGUI textbox)
    {
        if (text != null&& !isTyping)
        {
            isTyping = true;
            textbox.text = text;
            textbox.maxVisibleWords = 0;
            string[] words = text.Split(" ");
            for (int i=0; i<words.Length; i++)
            {
                textbox.maxVisibleWords++;
                yield return new WaitForSeconds(delay);
            }
            
        }
        isTyping = false;
    }

    public void NextButtonClicked()
    {
        if (isTyping)
        {
            StopCoroutine(lastTypeTextCoroutine);
            storyText.maxVisibleWords = MAX_VISIBLE_WORDS;
            isTyping = false;
        }
        else 
        {
            EventManager.OnNextButtonPressed();
        }
    }

    private void OnDisable()
    {
        EventManager.StoryAdvance -= TypeText;
        EventManager.AnimationEnd -= ManageAnimations;
    }
}
