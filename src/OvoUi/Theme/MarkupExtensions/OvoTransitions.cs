using Avalonia;
using Avalonia.Animation;
using Avalonia.Controls;
using Avalonia.Controls.Documents;
using Avalonia.Markup.Xaml;

namespace OvoUi.Theme.MarkupExtensions;

public abstract class OvoTransitionExtension : MarkupExtension
{
    protected abstract IEnumerable<AvaloniaProperty> Properties { get; }

    protected virtual TimeSpan Duration =>
        TimeSpan.FromMilliseconds(100);

    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        var transitions = new Transitions();

        foreach (var property in Properties)
        {
            transitions.Add(new BrushTransition
            {
                Duration = Duration,
                Property = property
            });
        }

        return transitions;
    }
}

public class OvoTransitions : OvoTransitionExtension
{
    protected override IEnumerable<AvaloniaProperty> Properties =>
    [
        Border.BackgroundProperty,
        TextElement.ForegroundProperty,
        Border.BorderBrushProperty
    ];
}

public class OvoBackgroundTransitions : OvoTransitionExtension
{
    protected override IEnumerable<AvaloniaProperty> Properties =>
    [
        Border.BackgroundProperty
    ];
}

public class OvoForegroundTransitions : OvoTransitionExtension
{
    protected override IEnumerable<AvaloniaProperty> Properties =>
    [
        TextElement.ForegroundProperty
    ];
}

public class OvoBorderTransitions : OvoTransitionExtension
{
    protected override IEnumerable<AvaloniaProperty> Properties =>
    [
        Border.BorderBrushProperty
    ];
}