using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueManager : MonoBehaviour
{
    //[Header("Dependencies")]
    //public DialogueUI dialogueUI;
    public static DialogueManager Instance;

    [Header("Action events")]
    public UnityEvent onConversationStarted;
    public UnityEvent onConversationEnded;

    public static event Action cutsceneEnded;

    private Queue<Sentence> sentences;


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        this.sentences = new Queue<Sentence>();
        //DialogueUI.Instance.InitializeAudioDictionary();
    }

    public void StartConversation(ConversationSO conversation)
    {
        if (this.sentences.Count != 0)
            return;

        foreach (var sentence in conversation.sentences)
        {
            this.sentences.Enqueue(sentence);
        }

        Debug.Log("El dialogo es :" + conversation.name + "y ademas");
        if(DialogueUI.Instance == null)
        {
            Debug.Log("Instance es null");
        }

        if (this.onConversationStarted != null)
            this.onConversationStarted.Invoke();

        DialogueUI.Instance.StartConversation(
            leftCharacterName: conversation.leftCharacter.fullname,
            leftCharacterPortrait: conversation.leftCharacter.portrait,
            rightCharacterName: conversation.rightCharacter.fullname,
            rightCharacterPortrait: conversation.rightCharacter.portrait
        );

        //if (this.onConversationStarted != null)
          //  this.onConversationStarted.Invoke();

        this.NextSentence();
    }

    public void NextSentence()
    {
        if (DialogueUI.Instance.IsSentenceInProcess())
        {
            DialogueUI.Instance.FinishDisplayingSentence();
            return;
        }

        if (this.sentences.Count == 0)
        {
            this.EndConversation();
            return;
        }

        var sentence = this.sentences.Dequeue();
        DialogueUI.Instance.DisplaySentence(
            characterName: sentence.character.fullname,
            sentenceText: sentence.text,
            audioID: sentence.character.audioID
        );
    }

    public void EndConversation()
    {
        DialogueUI.Instance.EndConversation();

        if (this.onConversationEnded != null)
            this.onConversationEnded.Invoke();

        //cutsceneEnded?.Invoke();
    }
}
