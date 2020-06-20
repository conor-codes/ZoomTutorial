using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using SampleZoom.Interfaces;
using SampleZoom.iOS.Platform;
using Xamarin.Forms;
using Zoomios;

[assembly: Dependency(typeof(ZoomService))]
namespace SampleZoom.iOS.Platform
{
    public class ZoomService : IZoomService
    {
        MobileRTC mobileRTC;
        MobileRTCAuthService authService;

        public ZoomService()
        {
            mobileRTC = MobileRTC.SharedRTC;
        }

        public void InitZoomLib(string appKey, string appSecret)
        {
            mobileRTC.Initialize(new MobileRTCSDKInitContext
            {
                EnableLog = true,
                Domain = "zoom.us"
            });

            authService = mobileRTC.GetAuthService();
            if(authService != null)
            {
                authService.ClientKey = appKey;
                authService.ClientSecret = appSecret;
                authService.SdkAuth();
            }
        }

        public bool IsInitialized() => mobileRTC?.IsRTCAuthorized() ?? false;

        public void JoinMeeting(string meetingId, string displayName, string meetingPassword)
        {
            if (IsInitialized())
            {
                var meetingService = mobileRTC.GetMeetingService();
                var meetingParamDict = new Dictionary<string, string>
                {
                    { Constants.kMeetingParam_Username, displayName},
                    { Constants.kMeetingParam_MeetingNumber, meetingId},
                    { Constants.kMeetingParam_MeetingPassword, meetingPassword},

                };

                var meetingParams = NSDictionary.FromObjectsAndKeys(meetingParamDict.Values.ToArray(), meetingParamDict.Keys.ToArray());

                var meetingJoinResponse = meetingService.JoinMeetingWithDictionary(meetingParams);
            }
        }
    }
}
