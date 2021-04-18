using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using GoogleCast.Channels;
using GoogleCast.Models.Media;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

namespace GoogleCast.SampleApp
{
    /// <summary>
    /// Main view model
    /// </summary>
    public class MainViewModel : ObservableObject
    {
        /// <summary>
        /// Initializes a new instance of <see cref="MainViewModel"/> class
        /// </summary>
        /// <param name="deviceLocator">GoogleCast device locator</param>
        /// <param name="sender">GoogleCast sender</param>
        public MainViewModel(IDeviceLocator deviceLocator, ISender sender)
        {
            DeviceLocator = deviceLocator;
            Sender = sender;
            sender.Disconnected += (s, e) => PlayerState = "DISCONNECTED";
            sender.GetChannel<IMediaChannel>().StatusChanged += MediaChannelStatusChanged;
            sender.GetChannel<IReceiverChannel>().StatusChanged += ReceiverChannelStatusChanged;
            PlayCommand = new RelayCommand(async () => await TryAsync(PlayAsync), () => AreButtonsEnabled);
            PauseCommand = new RelayCommand(async () => await TryAsync(PauseAsync), () => AreButtonsEnabled);
            StopCommand = new RelayCommand(async () => await TryAsync(StopAsync), () => AreButtonsEnabled);
            RefreshCommand = new RelayCommand(async () => await TryAsync(RefreshAsync), () => IsLoaded);
        }

        private IDeviceLocator DeviceLocator { get; }
        private ISender Sender { get; }

        private IEnumerable<IReceiver>? _receivers;
        /// <summary>
        /// Gets the available receivers
        /// </summary>
        public IEnumerable<IReceiver>? Receivers
        {
            get => _receivers;
            private set => SetProperty(ref _receivers, value);
        }

        private bool IsInitialized { get; set; }

        private IReceiver? _selectedReceiver;
        /// <summary>
        /// Gets or sets the selected receiver
        /// </summary>
        public IReceiver? SelectedReceiver
        {
            get => _selectedReceiver;
            set
            {
                if (_selectedReceiver != null && !_selectedReceiver.Equals(value) ||
                    _selectedReceiver == null && value != null)
                {
                    _selectedReceiver = value;
                    IsInitialized = false;
                    OnPropertyChanged(nameof(SelectedReceiver));
                    NotifyButtonsCommandsCanExecuteChanged();
                }
            }
        }

        private bool _isLoaded;
        /// <summary>
        /// Gets a value indicating whether the list of the GoogleCast devices is loaded or not 
        /// </summary>
        public bool IsLoaded
        {
            get => _isLoaded;
            private set
            {
                if (_isLoaded != value)
                {
                    _isLoaded = value;
                    OnPropertyChanged(nameof(IsLoaded));
                    RefreshCommand.NotifyCanExecuteChanged();
                    NotifyButtonsCommandsCanExecuteChanged();
                }
            }
        }

        /// <summary>
        /// Gets a value indicating whether the Play, Pause and Stop buttons must be enabled or not
        /// </summary>
        public bool AreButtonsEnabled => IsLoaded && SelectedReceiver != null && !string.IsNullOrWhiteSpace(Link);

        private string? _playerState;
        /// <summary>
        /// Gets the player state
        /// </summary>
        public string? PlayerState
        {
            get => _playerState;
            private set => SetProperty(ref _playerState, value);
        }

        private string _link = "https://commondatastorage.googleapis.com/gtv-videos-bucket/CastVideos/mp4/DesigningForGoogleCast.mp4";
        /// <summary>
        /// Gets or sets the link to play
        /// </summary>
        public string Link
        {
            get => _link;
            set
            {
                if (_link != value)
                {
                    _link = value;
                    IsInitialized = false;
                    OnPropertyChanged(nameof(Link));
                    NotifyButtonsCommandsCanExecuteChanged();
                }
            }
        }

        private string _subtitle = "https://commondatastorage.googleapis.com/gtv-videos-bucket/CastVideos/tracks/DesigningForGoogleCast-en.vtt";
        /// <summary>
        /// Gets or sets the subtitle file
        /// </summary>
        public string Subtitle
        {
            get => _subtitle;
            set => SetProperty(ref _subtitle, value);
        }

        private bool _isMuted;
        /// <summary>
        /// Gets or sets a value indicating whether the audio is muted
        /// </summary>
        public bool IsMuted
        {
            get => _isMuted;
            set
            {
                if (_isMuted != value)
                {
                    _isMuted = value;
                    OnPropertyChanged(nameof(IsMuted));
                    Task.Run(SetIsMutedAsync);
                }
            }
        }

        private float _volume = 1;
        /// <summary>
        /// Gets or sets the volume
        /// </summary>
        public float Volume
        {
            get => _volume;
            set
            {
                if (_volume != value)
                {
                    if (value > _volume)
                    {
                        IsMuted = false;
                    }
                    _volume = value;
                    OnPropertyChanged(nameof(Volume));
                    Task.Run(SetVolumeAsync);
                }
            }
        }

        private bool IsStopped
        {
            get
            {
                var mediaChannel = Sender.GetChannel<IMediaChannel>();
                return mediaChannel.Status == null || !string.IsNullOrEmpty(mediaChannel.Status.FirstOrDefault()?.IdleReason);
            }
        }

        /// <summary>
        /// Get the play command
        /// </summary>
        public RelayCommand PlayCommand { get; }
        /// <summary>
        /// Gets the pause command
        /// </summary>
        public RelayCommand PauseCommand { get; }
        /// <summary>
        /// Gets the stop command
        /// </summary>
        public RelayCommand StopCommand { get; }
        /// <summary>
        /// Gets the refresh command
        /// </summary>
        public RelayCommand RefreshCommand { get; }

        private void NotifyButtonsCommandsCanExecuteChanged()
        {
            OnPropertyChanged(nameof(AreButtonsEnabled));
            PlayCommand.NotifyCanExecuteChanged();
            PauseCommand.NotifyCanExecuteChanged();
            StopCommand.NotifyCanExecuteChanged();
        }

        private async Task TryAsync(Func<Task> action)
        {
            try
            {
                await action();
            }
            catch (Exception ex)
            {
                PlayerState = ex.GetBaseException().Message;
                IsInitialized = false;
            }
        }

        private async Task InvokeAsync<TChannel>(Func<TChannel, Task>? action) where TChannel : IChannel
        {
            if (action != null)
            {
                await action.Invoke(Sender.GetChannel<TChannel>());
            }
        }

        private async Task SendChannelCommandAsync<TChannel>(bool condition, Func<TChannel, Task>? action, Func<TChannel, Task> otherwise)
            where TChannel : IChannel
        {
            await InvokeAsync(condition ? action : otherwise);
        }

        private async Task<bool> ConnectAsync()
        {
            var selectedReceiver = SelectedReceiver;
            if (selectedReceiver != null)
            {
                await Sender.ConnectAsync(selectedReceiver);
                return true;
            }
            return false;
        }

        private async Task PlayAsync()
        {
            await SendChannelCommandAsync<IMediaChannel>(!IsInitialized || IsStopped,
                async c =>
                {
                    var link = Link;
                    if (!string.IsNullOrWhiteSpace(link) && await ConnectAsync())
                    {
                        var sender = Sender;
                        var mediaChannel = sender.GetChannel<IMediaChannel>();
                        await sender.LaunchAsync(mediaChannel);
                        var mediaInfo = new MediaInformation() { ContentId = link };
                        var subtitle = Subtitle;
                        if (!string.IsNullOrWhiteSpace(subtitle))
                        {
                            mediaInfo.Tracks = new Track[]
                            {
                                new Track() {  TrackId = 1, Language = "en-US", Name = "English", TrackContentId = subtitle }
                            };
                            mediaInfo.TextTrackStyle = new TextTrackStyle()
                            {
                                BackgroundColor = Color.Transparent,
                                EdgeColor = Color.Black,
                                EdgeType = TextTrackEdgeType.DropShadow
                            };
                            await mediaChannel.LoadAsync(mediaInfo, true, 1);
                        }
                        else
                        {
                            await mediaChannel.LoadAsync(mediaInfo);
                        }
                        IsInitialized = true;
                    }
                }, c => c.PlayAsync());
        }

        private async Task PauseAsync()
        {
            await SendChannelCommandAsync<IMediaChannel>(IsStopped, null, async c => await c.PauseAsync());
        }

        private async Task StopAsync()
        {
            if (IsStopped)
            {
                if (IsInitialized || await ConnectAsync())
                {
                    await InvokeAsync<IReceiverChannel>(c => c.StopAsync());
                }
            }
            else
            {
                await InvokeAsync<IMediaChannel>(c => c.StopAsync());
            }
        }

        /// <summary>
        /// Finds the receivers
        /// </summary>
        /// <returns>a <see cref="Task"/> that represents the asynchronous operation</returns>
        public async Task RefreshAsync()
        {
            IsLoaded = false;
            Receivers = await DeviceLocator.FindReceiversAsync();
            SelectedReceiver = Receivers.FirstOrDefault();
            IsLoaded = true;
        }

        private async Task SetVolumeAsync()
        {
            await SendChannelCommandAsync<IReceiverChannel>(IsStopped, null, async c => await c.SetVolumeAsync(Volume));
        }

        private async Task SetIsMutedAsync()
        {
            await SendChannelCommandAsync<IReceiverChannel>(IsStopped, null, async c => await c.SetIsMutedAsync(IsMuted));
        }

        private void MediaChannelStatusChanged(object? sender, EventArgs e)
        {
            var status = (sender as IMediaChannel)?.Status?.FirstOrDefault();
            var playerState = status?.PlayerState;
            if (playerState == "IDLE" && !string.IsNullOrEmpty(status!.IdleReason))
            {
                playerState = status.IdleReason;
            }
            PlayerState = playerState;
        }

        private void ReceiverChannelStatusChanged(object? sender, EventArgs e)
        {
            if (!IsInitialized)
            {
                var status = (sender as IReceiverChannel)?.Status;
                if (status != null)
                {
                    if (status.Volume.Level != null)
                    {
                        Volume = (float)status.Volume.Level;
                    }
                    if (status.Volume.IsMuted != null)
                    {
                        IsMuted = (bool)status.Volume.IsMuted;
                    }
                }
            }
        }
    }
}
