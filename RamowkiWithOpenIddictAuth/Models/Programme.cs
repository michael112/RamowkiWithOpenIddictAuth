using System;

namespace RamowkiWithOpenIddictAuth.Models
{
    public class Programme
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public String Description { get; set; }

        public Programme() { }
        public Programme(string title)
        {
            this.Title = title;
        }
        public Programme(string title, string description) : this(title)
        {
            this.Description = description;
        }
        public Programme(Guid id, string title) : this(title)
        {
            this.Id = id;
        }
        public Programme(Guid id, string title, string description) : this(title, description)
        {
            this.Id = id;
        }
    }
}
