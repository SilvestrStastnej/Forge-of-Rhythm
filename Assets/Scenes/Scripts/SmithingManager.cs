using NUnit.Framework.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SmithingManager : MonoBehaviour
{
    public static event Action SmithEnd;

    public Camera cam1;
    public Camera cam2;
    public HitCircle HCprefab;
    private Song SongInUse;
    private Character Chara;
    public AudioSource AuSource;
    public RectTransform SpawnArea;
    private double StartDspTime;
    private double SongPosition;
    private bool SongInSession = false;
    [SerializeField] private TextMeshProUGUI Vyhodnoceni;
    private List<Song.HitObjectData> Pouzite = new List<Song.HitObjectData>();

    private void OnEnable()
    {
        Controlz.SmithStart += SmithSeqStart;
    }

    private void OnDisable()
    {
        Controlz.SmithStart -= SmithSeqStart;
    }

    private void Update()
    {
        if (!SongInSession)
            return;

        SongPosition = AudioSettings.dspTime - StartDspTime;

        for (int i = 0; i < SongInUse.hitObjects.Count; i++)
        {
            double spawnTime = SongInUse.hitObjects[i].timeInSong;

            if (SongPosition >= spawnTime && !Pouzite.Contains(SongInUse.hitObjects[i]))
            {
                Pouzite.Add(SongInUse.hitObjects[i]);
                SpawnHC(SongInUse.hitObjects[i]);
            }
        }
        if (Pouzite.Count == SongInUse.hitObjects.Count && SongInSession)
        {
            SongInSession = false;
            StartCoroutine(Cakaj());
        }
    }

    IEnumerator Cakaj()
    {
        yield return new WaitForSeconds(1f);
        Vyhodnoceni.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        Pouzite.Clear();
        cam1.gameObject.SetActive(true);
        cam2.gameObject.SetActive(false);
        AuSource.Stop();
        SmithEnd?.Invoke();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Vyhodnoceni.gameObject.SetActive(false);
    }

    private void SmithSeqStart()
    {
        Chara = FindAnyObjectByType<Character>();

        var random = UnityEngine.Random.Range(0, Chara.CharData.item.songs.Count);
        SongInUse = Chara.CharData.item.songs[random];

        StartDspTime = AudioSettings.dspTime + 0.1;

        AuSource.clip = SongInUse.audioClip;
        AuSource.PlayScheduled(StartDspTime);
        StartDspTime = (float)AudioSettings.dspTime;
        SongInSession = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        cam1.gameObject.SetActive(false);
        cam2.gameObject.SetActive(true);
    }

    private void SpawnHC(Song.HitObjectData hit)
    {
        HitCircle newHC = Instantiate(HCprefab, SpawnArea);

        RectTransform rect = newHC.GetComponentInChildren<RectTransform>();

        rect.anchorMin = new Vector2(0.5f, 0.5f);
        rect.anchorMax = new Vector2(0.5f, 0.5f);

        rect.pivot = new Vector2(0.5f, 0.5f);

        rect.anchoredPosition = hit.position;
    }
}
