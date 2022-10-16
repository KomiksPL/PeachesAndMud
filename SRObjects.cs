using System.Collections.Generic;
using System.Linq;
using UnhollowerRuntimeLib;

namespace PAM;

public static class SRObjects
{
	
	public static T Get<T>(string name) where T : Object
	{
		return Resources.FindObjectsOfTypeAll<T>().FirstOrDefault(found => found.name.Equals(name));
	}

	public static Object Get(string name, System.Type type)
	{
		return Resources.FindObjectsOfTypeAll(Il2CppType.From(type)).FirstOrDefault(found => found.name.Equals(name));
	}
	public static List<T> GetAll<T>() where T : Object
	{
		return new List<T>(Resources.FindObjectsOfTypeAll<T>());
	}
	public static T GetInst<T>(string name) where T : Object
	{
			foreach (T found in Resources.FindObjectsOfTypeAll<T>())
			{
				if (!found.name.Equals(name)) continue;
				var instantiate = Object.Instantiate(found);
				instantiate.hideFlags |= HideFlags.HideAndDontSave;
				return instantiate;
			}

			return null;
		}

		
		public static Object GetInst(string name, System.Type type)
		{
			return (from found in Resources.FindObjectsOfTypeAll(Il2CppType.From(type)) where found.name.Equals(name) select Object.Instantiate(found)).FirstOrDefault();
		}

		public static T GetWorld<T>(string name) where T : Object
		{
			return Object.FindObjectsOfType<T>().FirstOrDefault(found => found.name.Equals(name));
		}

	
		public static Object GetWorld(string name, System.Type type)
		{
			
			return Object.FindObjectsOfType( Il2CppType.From(type)).FirstOrDefault(found => found.name.Equals(name));
		}

		public static List<T> GetAllWorld<T>() where T : Object
		{
			return new List<T>(Object.FindObjectsOfType<T>());
		}
	}
