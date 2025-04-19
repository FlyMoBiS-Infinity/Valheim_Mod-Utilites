using UnityEngine;


namespace ModUtilites;


public interface ILife
{
	public string Id {get;}
	public string Name {get;}
	public byte Rank {get;}
	public float Health {get;}
	public string[] DropItems {get;}
	public AssetBundle Model {get;}
	public void Create ();
	public void AddToBioms(IBiom[] biom);
}

public class Life : ILife {
	public string Id {get;}
	public string Name {get;}
	public byte Rank {get;}
	public float Health {get;}
	public string[] DropItems {get;}
	public AssetBundle Model {get;}

	public void Create() {
		Model.LoadAsset("main");
	}
	public void AddToBioms(IBiom[] bioms) {

	}

	public Life(string id, string name, float health, byte rank, string[] dropItems, AssetBundle model) {
		this.Id = id; this.Name = name; this.Rank = rank; this.Health = health; this.DropItems = dropItems; this.Model = model;
	}
}