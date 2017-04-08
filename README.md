# Safe-Note
*A Windows Universal Platform application, written in C#, that acts as a secretnote/password vault with facial recognition for authentication. Third year, Data Representation and Querying, Software Development.*

**Update: now called Note Safe. The application title, Safe Note, was not available.**

**Application Description**

Safe Nofe is an application designed to store the user's confidential data. However, this is no ordinary note/password vault, Safe Note allows the user to log in using both facial authenication and the usual password authentication. The user does not have to even touch the screen to log in. Although the facial verification is quite accurate, I would strongly advise any user to remember their password to avoid not being able to authenticate themselves with the facial verification in the case of no internet, no light, and (extremely unlikely) exceeding their API call limit.

**Additional Points**

1. The user cannot log in or utilize any of the features until they have entered an API Key, a password and taken a picture of their face to be analyzed.

2. The user cannot just delete any of this information, they have to replace it to do so.

**Requirements**

Create a Universal Windows Project (UWP) that will each demonstrate the use of Isolated Storage
and at least one other sensor or service available on the devices.

These include:

- [ ] Accelerometer or Gyroscope
- [ ] GPS (Location Based Services)
- [ ] Sound
- [x] Network Services (connecting to server for data updates etc)
- [x] Camera
- [ ] Multi Touch Gesture Management

**Technologies**

1. C#

2. XAML

3. Microsoft Cognitive Services; the Face API (detection and verification).


**References:**

https://www.microsoft.com/cognitive-services/en-us/face-api/documentation/face-api-how-to-topics/HowtoAnalyzeVideo_Face

https://github.com/Microsoft/Cognitive-Face-Windows

https://github.com/Microsoft/Cognitive-Samples-VideoFrameAnalysis

https://github.com/smajida/FaceApi-Verify

http://windowsapptutorials.com/tips/how-to-disable-hardware-back-button-in-windows-phone-app/

https://msdn.microsoft.com/en-us/library/system.datetime.now(v=vs.110).aspx

https://msdn.microsoft.com/en-us/library/system.datetime.compare(v=vs.110).aspx

http://stackoverflow.com/questions/13759278/how-can-i-subtract-6-hour-from-the-current-time

http://stackoverflow.com/questions/39231192/add-day-to-datetime

https://www.youtube.com/watch?v=X53Gvqj5vzw

https://docs.microsoft.com/en-us/windows/uwp/controls-and-patterns/password-box

https://msdn.microsoft.com/fr-fr/library/cc221360(v=vs.95).aspx

https://code.msdn.microsoft.com/windowsapps/Popup-Control-in-universel-700d46d4

http://stackoverflow.com/questions/8887794/check-if-string-has-space-in-between-or-anywhere

https://msdn.microsoft.com/en-us/library/fbh501kz(v=vs.110).aspx

http://stackoverflow.com/questions/32677597/how-to-exit-or-close-an-uwp-app-programmatically-windows-10

https://forums.xamarin.com/discussion/52076/winphone-display-image-from-storagefolder

https://docs.microsoft.com/en-us/windows/uwp/audio-video-camera/imaging

http://stackoverflow.com/questions/27163604/cannot-implicitly-convert-type-string-to-windows-ui-xaml-media-imaging-bitmap

https://docs.microsoft.com/en-us/windows/uwp/audio-video-camera/simple-camera-preview-access

https://docs.microsoft.com/en-us/windows/uwp/audio-video-camera/get-a-preview-frame

http://stackoverflow.com/questions/29947225/access-preview-frame-from-mediacapture

https://jeremylindsayni.wordpress.com/2016/04/18/how-to-use-the-camera-on-your-device-with-c-in-a-uwp-application-part-1-previewing-the-output/

https://docs.microsoft.com/en-us/windows/uwp/audio-video-camera/detect-and-track-faces-in-an-image

https://docs.microsoft.com/en-us/windows/uwp/audio-video-camera/simple-camera-preview-ac

https://docs.microsoft.com/en-us/windows/uwp/audio-video-camera/basic-photo-video-and-audio-capture-with-mediacapture

https://www.youtube.com/watch?v=AZ59EnmoI7I

https://www.youtube.com/watch?v=E3kFkzeaynw

http://stackoverflow.com/questions/350500/how-to-convert-a-string-to-a-guid

https://github.com/smajida/FaceApi-Verify

http://www.joeljoseph.net/customizing-title-bar-and-status-bar-in-universal-windows-platform-uwp/

http://stackoverflow.com/questions/4140723/how-to-remove-new-line-characters-from-a-string

https://msdn.microsoft.com/en-us/library/system.string.isnullorempty(v=vs.110).aspx

http://stackoverflow.com/questions/390491/how-to-add-item-to-the-beginning-of-listt

http://www.c-sharpcorner.com/UploadFile/5ef5aa/binding-collection-to-listview-control-in-uwp-explained/

-----

__*Tara O'Kelly - G00322214@gmit.ie*__