using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sniptfisher.Model.Public
{
    public class SniptModel
    {
        public int id { get; set; }
        public string title { get; set; }
        public string slug { get; set; }
        public string lexer { get; set; }
        public string code { get; set; }
        public string description { get; set; }
        public int line_count { get; set; }
        public string stylized { get; set; }
        public string created { get; set; }
        public string modified { get; set; }
        public string embed_url { get; set; }
        public string full_absolute_url { get; set; }
        public string description_rendered { get; set; }
        public UserModel user { get; set; }
        public List<TagModel> tags { get; set; }
    }

    public class UserModel
    {
        public int id { get; set; }
        public string username { get; set; }
        public string snipts { get; set; }
        public string email_md5 { get; set; }
        public string gravatar
        {
            get { return "http://www.gravatar.com/avatar/" + email_md5; }
        }
    }

    public class TagModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string absolute_url { get; set; }
        public string snipts { get; set; }
        public int count { get; set; }
    }

    public class Metadata
    { }

    public class PublicResponse
    {
        public Metadata meta { get; set; }
        public List<SniptModel> objects { get; set; }
    }
}
