using System;
using System.Numerics;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Interactivity;
using Avalonia.Media;
using MavraLib;

namespace MavraGUI;

public partial class MainWindow : Window
{
	private Universe seed = new Universe(6.67408E-11f);
	private EvolutiveUniverse _evolutiveUniverse;
	public MainWindow()
	{
		InitializeComponent();
		DataContext = new MainViewModel();
	}

	private void BtnAddBody_OnClick(object? sender, RoutedEventArgs e)
	{
		/*
		float mass;
		float x, y;
		try
		{
			mass = Convert.ToSingle(tbMassInput.Text);
			x = Convert.ToSingle(tbXPos.Text);
			y = Convert.ToSingle(tbYPos.Text);
		}
		catch (FormatException er)
		{
			tbErrorIndicator.Text = "One or more values are incorrects.";
			return;
		}
	
		var b = new Body(mass, new Vector2(x ,y), radius:75);
		seed.Bodies.Add(b);

		var body_circle = new Ellipse();
		body_circle.Margin = new Thickness(x, 0, 0, y);
		body_circle.Width  = b.Radius;
		body_circle.Height = b.Radius;
		body_circle.Fill   = new SolidColorBrush(Colors.Red);
		
		Layout.Children.Add(body_circle);
		
		tbErrorIndicator.Text = mass.ToString();*/
	}
}