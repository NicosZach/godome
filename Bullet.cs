using Godot;
using System;

public partial class Bullet : Area2D
{
	[Export] public float BulletSpeed { get; set; } = 1000f;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// Queue free after 1 second
		GD.Print("Bullet created");
		GetTree().CreateTimer(1.0f).Timeout += () => QueueFree();
		BodyEntered += OnCollision;
	}

	private void OnCollision(Node2D body)
	{
		switch (body)
		{
			case Player:
				GD.Print("Collided with player");
				break;
			default:
				QueueFree();
				GD.Print("Bullet ollided with other stuff");
				break;
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Position += Transform.X * BulletSpeed * (float) delta;
	}
}
