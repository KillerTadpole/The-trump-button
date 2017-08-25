using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace theTrumpButton
{
    public class ImageDownloader
    {
        public ImageDownloader(string URL)
        {
            m_URL = URL;
        }

        public System.Drawing.Image download()
        {
            // I coppied this section from http://forgetcode.com/CSharp/2052-Download-images-from-a-URL and tweeked it to my needs
            // The rest of this class is mine


            // **********************************************************HERE***********************************************************************************

            System.Drawing.Image image = null;
            try
            {
                System.Net.HttpWebRequest webRequest = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(m_URL);
                webRequest.AllowWriteStreamBuffering = true;
                webRequest.Timeout = 30000;

                System.Net.WebResponse webResponse = webRequest.GetResponse();

                System.IO.Stream stream = webResponse.GetResponseStream();

                image = System.Drawing.Image.FromStream(stream);

                webResponse.Close();
                return image;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("something went wrong with downloading the image. " + ex.Message);
                return image;
            }

            // *********************************************************TO HERE*************************************************************************
        }

        string m_URL;
    }
}
