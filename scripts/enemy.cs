using Godot;
using System;

public partial class enemy : CharacterBody2D
{
	private const int Speed = 30;
	private const int Gravity = 10;
	private bool isMovingLeft = true;

	private Vector2 Motion = new Vector2(0, 0);
	
	private Sprite2D sprite;
	private AnimationPlayer animationPlayer;
	
	[Signal]
	public delegate void CollisionEventHandler(int EnemyPosX);
	
	public override void _Ready()
	{
		sprite = GetNode<Sprite2D>("Sprite2D");
		animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		
		animationPlayer.Play("walk");
	}
	
	public override void _PhysicsProcess(double delta)
	{
		MoveCharacter();
		// Turn();
	}
	
	private void MoveCharacter()
	{
		Motion.Y += Gravity;
		if(isMovingLeft)
		{
			Motion.X = -Speed;
		}
		else
		{		
			Motion.X = Speed;
		}
		Velocity = Motion;
		MoveAndSlide();
	}
	
	private void Turn()
	{
		bool isColliding = GetNode<RayCast2D>("RayCast2D").IsColliding();
		if(!isColliding) 
		{
			isMovingLeft = !isMovingLeft;
			Scale = new Vector2(-Scale.X, Scale.Y);
		}
	}
	
	private void OnBodyEntered(Node2D body)
	{
		if (body.Name == "Player")
		{
			EmitSignal(SignalName.Collision, Position.X);
		}
	}
}


