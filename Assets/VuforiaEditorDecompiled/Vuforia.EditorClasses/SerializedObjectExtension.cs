using System;
using UnityEditor;
using UnityEngine;

namespace Vuforia.EditorClasses
{
	public static class SerializedObjectExtension
	{
		public class EditHandle : System.IDisposable
		{
			private readonly SerializedObject mSerializedObject;

			public EditHandle(SerializedObject serializedObject)
			{
				this.mSerializedObject = serializedObject;
				this.mSerializedObject.Update();
			}

			public void Dispose()
			{
				this.mSerializedObject.ApplyModifiedProperties();
			}
		}

		public static SerializedObjectExtension.EditHandle Edit(this SerializedObject objectToEdit)
		{
			return new SerializedObjectExtension.EditHandle(objectToEdit);
		}

		public static bool FixApproximatelyEqualFloatValues(this SerializedProperty property)
		{
			if (property.get_hasMultipleDifferentValues())
			{
				float floatValue = property.get_floatValue();
				UnityEngine.Object[] targetObjects = property.get_serializedObject().get_targetObjects();
				for (int i = 0; i < targetObjects.Length; i++)
				{
					float floatValue2 = new SerializedObject(targetObjects[i]).FindProperty(property.get_propertyPath()).get_floatValue();
					if (!Mathf.Approximately(floatValue, floatValue2))
					{
						return false;
					}
				}
				property.set_floatValue(floatValue);
			}
			return true;
		}

		public static void GetArrayItems(this SerializedProperty property, out string[] result)
		{
			SerializedProperty serializedProperty = property.Copy();
			int arraySizeAndAdvanceToFirstItem = SerializedObjectExtension.GetArraySizeAndAdvanceToFirstItem(serializedProperty);
			result = new string[arraySizeAndAdvanceToFirstItem];
			for (int i = 0; i < arraySizeAndAdvanceToFirstItem; i++)
			{
				serializedProperty.Next(false);
				result[i] = serializedProperty.get_stringValue();
			}
		}

		public static void RemoveArrayItem(this SerializedProperty property, string item)
		{
			SerializedProperty serializedProperty = property.Copy();
			int arraySizeAndAdvanceToFirstItem = SerializedObjectExtension.GetArraySizeAndAdvanceToFirstItem(serializedProperty);
			for (int i = 0; i < arraySizeAndAdvanceToFirstItem; i++)
			{
				serializedProperty.Next(false);
				if (serializedProperty.get_stringValue() == item)
				{
					property.DeleteArrayElementAtIndex(i);
					return;
				}
			}
		}

		public static void AddArrayItem(this SerializedProperty property, string item)
		{
			property.InsertArrayElementAtIndex(0);
			property.GetArrayElementAtIndex(0).set_stringValue(item);
		}

		private static int GetArraySizeAndAdvanceToFirstItem(SerializedProperty property)
		{
			if (!property.get_isArray())
			{
				Debug.LogError("Property " + property.get_name() + " is not an array");
				return 0;
			}
			property.Next(true);
			property.Next(true);
			return property.get_intValue();
		}
	}
}
