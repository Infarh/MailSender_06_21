using System.Windows.Controls;

namespace MailSender.Views
{
    public partial class RecipientsEditor
    {
        public RecipientsEditor() => InitializeComponent();

        private void OnIdValidationError(object Sender, ValidationErrorEventArgs E)
        {
            if (E.Action == ValidationErrorEventAction.Added)
            {
                ((Control)Sender).ToolTip = E.Error.ErrorContent.ToString();
            }
            else
            {
                ((Control)Sender).ClearValue(ToolTipProperty);
            }
        }
    }
}
