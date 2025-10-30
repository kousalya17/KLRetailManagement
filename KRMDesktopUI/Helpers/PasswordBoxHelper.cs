using System.Windows;
using System.Windows.Controls;

namespace KRMDesktopUI.Helpers
{
    public static class PasswordBoxHelper
    {
        // 1. The Attached Property itself (BoundPassword)
        public static readonly DependencyProperty BoundPasswordProperty =
            DependencyProperty.RegisterAttached("BoundPassword", typeof(string), typeof(PasswordBoxHelper),
                new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnBoundPasswordChanged));

        // 2. The property to tell us when to attach the event handler (Attach)
        public static readonly DependencyProperty AttachProperty =
            DependencyProperty.RegisterAttached("Attach", typeof(bool), typeof(PasswordBoxHelper), new PropertyMetadata(false, Attach));

        // 3. A property to prevent re-entrancy during updates
        private static readonly DependencyProperty IsUpdatingProperty =
            DependencyProperty.RegisterAttached("IsUpdating", typeof(bool), typeof(PasswordBoxHelper), new PropertyMetadata(false));

        // Getters and Setters for BoundPassword
        public static string GetBoundPassword(DependencyObject dp) => (string)dp.GetValue(BoundPasswordProperty);
        public static void SetBoundPassword(DependencyObject dp, string value) => dp.SetValue(BoundPasswordProperty, value);

        // Getters and Setters for Attach
        public static bool GetAttach(DependencyObject dp) => (bool)dp.GetValue(AttachProperty);
        public static void SetAttach(DependencyObject dp, bool value) => dp.SetValue(AttachProperty, value);

        // Getters and Setters for IsUpdating (private accessors, mainly for internal use)
        private static bool GetIsUpdating(DependencyObject dp) => (bool)dp.GetValue(IsUpdatingProperty);
        private static void SetIsUpdating(DependencyObject dp, bool value) => dp.SetValue(IsUpdatingProperty, value);

        // --- Event Handlers ---

        private static void Attach(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var passwordBox = sender as PasswordBox;
            if (passwordBox == null) return;

            if ((bool)e.OldValue && !(bool)e.NewValue)
            {
                // Detach event handler
                passwordBox.PasswordChanged -= HandlePasswordChanged;
            }
            else if (!(bool)e.OldValue && (bool)e.NewValue)
            {
                // Attach event handler
                passwordBox.PasswordChanged += HandlePasswordChanged;
            }
        }

        private static void HandlePasswordChanged(object sender, RoutedEventArgs e)
        {
            var passwordBox = sender as PasswordBox;
            if (passwordBox != null)
            {
                // Update ViewModel only if the change wasn't from the ViewModel itself
                if (!GetIsUpdating(passwordBox))
                {
                    // Set IsUpdating to true to prevent OnBoundPasswordChanged from firing immediately
                    SetIsUpdating(passwordBox, true);
                    SetBoundPassword(passwordBox, passwordBox.Password);
                    // Set IsUpdating back to false
                    SetIsUpdating(passwordBox, false);
                }
            }
        }

        private static void OnBoundPasswordChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var passwordBox = d as PasswordBox;
            if (passwordBox != null)
            {
                // Update PasswordBox only if the change wasn't from the PasswordBox itself
                if (!GetIsUpdating(passwordBox))
                {
                    SetIsUpdating(passwordBox, true);
                    passwordBox.Password = (string)e.NewValue;
                    SetIsUpdating(passwordBox, false);
                }
            }
        }
    }
}