using Godot;
using System;

public partial class saw : Node2D
{
	[Signal]
	public delegate void SawCollisionEventHandler();
	
	public override void _Ready()
	{
		GetNode<AnimationPlayer>("AnimationPlayer").Play("saw");
	}
	
	private void OnSawEntered(Node2D body)
	{
		if (body.Name == "Player")
		{
			EmitSignal(SignalName.SawCollision);
		}
	}
}

