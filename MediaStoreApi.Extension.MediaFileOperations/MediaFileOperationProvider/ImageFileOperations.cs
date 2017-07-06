using MediaStoreApi.Domain.Interfaces;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
using MediaStoreApi.Domain.Exceptions;

namespace MediaStoreApi.Extension.MediaFileOperations
{
    public class ImageFileOperations : IMediaFileOperations, IMediaFileDynamicResizable
    {
        private int _width;
        private int _height;
        public ImageFileOperations(int width, int height)
        {
            _width = width;
            _height = height;
        }
        public byte[] GetMiniature(byte[] content)
        {
            var image = ByteArrayToImage(content);
            var w = _width;
            var h = _height;

            if ((_width > image.Width) || _width <= 0)
                w = image.Width;
            if ((_height > image.Height) || _height <= 0)
                h = image.Height;



            return ImageToByteArray(ResizeImg(image, w, h));
        }



        private Image ResizeImg(Image image, int nWidth, int nHeight)
        {
            return ResizeImg(image, nWidth, nHeight, InterpolationMode.Low);
        }
        private Image ResizeImg(Image image, int nWidth, int nHeight, InterpolationMode interpolationMode)
        {

            Image result = new Bitmap(nWidth, nHeight);
            using (Graphics g = Graphics.FromImage((Image)result))
            {
                g.InterpolationMode = interpolationMode;
                g.DrawImage(image, 0, 0, nWidth, nHeight);
            }
            return result;
        }

        private Image ByteArrayToImage(byte[] byteArrayIn)
        {

            return StreamToImage(new MemoryStream(byteArrayIn));

        }
        private Stream ImageToStream(Image image)
        {
            return new MemoryStream(ImageToByteArray(image));
        }
        private Image StreamToImage(Stream stream)
        {
            using (stream)
            {
                var image = Image.FromStream(stream);
                return image;
            }

        }
        private byte[] ImageToByteArray(Image image)
        {
            ImageConverter imgCon = new ImageConverter();
            return (byte[])imgCon.ConvertTo(image, typeof(byte[]));
        }

        public Stream DynamicResizeImage(byte[] content, int nWidth, int nHeight)
        {
            var original = ByteArrayToImage(content);

            if ((nWidth > original.Width) || nWidth <= 0)
                throw new BadRequestValues("the width value is less than or equal to zero or exceeds the value of the original");
            if ((nHeight > original.Height) || nHeight <= 0)
                throw new BadRequestValues("the height value is less than or equal to zero or exceeds the value of the original");

            var resize = ResizeImg(original, nWidth, nHeight,InterpolationMode.HighQualityBicubic);


            return ImageToStream(resize);

        }

       
    }
}
