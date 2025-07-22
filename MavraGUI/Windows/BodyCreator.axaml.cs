using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media;
using MavraGUI.Controls;
using MavraLib;

namespace MavraGUI.Windows;

public partial class BodyCreator : UserControl
{

	#region Fields

		#region Infos

			public static readonly StyledProperty<string> BodyNameProperty = AvaloniaProperty.Register<BodyCreator, string>(
					nameof(BodyName), "", defaultBindingMode: BindingMode.TwoWay);
			public string BodyName
			{
				get => GetValue(BodyNameProperty);
				set => SetValue(BodyNameProperty, value);
			}


			#region Color

				public static readonly StyledProperty<float> RProperty = AvaloniaProperty.Register<BodyCreator, float>(
					nameof(R), 1, defaultBindingMode: BindingMode.TwoWay);
				public float R
				{
					get => GetValue(RProperty);
					set => SetValue(RProperty, value);
				}


				public static readonly StyledProperty<float> GProperty = AvaloniaProperty.Register<BodyCreator, float>(
					nameof(G), 1, defaultBindingMode: BindingMode.TwoWay);
				public float G
				{
					get => GetValue(GProperty);
					set => SetValue(GProperty, value);
				}

				public static readonly StyledProperty<float> BProperty = AvaloniaProperty.Register<BodyCreator, float>(
					nameof(B),  1, defaultBindingMode: BindingMode.TwoWay);
				public float B
				{
					get => GetValue(BProperty);
					set => SetValue(BProperty, value);
				}
			
			#endregion

				
			public static readonly StyledProperty<float> MassProperty = AvaloniaProperty.Register<BodyCreator, float>(
				nameof(Mass), 10, defaultBindingMode:BindingMode.TwoWay);
			public float Mass
			{
				get => GetValue(MassProperty);
				set => SetValue(MassProperty, value);
			}

			
			public static readonly StyledProperty<float> RadiusProperty = AvaloniaProperty.Register<BodyCreator, float>(
				nameof(Radius), 10, defaultBindingMode:BindingMode.TwoWay);
			public float Radius
			{
				get => GetValue(RadiusProperty);
				set => SetValue(RadiusProperty, value);
			}

		#endregion

		#region Position

			public static readonly StyledProperty<float> XProperty = AvaloniaProperty.Register<BodyCreator, float>(
				nameof(XPosition), 0, defaultBindingMode:BindingMode.TwoWay);
			public float XPosition
			{
				get => GetValue(XProperty);
				set => SetValue(XProperty, value);
			}

			
			public static readonly StyledProperty<float> YPositonProperty = AvaloniaProperty.Register<BodyCreator, float>(
				nameof(YPositon), 0, defaultBindingMode:BindingMode.TwoWay);
			public float YPositon
			{
				get => GetValue(YPositonProperty);
				set => SetValue(YPositonProperty, value);
			}

		#endregion

		#region Velocity

			public static readonly StyledProperty<float> XVelocityProperty = AvaloniaProperty.Register<BodyCreator, float>(
				nameof(XVelocity), 0, defaultBindingMode:BindingMode.TwoWay);
			public float XVelocity
			{
				get => GetValue(XVelocityProperty);
				set => SetValue(XVelocityProperty, value);
			}

			
			public static readonly StyledProperty<float> YVelocityProperty = AvaloniaProperty.Register<BodyCreator, float>(
				nameof(YVelocity), 0, defaultBindingMode:BindingMode.TwoWay);
			public float YVelocity
			{
				get => GetValue(YVelocityProperty);
				set => SetValue(YVelocityProperty, value);
			}

		#endregion
	
	#endregion
	
	
	public BodyCreator()
	{
		InitializeComponent();
		ResetFields();
	}
	
	
	
	public event EventHandler<Body> BodyCreated;
	private void BtnAddBody_OnClick(object? sender, RoutedEventArgs e)
	{
		Body created = new(Mass, new(XPosition, YPositon), Radius, new(XVelocity, YVelocity));
		if (!String.IsNullOrWhiteSpace(BodyName))
			created.Name = BodyName;

		created.Color = GetColorArray();
		
		BodyCreated?.Invoke(this, created);
		
		ResetFields();

		Console.WriteLine(created);
		Console.WriteLine(created.Color[1]);
	}

	public void ResetFields()
	{
		BodyName = "";
		Mass = 10;
		Radius = 10;
		
		XPosition = 0;
		YPositon = 0;
		XVelocity = 0;
		YVelocity = 0;
	}

	private byte[] GetColorArray()
		=> [255, (byte)(R * 255), (byte)(G * 255), (byte)(B * 255)];

	private Color GetColor()
	{
		var array = GetColorArray();
		return new Color(array[0], array[1], array[2], array[3]);
	}
}