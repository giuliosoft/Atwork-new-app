using System;
using System.Collections.Generic;
using AtWork.Helpers;
using AtWork.iOS.HelperServices;
using MobileCoreServices;

[assembly: Xamarin.Forms.Dependency(typeof(FilePickerList))]
namespace AtWork.iOS.HelperServices
{
    public class FilePickerList : FileNameInterface
    {
        public string[] FileTypeList()
        {
            var allowedUTIs = new string[]
            {
                UTType.UTF8PlainText,
                UTType.PlainText,
                UTType.RTF,
                UTType.Text,
                UTType.PDF,
                UTType.UTF16PlainText,
                UTType.FileURL,
                UTType.Spreadsheet,
                UTType.MP3, 
                UTType.MPEG4,
                UTType.BMP,
                UTType.MPEG4Audio,
                UTType.WaveformAudio,
                UTType.Audio,
                UTType.Video,
                UTType.ZipArchive,
                UTType.RawImage,
                UTType.AVIMovie,
                "org.openxmlformats.wordprocessingml.document",
                "com.microsoft.powerpoint.​ppt",
                "org.openxmlformats.spreadsheetml.sheet",
                "org.openxmlformats.presentationml.presentation",
                "com.microsoft.excel.xls",
                "com.microsoft.waveform-​audio",
                "com.microsoft.windows-​media-wmv",
                "com.adobe.illustrator.ai-​image",
            };
            return allowedUTIs;
        }
    }
}
