using UnityEngine;


using System;
using System.Collections.Generic;



namespace ModUtilites;


public interface IBiom {
	public string Id {get;}
	public string Effect {get;}
	public List<Texture2D> Textures {get;}
	public string GenerateType {get;}

	public void Regenerate ();
}

public class CreateBiom : IBiom {
	public string Id {get;}
	public string Effect {get;}
	public List<Texture2D> Textures {get;}
	public string GenerateType {get;}

	public void Regenerate () {}
}