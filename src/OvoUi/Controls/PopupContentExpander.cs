using Avalonia;
using Avalonia.Animation;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using Avalonia.Threading;
using Avalonia.VisualTree;

namespace OvoUi.Controls;

public sealed class PopupContentExpander : ContentExpander
{
    private static readonly TimeSpan ExpandDuration = TimeSpan.FromMilliseconds(280);
    private static readonly TimeSpan FadeDuration = TimeSpan.FromMilliseconds(75);
    private int _animationVersion;

    public static readonly StyledProperty<bool> ExpandUpProperty =
        AvaloniaProperty.Register<PopupContentExpander, bool>(nameof(ExpandUp));

    public bool ExpandUp
    {
        get => GetValue(ExpandUpProperty);
        set => SetValue(ExpandUpProperty, value);
    }

    public PopupContentExpander()
    {
        Orientation = Avalonia.Layout.Orientation.Vertical;
        Multiplier = 1;
        Opacity = 1;
        ClipToBounds = true;
        PreserveDesiredSize = true;
    }

    protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnAttachedToVisualTree(e);

        var animationVersion = ++_animationVersion;
        Dispatcher.UIThread.Post(() =>
        {
            if (animationVersion != _animationVersion)
                return;

            Multiplier = 0;
            Opacity = 0;
            Transitions = new Transitions
            {
                new DoubleTransition
                {
                    Property = MultiplierProperty,
                    Duration = ExpandDuration,
                    Easing = new Avalonia.Animation.Easings.ExponentialEaseOut()
                },
                new DoubleTransition
                {
                    Property = OpacityProperty,
                    Duration = FadeDuration,
                    Easing = new Avalonia.Animation.Easings.QuadraticEaseOut()
                }
            };

            Multiplier = 1;
            Opacity = 1;
        }, DispatcherPriority.Render);
    }

    protected override Size ArrangeOverride(Size finalSize)
    {
        var result = base.ArrangeOverride(finalSize);
        if (PreserveDesiredSize && ExpandUp)
        {
            Clip = new Avalonia.Media.RectangleGeometry(
                new Rect(0, finalSize.Height * (1 - Multiplier), finalSize.Width, finalSize.Height * Multiplier));
        }

        return result;
    }

    protected override void OnDetachedFromVisualTree(VisualTreeAttachmentEventArgs e)
    {
        _animationVersion++;
        base.OnDetachedFromVisualTree(e);
    }
}
