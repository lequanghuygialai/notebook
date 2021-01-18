using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using Microsoft.AspNetCore.Mvc;

namespace Notebook.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NoteController
    {
        private readonly NoteBookDbContext _dbContext;

        public NoteController(NoteBookDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IList<Note> GetNotes()
        {
            return _dbContext.Notes.ToList();
        }

        [HttpPost]
        public int AddNote([FromBody] Note note)
        {
            _dbContext.Notes.Add(note);
            return _dbContext.SaveChanges();
        }

        [HttpPut]
        public int UpdateNote([FromBody] Note note)
        {
            var result = _dbContext.Notes.FirstOrDefault(t => t.Id == note.Id);
            if (result == null)
            {
                return -1;
            }
            result.Title = note.Title ?? result.Title;
            result.NoteContent = note.NoteContent ?? result.NoteContent;

            _dbContext.Update(result);
            return _dbContext.SaveChanges();
        }

        [HttpDelete]
        public int DeleteNote(int id)
        {
            var result = _dbContext.Notes.FirstOrDefault(t => t.Id == id);
            if (result == null)
            {
                return -1;
            }

            _dbContext.Notes.Remove(result);
            return _dbContext.SaveChanges();
        }

        [HttpGet("ip")]
        public string GetIP()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            return "Not found";
        }
    }
}
