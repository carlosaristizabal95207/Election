using System;
using System.Collections.Generic;
using System.Text;

namespace Election.Common.Interfaces
{
    using System;
    using System.IO;

    public interface IMvxPictureChooserTask
    {
        void ChoosePictureFromLibrary(int maxPixelDimension, int percentQuality, Action<Stream> pictureAvailable, Action assumeCancelled);

        void TakePicture(int maxPixelDimension, int percentQuality, Action<Stream> pictureAvailable, Action assumeCancelled);
    }
}
