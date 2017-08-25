using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Data.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tweetinvi;
using Tweetinvi.Core.Interfaces;


namespace theTrumpButton
{
    public partial class new_tweet_form : Form
    {
        public new_tweet_form()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
                TweetPackage row1 = new TweetPackage(profile_pic1, identifier1, tweet1, twitter_logo1, likeButton1, pictureStar1);
                TweetPackage row2 = new TweetPackage(profile_pic2, identifier2, tweet2, twitter_logo2, likeButton2, pictureStar2);
                TweetPackage row3 = new TweetPackage(profile_pic3, identifier3, tweet3, twitter_logo3, likeButton3, pictureStar3);
                TweetPackage row4 = new TweetPackage(profile_pic4, identifier4, tweet4, twitter_logo4, likeButton4, pictureStar4);
                TweetPackage row5 = new TweetPackage(profile_pic5, identifier5, tweet5, twitter_logo5, likeButton5, pictureStar5);

                table = new TweetPackage[] { row1, row2, row3, row4, row5 };

                int numberOfRows = 5;

                TweetRetreaver twitter = new TweetRetreaver(numberOfRows);
                var tweets = twitter.getTweets("Donald J. Trump");

                for (int i = 0; i < numberOfRows; i++)
                {
                    table[i].pack(tweets[i]);
                    table[i].makeVisible();
                }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            foreach (TweetPackage row in table)
            {
                if (row.getLikeButton().Equals(sender))
                {
                    row.addToDB();
                    break;
                }
            }
        }

        private void refreshGrid(object sender, EventArgs e)
        {
            // Console.WriteLine("event triggered");
            using (DataContext dbconnection = new DataContext(TweetPackage.connectionString))
            {
                var favoriteTweets = (from tweet in dbconnection.GetTable<TweetFave>()
                                      select tweet).ToList();
                dataGridFaveTweets.DataSource = favoriteTweets;
            }
        }

        private void sToolStripMenuItem_Click(object sender, EventArgs e)
        {

            using (DataContext context = new DataContext(TweetPackage.connectionString))
            {
//                context.ExecuteCommand(
//                    @"CREATE TABLE TweetFaves 
//                        (
//                        ID INT NOT NULL PRIMARY KEY IDENTITY,
//                        userName varchar(20) not null,
//                        name varchar(20) not null,
//                        dateTweeted datetime not null,
//                        tweetBody varchar(140) not null,
//                        tweetID varchar(25) not null);");
                // context.ExecuteCommand(@"alter table TweetFaves alter column tweetBody varchar(1000) not null");
            }
        }
    }
}
