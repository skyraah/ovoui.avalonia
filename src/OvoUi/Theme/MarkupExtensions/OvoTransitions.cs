using Avalonia.Animation;
using Avalonia.Controls;
using Avalonia.Controls.Documents;

namespace OvoUi.Theme.MarkupExtensions;

public class OvoTransitions : Avalonia.Markup.Xaml.MarkupExtension
{
    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return new Transitions
        {
            new BrushTransition
            {
                Duration = TimeSpan.FromMilliseconds(100),
                Property = Border.BackgroundProperty
            },
            new BrushTransition
            {
                Duration = TimeSpan.FromMilliseconds(100),
                Property = TextElement.ForegroundProperty
            },
            new BrushTransition
            {
                Duration = TimeSpan.FromMilliseconds(100),
                Property = Border.BorderBrushProperty
            }
        };
    }
}