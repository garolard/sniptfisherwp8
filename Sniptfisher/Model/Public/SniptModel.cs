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

    public class User
    {
        public int id { get; set; }
        public string email { get; set; }
        public string username { get; set; }
        public string snipts { get; set; }
        public string email_md5 { get; set; }
        public string gravatar
        {
            get { return "http://www.gravatar.com/avatar/" + email_md5; }
        }
        public bool is_pro { get; set; }
        public List<string> lexers { get; set; }
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
    { }

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
