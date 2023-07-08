using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AttributeSet", menuName = "Attribute System/Attribute Set", order = 1)]
public class AttributeSet : ScriptableObject
{
    protected List<ModifiableAttribute> _attributes = new List<ModifiableAttribute>();
    public List<ModifiableAttribute> Attributes => _attributes;

    protected void OnValidate()
    {
        SetBaseValuesOnValidate();
        
        foreach(ModifiableAttribute attribute in _attributes)
        {
            attribute.CalculateValue();
        }
    }
    
    protected void InitAttributes()
    {
        foreach(ModifiableAttribute attribute in _attributes)
        {
            attribute.Awake();
            attribute.CalculateValue();
        }
    }

    public virtual void ApplyModifier(Modifier mod) {}

    public virtual void RemoveModifier(Modifier mod) {}

    public virtual void SetBaseValuesOnValidate() {}
}
