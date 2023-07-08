using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CurseList", menuName = "Curses/Curse List", order = 1)]
public class CurseList : ScriptableObject
{
    [Tooltip("The curses included in this list")]
    [SerializeField] private List<CurseData> _elements = new List<CurseData>();
    public List<CurseData> Elements => _elements; 

    [Tooltip("The maximum number of elements allowed in this list")]
    [SerializeField] int _maxLength = 0;
    public int MaxLength => _maxLength;

    public delegate void AddCurse(CurseData curse);
    public event AddCurse OnAddCurse;


    public void Add(CurseData curse)
    {
        if (_elements.Count < _maxLength && !_elements.Contains(curse))
        {
            _elements.Add(curse);
            OnAddCurse(curse);
        }
    }

    public void Remove(CurseData curse)
    {
        if (_elements.Count > 0 && _elements.Contains(curse))
        {
            _elements.Remove(curse);
        }
    }

    public void Clear()
    {
        _elements.Clear();
    }
}
