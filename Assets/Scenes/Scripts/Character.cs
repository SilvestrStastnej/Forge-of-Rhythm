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
    public TextMeshProUGUI text;
    protected TextManager TextMan;

    void Awake()
    {
        TextMan = FindAnyObjectByType<TextManager>();
        SpriteRen = GetComponent<SpriteRenderer>();
        text = FindAnyObjectByType<TextMeshProUGUI>();
        SpriteRen.sprite = CharData.model2D;
    }

    IEnumerator SpeakAndLeap()
    {
        text.text = TextMan.GetRandomByeQuote(CharData.characterTypeName);
        yield return new WaitForSeconds(2f);
        text.text = "";
    }

    private void MeziCas()
    {
        text.text = "Čekám na dokončení mé objednávky.\n1.\tPředat předmět a dokončit objednávku.";
    }

    private void OnEnable()
    {
        CharManager.OnStart += Speak;
        CharManager.OnEnd += Speak;
        OrderManager.OnOrderStart += MeziCas;
    }

    private void OnDisable()
    {
        CharManager.OnStart -= Speak;
        CharManager.OnEnd -= Speak;
        OrderManager.OnOrderStart -= MeziCas;
    }

    protected void Speak(bool Start)
    {
        if (Start)
        {
            text.text = TextMan.GetRandomHelloQuote(CharData.characterTypeName);
            text.text += "\n1.\tprijmout\n2.\todmitnout";
        }
        else
            StartCoroutine(SpeakAndLeap());
    }
}
