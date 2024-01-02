using Godot;
using System;

public partial class menu : Control
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GetNode<Button>("StartGameBtn").GrabFocus();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	
	private void OnStartGameBtnPressed()
	{
		GetTree().ChangeSceneToFile("res://scenes/World.tscn");
	}
	
	private void OnQuitGameBtnPressed()
	{
		GetTree().Quit();
	}
}


