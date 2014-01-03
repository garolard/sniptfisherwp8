using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sniptfisher.Model.Public
{
    public class Snipt
    {
        public int id { get; set; }
        public string title { get; set; }
        public string slug { get; set; }
        public string lexer { get; set; }
        public string code { get; set; }
        public string description { get; set; }
        public int line_count { get; set; }
        public string stylized { get; set; }
        public string key { get; set; }
        public bool @public { get; set; }
        public bool blog_post { get; set; }
        public string created { get; set; }
        public string modified { get; set; }
        public DateTime publish_date { get; set; }
        public string embed_url { get; set; }
        public string full_absolute_url { get; set; }
        public string description_rendered { get; set; }
        public int favs { get; set; }
        public int views { get; set; }
        public User user { get; set; }
        public List<Tag> tags { get; set; }
    }

    public class User : CommonBindableBase
    {
        private int _id;
        public int id 
        { 
            get
            {
                return _id;
            }
            set
            {
                if (value != _id)
                {
                    RaisePropertyChanging("id");
                    _id = value;
                    RaisePropertyChanged("id");
                }
            }
        }

        private string _email;
        public string email 
        {
            get
            {
                return _email;
            }
            set
            {
                if (value != _email)
                {
                    RaisePropertyChanging("email");
                    _email = value;
                    RaisePropertyChanged("email");
                }
            }
        }

        private string _username;
        public string username
        {
            get
            {
                return _username;
            }
            set
            {
                if (value != _username)
                {
                    RaisePropertyChanging("username");
                    _username = value;
                    RaisePropertyChanged("username");
                }
            }
        }

        private string _snipts;
        public string snipts
        {
            get
            {
                return _snipts;
            }
            set
            {
                if (value != _snipts)
                {
                    RaisePropertyChanging("snipts");
                    _snipts = value;
                    RaisePropertyChanged("snipts");
                }
            }
        }

        private string _email_md5;
        public string email_md5
        {
            get
            {
                return _email_md5;
            }
            set
            {
                if (value != _email_md5)
                {
                    RaisePropertyChanging("email_md5");
                    _email_md5 = value;
                    RaisePropertyChanged("email_md5");
                }
            }
        }
        public string gravatar
        {
            get { return "http://www.gravatar.com/avatar/" + email_md5; }
        }

        private bool _is_pro;
        public bool is_pro
        {
            get
            {
                return _is_pro;
            }
            set
            {
                if (value != _is_pro)
                {
                    RaisePropertyChanging("is_pro");
                    _is_pro = value;
                    RaisePropertyChanged("is_pro");
                }
            }
        }

        private List<string> _lexers;
        public List<string> lexers
        {
            get
            {
                return _lexers;
            }
            set
            {
                if (value != _lexers)
                {
                    RaisePropertyChanging("lexers");
                    _lexers = value;
                    RaisePropertyChanged("lexers");
                }
            }
        }
    }

    public class Tag
    {
        public int id { get; set; }
        public string name { get; set; }
        public string absolute_url { get; set; }
        public string snipts { get; set; }
        public int count { get; set; }
    }

    public class Favorite
    {
        public int id { get; set; }
        public string snipt { get; set; }
        public User user { get; set; }
    }

    public class Metadata
    {
        public int limit { get; set; }
        public string next { get; set; }
        public int offset { get; set; }
        public string previous { get; set; }
        public int total_count { get; set; }
    }

    public class ApiResponse
    {
        public Metadata meta { get; set; }
        public List<Snipt> objects { get; set; }
    }

    public class UserApiResponse
    {
        public Metadata meta { get; set; }
        public List<User> objects { get; set; }
    }
}
