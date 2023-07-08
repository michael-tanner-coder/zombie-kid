using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(StringStringDictionary))]
[CustomPropertyDrawer(typeof(ObjectColorDictionary))]
[CustomPropertyDrawer(typeof(StringColorArrayDictionary))]
[CustomPropertyDrawer(typeof(FloatGameObjectDictionary))]
[CustomPropertyDrawer(typeof(IntGameObjectDictionary))]
[CustomPropertyDrawer(typeof(IntEnemyParametersDictionary))]
[CustomPropertyDrawer(typeof(SoundEffectDictionary))]
[CustomPropertyDrawer(typeof(MusicDictionary))]
[CustomPropertyDrawer(typeof(SpriteDictionary))]
[CustomPropertyDrawer(typeof(IntStringDictionary))]
[CustomPropertyDrawer(typeof(ItemDropTableDictionary))]
[CustomPropertyDrawer(typeof(DialogFlagDictionary))]
public class AnySerializableDictionaryPropertyDrawer : SerializableDictionaryPropertyDrawer {}

[CustomPropertyDrawer(typeof(ColorArrayStorage))]
public class AnySerializableDictionaryStoragePropertyDrawer: SerializableDictionaryStoragePropertyDrawer {}
