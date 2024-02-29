using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[Serializable]
public struct pl_dialogue
{
    public string dialogue;
    public float wait;
}

[Serializable]
public class playable_dialogue : PlayableBehaviour
{
    [SerializeField]
    public pl_dialogue[] dialogueText = new pl_dialogue[1];

    [SerializeField]
    public TextDialogue dialogueCharacter;

    [SerializeField]
    public Vector2 position;

    private TextDialogue m_textBubble;
    private double m_prevFrame = 0;


    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        base.ProcessFrame(playable, info, playerData);

        m_textBubble = playerData as TextDialogue;
        m_textBubble.ShowText();
        string entireSentence = "";
        Tuple<int, float> stoppingPoint = new Tuple<int, float>(-1, 0f);

        int counter = 0;
        if (dialogueText.Length > 0)
        {
            foreach (var a in dialogueText)
            {
                entireSentence += a.dialogue + " ";
            }
        }
        float delta = (float)(playable.GetDuration() / entireSentence.Length);
        for (int i = 0; i < entireSentence.Length; i++)
        {
            if ((i * delta) <= playable.GetTime())
            {
                counter++;
            }
        }
        if (dialogueText.Length > 1)
        {
            int dialogueLeng = 0;
            foreach (var a in dialogueText)
            {
                dialogueLeng += a.dialogue.Length + 1;
                if (counter == dialogueLeng)
                    if (a.wait > 0)
                        stoppingPoint = new Tuple<int, float>(dialogueLeng, a.wait);
            }
        }
        string dialogue = "";

        for (int i = 0; i < entireSentence.Length; i++)
        {
            if ((i * delta) <= playable.GetTime())
            {
                dialogue += entireSentence[i];
            }
        }
        m_textBubble.text.text = dialogue;
        if (!m_textBubble.directorPaused())
        {
            if (dialogueCharacter != null)
            {
                //m_textBubble.TypeVoice(dialogueCharacter.voicePitch, dialogueCharacter.voice);
            }
            else
            {
                m_textBubble.TypeVoice(1f, null);
            }
        }
        if (stoppingPoint.Item1 == counter)
        {
            if (((stoppingPoint.Item1 - 1) * delta) <= (float)(playable.GetTime()) && m_prevFrame < ((stoppingPoint.Item1 - 1) * delta))
            {
                if (Application.isPlaying)
                {
                    m_textBubble.PauseDialogueForTime(stoppingPoint.Item2);
                }
                if ((stoppingPoint.Item1 * delta) == playable.GetTime())
                {
                }
            }
        }
        if (counter == entireSentence.Length)
        {
            if (((entireSentence.Length - 1) * delta) <= (float)(playable.GetTime()) && m_prevFrame < ((entireSentence.Length - 1) * delta))
            {
                if (Application.isPlaying)
                {
                    m_textBubble.PauseDialogueForTime(0.5f);
                }
            }
        }
        m_prevFrame = playable.GetTime();
    }

    public override void OnBehaviourPause(Playable playable, FrameData info)
    {
        // m_index = 0;
        if (m_textBubble != null)
            m_textBubble.ClearText();
        base.OnBehaviourPause(playable, info);
        //m_textBubble.text.text = "";
    }
}
