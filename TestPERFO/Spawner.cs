using Godot;
using System;

public partial class Spawner : Node3D
{
    [Export] PackedScene Cube {  get; set; }

    private int _count = 0;
    private float _fps = 0F;
    private int _countDown = 120;
    private float _average = 0F;
    private float _totalFps = 0F;
    private float _frameCount = 0F;
    private bool _stop = false;


    private Label _lb_total;
    private Label _lb_fps;
    private Label _lb_countDown;
    private Label _lb_fpsAverage;
    private Timer _timer;

    public override void _Ready()
    {
        base._Ready();
        Engine.MaxFps = 60;
        _lb_total = GetNode<Label>("CanvasLayer/LabelTotal");
        _lb_fps = GetNode<Label>("CanvasLayer/LabelFps");
        _lb_countDown = GetNode<Label>("CanvasLayer/LabelCountDown");
        _lb_fpsAverage = GetNode<Label>("CanvasLayer/LabelFpsMoyenne");
        _timer = GetNode<Timer>("Timer");
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
        if (_stop) { return; }

        var instance = Cube.Instantiate();
        AddChild(instance);
        _count++;
        _lb_total.Text = $"Nombre de cubes {_count}";
        _fps = (float)Engine.GetFramesPerSecond();
        _totalFps += _fps;
        _frameCount += 1;
        _lb_fps.Text = $"Fps: {_fps}";

    }

    private void OnTimerTimeOut()
    {
        _countDown--;
        _lb_countDown.Text = $"{_countDown}";
        if (_countDown == 0)
        {
            _stop = true;
            _timer.Stop();
            _lb_fpsAverage.Text = $"Moyenne FPS : {CalcultateAvg()}";
            SetPhysicsProcess(false);
        }
    }

    //private void OnAreaEntered(Area2D area)
    //{
    //    //if (area == null) return;
    //    //var cube = area.GetParent();
    //    //cube.QueueFree();
    //}

    private float CalcultateAvg()
    {
        float avg = _totalFps / _frameCount;

        return avg;
    }
}
