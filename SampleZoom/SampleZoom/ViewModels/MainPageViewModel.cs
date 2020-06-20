using System;
using System.Windows.Input;
using SampleZoom.Interfaces;
using Xamarin.Forms;

namespace SampleZoom.ViewModels
{
    public class MainPageViewModel
    {
        public ICommand JoinMeetingCommand { get; set; }
        public string MeetingId { get; set; }
        public string MeetingPassword { get; set; }
        IZoomService zoomService;

        public MainPageViewModel()
        {
            zoomService = DependencyService.Get<IZoomService>();
            zoomService.InitZoomLib("rsdFnExcx1Hzv3TRthDCxo7osS2ZXXaNJ9Yi", "TSyXY3XRCY3ZoCFsLUZkDrcdhciVjTkRD77m");
            JoinMeetingCommand = new Command(JoinMeeting);
        }

        private void JoinMeeting()
        {
            if (!zoomService.IsInitialized())
            {
                return;
            }
            zoomService.JoinMeeting(MeetingId, "ZoomSample", MeetingPassword);
        }
    }
}
