using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogLine
{
    [TextArea(15,20)]
    [SerializeField] private string _content;
    public string Content => _content;

    [SerializeField] private Sprite _image;
    public Sprite Image => _image;

    public void SetContent(string content)
    {
        _content = content;
    }

    public void SetImage(Sprite image)
    {
        _image = image;
    }
}
