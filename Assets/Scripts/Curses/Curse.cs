using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjectArchitecture;

public class Curse : MonoBehaviour, IInteractable
{
    [SerializeField] List<CurseData> _allCurses = new List<CurseData>();
    [SerializeField] private CurseData _curseData;
    public CurseData Parameters => _curseData;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] public bool RandomCurse;

    public string Prompt {get; set; }
    public bool InteractionEnabled {get; set; }

    void Awake()
    {
        // set interaface members
        InteractionEnabled = true;
        Prompt = "E: Take the curse";

        // get necesssary components
        _renderer = gameObject.GetComponent<SpriteRenderer>();

        // 
        if (RandomCurse)
        {
            _curseData = _allCurses[Random.Range(0, _allCurses.Count)];
        }

        // apply parameters to individual components
        SetAttributes(_curseData);
    }

    public void SetAttributes(CurseData parameters)
    {
        _curseData = parameters;
        _renderer.sprite = _curseData.Image;
    }

    public void ActivateEffects(PlayerCurseStore curseStore)
    {
        if (curseStore != null)
        {
            curseStore.AddCurse(_curseData);
        }
    }

    public void ReceiveInteraction(GameObject interactor)
    {
        PlayerCurseStore curseStore = interactor.GetComponent<PlayerCurseStore>();
        ActivateEffects(curseStore);
        Destroy(gameObject);
    }
}
