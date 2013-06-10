﻿using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.ComponentModel;
using Catrobat.IDEWindowsPhone.Annotations;

namespace Catrobat.IDEWindowsPhone.Controls.Buttons
{
  public delegate void PlayStateChanged(object sender, PlayButtonState state);
  public enum PlayButtonState {Play, Pause}

  public partial class PlayButton : UserControl, INotifyPropertyChanged
  {
    public event PlayStateChanged PlayStateChanged;
    public event RoutedEventHandler Click;

    public static readonly DependencyProperty PlayButtonStateProperty = 
      DependencyProperty.Register("State", typeof(PlayButtonState), typeof(PlayButton), 
      new PropertyMetadata(PlayButtonState.Pause, new PropertyChangedCallback(PlayButtonStatePropertyChanged)));

    static void PlayButtonStatePropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
    {
      var playButton = (PlayButton) sender;
      var value = (PlayButtonState)e.NewValue;

      if (value == PlayButtonState.Play)
      {
        playButton.buttonPause.Visibility = System.Windows.Visibility.Visible;
        playButton.buttonPlay.Visibility = System.Windows.Visibility.Collapsed;
      }
      else
      {
        playButton.buttonPause.Visibility = System.Windows.Visibility.Collapsed;
        playButton.buttonPlay.Visibility = System.Windows.Visibility.Visible;
      }

      playButton.OnPlayStateChanged();
    }

    public PlayButtonState State
    {
      get { return (PlayButtonState)(this.GetValue(PlayButtonStateProperty)); }

      set
      {
        this.SetValue(PlayButtonStateProperty, value);

        this.RaisePropertyChanged();
      }
    }

    private void OnPlayStateChanged()
    {
      if (PlayStateChanged != null)
        PlayStateChanged.Invoke(this, State);
    }

    public ImageSource PressedImage
    {
      
      set { this.SetValue(PlayButtonStateProperty, value); }
    }

    public PlayButton()
    {
      InitializeComponent();
    }

    private void buttonPause_Click(object sender, RoutedEventArgs e)
    {
      State = PlayButtonState.Pause;

      if (PlayStateChanged != null)
        PlayStateChanged.Invoke(this, State);

      if (Click != null)
        Click.Invoke(this, new RoutedEventArgs());
    }

    private void buttonPlay_Click(object sender, RoutedEventArgs e)
    {
      State = PlayButtonState.Play;

      if (PlayStateChanged != null)
        PlayStateChanged.Invoke(this, State);

      if (Click != null)
        Click.Invoke(this, new RoutedEventArgs());
    }

    #region PropertyChanged
    public event PropertyChangedEventHandler PropertyChanged;
    [NotifyPropertyChangedInvocator]

    protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
    {
      PropertyChangedEventHandler handler = PropertyChanged;
      if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
    }
    #endregion
  }
}
