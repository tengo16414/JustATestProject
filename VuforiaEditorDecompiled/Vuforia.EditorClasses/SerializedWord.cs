using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Vuforia.EditorClasses
{
	public class SerializedWord : SerializedTrackable
	{
		private readonly SerializedProperty mMode;

		private readonly SerializedProperty mSpecificWord;

		public SerializedProperty ModeProperty
		{
			get
			{
				return this.mMode;
			}
		}

		public WordTemplateMode Mode
		{
			get
			{
				return (WordTemplateMode)this.mMode.get_enumValueIndex();
			}
			set
			{
				this.mMode.set_enumValueIndex((int)value);
			}
		}

		public SerializedProperty SpecificWordProperty
		{
			get
			{
				return this.mSpecificWord;
			}
		}

		public string SpecificWord
		{
			get
			{
				return this.mSpecificWord.get_stringValue();
			}
			set
			{
				this.mSpecificWord.set_stringValue(value);
			}
		}

		public bool IsTemplateMode
		{
			get
			{
				return this.Mode == WordTemplateMode.Template;
			}
		}

		public bool IsSpecificWordMode
		{
			get
			{
				return this.Mode == WordTemplateMode.SpecificWord;
			}
		}

		public SerializedWord(SerializedObject target) : base(target)
		{
			this.mMode = target.FindProperty("mMode");
			this.mSpecificWord = target.FindProperty("mSpecificWord");
		}

		public System.Collections.Generic.List<WordAbstractBehaviour> GetBehaviours()
		{
			System.Collections.Generic.List<WordAbstractBehaviour> list = new System.Collections.Generic.List<WordAbstractBehaviour>();
			UnityEngine.Object[] targetObjects = this.mSerializedObject.get_targetObjects();
			for (int i = 0; i < targetObjects.Length; i++)
			{
				UnityEngine.Object @object = targetObjects[i];
				list.Add((WordAbstractBehaviour)@object);
			}
			return list;
		}
	}
}
