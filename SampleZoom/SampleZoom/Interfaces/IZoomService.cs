using System;
namespace SampleZoom.Interfaces
{
    public interface IZoomService
    {
        bool IsInitialized();
        void InitZoomLib(string appKey, string appSecret);
        void JoinMeeting(string meetingId, string displayName, string meetingPassword);
    }
}
