using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class KeyboardSettings{

	public enum KeyName
	{
		MoveForward,
		MoveBackward,
		MoveLeft,
		MoveRight
	}

	private static Dictionary<KeyName, KeyCode> mappedKeys;

	private static Dictionary<KeyName, KeyCode> mappedKeysStandard;

	// Use this for initialization
	static void Init () {
		mappedKeys = new Dictionary<KeyName, KeyCode>();
		mappedKeysStandard = new Dictionary<KeyName, KeyCode>();
		initStandard ();
	}
	
	public static KeyCode getMappedKey(KeyName name)
	{
		if (mappedKeys == null)
						Init ();
		KeyCode keyCode;
		mappedKeys.TryGetValue(name, out keyCode);
		return keyCode;
	}

	public static void setMappedKey(KeyName name, KeyCode keyCode)
	{
		if (mappedKeys == null)
			Init ();
		if (mappedKeys.ContainsKey (name))
		{
			mappedKeys.Remove(name);
		}
		mappedKeys.Add (name, keyCode);
	}

	public static void resetKeys()
	{
		if (mappedKeys == null)
			Init ();
		mappedKeys = mappedKeysStandard;
	}

	private static void initStandard()
	{
		mappedKeysStandard.Add (KeyName.MoveForward, KeyCode.W);
		mappedKeysStandard.Add (KeyName.MoveBackward, KeyCode.S);
		mappedKeysStandard.Add (KeyName.MoveLeft, KeyCode.A);
		mappedKeysStandard.Add (KeyName.MoveRight, KeyCode.D);
	}
}
