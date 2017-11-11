using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEvernote.Model
{
    public class Note
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public Guid Category { get; set; }
        public DateTime Created { get; set; }
        public DateTime Changed { get; set; }
        public Guid Creator { get; set; }
        public List<Guid> Shared { get; set; }
    }
}
