using System.Collections;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.U2D;

public abstract class Character : MonoBehaviour
{
    [SerializeField] protected CharacterData CharData;
    private SpriteRenderer SpriteRen;
    protected TextManager TextMan;

    void Awake()
    {
        TextMan = FindAnyObjectByType<TextManager>();
        SpriteRen = GetComponent<SpriteRenderer>();
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
            Debug.Log(TextMan.GetRandomHelloQuote(CharData.characterTypeName));
        else
            Debug.Log(TextMan.GetRandomByeQuote(CharData.characterTypeName));

    }
}
