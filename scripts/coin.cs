using Godot;
using System;

public partial class coin : Area2D
{
	[Signal]
	public delegate void CoinCollectedEventHandler();
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	private async void OnBodyEntered(Node2D body)
	{
		if(body.Name == "Player")
		{
			GetNode<AudioStreamPlayer>("AudioStreamPlayer").Playing = true;
			EmitSignal(SignalName.CoinCollected);
			await ToSignal(GetTree().CreateTimer(0.1), "timeout");
			QueueFree();
		} 
	}
	
}
