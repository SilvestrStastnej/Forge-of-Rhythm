using System.Collections;
using System.Net.NetworkInformation;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.U2D;

public abstract class Character : MonoBehaviour
{
    [SerializeField] public CharacterData CharData;
    private SpriteRenderer SpriteRen;
    private TextMeshProUGUI text;
    protected TextManager TextMan;

    void Awake()
    {
        TextMan = FindAnyObjectByType<TextManager>();
        SpriteRen = GetComponent<SpriteRenderer>();
        text = GetComponentInChildren<TextMeshProUGUI>();
        SpriteRen.sprite = CharData.model2D;
    }

    private void OnEnable()
    {
        CharManager.OnStart += Speak;
        CharManager.OnEnd += Speak;
    }

    private void OnDisable()
    {
        CharManager.OnStart -= Speak;
        CharManager.OnEnd -= Speak;
    }

    protected void Speak(bool Start)
    {
        if (Start)
            text.text = TextMan.GetRandomHelloQuote(CharData.characterTypeName);
        else
            text.text = TextMan.GetRandomByeQuote(CharData.characterTypeName);
        text.text += "\n1.\tprijmout\n2.\todmitnout";

    }
}
