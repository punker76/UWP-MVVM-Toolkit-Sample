﻿using Microsoft.Toolkit.Mvvm.Messaging;
using XamlBrewer.UWP.MvvmToolkit.Sample.Models;
using XamlBrewer.UWP.MvvmToolkit.Sample.Services.Messenger.Messages;

namespace XamlBrewer.UWP.MvvmToolkit.Sample.ViewModels
{
    public class ThemeModuleViewModel : MyViewModelBase
    {
        private Theme _theme;
        private bool _isDefaultTheme;

        public ThemeModuleViewModel()
        {
            // 'ThemeAwareViewModel'
            _theme = Messenger.Send<ThemeRequestMessage>();
            LoggingService.Log($"ThemeModule requested theme and received {_theme.Name}.");
            _isDefaultTheme = _theme.Name == Theme.Default.Name;
            if (!_isDefaultTheme)
            {
                ToggleTheme(false);
            }
        }

        public bool IsDefaultTheme
        {
            get => _isDefaultTheme;
            set
            {
                if (_isDefaultTheme != value)
                {
                    ToggleTheme(true);
                }

                SetProperty(ref _isDefaultTheme, value);
            }
        }

        public void ToggleTheme(bool broadcast)
        {
            if (_isDefaultTheme)
            {
                _theme = Theme.Blue;
            }
            else
            {
                _theme = Theme.Red;
            }

            if (broadcast)
            {
                LoggingService.Log($"ThemeModule requested thema change to {_theme.Name}.");
                Messenger.Send(new ThemeChangedMessage(_theme));
            }
        }
    }
}
