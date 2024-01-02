using Godot;
using System;

public partial class hud : Node
{
	private int coins = 0;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		UpdateScore();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	public void OnCoinCollected()
	{
		coins+=1;
		UpdateScore();
		if(coins == 3)
		{
			//GetTree().ChangeSceneToFile("res://scenes/" + ((int)GetTree().CurrentScene.Name + 1).toString() + ".tscn");
			GetTree().ChangeSceneToFile("res://scenes/World2.tscn");
		}
	}
	
	private void UpdateScore(){
		GetNode<Label>("CoinsCollected").Text = coins.ToString();
	}
	
	private void OnSpikeBodyEntered(Node2D body)
	{
		if(body.Name == "Player")
		{
			GD.Print(body.Name);
			GetTree().ReloadCurrentScene();
		}
	}
}



