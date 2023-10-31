using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MindmanDialogue : MonoBehaviour
{
    [SerializeField] GameObject mindman, playerMindman;
    [SerializeField] GameObject questionParent, answerParent;
    [SerializeField] GameObject[] questions;
    [SerializeField] GameObject[] answers;
    [SerializeField] EventReference[] eventRefs;
    [SerializeField] EventReference tableThud;
    [SerializeField] TextMeshProUGUI[] questionText, answerText;
    [SerializeField] MindmanTrigger trigger;
    [SerializeField] Image screen;
    [SerializeField] Animator mindManAnimator, tableAnimator;
    public bool badEnd, screenOff, answersFinished;
    int selectIndex = 0;
    public bool interact, sitting;
    int randBubble = 0;
    bool inRoom = true;

    private void Start()
    {
        screen.color = new Color(screen.color.r, screen.color.g, screen.color.b, 0f);
        badEnd = BedroomUtility.BadEndingFinal();
    }

    public void PressInput(InputAction.CallbackContext context)
    {
        interact = context.performed;
        Debug.Log("Input");
    }

    private void Update()
    {
        if (inRoom && trigger.GetEntered())
        {
            StartCoroutine(StartDialogue());
            inRoom = false;
        }
        if (screenOff)
        {
            screen.color += new Color(screen.color.r, screen.color.g, screen.color.b, Mathf.MoveTowards(0, 255, Time.deltaTime * 0.25f));
        }
    }

    IEnumerator StartDialogue()
    {
        answerParent.SetActive(true);
        RuntimeManager.PlayOneShotAttached(eventRefs[0], mindman);
        randBubble = Random.Range(0, answers.Length);
        mindManAnimator.SetInteger("animationNum", 4);
        answerText[randBubble].text = "Come, have a seat.";
        yield return new WaitForSeconds(mindManAnimator.GetCurrentAnimatorClipInfo(0).Length);
        mindManAnimator.SetInteger("animationNum", 0);
        answers[randBubble].SetActive(true);
        yield return new WaitForSeconds(2f);
        yield return new WaitUntil(() => sitting);
        answers[randBubble].SetActive(false);
        answerParent.SetActive(false);
        PlayerDisable.ToggledDisabled();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        for (int i = 0; i < 3; i++)
        {
            if (i == 0)
            {
                questionText[0].text = "Who are you?";
                questionText[1].text = "What is all this?";
                questionText[2].text = "How do I wake up?";
            }
            else if (i == 1)
            {
                questionText[0].text = "What is the spider monster?";
                questionText[1].text = "What is the shadow monster?";
                questionText[2].text = "What are the robotic monsters?";
            }
            else if (i == 2)
            {
                questionText[0].text = "I am tired of this.";
                questionText[1].text = "I need to see what happens.";
                questionText[2].text = "I am sorry.";
            }
            questionParent.SetActive(true);
            questions[0].SetActive(true);
            questions[1].SetActive(true);
            questions[2].SetActive(true);
            EventSystem.current.SetSelectedGameObject(questions[0]);
            yield return new WaitUntil(() => interact);
            interact = false;
            answerParent.SetActive(false);
            questions[0].SetActive(false);
            questions[1].SetActive(false);
            questions[2].SetActive(false);
            questions[selectIndex].SetActive(true);
            yield return new WaitForSeconds(1f);
            questions[selectIndex].SetActive(false);
            questionParent.SetActive(false);
            answersFinished = false;
            StartCoroutine(PlayAnswer(selectIndex, i));
            yield return new WaitUntil(() => answersFinished);
        }
        yield return new WaitForSeconds(1f);
        if (!badEnd)
        {
            randBubble = Random.Range(0, answers.Length);
            answerParent.SetActive(true);
            answers[randBubble].SetActive(true);

            RuntimeManager.PlayOneShotAttached(eventRefs[0], mindman);
            mindManAnimator.SetInteger("animationNum", 4);
            answerText[randBubble].text = "I've grown weary from all of this.";
            yield return new WaitUntil(() => interact);
            interact = false;
            mindManAnimator.SetInteger("animationNum", 4);
            answers[randBubble].SetActive(false);
            randBubble = Random.Range(0, answers.Length);
            answers[randBubble].SetActive(true);
            answerText[randBubble].text = "And I think you understand what must be done.";
            yield return new WaitUntil(() => interact);
            interact = false;
            mindManAnimator.SetInteger("animationNum", 4);
            answers[randBubble].SetActive(false);
            randBubble = Random.Range(0, answers.Length);
            answers[randBubble].SetActive(true);
            answerText[randBubble].text = "It won't be easy, nothing ever is.";
            yield return new WaitUntil(() => interact);
            interact = false;
            mindManAnimator.SetInteger("animationNum", 4);
            answers[randBubble].SetActive(false);
            randBubble = Random.Range(0, answers.Length);
            answers[randBubble].SetActive(true);
            answerText[randBubble].text = "But it will be better.";
            yield return new WaitUntil(() => interact);
            interact = false;
            mindManAnimator.SetInteger("animationNum", 4);
            answers[randBubble].SetActive(false);
            randBubble = Random.Range(0, answers.Length);
            answers[randBubble].SetActive(true);
            answerText[randBubble].text = "Wake up.";
            RuntimeManager.PlayOneShotAttached(eventRefs[10], mindman);
            yield return new WaitUntil(() => interact);
            interact = false;
            answers[randBubble].SetActive(false);
            answerParent.SetActive(false);
            SceneManager.LoadScene("GoodEnding");
        }
        else if (badEnd)
        {
            answerParent.SetActive(true);
            randBubble = Random.Range(0, answers.Length);
            answers[randBubble].SetActive(true);

            RuntimeManager.PlayOneShotAttached(eventRefs[1], mindman);
            mindManAnimator.SetInteger("animationNum", 4);
            answerText[randBubble].text = "There's no need to continue talking.";
            yield return new WaitUntil(() => interact);
            interact = false;
            mindManAnimator.SetInteger("animationNum", 4);
            answers[randBubble].SetActive(false);
            randBubble = Random.Range(0, answers.Length);
            answers[randBubble].SetActive(true);
            answerText[randBubble].text = "I know you won't listen anyways.";
            yield return new WaitUntil(() => interact);
            interact = false;
            mindManAnimator.SetInteger("animationNum", 4);
            answers[randBubble].SetActive(false);
            randBubble = Random.Range(0, answers.Length);
            answers[randBubble].SetActive(true);
            answerText[randBubble].text = "You don't need to say a word.";
            yield return new WaitUntil(() => interact);
            interact = false;
            mindManAnimator.SetInteger("animationNum", 4);
            RuntimeManager.PlayOneShotAttached(eventRefs[9], mindman);
            answers[randBubble].SetActive(false);
            randBubble = Random.Range(0, answers.Length);
            answers[randBubble].SetActive(true);
            answerText[randBubble].text = "It's time to go back to sleep.";
            yield return new WaitUntil(() => interact);
            interact = false;
            answers[randBubble].SetActive(false);
            answerParent.SetActive(false);

            PlayerDisable.ToggledDisabled();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            playerMindman.SetActive(true);
            screenOff = true;
            yield return new WaitForSeconds(5f);
            SceneManager.LoadScene("BadEnding");
        }
    }

    IEnumerator PlayAnswer(int choice, int section)
    {
        answersFinished = false;
        if (section == 0)
        {
            if (choice == 0)
            {
                answerParent.SetActive(true);
                randBubble = Random.Range(0, answers.Length);
                answers[randBubble].SetActive(true);

                RuntimeManager.PlayOneShotAttached(eventRefs[1], mindman);
                mindManAnimator.SetInteger("animationNum", 4);
                answerText[randBubble].text = "What a strange question to ask, this is your dream after all.";
                yield return new WaitUntil(() => interact);
                interact = false;
                mindManAnimator.SetInteger("animationNum", 4);
                answers[randBubble].SetActive(false);
                randBubble = Random.Range(0, answers.Length);
                answers[randBubble].SetActive(true);
                answerText[randBubble].text = "You should know the answer to that already.";
                RuntimeManager.PlayOneShotAttached(eventRefs[1], mindman);
                yield return new WaitUntil(() => interact);
                interact = false;
                mindManAnimator.SetInteger("animationNum", 3);
                answers[randBubble].SetActive(false);
                randBubble = Random.Range(0, answers.Length);
                answers[randBubble].SetActive(true);
                answerText[randBubble].text = "I am a part of you as much as you are a part of me.";
                yield return new WaitUntil(() => interact);
                interact = false;
                answers[randBubble].SetActive(false);
                answerParent.SetActive(false);
            }
            else if (choice == 1)
            {
                answerParent.SetActive(true);
                randBubble = Random.Range(0, answers.Length);
                answers[randBubble].SetActive(true);

                RuntimeManager.PlayOneShotAttached(eventRefs[1], mindman);
                mindManAnimator.SetInteger("animationNum", 4);
                answerText[randBubble].text = "Nothing more than a figment of your imagination.";
                yield return new WaitUntil(() => interact);
                interact = false;
                mindManAnimator.SetInteger("animationNum", 4);
                answers[randBubble].SetActive(false);
                randBubble = Random.Range(0, answers.Length);
                answers[randBubble].SetActive(true);
                answerText[randBubble].text = "You're oddly fond of this dream, aren't you?";
                RuntimeManager.PlayOneShotAttached(eventRefs[2], mindman);
                yield return new WaitUntil(() => interact);
                interact = false;
                mindManAnimator.SetInteger("animationNum", 5);
                answers[randBubble].SetActive(false);
                randBubble = Random.Range(0, answers.Length);
                answers[randBubble].SetActive(true);
                answerText[randBubble].text = "You keep going back here, hoping for a happy outcome.";
                yield return new WaitUntil(() => interact);
                interact = false;
                mindManAnimator.SetInteger("animationNum", 6);
                answers[randBubble].SetActive(false);
                randBubble = Random.Range(0, answers.Length);
                answers[randBubble].SetActive(true);
                answerText[randBubble].text = "Hoping your next dream will be a pleasant one.";
                yield return new WaitUntil(() => interact);
                interact = false;
                answers[randBubble].SetActive(false);
                answerParent.SetActive(false);
            }
            else if (choice == 2)
            {
                answerParent.SetActive(true);
                randBubble = Random.Range(0, answers.Length);
                answers[randBubble].SetActive(true);

                RuntimeManager.PlayOneShotAttached(eventRefs[2], mindman);
                mindManAnimator.SetInteger("animationNum", 4);
                answerText[randBubble].text = "You don't want to wake up do you?";
                yield return new WaitUntil(() => interact);
                interact = false;
                mindManAnimator.SetInteger("animationNum", 2);
                answers[randBubble].SetActive(false);
                randBubble = Random.Range(0, answers.Length);
                answers[randBubble].SetActive(true);
                answerText[randBubble].text = "I have seen you do this night after night after night.";
                yield return new WaitUntil(() => interact);
                interact = false;
                mindManAnimator.SetInteger("animationNum", 4);
                RuntimeManager.PlayOneShotAttached(eventRefs[4], mindman);
                answers[randBubble].SetActive(false);
                randBubble = Random.Range(0, answers.Length);
                answers[randBubble].SetActive(true);
                answerText[randBubble].text = "Each night you choose to dream, hoping they'll be better than the day.";
                yield return new WaitUntil(() => interact);
                interact = false;
                mindManAnimator.SetInteger("animationNum", 4);
                answers[randBubble].SetActive(false);
                randBubble = Random.Range(0, answers.Length);
                answers[randBubble].SetActive(true);
                answerText[randBubble].text = "And now you have the courage to ask to wake up, now that you don't like the result?";
                yield return new WaitUntil(() => interact);
                interact = false;
                mindManAnimator.SetInteger("animationNum", 4);
                answers[randBubble].SetActive(false);
                randBubble = Random.Range(0, answers.Length);
                answers[randBubble].SetActive(true);
                answerText[randBubble].text = "This is your own doing. You will accept the concequences of your actions.";
                yield return new WaitUntil(() => interact);
                interact = false;
                answers[randBubble].SetActive(false);
                answerParent.SetActive(false);
            }
        }
        else if (section == 1)
        {
            if (choice == 0)
            {
                answerParent.SetActive(true);
                randBubble = Random.Range(0, answers.Length);
                answers[randBubble].SetActive(true);

                RuntimeManager.PlayOneShotAttached(eventRefs[2], mindman);
                mindManAnimator.SetInteger("animationNum", 3);
                answerText[randBubble].text = "You were in their territory, but you fear their gaze as they fear yours.";
                yield return new WaitUntil(() => interact);
                interact = false;
                mindManAnimator.SetInteger("animationNum", 3);
                answers[randBubble].SetActive(false);
                randBubble = Random.Range(0, answers.Length);
                answers[randBubble].SetActive(true);
                answerText[randBubble].text = "You only get through the day by keeping them at a distance.";
                yield return new WaitUntil(() => interact);
                interact = false;
                mindManAnimator.SetInteger("animationNum", 3);
                RuntimeManager.PlayOneShotAttached(eventRefs[4], mindman);
                answers[randBubble].SetActive(false);
                randBubble = Random.Range(0, answers.Length);
                answers[randBubble].SetActive(true);
                answerText[randBubble].text = "Their creation is a fault of your own perception.";
                yield return new WaitUntil(() => interact);
                interact = false;
                mindManAnimator.SetInteger("animationNum", 3);
                answers[randBubble].SetActive(false);
                randBubble = Random.Range(0, answers.Length);
                answers[randBubble].SetActive(true);
                answerText[randBubble].text = "It's your own unwillingness to let them in.";
                yield return new WaitUntil(() => interact);
                interact = false;
                answers[randBubble].SetActive(false);
                answerParent.SetActive(false);
            }
            else if (choice == 1)
            {
                answerParent.SetActive(true);
                randBubble = Random.Range(0, answers.Length);
                answers[randBubble].SetActive(true);

                RuntimeManager.PlayOneShotAttached(eventRefs[5], mindman);
                mindManAnimator.SetInteger("animationNum", 4);
                answerText[randBubble].text = "Is he what you fear? Or is it the dolls?";
                yield return new WaitUntil(() => interact);
                interact = false;
                mindManAnimator.SetInteger("animationNum", 4);
                answers[randBubble].SetActive(false);
                randBubble = Random.Range(0, answers.Length);
                answers[randBubble].SetActive(true);
                answerText[randBubble].text = "Giggling children, the lost wandering of childhood.";
                yield return new WaitUntil(() => interact);
                interact = false;
                mindManAnimator.SetInteger("animationNum", 5);
                answers[randBubble].SetActive(false);
                randBubble = Random.Range(0, answers.Length);
                answers[randBubble].SetActive(true);
                answerText[randBubble].text = "Finding your way to the end of the maze, but still dissatisfied with the payoff.";
                yield return new WaitUntil(() => interact);
                interact = false;
                mindManAnimator.SetInteger("animationNum", 5);
                RuntimeManager.PlayOneShotAttached(eventRefs[4], mindman);
                answers[randBubble].SetActive(false);
                randBubble = Random.Range(0, answers.Length);
                answers[randBubble].SetActive(true);
                answerText[randBubble].text = "It's a pitiful outlook you have.";
                yield return new WaitUntil(() => interact);
                interact = false;
                answers[randBubble].SetActive(false);
                answerParent.SetActive(false);
            }
            else if (choice == 2)
            {
                answerParent.SetActive(true);
                randBubble = Random.Range(0, answers.Length);
                answers[randBubble].SetActive(true);

                RuntimeManager.PlayOneShotAttached(eventRefs[0], mindman);
                mindManAnimator.SetInteger("animationNum", 4);
                answerText[randBubble].text = "You know very well what they are.";
                yield return new WaitUntil(() => interact);
                interact = false;
                mindManAnimator.SetInteger("animationNum", 1);
                tableAnimator.SetBool("shake", true);
                yield return new WaitForSeconds(0.25f);
                RuntimeManager.PlayOneShotAttached(tableThud, mindman);
                answers[randBubble].SetActive(false);
                RuntimeManager.PlayOneShotAttached(eventRefs[6], mindman);
                yield return new WaitForSeconds(0.5f);
                tableAnimator.SetBool("shake", false);
                randBubble = Random.Range(0, answers.Length);
                answers[randBubble].SetActive(true);
                answerText[randBubble].text = "After such a vile fight for survival, you dare play victim?";
                yield return new WaitUntil(() => interact);
                interact = false;
                mindManAnimator.SetInteger("animationNum", 11);
                answers[randBubble].SetActive(false);
                randBubble = Random.Range(0, answers.Length);
                answers[randBubble].SetActive(true);
                answerText[randBubble].text = "You would tear them piece by piece to put yourself back together.";
                yield return new WaitUntil(() => interact);
                interact = false;
                mindManAnimator.SetInteger("animationNum", 11);
                answers[randBubble].SetActive(false);
                randBubble = Random.Range(0, answers.Length);
                answers[randBubble].SetActive(true);
                answerText[randBubble].text = "And yet you call them monsters for trying to do the same.";
                yield return new WaitUntil(() => interact);
                interact = false;
                answers[randBubble].SetActive(false);
                answerParent.SetActive(false);
            }
        }
        else if (section == 2)
        {
            if (choice == 0)
            {
                answerParent.SetActive(true);
                randBubble = Random.Range(0, answers.Length);
                answers[randBubble].SetActive(true);

                RuntimeManager.PlayOneShotAttached(eventRefs[0], mindman);
                mindManAnimator.SetInteger("animationNum", 4);
                answerText[randBubble].text = "As am I.";
                yield return new WaitUntil(() => interact);
                interact = false;
                mindManAnimator.SetInteger("animationNum", 4);
                answers[randBubble].SetActive(false);
                randBubble = Random.Range(0, answers.Length);
                answers[randBubble].SetActive(true);
                answerText[randBubble].text = "We're only repeating ourselves aren't we?";
                yield return new WaitUntil(() => interact);
                interact = false;
                mindManAnimator.SetInteger("animationNum", 4);
                RuntimeManager.PlayOneShotAttached(eventRefs[2], mindman);
                answers[randBubble].SetActive(false);
                randBubble = Random.Range(0, answers.Length);
                answers[randBubble].SetActive(true);
                answerText[randBubble].text = "Trying the same things over and over again, hoping for a new result.";
                yield return new WaitUntil(() => interact);
                interact = false;
                mindManAnimator.SetInteger("animationNum", 4);
                answers[randBubble].SetActive(false);
                randBubble = Random.Range(0, answers.Length);
                answers[randBubble].SetActive(true);
                answerText[randBubble].text = "It's almost like a part of you doesn't want to improve.";
                yield return new WaitUntil(() => interact);
                interact = false;
                mindManAnimator.SetInteger("animationNum", 4);
                answers[randBubble].SetActive(false);
                randBubble = Random.Range(0, answers.Length);
                answers[randBubble].SetActive(true);
                answerText[randBubble].text = "Do you fear what you'll be without me?";
                yield return new WaitUntil(() => interact);
                interact = false;
                answers[randBubble].SetActive(false);
                answerParent.SetActive(true);
            }
            else if (choice == 1)
            {
                answerParent.SetActive(true);

                mindManAnimator.SetInteger("animationNum", 1);
                tableAnimator.SetBool("shake", true);
                yield return new WaitForSeconds(0.25f);
                RuntimeManager.PlayOneShotAttached(tableThud, mindman);
                yield return new WaitForSeconds(0.5f);
                tableAnimator.SetBool("shake", false);
                randBubble = Random.Range(0, answers.Length);
                answers[randBubble].SetActive(true);
                answerText[randBubble].text = "You know what happens!";
                RuntimeManager.PlayOneShotAttached(eventRefs[6], mindman);
                yield return new WaitUntil(() => interact);
                interact = false;
                mindManAnimator.SetInteger("animationNum", 11);
                answers[randBubble].SetActive(false);
                randBubble = Random.Range(0, answers.Length);
                answers[randBubble].SetActive(true);
                answerText[randBubble].text = "You just refuse to accept it.";
                yield return new WaitUntil(() => interact);
                interact = false;
                mindManAnimator.SetInteger("animationNum", 11);
                answers[randBubble].SetActive(false);
                randBubble = Random.Range(0, answers.Length);
                answers[randBubble].SetActive(true);
                answerText[randBubble].text = "You're wallowing in your own self pity, refusing to move forward from it.";
                yield return new WaitUntil(() => interact);
                interact = false;
                mindManAnimator.SetInteger("animationNum", 11);
                answers[randBubble].SetActive(false);
                randBubble = Random.Range(0, answers.Length);
                answers[randBubble].SetActive(true);
                answerText[randBubble].text = "And yet you blame anything but yourself for what comes next.";
                yield return new WaitUntil(() => interact);
                interact = false;
                mindManAnimator.SetInteger("animationNum", 4);
                RuntimeManager.PlayOneShotAttached(eventRefs[4], mindman);
                answers[randBubble].SetActive(false);
                randBubble = Random.Range(0, answers.Length);
                answers[randBubble].SetActive(true);
                answerText[randBubble].text = "You are a victim by choice. The past is already behind you.";
                yield return new WaitUntil(() => interact);
                interact = false;
                mindManAnimator.SetInteger("animationNum", 4);
                answers[randBubble].SetActive(false);
                randBubble = Random.Range(0, answers.Length);
                answers[randBubble].SetActive(true);
                answerText[randBubble].text = "You just have to move forward from it.";
                yield return new WaitUntil(() => interact);
                interact = false;
                answers[randBubble].SetActive(false);
                answerParent.SetActive(true);
            }
            else if (choice == 2)
            {
                answerParent.SetActive(true);
                randBubble = Random.Range(0, answers.Length);
                answers[randBubble].SetActive(true);

                RuntimeManager.PlayOneShotAttached(eventRefs[7], mindman);
                mindManAnimator.SetInteger("animationNum", 5);
                answerText[randBubble].text = "I forgive you.";
                yield return new WaitUntil(() => interact);
                interact = false;
                mindManAnimator.SetInteger("animationNum", 6);
                answers[randBubble].SetActive(false);
                randBubble = Random.Range(0, answers.Length);
                answers[randBubble].SetActive(true);
                answerText[randBubble].text = "And I do not.";
                RuntimeManager.PlayOneShotAttached(eventRefs[8], mindman);
                yield return new WaitUntil(() => interact);
                interact = false;
                mindManAnimator.SetInteger("animationNum", 16);
                answers[randBubble].SetActive(false);
                randBubble = Random.Range(0, answers.Length);
                answers[randBubble].SetActive(true);
                answerText[randBubble].text = "You do not need forgiveness.";
                yield return new WaitUntil(() => interact);
                interact = false;
                mindManAnimator.SetInteger("animationNum", 16);
                answers[randBubble].SetActive(false);
                randBubble = Random.Range(0, answers.Length);
                answers[randBubble].SetActive(true);
                answerText[randBubble].text = "Closure will never come. You must make it for yourself.";
                yield return new WaitUntil(() => interact);
                interact = false;
                answers[randBubble].SetActive(false);
                answerParent.SetActive(true);
            }
        }
        answersFinished = true;
    }

    public void SetInput(int i)
    {
        interact = true;
        selectIndex = i;
    }

}
