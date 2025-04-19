using System;
using System.Collections.Generic;


namespace ModUtilites;


static public class ObjectExtensions {
	/// <summary>
	/// Convert Object To Dictionary<KeyType, ValueType>. Add (Key: Value) IF object[N] type IS KeyType AND object[N + 1] type IS ValueType
	/// </summary>
	static public Dictionary<KeyType, ValueType> ToDictionary<KeyType, ValueType>(this object[] objects) {
		//TODO change loop object[] to get key n values to this method in [SmelterPatch.OnAwake, BeehivePatch.OnAwake]

		int i = 0;
		Dictionary<KeyType, ValueType> dictionary = new Dictionary<KeyType, ValueType>();

		goto FILLING;

		DICTIONARY_TRYADD:
			if (objects[i].GetType() != typeof(KeyType)) {i = i + 1; goto FILLING;}
			i = i + 1;
			if (i > objects.Length || i == objects.Length) {goto END_FILLING;}
			if (objects[i].GetType() != typeof(ValueType)) {goto FILLING;}

			KeyType key = (KeyType)objects[i - 1];
			if (key == null) { goto FILLING; }

			dictionary[key] = (ValueType)objects[i];
			i = i + 1;
			goto FILLING;

		FILLING:
			if (i > objects.Length || i == objects.Length) {goto END_FILLING;}
			goto DICTIONARY_TRYADD;

		END_FILLING:
			return dictionary;
	}
	/// <summary>
	/// Convert Object To Dictionary<KeyType, ValueType>. Add (Key: Value) IF object[N] type IS KeyType AND object[N + 1] type IS ValueType
	/// </summary>
	static public Dictionary<KeyType, ValueType> ToDictionary<KeyType, ValueType>(this object[] objects, List<Type> types) {
		//TODO change loop object[] to get key n values to this method in [SmelterPatch.OnAwake, BeehivePatch.OnAwake]

		int i = 0;
		Dictionary<KeyType, ValueType> dictionary = new Dictionary<KeyType, ValueType>();

		goto FILLING;

		ADD:
			KeyType key = (KeyType)objects[i - 1];
			if (key == null) { goto FILLING; }
			
			dictionary[key] = (ValueType)System.Convert.ChangeType(objects[i], typeof(ValueType));
			i = i + 1;
			goto FILLING;

		DICTIONARY_TRYADD:
			if (objects[i].GetType() != typeof(KeyType)) {i = i + 1; goto FILLING;}
			i = i + 1;
			if (i > objects.Length || i == objects.Length) {goto END_FILLING;}

			Type objType = objects[i].GetType();
			if (objType == typeof(ValueType)) {goto ADD;}
			foreach (Type type in types) {
				
				if (type == objType) {goto ADD;}
			}
			i = i + 1;
			goto FILLING;

		FILLING:
			if (i > objects.Length || i == objects.Length) {goto END_FILLING;}
			goto DICTIONARY_TRYADD;

		END_FILLING:
			return dictionary;
	}
	/// <summary>
	/// Convert Object To Dictionary<KeyType, ValueType>. Add (Key: Value) IF object[N] type IS KeyType AND object[N + 1] type IS ValueType
	/// </summary>
	static public Dictionary<KeyType, ValueType> ToDictionary<KeyType, ValueType>(this object[] objects, List<int> allowKeysHash) {

		int i = 0;
		Dictionary<KeyType, ValueType> dictionary = new Dictionary<KeyType, ValueType>();

		goto FILLING;

		DICTIONARY_TRYADD:
			if (objects[i].GetType() != typeof(KeyType)) {i = i + 1; goto FILLING;}
			i = i + 1;
			if (i > objects.Length || i == objects.Length) {goto END_FILLING;}
			if (objects[i].GetType() != typeof(ValueType)) {goto FILLING;}

			KeyType key = (KeyType)objects[i - 1];
			if (key == null) { goto FILLING; }

			int h_key = key.GetHashCode();
			for (int b = 0; b < allowKeysHash.Count; b = b + 1) {
				if (h_key == allowKeysHash[b]) {
					dictionary[key] = (ValueType)objects[i];
					allowKeysHash.RemoveAt(b);
					i = i + 1;
				}
			}
			goto FILLING;

		FILLING:
			if (i > objects.Length || i == objects.Length) {goto END_FILLING;}
			goto DICTIONARY_TRYADD;
		
		END_FILLING:
			return dictionary;
	}
	/// <summary>
	/// Convert Object To Dictionary<KeyType, ValueType>. Add (Key: Value) IF object[N] type IS KeyType AND object[N + 1] type IS ValueType
	/// </summary>
	static public Dictionary<KeyType, ValueType> ToDictionary<KeyType, ValueType>(this object[] objects, List<Type> types, List<int> allowKeysHash) where ValueType : IConvertible {

		int i = 0;
		int c = 0;
		Dictionary<KeyType, ValueType> dictionary = new Dictionary<KeyType, ValueType>();

		goto FILLING;

		ADD:
			KeyType key = (KeyType)objects[i - 1];
			if (key == null) {goto FILLING;}

			int h_key = key.GetHashCode();
			for (int b = 0; b < allowKeysHash.Count; b = b + 1) {
				if (h_key == allowKeysHash[b]) {
					dictionary[key] = (ValueType)System.Convert.ChangeType(objects[i], typeof(ValueType));
					allowKeysHash.RemoveAt(b);
					i = i + 1;
				}
			}
			goto FILLING;

		DICTIONARY_TRYADD:
			if (objects[i].GetType() != typeof(KeyType)) {i = i + 1; goto FILLING;}
			i = i + 1;
			if (i > objects.Length || i == objects.Length) {goto END_FILLING;}

			Type objType = objects[i].GetType();
			if (objType == typeof(ValueType)) {goto ADD;}
			for (c = 0; c < types.Count; c = c + 1) {
				if (types[c] == objType) {goto ADD;}
			}

			goto FILLING;

		FILLING:
			if (i > objects.Length || i == objects.Length) {goto END_FILLING;}
			goto DICTIONARY_TRYADD;
		
		END_FILLING:
			return dictionary;
	}
	/// <summary>
	/// Convert Object To Dictionary<KeyType, ValueType>. Add (Key: Value) IF object[N] type IS KeyType AND object[N + 1] type IS ValueType
	/// </summary>
	static public Dictionary<KeyType, ValueType> ToDictionary<KeyType, ValueType> (this object[] objects, Action<KeyType, ValueType> function, List<int> allowKeysHash) {

		int i = 0;
		Dictionary<KeyType, ValueType> dictionary = new Dictionary<KeyType, ValueType>();

		goto FILLING;

		DICTIONARY_TRYADD:
			if (objects[i].GetType() != typeof(KeyType)) {i = i + 1; goto FILLING;}
			i = i + 1;
			if (i > objects.Length || i == objects.Length) {goto END_FILLING;}
			if (objects[i].GetType() != typeof(ValueType)) {goto FILLING;}

			KeyType key = (KeyType)objects[i - 1];
			ValueType value = (ValueType)objects[i];
			if (key == null) { goto FILLING; }
			
			int h_key = key.GetHashCode();
			for (int b = 0; b < allowKeysHash.Count; b = b + 1) {
				if (h_key == allowKeysHash[b]) {
					dictionary[key] = value;
					allowKeysHash.RemoveAt(b);
					i = i + 1;
					if (function != null) {function.Invoke(key, value);}
				}
			}
			goto FILLING;

		FILLING:
			if (i > objects.Length || i == objects.Length) {goto END_FILLING;}
			goto DICTIONARY_TRYADD;
		
		END_FILLING:
			return dictionary;
	}
}

static public class ContainerExtensions {
	static public void setting(this Container container, object[] args) {

		string s_m_height = "m_height";
		string s_m_width = "m_width";

		int h_m_height = s_m_height.GetHashCode();
		int h_m_width = s_m_width.GetHashCode();

		Dictionary<string, long> dict = args.ToDictionary<string, long>(new List<Type>(){typeof(double)},
			new List<int>() {h_m_height, h_m_width});
		
		long _value;

		if (dict.TryGetValue(s_m_height, out _value)) {
			container.m_height = System.Convert.ToInt32(_value);
		}
		if (dict.TryGetValue(s_m_width, out _value)) {
			container.m_width = System.Convert.ToInt32(_value);
		}
	}
}