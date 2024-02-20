using CommunityToolkit.Mvvm.ComponentModel;

namespace DemoApp;

public sealed class TimeProgression : ObservableObject
{
    private TimeSpan _duration;

    private TimeSpan _elapsedTime;

    public TimeSpan RemainingTime => Duration.Subtract(ElapsedTime);

    public double ProgressionRatio { get; private set; }

    public double RemainingRatio => 1 - ProgressionRatio;

    public TimeSpan Duration
    {
        get => _duration;
        set
        {
            if (_duration != value)
            {
                _duration = value;
                OnPropertyChanged(nameof(Duration));
                OnPropertyChanged(nameof(ProgressionRatio));
            }
        }
    }

    public TimeSpan ElapsedTime
    {
        get => _elapsedTime;
        set
        {
            if (_elapsedTime != value)
            {
                _elapsedTime = value;
                ProgressionRatio = Math.Clamp(ElapsedTime.TotalSeconds / Duration.TotalSeconds, 0, 1);
                OnPropertyChanged(nameof(ElapsedTime));
                OnPropertyChanged(nameof(RemainingTime));
                OnPropertyChanged(nameof(ProgressionRatio));
                OnPropertyChanged(nameof(RemainingRatio));
            }
        }
    }

    public TimeProgression(TimeSpan duration)
    {
        Duration = duration;
    }

    public TimeProgression()
    {

    }

    public void AddElapsedMilliseconds(int milliSeconds)
    {
        ElapsedTime = ElapsedTime.Add(TimeSpan.FromMilliseconds(milliSeconds));
    }

    public void AddElapsedSeconds(int seconds)
    {
        ElapsedTime = ElapsedTime.Add(TimeSpan.FromSeconds(seconds));
    }

    public void IncreaseDurationSeconds(int seconds)
    {
        Duration = Duration.Add(TimeSpan.FromSeconds(seconds));
    }

    public void DecreaseDurationSeconds(int seconds)
    {
        Duration = Duration.Subtract(TimeSpan.FromSeconds(seconds));
    }

    public void Reset(TimeSpan duration)
    {
        ElapsedTime = new TimeSpan();
        Duration = duration;
    }
}
