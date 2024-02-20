using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp
{
    public class ProgressBar : GraphicsView
    {
        private readonly ProgressBarDrawable _progressDrawable;

        public static readonly BindableProperty ProgressProperty = BindableProperty.Create(nameof(Progress), typeof(double), typeof(ProgressBarDrawable), 0.0, BindingMode.TwoWay,
             propertyChanged: (bindableObject, oldValue, newValue) =>
             {
                 if (newValue != null && bindableObject is ProgressBar progressControl)
                 {
                     progressControl.UpdateProgress();
                 }
             });

        public static readonly BindableProperty ProgressBrushProperty = BindableProperty.Create(nameof(ProgressBrush), typeof(Brush), typeof(ProgressBarDrawable), new SolidColorBrush(Colors.Blue),
            propertyChanged: (bindableObject, oldValue, newValue) =>
            {
                if (newValue != null && bindableObject is ProgressBar progressBar)
                {
                    progressBar.UpdateProgressBrush();
                }
            });

        public static readonly BindableProperty StrokeBrushProperty = BindableProperty.Create(nameof(StrokeBrush), typeof(Brush), typeof(ProgressBarDrawable), new SolidColorBrush(Colors.Gray),
                propertyChanged: (bindableObject, oldValue, newValue) =>
                {
                    if (newValue != null && bindableObject is ProgressBar progressBar)
                    {
                        progressBar.UpdateStrokeBrush();
                    }
                });

        public double Progress
        {
            get => (double)GetValue(ProgressProperty);
            set => SetValue(ProgressProperty, value);
        }

        public Brush ProgressBrush
        {
            get { return (Brush)GetValue(ProgressBrushProperty); }
            set { SetValue(ProgressBrushProperty, value); }
        }

        public Brush StrokeBrush
        {
            get { return (Brush)GetValue(StrokeBrushProperty); }
            set { SetValue(StrokeBrushProperty, value); }
        }

        public ProgressBar()
        {
            Drawable = _progressDrawable = new ProgressBarDrawable();
        }

        protected override void OnParentChanged()
        {
            base.OnParentChanged();

            if (Parent != null)
            {
                UpdateStrokeBrush();
                UpdateProgressBrush();
                UpdateProgress();
            }
        }

        protected void UpdateProgress()
        {
            if (_progressDrawable == null)
            {
                return;
            }

            _progressDrawable.Progress = Progress;
            Invalidate();
        }

        void UpdateStrokeBrush()
        {
            if (_progressDrawable == null)
            {
                return;
            }

            _progressDrawable.StrokePaint = StrokeBrush;
            Invalidate();
        }

        void UpdateProgressBrush()
        {
            if (_progressDrawable == null)
            {
                return;
            }

            _progressDrawable.ProgressPaint = ProgressBrush;
            Invalidate();
        }

        public class ProgressBarDrawable : IDrawable
        {
            public Paint StrokePaint { get; set; }
            public Paint ProgressPaint { get; set; }
            public double Progress { get; set; }

            public CornerRadius ProgressCornerRadius { get; set; } = 6f;
            public CornerRadius CornerRadius { get; set; } = 6f;

            public void Draw(ICanvas canvas, RectF dirtyRect)
            {
                canvas.Antialias = true;

                DrawTrack(canvas, dirtyRect);
                DrawProgress(canvas, dirtyRect);
            }

            public void DrawTrack(ICanvas canvas, RectF dirtyRect)
            {
                canvas.SaveState();

                canvas.SetFillPaint(StrokePaint, dirtyRect);

                canvas.FillRoundedRectangle(dirtyRect, CornerRadius.TopLeft, CornerRadius.TopRight, CornerRadius.BottomLeft, CornerRadius.BottomRight);

                canvas.RestoreState();
            }

            public void DrawProgress(ICanvas canvas, RectF dirtyRect)
            {
                canvas.SaveState();

                RectF rect = new Rect(dirtyRect.X, dirtyRect.Y, dirtyRect.Width * Progress, dirtyRect.Height);

                canvas.SetFillPaint(ProgressPaint, dirtyRect);

                canvas.FillRoundedRectangle(rect, ProgressCornerRadius.TopLeft, ProgressCornerRadius.TopRight, ProgressCornerRadius.BottomLeft, ProgressCornerRadius.BottomRight);

                canvas.RestoreState();
            }
        }
    }
}
