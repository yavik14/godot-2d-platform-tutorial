using Godot;
using System;

public partial class hud : Node
{
	private int coins = 0;

	private Sprite2D Heart1;
	private Sprite2D Heart2;
	private Sprite2D Heart3;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Heart1 = GetNode<Sprite2D>("Heart1");

		Heart2 = GetNode<Sprite2D>("Heart2");
		Heart3 = GetNode<Sprite2D>("Heart3");

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
	
	private void OnPlayerLife(long Lives)
	{
		if(Lives == 0)
		{
			Heart1.Visible = false;
		}

		if(Lives == 1)
		{
			Heart2.Visible = false;
		}

		if(Lives == 2)
		{
			Heart3.Visible = false;
		}
	}
}

