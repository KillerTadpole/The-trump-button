using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweetinvi;
using Tweetinvi.Core.Interfaces;


namespace theTrumpButton
{
    public class TweetRetreaver
    {
        public TweetRetreaver(int numberOfTweets)
        {
            m_numOfTweets = numberOfTweets;
        }

        public dynamic[] getTweets (string ofWho)
        {
            dynamic[] list = new dynamic[m_numOfTweets];
            try
            {
                Tweetinvi.Auth.SetUserCredentials("ZbPzHjoEWf0yM4eiTaeLqgMh7", "yntiYekU8GDSDhdMTMeqIo2x03GwGzehNMqbIUecCAb6gv80hR", "898518683311419393-ldKj4IGIJtECWWJZgdUb3Rh7INZtfTm", "QerlPtRaGrGTdC3IDZHfBQf2W19HhQocM8Eqzeelxhm1Z");
                var user = User.GetAuthenticatedUser();

                if (user == null) // When something goes wrong, user is retruned null
                {
                    var latestException = ExceptionHandler.GetLastException();
                    System.Windows.Forms.MessageBox.Show("The following error occured : '{0}'", latestException.TwitterDescription);
                }

                // I pull ten tweets from my home timeline and fillter all excepte for the ones made by trump
                list = Timeline.GetHomeTimeline(m_numOfTweets * 2).Where(x => x.CreatedBy.Name.Equals(ofWho)).ToArray();
                return list;
            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("Error has occured connecting to Twitter");
                return list;
            }
        }

        private int m_numOfTweets;
    }
}
