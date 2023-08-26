using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System;
using Utill.SeralizableDictionary;
using HitBox;
#if UNITY_EDITOR
using UnityEditor;

	[CustomPropertyDrawer(typeof(StringListHitBoxData))]
public class HitboxPropertyDrawer : SerializableDictionaryPropertyDrawer
{
}
#endif