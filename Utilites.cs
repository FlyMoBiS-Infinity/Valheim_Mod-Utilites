//using ;

using UnityEngine;

using System.Collections.Generic;


namespace ModUtilites;


static public class Components {
	static public void AddComponent(GameObject gameObject, string ComponentTypeName, params object[] args) {
		switch (ComponentTypeName.ToLower()) {
			case ("container"):
				gameObject.AddComponent<Container>().setting(args);
				break;
		}
	}
}