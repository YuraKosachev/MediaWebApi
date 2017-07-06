using MediaStoreApi.Domain.Interfaces;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
using MediaStoreApi.Domain.Exceptions;

namespace MediaStoreApi.ExtensionLibraries.Images
{
    public class ImageOperations : IImageOperations
    {
        private int _width;
        private int _height;
        public ImageOperations(int width,int height)
        {
            _width = width;
            _height = height;
        }
        public byte[] GetResizeImage(byte[] content)
        {
            var image = ByteArrayToImage(content);

            return ImageToByteArray(ResizeImg(image, _width, _height));
        }

       

        private Image ResizeImg(Image image, int nWidth, int nHeight)
        {
            Image result = new Bitmap(nWidth, nHeight);
            using (Graphics g = Graphics.FromImage((Image)result))
            {
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
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
                stream.Close();
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
            if((nHeight > original.Height) || nHeight <= 0)
                throw new BadRequestValues("the height value is less than or equal to zero or exceeds the value of the original");

            var resize = ResizeImg(original, nWidth, nHeight);


            return ImageToStream(resize);

        }
    }
}
