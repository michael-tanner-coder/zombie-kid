using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class StringStringDictionary : SerializableDictionary<string, string> {}

[Serializable]
public class ObjectColorDictionary : SerializableDictionary<UnityEngine.Object, Color> {}

[Serializable]
public class ColorArrayStorage : SerializableDictionary.Storage<Color[]> {}

[Serializable]
public class StringColorArrayDictionary : SerializableDictionary<string, Color[], ColorArrayStorage> {}

[Serializable]
public class MyClass
{
    public int i;
    public string str;
}

[Serializable]
public class QuaternionMyClassDictionary : SerializableDictionary<Quaternion, MyClass> {}

[Serializable]
public class FloatGameObjectDictionary : SerializableDictionary<float, GameObject> {}

[Serializable]
public class IntGameObjectDictionary : SerializableDictionary<int, GameObject> {}

[Serializable]
public class IntEnemyParametersDictionary : SerializableDictionary<int, EnemyAttributes> {}

[Serializable]
public class SoundEffectDictionary : SerializableDictionary<string, SoundEffect> {}

[Serializable]
public class MusicDictionary : SerializableDictionary<string, MusicTrack> {}

[Serializable]
public class SpriteDictionary : SerializableDictionary<string, Sprite> {}

[Serializable]
public class IntStringDictionary : SerializableDictionary<int, string> {}

[Serializable]
public class DialogFlagDictionary : SerializableDictionary<string, DialogSequence> {}