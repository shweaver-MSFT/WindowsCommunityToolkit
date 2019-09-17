using System;
using Microsoft.Toolkit.Uwp.UI.Controls;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Microsoft.Toolkit.Uwp.SampleApp.SamplePages
{
    public sealed partial class TokenizingTextBoxPage : Page, IXamlRenderListener
    {
        private TokenizingTextBox tokenizingTextBoxControl;

        public TokenizingTextBoxPage()
        {
            InitializeComponent();
        }

        public void OnXamlRendered(FrameworkElement control)
        {
            tokenizingTextBoxControl = control.FindName("TokenizingTextBox") as TokenizingTextBox;
            if (tokenizingTextBoxControl != null)
            {
                tokenizingTextBoxControl.QueryTextChanged += TokenizingTextBox_QueryTextChanged;
                tokenizingTextBoxControl.SuggestionChosen += TokenizingTextBox_SuggestionChosen;
                tokenizingTextBoxControl.QuerySubmitted += TokenizingTextBox_QuerySubmitted;
                tokenizingTextBoxControl.TokenItemClicked += TokenizingTextBox_TokenItemClicked;
                tokenizingTextBoxControl.TokenItemAdded += TokenizingTextBox_TokenItemAdded;
            }
        }

        private async void TokenizingTextBox_QueryTextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                if (!args.CheckCurrent())
                {
                    return;
                }

                switch (args.Reason)
                {
                    case AutoSuggestionBoxTextChangeReason.ProgrammaticChange:
                        break;

                    case AutoSuggestionBoxTextChangeReason.SuggestionChosen:
                        break;

                    case AutoSuggestionBoxTextChangeReason.UserInput:
                        //_vm.TextChangedCommand.Execute(sender.Text);
                        break;

                    default:
                        break;
                }
            });
        }

        private void TokenizingTextBox_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            //_vm.SuggestionChosenCommand.Execute(args.SelectedItem);
        }

        private void TokenizingTextBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            if (args.ChosenSuggestion == null)
            {
                //_vm.QuerySubmittedCommand.Execute(args.QueryText);
            }
        }

        private void TokenizingTextBox_TokenItemClicked(TokenizingTextBox sender, object item)
        {
            //System.Diagnostics.Debug.WriteLine(item.ToString());
        }

        private void TokenizingTextBox_TokenItemAdded(TokenizingTextBox sender, object args)
        {
            //System.Diagnostics.Debug.WriteLine(sender.GetUntokenizedText());
        }

        private class TokenizingTextBoxItemTemplateSelector : DataTemplateSelector
        {
            public DataTemplate DataItemTemplate { get; set; }

            public DataTemplate StringItemTemplate { get; set; }

            protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
            {
                return (item is string) ? StringItemTemplate : DataItemTemplate;
            }
        }
    }
}
