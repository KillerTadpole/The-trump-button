using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweetinvi;
using Npgsql;
using System.IO;

namespace theTrumpButton
{
    public class TweetPackage
    {
        public TweetPackage(System.Windows.Forms.PictureBox profile,
            System.Windows.Forms.Label identifier,
            System.Windows.Forms.Label message,
            System.Windows.Forms.PictureBox logo,
            System.Windows.Forms.Button likeButton,
            System.Windows.Forms.PictureBox likedImage)
        {
            this.m_profile = profile;
            this.m_identifier = identifier;
            this.m_text = message;
            this.m_logo = logo;
            this.m_fullName = null;
            this.m_userName = null;
            this.m_likeButton = likeButton;
            this.m_likedImage = likedImage;
            this.m_id = null;
        }

        public void pack(Tweetinvi.Models.ITweet tweet)
        {
            ImageDownloader image = new ImageDownloader(tweet.CreatedBy.ProfileImageUrl400x400);

            m_date = tweet.CreatedAt;
            m_userName = "@" + tweet.CreatedBy.UserIdentifier.ScreenName;
            m_fullName = tweet.CreatedBy.Name;
            m_profile.Image = image.download();
            m_identifier.Text = m_fullName + " " + m_userName + " " + m_date.ToString();
            m_text.Text = tweet.Text;
            m_id = tweet.IdStr;
        }

        public void makeVisible()
        {
            m_profile.Visible = true;
            m_identifier.Visible = true;
            m_text.Visible = true;
            m_logo.Visible = true;
            m_likeButton.Visible = true;
        }

        public void addToDB()
        {
            try
            {
                using (DataContext context = new DataContext(connectionString))
                {
                    // Console.WriteLine(m_userName.Length + "\n" + m_fullName.Length + "\n" + m_text.Text.Length + "\n" + m_id.Length);
                    TweetFave entry = new TweetFave
                    {
                        userName = m_userName,
                        name = m_fullName,
                        dateTweeted = m_date,
                        tweetBody = m_text.Text,
                        tweetID = m_id
                    };

                    context.GetTable<TweetFave>().InsertOnSubmit(entry);
                    context.SubmitChanges();
                }
                m_likeButton.Visible = false;
                m_likedImage.Visible = true;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }

        public System.Windows.Forms.Button getLikeButton()
        {
            return m_likeButton;
        }
            
        private System.Windows.Forms.PictureBox m_profile;
        private System.Windows.Forms.PictureBox m_logo;
        private System.Windows.Forms.Label m_identifier;
        private System.Windows.Forms.Label m_text;
        private DateTime m_date;
        private string m_fullName;
        private string m_userName;
        private System.Windows.Forms.Button m_likeButton;
        private System.Windows.Forms.PictureBox m_likedImage;
        private string m_id;
        public static readonly string connectionString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename='" +
            System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Tweets.mdf") + "';Integrated Security=True"; // C:\Users\charles.merritt\Documents\Visual Studio 2013\Projects\theTrumpButton\theTrumpButton\Tweets.mdf';Integrated Security=True";
    }
}
