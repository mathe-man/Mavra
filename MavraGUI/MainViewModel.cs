using System;
using System.ComponentModel;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Threading;
using Avalonia;
using Avalonia.Threading;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Media;
using MavraGUI.Windows;
using MavraLib;

namespace MavraGUI;

public class MainViewModel : PropertyChanger
{
	
	private Universe _onScreen;
	public Universe OnScreen
	{
		get => _onScreen;
		set {
			if (Equals(value, _onScreen)) return;
			_onScreen = value;
			OnPropertyChanged();
			DrawUniverse(value);
		} 
	}  
	

	
	private Grid UniverseGrid;
	public MainViewModel(Grid universeGrid, BodyCreator bodyCreator)
	{
		_evolutiveUniverse = new (_seed);
		OnScreen = _seed;
		UniverseGrid = universeGrid;


		bodyCreator.BodyCreated += (sender, body) =>
		{
			AddBody(body);
		};
	}

	private DispatcherTimer? timer;
	
	private Universe _seed = new Universe(6.67408E-11f);
	private EvolutiveUniverse _evolutiveUniverse;
	private int index = 0;
	public void AddBody(Body body)
	{
		_seed.Bodies.Add(body);
		DrawUniverse(_seed);
		
		Console.WriteLine(_seed);
	}

	public bool MoveIndex(int amount)
	{
		var requested_index = index + amount;

		if (requested_index >= _evolutiveUniverse.Evolution.Count)
			return false;
		
		
		if (requested_index < 0)
			return false;
		
		index += amount;
		Console.WriteLine(index);
		DrawUniverse(_evolutiveUniverse.Evolution[index]);
		return true;
	}

	
	
	private int _evolutionToSimulate;
	public int EvolutionToSimulate
	{
		get => _evolutionToSimulate;
		set {
			if (Equals(value, _evolutionToSimulate)) return;
			_evolutionToSimulate = value;
			OnPropertyChanged();
		} 
	}  
	public void PreSimulate()
	{
		_evolutiveUniverse.ComputeEvolution(100000);
	}

	
	private int _animationSpeed;
	public int AnimationSpeed
	{
		get => _animationSpeed;
		set {
			if (Equals(value, _animationSpeed)) return;
			_animationSpeed = value;
			OnPropertyChanged();
		} 
	}  
	public void AnimateSimulation(int frame_milliseconds = 16) // 16 milliseconds for 60fps
	{
		if (timer == null || timer.IsEnabled)
			timer?.Stop();
		
		timer = new DispatcherTimer
		{
			Interval = TimeSpan.FromMilliseconds(frame_milliseconds)
		};
		timer.Tick += (_, _) =>
		{
			if (!MoveIndex(AnimationSpeed))
				timer.Stop();
			
			Console.WriteLine($"Frame: {DateTime.Now - lastAnimationFrameTime}");
			lastAnimationFrameTime = DateTime.Now;
		};
		
		timer.Start();
	}

	private static DateTime lastAnimationFrameTime;
	
	private void DrawUniverse(Universe universe)
	{
		if (UniverseGrid == null)
			return;
		
		UniverseGrid.Children.Clear();
		foreach (var body in universe.Bodies)
		{
			var circle = new Ellipse();
			circle.Height = body.Radius;
			circle.Width  = body.Radius;

			circle.Margin = new Thickness(body.Position.X, 0, 0, body.Position.Y);
			circle.Fill = new SolidColorBrush(new Color(255, body.Color[1], body.Color[2], body.Color[3]));
			
			UniverseGrid.Children.Add(circle);
		}
	}
}